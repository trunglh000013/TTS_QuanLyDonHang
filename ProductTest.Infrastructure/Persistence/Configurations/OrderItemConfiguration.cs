using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Persistence;

namespace ProductTest.Infrastructure.Persistence.Configurations;

public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).AsChar10Id();
        builder.Property(e => e.Code).AsVarchar20Code();
        builder.Property(e => e.OrderId).AsChar10Id();
        builder.Property(e => e.ProductId).AsChar10Id();

        builder.Property(e => e.UnitPrice).HasPrecision(18, 2);
        builder.Property(e => e.TaxRate).HasPrecision(3, 2);
        builder.Property(e => e.LineSubTotal).HasPrecision(18, 2);
        builder.Property(e => e.TaxAmount).HasPrecision(18, 2);
        builder.Property(e => e.LineTotal).HasPrecision(18, 2);

        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        builder.Property(e => e.IsActive).HasDefaultValue(true);

        builder.HasIndex(e => e.Code).IsUnique();

        builder.HasOne(e => e.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(e => e.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
