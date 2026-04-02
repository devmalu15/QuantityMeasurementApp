using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Ocelot configuratio
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

//CORS POLICY
builder.Services.AddCors(options =>
{
    options.AddPolicy("GatewayCorsPolicy", pb =>
    {
        pb.WithOrigins("https://qmabydevmalu.vercel.app")
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials(); 
    });
});

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();


app.UseCors("GatewayCorsPolicy");


await app.UseOcelot();

app.Run();