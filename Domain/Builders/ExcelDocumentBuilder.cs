using NDocument.Domain.DocumentWriters;
using NDocument.Domain.Exceptions;
using NDocument.Domain.Factories;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Model;
using NDocument.Domain.Model.Excel;
using NDocument.Domain.Options;

namespace NDocument.Domain.Builders
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

        public ExcelDocumentBuilder WithTable<T>(IEnumerable<T> tableRows)
        {
            if (_currentWorksheet == null)
            {
                throw new NDocumentException(NDocumentErrorCode.NoWorksheetInstantiated);
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
