using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Persistence;

namespace ProductTest.Infrastructure.Persistence.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).AsChar10Id();
        builder.Property(e => e.Code).AsVarchar20Code();
        builder.Property(e => e.CustomerId).AsChar10Id();
        builder.Property(e => e.CartId).AsChar10IdNullable();

        builder.Property(e => e.Status).HasMaxLength(50).IsRequired();
        builder.Property(e => e.SubTotal).HasPrecision(18, 2);
        builder.Property(e => e.TaxTotal).HasPrecision(18, 2);
        builder.Property(e => e.TotalAmount).HasPrecision(18, 2);

        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        builder.Property(e => e.IsActive).HasDefaultValue(true);

        builder.HasIndex(e => e.Code).IsUnique();

        builder.HasOne(e => e.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Cart)
            .WithMany()
            .HasForeignKey(e => e.CartId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
