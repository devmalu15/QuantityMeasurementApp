
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementRepositoryLayer.Repositories;
 
var builder = WebApplication.CreateBuilder(args);
 
// ── Service Registration ──────────────────────────────────────────────────────
 
// Controllers — scans for all classes that extend ControllerBase
builder.Services.AddControllers();
 
// Swagger — API documentation UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
// Redis Cache — registers IDistributedCache backed by Redis
// InstanceName prefixes all keys: "QMA_measurement_xxx", "QMA_all_measurement_keys"
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
    options.InstanceName  = "QMA_";
});
 
// Repository Layer
// IQuantityMeasurementRepository → QuantityMeasurementRedisRepository (NEW Redis-backed cache)
// IQuantityMeasurementRepositorySql → QuantityMeasurementSqlRepository (unchanged ADO.NET)
builder.Services.AddScoped<IQuantityMeasurementRepository,    QuantityMeasurementRedisRepository>();
builder.Services.AddScoped<IQuantityMeasurementRepositorySql, QuantityMeasurementSqlRepository>();
 
// Business Layer
// QuantityMeasurementServiceImpl gets both repos injected automatically
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();
 
// IConfiguration — needed by SqlRepository constructor to read connection string
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
 
// ── Build ─────────────────────────────────────────────────────────────────────
 
var app = builder.Build();
 
// ── Middleware Pipeline ───────────────────────────────────────────────────────
 
// Swagger UI — only in Development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
// Routing and Controllers
app.UseRouting();
app.MapControllers();
 
app.Run();
 
