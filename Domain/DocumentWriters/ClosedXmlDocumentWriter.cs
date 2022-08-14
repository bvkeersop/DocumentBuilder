using ClosedXML.Excel;
using NDocument.Domain.Exceptions;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Model.Excel;
using NDocument.Domain.Options;

namespace NDocument.Domain.DocumentWriters
{
    internal class ClosedXmlDocumentWriter : IExcelDocumentWriter, IDisposable
    {
        private bool _disposedValue;
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

        public void WriteToStream(Stream outputStream)
        {
            _workbook.SaveAs(outputStream);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _currentWorksheet = null;
                    _workbook.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~ClosedXmlDocumentWriter()
        {
            Dispose(disposing: false);
        }
    }
}
