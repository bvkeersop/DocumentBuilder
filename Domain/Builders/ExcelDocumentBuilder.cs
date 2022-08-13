using NDocument.Domain.Interfaces;
using NDocument.Domain.Model;
using NDocument.Domain.Options;
using NDocument.Domain.Writers;

namespace NDocument.Domain.Builders
{
    internal class ExcelDocumentBuilder
    {
        public IEnumerable<IExcelConvertable> ExcelConvertables { get; private set; } = Enumerable.Empty<IExcelConvertable>();
        private readonly IExcelDocumentWriter _excelDocumentWriter;

        public ExcelDocumentBuilder(ExcelDocumentOptions options)
        {
            _excelDocumentWriter = new ClosedXmlDocumentWriter(options);
        }

        public ExcelDocumentBuilder AddWorksheet(string worksheetName)
        {
            _excelDocumentWriter.AddWorksheet(worksheetName);
            return this;
        }

        public ExcelDocumentBuilder WithTable<T>(IEnumerable<T> tableRows)
        {
            ExcelConvertables = ExcelConvertables.Append(new Table<T>(tableRows));
            return this;
        }

        public void Build(string filePath)
        {
            _excelDocumentWriter.Save(filePath);
        }
    }
}
