using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OperationService.Data;
using OperationService.Models;
 
namespace OperationService.Controllers;
 
[ApiController]
[Route("api/quantity")]
[Authorize]
public class OperationController : ControllerBase
{
    private readonly OperationDbContext _db;
    private readonly IDistributedCache  _cache;
    private const string CacheKey = "all_measurements";
 
    public OperationController(OperationDbContext db, IDistributedCache cache)
    {
        _db    = db;
        _cache = cache;
    }
 
    [HttpPost("add")]
    public IActionResult Add([FromBody] OperationRequest req)
    {
        try
        {
            double v1 = ToBase(req.Q1.Value, req.Q1.Unit);
            double v2 = ToBase(req.Q2.Value, req.Q2.Unit);
            double sum = v1 + v2;
            Save("ADD", req.Q1.Value, req.Q2.Value, sum.ToString());
            return Ok(new QuantityDTO { Value = sum, Unit = req.Q1.Unit });
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (Exception ex)         { return StatusCode(500, ex.Message); }
    }
 
    [HttpPost("subtract")]
    public IActionResult Subtract([FromBody] OperationRequest req)
    {
        try
        {
            double v1  = ToBase(req.Q1.Value, req.Q1.Unit);
            double v2  = ToBase(req.Q2.Value, req.Q2.Unit);
            double diff = v1 - v2;
            Save("SUBTRACT", req.Q1.Value, req.Q2.Value, diff.ToString());
            return Ok(new QuantityDTO { Value = diff, Unit = req.Q1.Unit });
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (Exception ex)         { return StatusCode(500, ex.Message); }
    }
 
    [HttpPost("divide")]
    public IActionResult Divide([FromBody] OperationRequest req)
    {
        try
        {
            double v1  = ToBase(req.Q1.Value, req.Q1.Unit);
            double v2  = ToBase(req.Q2.Value, req.Q2.Unit);
            if (v2 == 0) return BadRequest("Division by zero.");
            double res = v1 / v2;
            Save("DIVIDE", req.Q1.Value, req.Q2.Value, res.ToString());
            return Ok(res);
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (Exception ex)         { return StatusCode(500, ex.Message); }
    }
 
    [HttpPost("compare")]
    public IActionResult Compare([FromBody] OperationRequest req)
    {
        try
        {
            double v1  = ToBase(req.Q1.Value, req.Q1.Unit);
            double v2  = ToBase(req.Q2.Value, req.Q2.Unit);
            bool   res = Math.Abs(v1 - v2) < 0.000001;
            Save("COMPARE", req.Q1.Value, req.Q2.Value, res.ToString());
            return Ok(res);
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (Exception ex)         { return StatusCode(500, ex.Message); }
    }
 
    [HttpPost("convert")]
    public IActionResult Convert([FromBody] ConvertRequest req)
    {
        try
        {
            double baseVal = ToBase(req.Input.Value, req.Input.Unit);
            double result  = FromBase(baseVal, req.TargetUnit);
            Save("CONVERSION", req.Input.Value, 0, req.TargetUnit);
            return Ok(new QuantityDTO { Value = result, Unit = req.TargetUnit });
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (Exception ex)         { return StatusCode(500, ex.Message); }
    }
 
    // ── Unit conversion helpers ─────────────────────────────────────────────
    private static double ToBase(double value, string unit) => unit.ToUpper() switch
    {
        // Length — base: FEET
        "FEET"        => value,
        "INCHES"      => value / 12.0,
        "YARDS"       => value * 3.0,
        "CENTIMETERS" => value / 30.48,
        // Weight — base: KILOGRAM
        "KILOGRAM"    => value,
        "GRAM"        => value * 0.001,
        "POUND"       => value * 0.45359237,
        // Volume — base: LITRE
        "LITRE"       => value,
        "MILLILITRE"  => value * 0.001,
        "GALLON"      => value * 3.78541,
        // Temperature — base: CELSIUS
        "CELSIUS"     => value,
        "FAHRENHEIT"  => (value - 32) * 5.0 / 9.0,
        _             => throw new ArgumentException($"Unsupported unit: {unit}")
    };
 
    private static double FromBase(double baseValue, string targetUnit) => targetUnit.ToUpper() switch
    {
        "FEET"        => baseValue,
        "INCHES"      => baseValue * 12.0,
        "YARDS"       => baseValue / 3.0,
        "CENTIMETERS" => baseValue * 30.48,
        "KILOGRAM"    => baseValue,
        "GRAM"        => baseValue / 0.001,
        "POUND"       => baseValue / 0.45359237,
        "LITRE"       => baseValue,
        "MILLILITRE"  => baseValue / 0.001,
        "GALLON"      => baseValue / 3.78541,
        "CELSIUS"     => baseValue,
        "FAHRENHEIT"  => (baseValue * 9.0 / 5.0) + 32,
        _             => throw new ArgumentException($"Unsupported unit: {targetUnit}")
    };
 
    // ── Save to DB and invalidate cache ────────────────────────────────────
    private void Save(string op, double o1, double o2, string result)
    {
        var entity = new MeasurementEntity(op, o1, o2, result);
        _db.Measurements.Add(entity);
        _db.SaveChanges();
 
        try { _cache.Remove(CacheKey); }  // invalidate Redis cache
        catch { /* Redis down — ignore */ }
    }
}
