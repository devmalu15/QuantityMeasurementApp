 
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementRepositoryLayer.Context;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementRepositoryLayer.Repositories;
 
var builder = WebApplication.CreateBuilder(args);
 
// ── Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
 
// Swagger configured with JWT support — allows entering token in Swagger UI
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quantity Measurement API", Version = "v1" });
 
    // Add JWT security definition to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter: Bearer {your JWT token}",
        Name        = "Authorization",
        In          = ParameterLocation.Header,
        Type        = SecuritySchemeType.ApiKey,
        Scheme      = "Bearer"
    });
 
    // Apply the security requirement to all endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
 
// Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
    options.InstanceName  = "QMA_";
});
 
// Entity Framework Core
builder.Services.AddDbContext<QuantityMeasurementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
 
// ASP.NET Core Identity
// Registers UserManager, SignInManager, RoleManager
// Uses IdentityUser (built-in user class) and QuantityMeasurementDbContext for storage
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Password rules — customize as needed
    options.Password.RequireDigit           = true;
    options.Password.RequireLowercase       = true;
    options.Password.RequireUppercase       = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength         = 6;
 
    // User rules
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<QuantityMeasurementDbContext>() // use EF to store users
.AddDefaultTokenProviders();                              // for password reset tokens
 
// JWT Authentication
// Configures how incoming tokens are validated
builder.Services.AddAuthentication(options =>
{
    // Set JWT as the default scheme for both authentication and challenges
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer           = true,  // check token issuer matches appsettings
        ValidateAudience         = true,  // check token audience matches appsettings
        ValidateLifetime         = true,  // reject expired tokens
        ValidateIssuerSigningKey = true,  // verify signature with secret key
 
        ValidIssuer      = builder.Configuration["Jwt:Issuer"],
        ValidAudience    = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});
 
// Authorization — checks [Authorize] attributes on controllers
builder.Services.AddAuthorization();
 
// Repository Layer 
builder.Services.AddScoped<IQuantityMeasurementRepository,    QuantityMeasurementRedisRepository>();
builder.Services.AddScoped<IQuantityMeasurementRepositorySql, QuantityMeasurementEFRepository>();
 
// Business Layer
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();
builder.Services.AddScoped<IAuthService, AuthServiceImpl>();  // NEW
 
// Configuration
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
 
// Build 
var app = builder.Build();
 
// Middleware Pipeline 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseRouting();
 
// ORDER MATTERS: Authentication must come before Authorization
app.UseAuthentication();  // NEW — validates JWT token on every request
app.UseAuthorization();   // NEW — checks [Authorize] attributes
 
app.MapControllers();
app.Run();
