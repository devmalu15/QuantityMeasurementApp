
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Context;
using QuantityMeasurementRepositoryLayer.Interfaces;
 
namespace QuantityMeasurementRepositoryLayer.Repositories;
 
public class QuantityMeasurementRedisRepository : IQuantityMeasurementRepository
{
    private readonly IDistributedCache _cache;
    private readonly QuantityMeasurementDbContext _context;
 
    // Single cache key for the full list of measurements
    // When this key exists in Redis, GetAll() returns it without touching the DB
    // When this key is absent (after Save or expiry), GetAll() reads from DB
    private const string CacheKey = "all_measurements";
 
    // Cache expiry — Redis auto-deletes the entry after 10 minutes
    // Forces a fresh DB read every 10 minutes even if no write happened
    private static readonly DistributedCacheEntryOptions CacheOptions =
        new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        };
 
    // Both IDistributedCache (Redis) and DbContext (SQL) injected by DI
    // DbContext is needed for the DB fallback read in GetAll()
    public QuantityMeasurementRedisRepository(
        IDistributedCache cache,
        QuantityMeasurementDbContext context)
    {
        _cache   = cache;
        _context = context;
    }
 
    // ── WRITE — Cache-Aside pattern ───────────────────────────────────────────
    // DB is the source of truth. After writing to DB, invalidate Redis cache.
    // Next GetAll() call will find no cache, read from DB, and repopulate Redis.
    public void Save(QuantityMeasurementEntity entity)
    {
        // NOTE: We do NOT write to the DB here.
        // The EFRepository (registered as IQuantityMeasurementRepositorySql) handles DB writes.
        // ServiceImpl calls both: cacheRepository.Save() AND sqlRepository.Save()
        // So the DB write already happens via sqlRepository.Save() in ServiceImpl.
        // Our job here is ONLY to invalidate the cache.
 
        try
        {
            // Invalidate the cached list so the next GetAll() reads fresh data from DB
            _cache.Remove(CacheKey);
            // After this, Redis has no "all_measurements" key.
            // Next GetAll() will be a cache miss → reads from DB → repopulates Redis.
        }
        catch (Exception)
        {
            // Redis is unavailable — silently ignore.
            // The DB write (via sqlRepository) already succeeded.
            // Cache invalidation failure means next read may return stale data,
            // but the data is safe in the DB.
        }
    }
 
    // ── READ — Cache-Aside pattern ────────────────────────────────────────────
    // Check Redis first. If found, return immediately (cache hit).
    // If not found (cache miss), read from DB, store in Redis, return.
    public List<QuantityMeasurementEntity> GetAll()
    {
        try
        {
            // STEP 1: Try to get data from Redis
            string? cachedJson = _cache.GetString(CacheKey);
 
            if (cachedJson != null)
            {
                // CACHE HIT — Redis has the data, return without touching the DB
                // This is the fast path — no SQL query, no network roundtrip to DB
                return JsonSerializer.Deserialize<List<QuantityMeasurementEntity>>(cachedJson)
                       ?? new List<QuantityMeasurementEntity>();
            }
        }
        catch (Exception)
        {
            // Redis is unavailable — fall through to DB read below
            // App continues working, just without the cache benefit
        }
 
        // STEP 2: CACHE MISS — Redis had nothing, read from the DB (source of truth)
        var entities = _context.Measurements.ToList();
 
        try
        {
            // STEP 3: Populate Redis with the fresh DB data for next time
            // Next GetAll() will hit Redis instead of the DB (cache hit)
            string json = JsonSerializer.Serialize(entities);
            _cache.SetString(CacheKey, json, CacheOptions);
            // CacheOptions sets 10-minute expiry — Redis auto-clears after that
        }
        catch (Exception)
        {
            // Redis unavailable — return DB data anyway, just won't be cached
        }
 
        return entities;
    }
}
