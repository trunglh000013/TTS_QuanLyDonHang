using Microsoft.EntityFrameworkCore;
using ProductTest.Domain.Entities;

namespace ProductTest.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task InitializeAsync(ProductDbContext dbContext, CancellationToken cancellationToken = default)
    {
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
    }
}