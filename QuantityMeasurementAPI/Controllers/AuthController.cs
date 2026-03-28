
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.DTO;
 
namespace QuantityMeasurementAPI.Controllers;
 
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;
 
    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger      = logger;
    }
 
    
    // Body: { "email": "user@test.com", "password": "Pass@123" }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
    {
        try
        {
            _logger.LogInformation("Register attempt for {Email}", dto.Email);
            var message = await _authService.RegisterAsync(dto);
            return Ok(new { message });
            
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
        catch (Exception ex)                 { return StatusCode(500, ex.Message); }
    }
 
    // Body: { "email": "user@test.com", "password": "Pass@123" }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        try
        {
            _logger.LogInformation("Login attempt for {Email}", dto.Email);
            var response = await _authService.LoginAsync(dto);
            return Ok(response);
            
        }
        catch (UnauthorizedAccessException ex) { return Unauthorized(ex.Message); }
        catch (Exception ex)                   { return StatusCode(500, ex.Message); }
    }
}
