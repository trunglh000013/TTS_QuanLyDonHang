using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Persistence;

namespace ProductTest.Infrastructure.Persistence.Configurations;

public sealed class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Carts");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).AsChar10Id();
        builder.Property(e => e.Code).AsVarchar20Code();
        builder.Property(e => e.CustomerId).AsChar10Id();

        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        builder.Property(e => e.IsActive).HasDefaultValue(true);

        builder.HasIndex(e => e.Code).IsUnique();

        builder.HasOne(e => e.Customer)
            .WithMany(c => c.Carts)
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
