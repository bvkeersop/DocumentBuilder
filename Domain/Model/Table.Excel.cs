using ClosedXML.Excel;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Options;
using NDocument.Domain.Utilities;

namespace NDocument.Domain.Model
{
    public partial class Table<TValue> : IExcelWritable
    {
        private void CreateExcelTable(ExcelDocumentOptions options)
        {
            if (string.IsNullOrEmpty(options.WorksheetName))
            {
                throw new ArgumentException($"{nameof(options.WorksheetName)} cannot be null or empty");
            }

            if (string.IsNullOrEmpty(options.FilePath))
            {
                throw new ArgumentException($"{nameof(options.FilePath)} cannot be null or empty");
            }

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(options.WorksheetName);

            CreateExcelTableHeader(worksheet);
            CreateExcelTableRows(worksheet);

            workbook.SaveAs(options.FilePath);
        }

        private void CreateExcelTableHeader(IXLWorksheet worksheet)
        {
            var numberOfColumns = TableValues.NumberOfColumns;

            for (var i = 0; i < numberOfColumns; i++)
            {
                var columnId = ExcelColumnIdentifierGenerator.GenerateColumnIdentifier(i + 1);
                var columnName = OrderedColumnAttributes.ElementAt(i).Name;
                CreateExcelTableCell(columnName, XLBorderStyleValues.Thin, 1, columnId, worksheet);
            }
        }

        private void CreateExcelTableRows(IXLWorksheet worksheet)
        {
            var numberOfRows = TableValues.NumberOfRows;

            for (var i = 0; i < numberOfRows; i++)
            {
                var currentRow = TableValues.GetRow(i);
                var excelRowNumber = i + 2;
                CreateExcelTableRow(currentRow, excelRowNumber, worksheet);
            }
        }

        private static void CreateExcelTableRow(TableCell[] tableRow, int rowNumber, IXLWorksheet worksheet)
        {
            for (var i = 0; i < tableRow.Length; i++)
            {
                var tableCell = tableRow[i];
                var columnId = ExcelColumnIdentifierGenerator.GenerateColumnIdentifier(i + 1);
                CreateExcelTableCell(tableCell.Value, XLBorderStyleValues.Thin, rowNumber, columnId, worksheet);
            }
        }

        private static void CreateExcelTableCell(string cellValue, XLBorderStyleValues borderStyle, int rowNumber, string columnIdentifier, IXLWorksheet worksheet)
        {
            var currentCell = worksheet.Cell(rowNumber, columnIdentifier);
            currentCell.SetValue(cellValue);
            currentCell.Style.Border.OutsideBorder = borderStyle;
        }
    }
}
