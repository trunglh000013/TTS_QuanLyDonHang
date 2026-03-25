using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Persistence;

namespace ProductTest.Infrastructure.Persistence.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(product => product.Id);

        builder.Property(product => product.Id).AsChar10Id();
        builder.Property(product => product.Code).AsVarchar20Code();

        builder.Property(product => product.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(product => product.Description)
            .HasMaxLength(1000);

        builder.Property(product => product.Category)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(product => product.Price)
            .HasPrecision(18, 2);

        builder.Property(product => product.TaxRate)
            .HasPrecision(5, 2)
            .HasDefaultValue(10.00m);

        builder.Property(product => product.SupplierId).AsChar10IdNullable();

        builder.Property(product => product.CreatedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        builder.Property(product => product.ExpiredDT)
            .IsRequired();

        builder.HasCheckConstraint("CK_Products_ExpiredDT", "ExpiredDT > GETUTCDATE()");

        builder.HasOne(product => product.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(product => product.SupplierId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(product => product.Code).IsUnique();
        builder.HasIndex(product => product.Name);
        builder.HasIndex(product => product.Category);
    }
}
