using Ocelot.DependencyInjection;
using Ocelot.Middleware;
 
var builder = WebApplication.CreateBuilder(args);
 
// Load ocelot.json alongside appsettings.json
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
 
// CORS — allow Angular frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
 
builder.Services.AddOcelot(builder.Configuration);
 
var app = builder.Build();
 
app.UseCors("AllowAngular");
 
// Ocelot middleware handles ALL routing — no MapControllers needed
await app.UseOcelot();
 
app.Run();
