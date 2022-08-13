using ClosedXML.Excel;
using NDocument.Domain.Exceptions;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Model;
using NDocument.Domain.Options;

namespace NDocument.Domain.Writers
{
    internal class ClosedXmlDocumentWriter : IExcelDocumentWriter
    {
        private readonly XLWorkbook _workbook;
        private readonly ExcelDocumentOptions _options;
        private IXLWorksheet? _currentWorksheet;

        public ClosedXmlDocumentWriter(ExcelDocumentOptions options)
        {
            _workbook = new XLWorkbook();
            _options = options;
        }

        public void AddWorksheet(string worksheetName)
        {
            _currentWorksheet = _workbook.Worksheets.Add(worksheetName);
        }

        public void Write(IExcelConvertable excelConvertable)
        {
            if (_currentWorksheet == null)
            {
                throw new NDocumentException(NDocumentErrorCode.NoWorksheetInstantiated);
            }

            var excelTableCells = excelConvertable.ToExcel(_options);
            WriteExcelTableCells(_currentWorksheet, excelTableCells);
        }

        private static void WriteExcelTableCells(IXLWorksheet worksheet, IEnumerable<ExcelTableCell> excelTableCells)
        {
            foreach (var excelTableCell in excelTableCells)
            {
                var currentCell = worksheet.Cell(excelTableCell.ExcelRowIdentifier, excelTableCell.ExcelColumnIdentifier);
                currentCell.SetValue(excelTableCell.Value);
            }
        }

        public void Save(string filePath)
        {
            _workbook.SaveAs(filePath);
        }
    }
}
