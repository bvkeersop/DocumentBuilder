namespace DocumentBuilder.Interfaces
{
    public interface IMarkdownDocumentBuilder
    {
        /// <summary>
        /// Writes the document to the provided output stream
        /// </summary>
        /// <param name="outputStream">The stream to write to</param>
        /// <returns><see cref="Task"/></returns>
        Task BuildAsync(Stream outputStream);

        /// <summary>
        /// Writes the document to the provided path
        /// </summary>
        /// <param name="filePath">The path which the file should be written to</param>
        /// <returns><see cref="Task"/></returns>
        Task BuildAsync(string filePath);

        /// <summary>
        /// Adds a header of type 1 to the document
        /// </summary>
        /// <param name="header1">The value of the header</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        IMarkdownDocumentBuilder AddHeader1(string header1);

        /// <summary>
        /// Adds a header of type 2 to the document
        /// </summary>
        /// <param name="header2">The value of the header</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        IMarkdownDocumentBuilder AddHeader2(string header2);

        /// <summary>
        /// Adds a header of type 3 to the document
        /// </summary>
        /// <param name="header3">The value of the header</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        IMarkdownDocumentBuilder AddHeader3(string header3);

        /// <summary>
        /// Adds a header of type 4 to the document
        /// </summary>
        /// <param name="header4">The value of the header</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        IMarkdownDocumentBuilder AddHeader4(string header4);

        /// <summary>
        /// Adds a paragraph to the document
        /// </summary>
        /// <param name="paragraph">The value of the paragraph</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        IMarkdownDocumentBuilder AddParagraph(string paragraph);

        /// <summary>
        /// Adds an ordered list to the document
        /// </summary>
        /// <param name="orderedList">The value of the ordered list</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        IMarkdownDocumentBuilder AddOrderedList<T>(IEnumerable<T> orderedList);

        /// <summary>
        /// Adds an unordered list to the document
        /// </summary>
        /// <param name="unorderedList">The value of the unordered list</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        IMarkdownDocumentBuilder AddUnorderedList<T>(IEnumerable<T> unorderedList);

        /// <summary>
        /// Adds a table to the document
        /// </summary>
        /// <typeparam name="TRow">The type of the row</typeparam>
        /// <param name="tableRows">The values of the table rows</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        IMarkdownDocumentBuilder AddTable<T>(IEnumerable<T> tableRows);

        /// <summary>
        /// Adds an image to the document
        /// </summary>
        /// <param name="name">The name of the image</param>
        /// <param name="path">The path to the image</param>
        /// <param name="caption">The caption of the image</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        IMarkdownDocumentBuilder AddImage(string name, string path, string? caption = null);
    }
}
