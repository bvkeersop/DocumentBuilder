using DocumentBuilder.Model.Excel;
using DocumentBuilder.Shared.Model;
using DocumentBuilder.Utilities;

namespace DocumentBuilder.Excel.Extensions;

internal static class TableCellExtensions
{
    public static ExcelTableCell ToExcelTableCell(this TableCell tableCell)
    {
        // +1 since excel starts at 1, not 0
        var excelColumnIdentifier = ExcelColumnIdentifierGenerator.GenerateColumnIdentifier(tableCell.ColumnPosition + 1);
        return new ExcelTableCell(tableCell.Value, tableCell.RowPosition + 1, excelColumnIdentifier);
    }
}
