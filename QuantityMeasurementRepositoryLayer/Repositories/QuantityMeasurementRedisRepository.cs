using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Interfaces;
 
namespace QuantityMeasurementRepositoryLayer.Repositories;
public class QuantityMeasurementRedisRepository : IQuantityMeasurementRepository
{
    private readonly IDistributedCache _cache;
 
    // This key stores the list of all entity keys in Redis
    // Acts as an index so GetAll() knows what to retrieve
    private const string MasterKey = "all_measurement_keys";
 
    // IDistributedCache is injected by DI — ASP.NET provides the Redis implementation
    public QuantityMeasurementRedisRepository(IDistributedCache cache)
    {
        _cache = cache;
    }
 
    public void Save(QuantityMeasurementEntity entity)
    {
        // Step 1 — create a unique key for this entity using a GUID
        string entityKey = "measurement_" + Guid.NewGuid().ToString();
 
        // Step 2 — serialize entity to JSON string (Redis only stores strings)
        string json = JsonSerializer.Serialize(entity);
 
        // Step 3 — store the JSON in Redis under the unique key
        _cache.SetString(entityKey, json);
 
        // Step 4 — update the master list of keys
        string existingJson = _cache.GetString(MasterKey) ?? "[]";
        List<string> allKeys = JsonSerializer.Deserialize<List<string>>(existingJson)!;
        allKeys.Add(entityKey);
        _cache.SetString(MasterKey, JsonSerializer.Serialize(allKeys));
    }
 
    public List<QuantityMeasurementEntity> GetAll()
    {
        // Step 1 — get the master list of all entity keys
        string existingJson = _cache.GetString(MasterKey) ?? "[]";
        List<string> allKeys = JsonSerializer.Deserialize<List<string>>(existingJson)!;
 
        // Step 2 — for each key, get and deserialize the entity
        var entities = new List<QuantityMeasurementEntity>();
        foreach (string key in allKeys)
        {
            string? json = _cache.GetString(key);
            if (json != null)
            {
                var entity = JsonSerializer.Deserialize<QuantityMeasurementEntity>(json);
                if (entity != null)
                    entities.Add(entity);
            }
        }
 
        return entities;
    }
}