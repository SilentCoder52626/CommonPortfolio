global using FastEndpoints;

using CommonPortfolio.Api.DBSeeder;
using CommonPortfolio.Infrastructure.Configurations;
using CommonPortfolio.Infrastructure.Context;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;

var bld = WebApplication.CreateBuilder();

bld.Services
    .AddFastEndpoints()
    .SwaggerDocument();

bld.Services
   .AddAuthenticationJwtBearer(s => s.SigningKey = bld.Configuration["JWTSecret"])
   .AddAuthorization();

bld.Services.AddHttpClient();

bld.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseNpgsql(bld.Configuration.GetConnectionString("CommonPortfolioConnection"));
});

bld.Services.ConfigureServices();


var app = bld.Build();

app.UseAuthentication()
   .UseAuthorization()
   .UseFastEndpoints()
   .UseSwaggerGen();


DataSeeder.SeedDefaultData(app).Wait();

app.Run();
