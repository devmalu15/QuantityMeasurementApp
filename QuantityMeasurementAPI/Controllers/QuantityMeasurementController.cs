using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementAPI.Models;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.Exceptions;
 
namespace QuantityMeasurementAPI.Controllers;
 
[ApiController]
[Route("api/quantity")]
public class QuantityMeasurementController : ControllerBase
{
    private readonly IQuantityMeasurementService _service;
    private readonly ILogger<QuantityMeasurementController> _logger;
 
    public QuantityMeasurementController(
        IQuantityMeasurementService service,
        ILogger<QuantityMeasurementController> logger)
    {
        _service = service;
        _logger  = logger;
    }
 
    // ── POST /api/quantity/add ───────────────────────────────────────────────
    [HttpPost("add")]
    public IActionResult Add([FromBody] QuantityOperationRequest request)
    {
        try
        {
            _logger.LogInformation("Add called");
            var result = _service.Add(request.Q1, request.Q2);
            return Ok(result);
        }
        catch (ArgumentException ex)             { return BadRequest(ex.Message); }
        catch (UnsupportedOperationException ex) { return BadRequest(ex.Message); }
        catch (Exception ex)                     { return StatusCode(500, ex.Message); }
    }
 
    // ── POST /api/quantity/subtract ──────────────────────────────────────────
    [HttpPost("subtract")]
    public IActionResult Subtract([FromBody] QuantityOperationRequest request)
    {
        try
        {
            _logger.LogInformation("Subtract called");
            var result = _service.Subtract(request.Q1, request.Q2);
            return Ok(result);
        }
        catch (ArgumentException ex)             { return BadRequest(ex.Message); }
        catch (UnsupportedOperationException ex) { return BadRequest(ex.Message); }
        catch (Exception ex)                     { return StatusCode(500, ex.Message); }
    }
 
    // ── POST /api/quantity/divide ────────────────────────────────────────────
    [HttpPost("divide")]
    public IActionResult Divide([FromBody] QuantityOperationRequest request)
    {
        try
        {
            _logger.LogInformation("Divide called");
            double result = _service.Divide(request.Q1, request.Q2);
            return Ok(result);
        }
        catch (ArgumentException ex)             { return BadRequest(ex.Message); }
        catch (ArithmeticException ex)           { return BadRequest(ex.Message); }
        catch (UnsupportedOperationException ex) { return BadRequest(ex.Message); }
        catch (Exception ex)                     { return StatusCode(500, ex.Message); }
    }
 
    // ── POST /api/quantity/compare ───────────────────────────────────────────
    [HttpPost("compare")]
    public IActionResult Compare([FromBody] QuantityOperationRequest request)
    {
        try
        {
            _logger.LogInformation("Compare called");
            bool result = _service.Compare(request.Q1, request.Q2);
            return Ok(result);
        }
        catch (ArgumentException ex)             { return BadRequest(ex.Message); }
        catch (Exception ex)                     { return StatusCode(500, ex.Message); }
    }
 
    // ── POST /api/quantity/convert ───────────────────────────────────────────
    [HttpPost("convert")]
    public IActionResult Convert([FromBody] QuantityConvertRequest request)
    {
        try
        {
            _logger.LogInformation("Convert called to {Unit}", request.TargetUnit);
            var result = _service.Convert(request.Input, request.TargetUnit);
            return Ok(result);
        }
        catch (ArgumentException ex)             { return BadRequest(ex.Message); }
        catch (UnsupportedOperationException ex) { return BadRequest(ex.Message); }
        catch (Exception ex)                     { return StatusCode(500, ex.Message); }
    }
 
    // ── GET /api/quantity/history/redis ──────────────────────────────────────
    [HttpGet("history/redis")]
    public IActionResult GetRedisHistory()
    {
        try
        {
            _logger.LogInformation("GetRedisHistory called");
            var history = _service.GetRedisHistory();
            return Ok(history);
        }
        catch (Exception ex) { return StatusCode(500, ex.Message); }
    }
 
    // ── GET /api/quantity/history/sql ────────────────────────────────────────
    [HttpGet("history/sql")]
    public IActionResult GetSqlHistory()
    {
        try
        {
            _logger.LogInformation("GetSqlHistory called");
            var history = _service.GetSqlHistory();
            return Ok(history);
        }
        catch (Exception ex) { return StatusCode(500, ex.Message); }
    }
}
