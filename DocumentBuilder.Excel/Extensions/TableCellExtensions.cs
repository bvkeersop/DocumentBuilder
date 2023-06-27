using DocumentBuilder.Model.Excel;
using DocumentBuilder.Core.Model;
using DocumentBuilder.Utilities;

namespace DocumentBuilder.Excel.Extensions;

internal static class TableCellExtensions
{
    public static DocumentBuilder.Model.Excel.TableCell ToExcelTableCell(this Shared.Model.TableCell tableCell)
    {
        // +1 since excel starts at 1, not 0
        var excelColumnIdentifier = ExcelColumnIdentifierGenerator.GenerateColumnIdentifier(tableCell.ColumnPosition + 1);
        return new DocumentBuilder.Model.Excel.TableCell(tableCell.Value, tableCell.RowPosition + 1, excelColumnIdentifier);
    }
}
