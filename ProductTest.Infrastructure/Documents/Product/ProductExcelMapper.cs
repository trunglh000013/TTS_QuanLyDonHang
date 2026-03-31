using ProductTest.Application.Abstractions.Helpers;
using ProductTest.Domain.Entities;

public static class ProductExcelMapper
{
    public static void Map(IExcelRowWriter writer, Product product, int row)
    {
        writer.Write(row, 0, row);
        writer.Write(row, 1, product.Id);
        writer.Write(row, 2, product.Code);
        writer.Write(row, 3, product.Name);
        writer.Write(row, 4, product.Description);
        writer.Write(row, 5, product.Category);
        writer.Write(row, 6, product.Price);
        writer.Write(row, 7, product.TaxRate);
        writer.Write(row, 8, product.Stock);
        writer.Write(row, 9, product.SupplierId);
        writer.Write(row, 10, product.IsActive);
        writer.Write(row, 11, product.CreatedAt);
        writer.Write(row, 12, product.UpdatedAt);
        writer.Write(row, 13, product.ExpiredDT);
    }
}