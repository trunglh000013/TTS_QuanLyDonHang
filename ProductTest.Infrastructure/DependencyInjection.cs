using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductTest.Application.Abstractions;
using ProductTest.Application.Abstractions.CartAbstractions;
using ProductTest.Application.Abstractions.OrderAbstractions;
using ProductTest.Application.Abstractions.CustomerAbstractions;
using ProductTest.Application.Abstractions.SupplierAbstractions;
using ProductTest.Application.Abstractions.ProductAbstractions;
using ProductTest.Infrastructure.Persistence;
using ProductTest.Infrastructure.Repositories.CartRepository;
using ProductTest.Infrastructure.Repositories.OrderRepository;
using ProductTest.Infrastructure.Repositories.ProductRepository;
using ProductTest.Infrastructure.Repositories.CustomerRepository;
using ProductTest.Infrastructure.Repositories.SupplierRepository;
using ProductTest.Infrastructure.Repositories.ProductRatingRepository;
using ProductTest.Application.Abstractions.ProductRatingAbstractions;
using ProductTest.Infrastructure.Common;
using ProductTest.Application.Abstractions.UserAbstractions;
using ProductTest.Application.Abstractions.RoleAbstractions;
using ProductTest.Application.Abstractions.PermissionAbstractions;
using ProductTest.Application.Abstractions.UserRoleAbstractions;
using ProductTest.Application.Abstractions.RolePermissionAbstractions;
using ProductTest.Application.Abstractions.UserPermissionAbstractions;
using ProductTest.Application.Abstractions.UserTokenAbstractions;
using ProductTest.Infrastructure.Repositories.UserRepository;
using ProductTest.Infrastructure.Repositories.RoleRepository;
using ProductTest.Infrastructure.Repositories.PermissionRepository;
using ProductTest.Infrastructure.Repositories.UserRoleRepository;
using ProductTest.Infrastructure.Repositories.RolePermissionRepository;
using ProductTest.Infrastructure.Repositories.UserPermissionRepository;
using ProductTest.Infrastructure.Repositories.UserTokenRepository;

namespace ProductTest.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

        services.AddDbContext<ProductDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IProductRepositoryV1, ProductRepositoryV1>();
        services.AddScoped<IProductRepositoryV2, ProductRepositoryV2>();
        services.AddScoped<ISupplierRepositoryV2, SupplierRepositoryV2>();
        services.AddScoped<ICustomerRepositoryV2, CustomerRepositoryV2>();
        services.AddScoped<ICartRepositoryV2, CartRepositoryV2>();
        services.AddScoped<IOrderRepositoryV2, OrderRepositoryV2>();
        services.AddScoped<IProductRatingRepositoryV2, ProductRatingRepositoryV2>();
        services.AddScoped<IUserRepositoryV2, UserRepositoryV2>();
        services.AddScoped<IRoleRepositoryV2, RoleRepositoryV2>();
        services.AddScoped<IPermissionRepositoryV2, PermissionRepositoryV2>();
        services.AddScoped<IUserRoleRepositoryV2, UserRoleRepositoryV2>();
        services.AddScoped<IRolePermissionRepositoryV2, RolePermissionRepositoryV2>();
        services.AddScoped<IUserPermissionRepositoryV2, UserPermissionRepositoryV2>();
        services.AddScoped<IUserTokenRepositoryV2, UserTokenRepositoryV2>();
        services.AddScoped<IStoreProcedureRunner, StoreProcedureRunner>();

        return services;
    }
}