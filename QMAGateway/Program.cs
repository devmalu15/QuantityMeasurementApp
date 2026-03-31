using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Ocelot configuration file
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// 2. DEFINE THE CORS POLICY
builder.Services.AddCors(options =>
{
    options.AddPolicy("GatewayCorsPolicy", pb =>
    {
        pb.WithOrigins("https://quantity-measurement-frontend.vercel.app")
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials(); 
    });
});

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// 3. APPLY THE CORS POLICY 
// This MUST come before app.UseOcelot().Wait()
app.UseCors("GatewayCorsPolicy");

// 4. Use Ocelot
await app.UseOcelot();

app.Run();