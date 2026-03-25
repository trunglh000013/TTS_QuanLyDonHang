using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Persistence;

namespace ProductTest.Infrastructure.Persistence.Configurations;

public sealed class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).AsChar10Id();
        builder.Property(e => e.Code).AsVarchar20Code();
        builder.Property(e => e.CartId).AsChar10Id();
        builder.Property(e => e.ProductId).AsChar10Id();

        builder.Property(e => e.Price).HasPrecision(18, 2);

        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        builder.Property(e => e.IsActive).HasDefaultValue(true);

        builder.HasIndex(e => e.Code).IsUnique();

        builder.HasOne(e => e.Cart)
            .WithMany(c => c.Items)
            .HasForeignKey(e => e.CartId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
