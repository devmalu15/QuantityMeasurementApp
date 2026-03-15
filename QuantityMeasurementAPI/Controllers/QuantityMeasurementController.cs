using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuantityMeasurementAPI.Models;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Exceptions;
using System.Collections.Generic;
using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementAPI.Controllers
{
    [Route("api/quantity")]
    [ApiController]
    public class QuantityMeasurementController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;
        private readonly ILogger<QuantityMeasurementController> _logger;

        public QuantityMeasurementController(IQuantityMeasurementService service, ILogger<QuantityMeasurementController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] QuantityOperationRequest request)
        {
            _logger.LogInformation("Add endpoint called");
            try
            {
                var result = _service.Add(request.Q1, request.Q2);
                _logger.LogInformation("Add endpoint finished successfully");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnsupportedOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in Add endpoint");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("subtract")]
        public IActionResult Subtract([FromBody] QuantityOperationRequest request)
        {
            _logger.LogInformation("Subtract endpoint called");
            try
            {
                var result = _service.Subtract(request.Q1, request.Q2);
                _logger.LogInformation("Subtract endpoint finished successfully");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnsupportedOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in Subtract endpoint");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("divide")]
        public IActionResult Divide([FromBody] QuantityOperationRequest request)
        {
            _logger.LogInformation("Divide endpoint called");
            try
            {
                var result = _service.Divide(request.Q1, request.Q2);
                _logger.LogInformation("Divide endpoint finished successfully");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnsupportedOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in Divide endpoint");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("compare")]
        public IActionResult Compare([FromBody] QuantityOperationRequest request)
        {
            _logger.LogInformation("Compare endpoint called");
            try
            {
                var result = _service.Compare(request.Q1, request.Q2);
                _logger.LogInformation("Compare endpoint finished successfully");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnsupportedOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in Compare endpoint");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("convert")]
        public IActionResult Convert([FromBody] QuantityConvertRequest request)
        {
            _logger.LogInformation("Convert endpoint called");
            try
            {
                var result = _service.Convert(request.Input, request.TargetUnit);
                _logger.LogInformation("Convert endpoint finished successfully");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnsupportedOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in Convert endpoint");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("history/cache")]
        public IActionResult GetCacheHistory()
        {
            _logger.LogInformation("GetCacheHistory endpoint called");
            try
            {
                var history = _service.GetCacheHistory();
                _logger.LogInformation("GetCacheHistory endpoint finished successfully");
                return Ok(history);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetCacheHistory endpoint");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("history/sql")]
        public IActionResult GetSqlHistory()
        {
            _logger.LogInformation("GetSqlHistory endpoint called");
            try
            {
                var history = _service.GetSqlHistory();
                _logger.LogInformation("GetSqlHistory endpoint finished successfully");
                return Ok(history);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetSqlHistory endpoint");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
