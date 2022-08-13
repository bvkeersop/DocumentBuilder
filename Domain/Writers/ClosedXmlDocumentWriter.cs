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

        public void Write(WorksheetExcelConvertable worksheetExcelConvertable)
        {
            if (_currentWorksheet == null)
            {
                throw new NDocumentException(NDocumentErrorCode.NoWorksheetInstantiated);
            }

            var excelTableCells = worksheetExcelConvertable.ExcelConvertable.ToExcel(_options);
            WriteExcelTableCells(worksheetExcelConvertable.WorksheetName, excelTableCells);
        }

        private void WriteExcelTableCells(string worksheetName, IEnumerable<ExcelTableCell> excelTableCells)
        {
            var worksheet = _workbook.Worksheet(worksheetName);

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
