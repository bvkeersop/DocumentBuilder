using DocumentBuilder.Exceptions;
using DocumentBuilder.Factories;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Model.Excel;
using DocumentBuilder.Options;
using DocumentBuilder.DocumentWriters;
using DocumentBuilder.Model;

namespace DocumentBuilder.DocumentBuilders
{
    internal class ExcelDocumentBuilder
    {
        private string? _currentWorksheet = string.Empty;
        public IEnumerable<WorksheetExcelConvertable> WorksheetExcelConvertables { get; private set; } = Enumerable.Empty<WorksheetExcelConvertable>();
        private readonly IExcelDocumentWriter _excelDocumentWriter;

        public ExcelDocumentBuilder(ExcelDocumentOptions options)
        {
            _excelDocumentWriter = new ClosedXmlDocumentWriter(XLWorkbookFactory.Create, options);
        }

        public ExcelDocumentBuilder AddWorksheet(string worksheetName)
        {
            _currentWorksheet = worksheetName;
            _excelDocumentWriter.AddWorksheet(worksheetName);
            return this;
        }

        public ExcelDocumentBuilder AddTable<T>(IEnumerable<T> tableRows)
        {
            if (_currentWorksheet == null)
            {
                throw new DocumentBuilderException(DocumentBuilderErrorCode.NoWorksheetInstantiated);
            }

            var worksheetExcelConvertable = new WorksheetExcelConvertable(_currentWorksheet, new Table<T>(tableRows));
            WorksheetExcelConvertables = WorksheetExcelConvertables.Append(worksheetExcelConvertable);
            return this;
        }

        public void WriteToStream(Stream outputStream)
        {
            foreach (var worksheetExcelConvertable in WorksheetExcelConvertables)
            {
                _excelDocumentWriter.Write(worksheetExcelConvertable);
            }
            _excelDocumentWriter.WriteToStream(outputStream);
        }
    }
}
