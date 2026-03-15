using Microsoft.EntityFrameworkCore;
using NLog.Web;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementRepositoryLayer.Context;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementRepositoryLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure NLog
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddSwaggerGen();

// Add DB Context
builder.Services.AddDbContext<QuantityMeasurementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add DI services
builder.Services.AddSingleton<IQuantityMeasurementRepository, QuantityMeasurementCacheRepository>();
builder.Services.AddScoped<IQuantityMeasurementRepositorySql, QuantityMeasurementEFRepository>();
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

// EF Core Migrations Comments
// Run the following commands in the Package Manager Console or CLI to create and apply migrations:
// dotnet ef migrations add InitialCreate --project QuantityMeasurementRepositoryLayer --startup-project QuantityMeasurementAPI
// dotnet ef database update --project QuantityMeasurementRepositoryLayer --startup-project QuantityMeasurementAPI

app.Run();
