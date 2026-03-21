using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementBusinessLayer.Interfaces;
 
namespace QuantityMeasurementBusinessLayer.Services;
 
public class AuthServiceImpl : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _config;
 
    // UserManager is ASP.NET Identity's user management class
    // It handles: CreateAsync, FindByEmailAsync, CheckPasswordAsync
    // IConfiguration reads JWT settings from appsettings.json
    public AuthServiceImpl(UserManager<IdentityUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config      = config;
    }
 
    public async Task<string> RegisterAsync(RegisterDTO dto)
    {
        // Check if user with this email already exists
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new InvalidOperationException("User with this email already exists.");
 
        // Create a new IdentityUser object
        var user = new IdentityUser
        {
            UserName = dto.Email,   // Identity requires UserName — use email as username
            Email    = dto.Email
        };
 
        // CreateAsync hashes the password automatically and stores the user in AspNetUsers
        // Returns IdentityResult — contains success flag and any errors
        var result = await _userManager.CreateAsync(user, dto.Password);
 
        if (!result.Succeeded)
        {
            // Combine all error messages into one string
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Registration failed: {errors}");
        }
 
        return "Registration successful.";
    }
 
    public async Task<AuthResponseDTO> LoginAsync(LoginDTO dto)
    {
        // Find user by email in AspNetUsers table
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password.");
 
        // CheckPasswordAsync hashes the input password and compares to stored hash
        // Returns true if they match, false if not
        var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!passwordValid)
            throw new UnauthorizedAccessException("Invalid email or password.");
 
        // Password is correct — generate JWT token
        var token  = GenerateJwtToken(user);
        var expiry = DateTime.UtcNow.AddHours(
            double.Parse(_config["Jwt:ExpiryHours"] ?? "2"));
 
        return new AuthResponseDTO
        {
            Token  = token,
            Email  = user.Email!,
            Expiry = expiry
        };
    }
 
    // Private helper — generates the JWT token string
    private string GenerateJwtToken(IdentityUser user)
    {
        // Claims are pieces of information embedded in the token
        // Any code that receives the token can read these without calling the database
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),     // user's unique ID
            new Claim(ClaimTypes.Email, user.Email!),          // user's email
            new Claim(JwtRegisteredClaimNames.Jti,             // unique token ID
                      Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email!) // subject
        };
 
        // Create the signing key from the secret in appsettings.json
        // This key is used to create and verify the signature
        var key   = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        // HmacSha256 — HMAC (Hash-based Message Authentication Code) using SHA256
        // Creates a signature that proves the token was created by this server
 
        // Build the token
        var token = new JwtSecurityToken(
            issuer:             _config["Jwt:Issuer"],
            audience:           _config["Jwt:Audience"],
            claims:             claims,
            expires:            DateTime.UtcNow.AddHours(
                                    double.Parse(_config["Jwt:ExpiryHours"] ?? "2")),
            signingCredentials: creds
        );
 
        // Serialize the token to its string form: header.payload.signature
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
