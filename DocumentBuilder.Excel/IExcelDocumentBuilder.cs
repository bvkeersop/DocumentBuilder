namespace DocumentBuilder.Interfaces
{
    public interface IExcelDocumentBuilder
    {
        /// <summary>
        /// Adds a new worksheet to the document
        /// </summary>
        /// <param name="worksheetName"></param>
        /// <returns><see cref="IExcelDocumentBuilder"/></returns>
        IExcelDocumentBuilder AddWorksheet(string worksheetName);

        /// <summary>
        /// Adds a table to the document at the current worksheet
        /// </summary>
        /// <typeparam name="TRow">The type of the row</typeparam>
        /// <param name="tableRows">The values of the table rows</param>
        /// <returns><see cref="IExcelDocumentBuilder"/></returns>
        IExcelDocumentBuilder AddTable<TRow>(IEnumerable<TRow> tableRows);

        /// <summary>
        /// Writes the document to the provided output stream
        /// </summary>
        /// <param name="outputStream">The output stream</param>
        void Build(Stream outputStream);

        /// <summary>
        /// Writes the document to the provided path, will replace existing documents
        /// </summary>
        /// <param name="filePath">The path which the file should be written to</param>
        /// <returns><see cref="Task"/></returns>
        void Build(string filePath);
    }
}
