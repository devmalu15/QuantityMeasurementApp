using System.Text.Json;
using HistoryService.Data;
using HistoryService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
 
namespace HistoryService.Controllers;
 
[ApiController]
[Route("api/quantity")]
[Authorize]
public class HistoryController : ControllerBase
{
    private readonly HistoryDbContext   _db;
    private readonly IDistributedCache  _cache;
    private const string CacheKey = "all_measurements";
 
    public HistoryController(HistoryDbContext db, IDistributedCache cache)
    {
        _db    = db;
        _cache = cache;
    }
 
    // Cache-Aside
    [HttpGet("history/redis")]
    public IActionResult GetRedisHistory()
    {
        try
        {
            string? cached = _cache.GetString(CacheKey);
            if (cached != null)
            {
                var fromCache = JsonSerializer.Deserialize<List<MeasurementEntity>>(cached)
                                ?? new List<MeasurementEntity>();
                return Ok(fromCache);
            }
        }
        catch { /* Redis down — fall through to DB */ }
 
        var entities = _db.Measurements.ToList();
 
        try
        {
            _cache.SetString(CacheKey, JsonSerializer.Serialize(entities),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });
        }
        catch { /* Redis down — return DB data */ }
 
        return Ok(entities);
    }
 
    //reads directly from SQL Server
    [HttpGet("history/ef")]
    public IActionResult GetEFHistory()
    {
        try
        {
            var entities = _db.Measurements.ToList();
            return Ok(entities);
        }
        catch (Exception ex) { return StatusCode(500, ex.Message); }
    }
 
    [HttpGet("history/sql")]
    public IActionResult GetSqlHistory() => GetEFHistory();
}
