using QuantityMeasurementApp.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;

namespace QuantityMeasurementApp.ConsoleApp.Repositories
{
    // simple in-memory cache with optional disk persistence
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        private static readonly Lazy<QuantityMeasurementCacheRepository> _instance
            = new Lazy<QuantityMeasurementCacheRepository>(() => new QuantityMeasurementCacheRepository());

        public static QuantityMeasurementCacheRepository Instance => _instance.Value;

        private readonly List<QuantityMeasurementEntity> _cache = new List<QuantityMeasurementEntity>();
        private readonly string _filePath;
        // simple DTO used for serialization to/from disk
        private record SerializableEntity(
            double? FirstValue,
            string? FirstUnit,
            double? SecondValue,
            string? SecondUnit,
            string Operation,
            double? ResultValue,
            string? ResultUnit,
            bool HasError,
            string? ErrorMessage,
            DateTime Timestamp);

        private QuantityMeasurementCacheRepository()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "measurements.dat");
            LoadFromDisk();
        }

        public void Save(QuantityMeasurementEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _cache.Add(entity);
            SaveToDisk(entity);
        }

        public IEnumerable<QuantityMeasurementEntity> GetAllMeasurements()
        {
            return _cache.AsReadOnly();
        }

        private void SaveToDisk(QuantityMeasurementEntity entity)
        {
            // write entire cache to disk as JSON
            try
            {
                var serializable = _cache.Select(e => new SerializableEntity(
                    e.FirstValue,
                    e.FirstUnit?.ToString(),
                    e.SecondValue,
                    e.SecondUnit?.ToString(),
                    e.Operation,
                    e.ResultValue,
                    e.ResultUnit?.ToString(),
                    e.HasError,
                    e.ErrorMessage,
                    e.Timestamp)).ToList();

                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(_filePath, JsonSerializer.Serialize(serializable, options));
            }
            catch
            {
                // ignore persistence errors; repository still works in memory
            }
        }

        private void LoadFromDisk()
        {
            if (!File.Exists(_filePath))
                return;
            try
            {
                string json = File.ReadAllText(_filePath);
                var list = JsonSerializer.Deserialize<List<SerializableEntity>>(json);
                if (list == null) return;
                foreach (var s in list)
                {
                    var entity = DeserializeEntity(s);
                    if (entity != null)
                        _cache.Add(entity);
                }
            }
            catch
            {
                // ignore load errors
            }
        }

        private QuantityMeasurementEntity? DeserializeEntity(SerializableEntity s)
        {
            try
            {
                if (s.HasError)
                    return new QuantityMeasurementEntity(s.Operation, s.ErrorMessage ?? string.Empty);

                if (s.SecondValue.HasValue)
                {
                    return new QuantityMeasurementEntity(
                        s.FirstValue ?? 0.0,
                        ParseUnit(s.FirstUnit),
                        s.SecondValue.Value,
                        ParseUnit(s.SecondUnit),
                        s.Operation,
                        s.ResultValue ?? 0.0,
                        ParseUnit(s.ResultUnit));
                }

                return new QuantityMeasurementEntity(
                    s.FirstValue ?? 0.0,
                    ParseUnit(s.FirstUnit),
                    s.Operation);
            }
            catch
            {
                return null;
            }
        }

        private IMeasurable ParseUnit(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            // search for any static IMeasurable field whose ToString matches
            var units = GetAllKnownUnits();
            var match = units.FirstOrDefault(u => u.ToString() == name);
            if (match != null) return match;
            throw new InvalidOperationException($"Unknown unit '{name}'");
        }

        private IEnumerable<IMeasurable> GetAllKnownUnits()
        {
            var list = new List<IMeasurable>();
            var asm = Assembly.GetAssembly(typeof(IMeasurable));
            if (asm == null) return list;
            foreach (var type in asm.GetTypes())
            {
                if (typeof(IMeasurable).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                {
                    foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
                    {
                        if (typeof(IMeasurable).IsAssignableFrom(field.FieldType))
                        {
                            if (field.GetValue(null) is IMeasurable u)
                                list.Add(u);
                        }
                    }
                }
            }
            return list;
        }
    }
}