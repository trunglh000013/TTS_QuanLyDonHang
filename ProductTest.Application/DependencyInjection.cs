using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductTest.Application.Common.Behaviors;

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

        return services;
    }
}