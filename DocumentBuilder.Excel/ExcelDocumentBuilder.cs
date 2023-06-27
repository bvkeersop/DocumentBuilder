using DocumentBuilder.Excel.Model;
using DocumentBuilder.Excel.Options;
using DocumentBuilder.Exceptions;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Model;

namespace DocumentBuilder.DocumentBuilders
{
    public class ExcelDocumentBuilder : IExcelDocumentBuilder
    {
        private string? _currentWorksheetName = string.Empty;
        private readonly ExcelDocument _excelDocument;

        public ExcelDocumentBuilder() : this(new ExcelDocumentOptions()) { }

        public ExcelDocumentBuilder(ExcelDocumentOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));
            _excelDocument = new ExcelDocument(options);
        }

        /// <summary>
        /// Adds a new worksheet to the document
        /// </summary>
        /// <param name="worksheetName"></param>
        /// <returns><see cref="IExcelDocumentBuilder"/></returns>
        public IExcelDocumentBuilder AddWorksheet(string worksheetName)
        {
            _ = worksheetName ?? throw new ArgumentNullException(nameof(worksheetName));
            _currentWorksheetName = worksheetName;
            var worksheet = new Worksheet(worksheetName);
            _excelDocument.AddWorksheet(worksheet);
            return this;
        }

        /// <summary>
        /// Adds a table to the document at the current worksheet
        /// </summary>
        /// <typeparam name="TRow">The type of the row</typeparam>
        /// <param name="tableRows">The values of the table rows</param>
        /// <returns><see cref="IExcelDocumentBuilder"/></returns>
        public IExcelDocumentBuilder AddTable<T>(IEnumerable<T> tableRows)
        {
            _ = tableRows ?? throw new ArgumentNullException(nameof(tableRows));

            if (_currentWorksheetName == null)
            {
                throw new DocumentBuilderException(DocumentBuilderErrorCode.NoWorksheetInstantiated);
            }

            if (!tableRows.Any())
            {
                return this;
            }

            var worksheet = _excelDocument.GetWorksheet(_currentWorksheetName);
            worksheet.AddExcelElement(new Table<T>(tableRows));
            return this;
        }

        public ExcelDocument Build() => _excelDocument;
    }
}
