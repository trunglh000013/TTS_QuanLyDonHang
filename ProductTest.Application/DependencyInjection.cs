using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductTest.Application.Common.Behaviors;
using ProductTest.Application.Abstractions.Helpers;
using ProductTest.Application.Helpers.Implements;

namespace ProductTest.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(_ => { }, typeof(DependencyInjection).Assembly);

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(RequestLoggingBehavior<,>));
        });

        // Auth helpers used by v2/Auth command handlers
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}