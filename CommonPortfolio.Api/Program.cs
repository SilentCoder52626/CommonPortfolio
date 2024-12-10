global using FastEndpoints;
global using CommonPortfolio.Domain.Models.User;
global using CommonPortfolio.Domain.Interfaces;
global using CommonPortfolio.Infrastructure.Context;

using CommonPortfolio.Api.DBSeeder;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Infrastructure.Configurations;
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

bld.Services.AddDbContext<IDBContext,AppDBContext>(options =>
{
    options.UseNpgsql(bld.Configuration.GetConnectionString("CommonPortfolioConnection"));
});

bld.Services.ConfigureServices();


var app = bld.Build();

app.UseAuthentication()
   .UseAuthorization()
   .UseDefaultExceptionHandler()
   .UseFastEndpoints()
   .UseSwaggerGen();


DataSeeder.SeedDefaultData(app).Wait();

app.Run();
