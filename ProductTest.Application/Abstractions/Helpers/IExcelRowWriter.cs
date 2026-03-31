namespace ProductTest.Application.Abstractions.Helpers;

public interface IExcelRowWriter
{
    void Write(int row, int col, object value);
}