using DocumentBuilder.Enumerations;

namespace DocumentBuilder.Interfaces
{
    public interface IGenericDocumentBuilder
    {
        /// <summary>
        /// Writes the document to the provided output stream
        /// </summary>
        /// <param name="outputStream">The stream to write to</param>
        /// <param name="documentType">In what format the document should be written</param>
        /// <returns><see cref="Task"/></returns>
        Task BuildAsync(Stream outputStream, DocumentType documentType);

        /// <summary>
        /// Writes the document to the provided path, will replace existing documents
        /// </summary>
        /// <param name="filePath">The path which the file should be written to</param>
        /// <param name="documentType">In what format the document should be written</param>
        /// <returns><see cref="Task"/></returns>
        Task BuildAsync(string filePath, DocumentType documentType);

        /// <summary>
        /// Adds a header of type 1 to the document
        /// </summary>
        /// <param name="header1">The value of the header</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        IGenericDocumentBuilder AddHeader1(string header1);

        /// <summary>
        /// Adds a header of type 2 to the document
        /// </summary>
        /// <param name="header2">The value of the header</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        IGenericDocumentBuilder AddHeader2(string header2);

        /// <summary>
        /// Adds a header of type 3 to the document
        /// </summary>
        /// <param name="header3">The value of the header</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        IGenericDocumentBuilder AddHeader3(string header3);

        /// <summary>
        /// Adds a header of type 4 to the document
        /// </summary>
        /// <param name="header4">The value of the header</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        IGenericDocumentBuilder AddHeader4(string header4);

        /// <summary>
        /// Adds a paragraph to the document
        /// </summary>
        /// <param name="paragraph">The value of the paragraph</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        IGenericDocumentBuilder AddParagraph(string paragraph);

        /// <summary>
        /// Adds an ordered list to the document
        /// </summary>
        /// <param name="orderedList">The value of the ordered list</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        IGenericDocumentBuilder AddOrderedList<T>(IEnumerable<T> orderedList);

        /// <summary>
        /// Adds an unordered list to the document
        /// </summary>
        /// <param name="unorderedList">The value of the unordered list</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        IGenericDocumentBuilder AddUnorderedList<T>(IEnumerable<T> unorderedList);

        /// <summary>
        /// Adds a table to the document
        /// </summary>
        /// <typeparam name="TRow">The type of the row</typeparam>
        /// <param name="tableRows">The values of the table rows</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        IGenericDocumentBuilder AddTable<TRow>(IEnumerable<TRow> tableRows);

        /// <summary>
        /// Adds an image to the document
        /// </summary>
        /// <param name="name">The name of the image</param>
        /// <param name="path">The path to the image</param>
        /// <param name="caption">The caption of the image</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        IGenericDocumentBuilder AddImage(string name, string path, string? caption = null);
    }
}
