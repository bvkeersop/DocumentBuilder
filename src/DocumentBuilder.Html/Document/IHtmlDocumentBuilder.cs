using DocumentBuilder.Html.Model;

namespace DocumentBuilder.Html.Document;

public interface IHtmlDocumentBuilder
{
    /// <summary>
    /// Adds a header of type 1 to the document
    /// </summary>
    /// <param name="header1">The value of the header</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    IHtmlElementBuilder AddHeader1(string header1);

    /// <summary>
    /// Adds a header of type 2 to the document
    /// </summary>
    /// <param name="header2">The value of the header</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    IHtmlElementBuilder AddHeader2(string header2);

    /// <summary>
    /// Adds a header of type 3 to the document
    /// </summary>
    /// <param name="header3">The value of the header</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    IHtmlElementBuilder AddHeader3(string header3);

    /// <summary>
    /// Adds a header of type 4 to the document
    /// </summary>
    /// <param name="header4">The value of the header</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    IHtmlElementBuilder AddHeader4(string header4);

    /// <summary>
    /// Adds a paragraph to the document
    /// </summary>
    /// <param name="paragraph">The value of the paragraph</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    IHtmlElementBuilder AddParagraph(string paragraph);

    /// <summary>
    /// Adds an ordered list to the document
    /// </summary>
    /// <param name="orderedList">The value of the ordered list</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    IHtmlElementBuilder AddOrderedList<T>(IEnumerable<T> orderedList);

    /// <summary>
    /// Adds an unordered list to the document
    /// </summary>
    /// <param name="unorderedList">The value of the unordered list</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    IHtmlElementBuilder AddUnorderedList<T>(IEnumerable<T> unorderedList);

    /// <summary>
    /// Adds a table to the document
    /// </summary>
    /// <typeparam name="TRow">The type of the row</typeparam>
    /// <param name="tableRows">The values of the table rows</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    IHtmlElementBuilder AddTable<T>(IEnumerable<T> tableRows);

    /// <summary>
    /// Adds a table to the document
    /// </summary>
    /// <typeparam name="TRow">The type of the row</typeparam>
    /// <param name="tableRow">The values of the table row</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    IHtmlElementBuilder AddTable<T>(T tableRow);

    /// <summary>
    /// Adds a figure to the document
    /// </summary>
    /// <param name="name">The name of the image</param>
    /// <param name="path">The path to the image</param>
    /// <param name="caption">The caption of the image</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    IHtmlElementBuilder AddFigure(string name, string path, string? caption = null);

    /// <summary>
    /// Adds the provided content directly into the document
    /// </summary>
    /// <param name="content">The content</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    IHtmlElementBuilder AddRaw(string content);

    /// <summary>
    /// Adds a stylesheet by reference
    /// </summary>
    /// <param name="href">The file path of the style sheet</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlDocumentBuilder AddStylesheetByRef(string href, string type = "text/css");

    /// <summary>
    /// Opens a div element
    /// </summary>
    /// <param name="content">The content</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder AddDivStart();

    /// <summary>
    /// Closes a div element
    /// </summary>
    /// <param name="content">The content</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder AddDivEnd();

    /// <summary>
    /// Builds the html document
    /// </summary>
    /// <returns><see cref="HtmlDocument"/></returns>
    public HtmlDocument Build();
}
