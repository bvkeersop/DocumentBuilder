using DocumentBuilder.Exceptions;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Model.Excel;
using DocumentBuilder.Model;
using DocumentBuilder.Excel.Options;
using DocumentBuilder.Excel;
using DocumentBuilder.Excel.Model;

namespace DocumentBuilder.DocumentBuilders
{
    internal class ExcelDocumentBuilder : IExcelDocumentBuilder
    {
        private string? _currentWorksheet = string.Empty;
        private readonly IExcelDocumentWriter _excelDocumentWriter;

        private readonly ExcelDocument _excelDocument;

        public ExcelDocumentBuilder() : this(new ExcelDocumentOptions()) { }

        public ExcelDocumentBuilder(ExcelDocumentOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));
            _excelDocumentWriter = new ClosedXmlDocumentWriter(XLWorkbookFactory.Create, options);
            _excelDocument = new ExcelDocument();
        }

        /// <summary>
        /// Adds a new worksheet to the document
        /// </summary>
        /// <param name="worksheetName"></param>
        /// <returns><see cref="IExcelDocumentBuilder"/></returns>
        public IExcelDocumentBuilder AddWorksheet(string worksheetName)
        {
            _ = worksheetName ?? throw new ArgumentNullException(nameof(worksheetName));
            _currentWorksheet = worksheetName;
            _excelDocumentWriter.AddWorksheet(worksheetName);
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

            if (_currentWorksheet == null)
            {
                throw new DocumentBuilderException(DocumentBuilderErrorCode.NoWorksheetInstantiated);
            }

            if (!tableRows.Any())
            {
                return this;
            }

            var worksheetExcelConvertable = new WorksheetExcelConvertable(_currentWorksheet, new Table<T>(tableRows));
            WorksheetExcelConvertables = WorksheetExcelConvertables.Append(worksheetExcelConvertable);
            return this;
        }

        /// <summary>
        /// Writes the document to the provided output stream
        /// </summary>
        /// <param name="outputStream">The output stream</param>
        public void Build(Stream outputStream)
        {
            _ = outputStream ?? throw new ArgumentNullException(nameof(outputStream));
            foreach (var worksheetExcelConvertable in WorksheetExcelConvertables)
            {
                _excelDocumentWriter.Write(worksheetExcelConvertable);
            }
            _excelDocumentWriter.WriteToStream(outputStream);
        }

        /// <summary>
        /// Writes the document to the provided path, will replace existing documents
        /// </summary>
        /// <param name="filePath">The path which the file should be written to</param>
        /// <returns><see cref="Task"/></returns>
        public void Build(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("filePath cannot be null or empty");
            }

            using FileStream fileStream = File.Create(filePath);
            foreach (var worksheetExcelConvertable in WorksheetExcelConvertables)
            {
                _excelDocumentWriter.Write(worksheetExcelConvertable);
            }
            _excelDocumentWriter.WriteToStream(fileStream);
        }
    }
}
