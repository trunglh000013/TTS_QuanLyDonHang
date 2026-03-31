using Aspose.Cells;
using ProductTest.Application.Abstractions.Helpers;

namespace ProductTest.Infrastructure.Documents;

public class AsposeExcelRowWriter : IExcelRowWriter
{
    private readonly Worksheet _sheet;

    public AsposeExcelRowWriter(Worksheet sheet)
    {
        _sheet = sheet;
    }

    public void Write(int row, int col, object value)
    {
        _sheet.Cells[row, col].PutValue(value);
    }
}