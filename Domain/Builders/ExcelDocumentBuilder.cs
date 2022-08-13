using NDocument.Domain.Exceptions;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Model;
using NDocument.Domain.Options;
using NDocument.Domain.Writers;

namespace NDocument.Domain.Builders
{
    internal class ExcelDocumentBuilder
    {
        private string? _currentWorksheet = string.Empty;
        public IEnumerable<WorksheetExcelConvertable> WorksheetExcelConvertables { get; private set; } = Enumerable.Empty<WorksheetExcelConvertable>();
        private readonly IExcelDocumentWriter _excelDocumentWriter;

        public ExcelDocumentBuilder(ExcelDocumentOptions options)
        {
            _excelDocumentWriter = new ClosedXmlDocumentWriter(options);
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

        public void Save(string filePath)
        {
            foreach (var worksheetExcelConvertable in WorksheetExcelConvertables)
            {
                _excelDocumentWriter.Write(worksheetExcelConvertable);
            }
            _excelDocumentWriter.Save(filePath);
        }
    }
}
