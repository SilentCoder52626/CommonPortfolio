﻿using CommonPortfolio.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CommonPortfolio.Infrastructure.Configurations;

public static class ServiceConfigurations
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
      
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
    }
}