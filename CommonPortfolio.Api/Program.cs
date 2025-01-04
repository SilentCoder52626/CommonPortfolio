global using FastEndpoints;
global using CommonPortfolio.Domain.Models.User;
global using CommonPortfolio.Domain.Interfaces;
global using CommonPortfolio.Infrastructure.Context;
global using CommonBoilerPlateEight.Domain.Constants;


using CommonPortfolio.Api.DBSeeder;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Infrastructure.Configurations;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using CommonPortfolio.Domain.Entity.Email;
using Microsoft.AspNetCore.Http.Features;

var bld = WebApplication.CreateBuilder();

bld.Services
    .AddFastEndpoints()
    .SwaggerDocument();

bld.Services
   .AddAuthenticationJwtBearer(s => s.SigningKey = bld.Configuration["JWTSecret"])
   .AddAuthorization();

bld.Services.AddHttpClient();

var corsSites = bld.Configuration.GetSection("CORSSites").Get<string[]>();

bld.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins(corsSites) // frontend's origin
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
var connectionString = bld.Configuration.GetConnectionString("CommonPortfolioConnection");
bld.Services.ConfigureServices(connectionString);


bld.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

var emailConfig = bld.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
bld.Services.AddSingleton(emailConfig);

var app = bld.Build();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication()
   .UseAuthorization()
   .UseDefaultExceptionHandler()
   .UseFastEndpoints()
   .UseSwaggerGen();


DataSeeder.SeedDefaultData(app).Wait();

app.Run();
