using CommonBoilerPlateEight.Domain.Helper;
using CommonPortfolio.Domain.Helper.CloudinaryHelper;
using CommonPortfolio.Domain.Helper.Hasher;
using CommonPortfolio.Domain.Interfaces;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Domain.Interfaces.Email;
using CommonPortfolio.Domain.Services;
using CommonPortfolio.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceModule.Service.Email;

namespace CommonPortfolio.Infrastructure.Configurations;

public static class ServiceConfigurations
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services,string connectionString)
    {
      services.AddDbContext<IDBContext, AppDBContext>(options =>
       {
           options.UseNpgsql(connectionString);
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
        services.AddScoped<IPhotoAccessor, PhotoAccessor>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISkillTypeService, SkillTypeService>();
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<IHighlightDetailsService, HighlightDetailsService>();
        services.AddScoped<IAccountLinksService, AccountLinksService>();
        services.AddScoped<IEducationService, EducationService>();
        services.AddScoped<IExperienceService, ExperienceService>();
        services.AddScoped<IAccountDetailsService, AccountDetailsService>();
        services.AddScoped<IEmailSenderService, EmailSenderService>();

    }
}
