using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductTest.Domain.Entities;
using ProductTest.Infrastructure.Persistence;

namespace ProductTest.Infrastructure.Persistence.Configurations;

public sealed class ProductRatingConfiguration : IEntityTypeConfiguration<ProductRating>
{
    public void Configure(EntityTypeBuilder<ProductRating> builder)
    {
        builder.ToTable("ProductRatings");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnType("char(10)").IsFixedLength().IsUnicode(false);
        builder.Property(e => e.Code).HasMaxLength(20).IsUnicode(false);
        builder.Property(e => e.ProductId).HasColumnType("char(10)").IsFixedLength().IsUnicode(false);
        builder.Property(e => e.CustomerId).HasColumnType("char(10)").IsFixedLength().IsUnicode(false);

        builder.Property(e => e.Content).IsRequired();

        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        builder.Property(e => e.IsActive).HasDefaultValue(true);

        builder.HasCheckConstraint("CK_ProductRatings_Stars", "Stars >= 1 AND Stars <= 5");

        builder.HasIndex(e => e.Code).IsUnique();

        builder.HasOne(e => e.Product)
            .WithMany(p => p.ProductRatings)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Customer)
            .WithMany(c => c.ProductRatings)
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => new { e.ProductId, e.CustomerId });
    }
}
