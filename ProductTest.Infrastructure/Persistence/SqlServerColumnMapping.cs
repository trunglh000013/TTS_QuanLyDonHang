using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProductTest.Infrastructure.Persistence;

/// <summary>
/// Quy ước cột khớp với <c>data.txt</c>: Id <c>CHAR(10)</c>, Code <c>VARCHAR(20)</c> (ASCII), FK Id cùng kiểu.
/// </summary>
internal static class SqlServerColumnMapping
{
    public static PropertyBuilder<string> AsChar10Id(this PropertyBuilder<string> builder) =>
        builder.HasColumnType("char(10)").IsFixedLength().IsUnicode(false);

    public static PropertyBuilder<string?> AsChar10IdNullable(this PropertyBuilder<string?> builder) =>
        builder.HasColumnType("char(10)").IsFixedLength().IsUnicode(false);

    public static PropertyBuilder<string> AsVarchar20Code(this PropertyBuilder<string> builder) =>
        builder.HasMaxLength(20).IsUnicode(false);

    public static PropertyBuilder<string?> AsVarchar20CodeNullable(this PropertyBuilder<string?> builder) =>
        builder.HasMaxLength(20).IsUnicode(false);
}
