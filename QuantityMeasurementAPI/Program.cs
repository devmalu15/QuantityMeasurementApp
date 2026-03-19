 
using Microsoft.EntityFrameworkCore;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementRepositoryLayer.Context;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementRepositoryLayer.Repositories;
 
var builder = WebApplication.CreateBuilder(args);
 
// Service Registration
 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
// Redis 
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
    options.InstanceName  = "QMA_";
});
 
// Entity Framework Core — DbContext registered with SQL Server provider
// Reads connection string from appsettings.json
builder.Services.AddDbContext<QuantityMeasurementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
 
// Repository Layer
// IQuantityMeasurementRepository
// IQuantityMeasurementRepositorySql 
builder.Services.AddScoped<IQuantityMeasurementRepository,    QuantityMeasurementRedisRepository>();
builder.Services.AddScoped<IQuantityMeasurementRepositorySql, QuantityMeasurementEFRepository>();
 
// Business Layer
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();
 
// IConfiguration 
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
 
//Build
 
var app = builder.Build();
 
// Middleware
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseRouting();
app.MapControllers();
 
app.Run();
