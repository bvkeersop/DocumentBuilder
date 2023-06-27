using ClosedXML.Excel;
using DocumentBuilder.Excel.Model;
using DocumentBuilder.Excel.Options;
using DocumentBuilder.Model.Excel;

namespace DocumentBuilder.Excel
{
    public interface IExcelDocumentWriter
    {
        public void WriteToStream(ExcelDocument excelDocument, Stream outputStream);
    }

    internal class ClosedXmlDocumentWriter : IExcelDocumentWriter, IDisposable
    {
        private bool _disposedValue;
        private readonly IXLWorkbook _workbook;
        private readonly ExcelDocumentOptions _options;

        public ClosedXmlDocumentWriter(Func<IXLWorkbook> factory, ExcelDocumentOptions options)
        {
            _workbook = factory();
            _options = options;
        }

        public void Write(ExcelDocument excelDocument)
        {
            foreach (var worksheet in excelDocument.Worksheets)
            {
                var closedXmlWorksheet = _workbook.Worksheet(worksheet.Name);
                foreach (var element in worksheet.ExcelElements)
                {
                    var excelTableCells = element.ToExcel(_options);
                    WriteExcelTableCells(closedXmlWorksheet, excelTableCells);
                }
            }
        }

        private static void WriteExcelTableCells(IXLWorksheet worksheet, IEnumerable<TableCell> excelTableCells)
        {
            foreach (var excelTableCell in excelTableCells)
            {
                var currentCell = worksheet.Cell(excelTableCell.ExcelRowIdentifier, excelTableCell.ExcelColumnIdentifier);
                currentCell.SetValue(excelTableCell.Value);
            }
        }

        public void WriteToStream(ExcelDocument excelDocument, Stream outputStream)
        {
            Write(excelDocument);
            _workbook.SaveAs(outputStream);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
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
