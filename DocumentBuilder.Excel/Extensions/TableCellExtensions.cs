namespace DocumentBuilder.Excel.Extensions;

internal static class TableCellExtensions
{
    public static Model.TableCell ToExcelTableCell(this Core.Model.TableCell tableCell)
    {
        // +1 since excel starts at 1, not 0
        var excelColumnIdentifier = ExcelColumnIdentifierGenerator.GenerateColumnIdentifier(tableCell.ColumnPosition + 1);
        return new Model.TableCell(tableCell.Value, tableCell.RowPosition + 1, excelColumnIdentifier);
    }
}
