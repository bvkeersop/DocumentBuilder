using NDocument.Domain.Model.Excel;
using NDocument.Domain.Model.Generic;
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
            // +1 since excel starts at 1, not 0
            var excelColumnIdentifier = ExcelColumnIdentifierGenerator.GenerateColumnIdentifier(tableCell.ColumnPosition + 1);
            return new ExcelTableCell(tableCell.Value, tableCell.RowPosition + 1, excelColumnIdentifier);
        }
    }
}
