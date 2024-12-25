using CommonBoilerPlateEight.Domain.Helper;
using CommonPortfolio.Domain.Helper.FileHelper;
using CommonPortfolio.Domain.Helper.Hasher;
using CommonPortfolio.Domain.Interfaces;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Domain.Services;
using CommonPortfolio.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommonPortfolio.Infrastructure.Configurations;

public static class ServiceConfigurations
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services,string connectionString)
    {
      services.AddDbContext<IDBContext, AppDBContext>(options =>
       {
           options.UseSqlServer(connectionString);
       });
        services.UseDIConfig();
        return services;
    }

    public static void UseDIConfig(this IServiceCollection services)
    {
        UseService(services);
    }

    private static void UseService(IServiceCollection services)
    {
        //Register services here
        services.AddScoped<IFileUploaderService, FileUploaderService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISkillTypeService, SkillTypeService>();
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<IHighlightDetailsService, HighlightDetailsService>();
        services.AddScoped<IAccountLinksService, AccountLinksService>();
        services.AddScoped<IEducationService, EducationService>();
        services.AddScoped<IExperienceService, ExperienceService>();
        services.AddScoped<IAccountDetailsService, AccountDetailsService>();

    }
}
