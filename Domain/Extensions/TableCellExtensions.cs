using NDocument.Domain.Model;
using NDocument.Domain.Utilities;

namespace NDocument.Domain.Extensions
{
    public static class TableCellExtensions
    {
        public static TableCell ShiftRow(this TableCell tableCell, int amountOfRows = 1)
        {
            var newRowPosition = tableCell.RowPosition + amountOfRows;
            return new TableCell(tableCell.Value, tableCell.Type, newRowPosition, tableCell.ColumnPosition);
        }

        public static ExcelTableCell ToExcelTableCell(this TableCell tableCell)
        {
            var excelColumnIdentifier = ExcelColumnIdentifierGenerator.GenerateColumnIdentifier(tableCell.ColumnPosition);
            return new ExcelTableCell(tableCell.Value, tableCell.RowPosition, excelColumnIdentifier);
        }
    }
}
