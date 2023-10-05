using DocumentBuilder.Html.Model;
using DocumentBuilder.Html.Model.Body;
using DocumentBuilder.Html.Options;

namespace DocumentBuilder.Html.Document;

public interface IHtmlDocumentBodyBuilder
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
}

public class HtmlDocumentBodyBuilder : IHtmlDocumentBodyBuilder
{
    private readonly HtmlDocumentBody _htmlDocumentBody;
    private readonly HtmlDocumentOptions _htmlDocumentOptions;

    public HtmlDocumentBodyBuilder(HtmlDocumentOptions htmlDocumentOptions)
    {
        _htmlDocumentOptions = htmlDocumentOptions;
    }

    /// <summary>
    /// Adds a header of type 1 to the document
    /// </summary>
    /// <param name="header1">The value of the header</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder AddHeader1(string header1)
    {
        _ = header1 ?? throw new ArgumentNullException(nameof(header1));
        var htmlElement = new Header1(header1);
        _htmlDocumentBody.AddHtmlElement(htmlElement);
        return new HtmlElementBuilder(htmlElement, this);
    }

    /// <summary>
    /// Adds a header of type 2 to the document
    /// </summary>
    /// <param name="header2">The value of the header</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder AddHeader2(string header2)
    {
        _ = header2 ?? throw new ArgumentNullException(nameof(header2));
        var htmlElement = new Header2(header2);
        HtmlDocument.AddHtmlElement(htmlElement);
        return new HtmlElementBuilder(htmlElement, this);
    }

    /// <summary>
    /// Adds a header of type 3 to the document
    /// </summary>
    /// <param name="header3">The value of the header</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder AddHeader3(string header3)
    {
        _ = header3 ?? throw new ArgumentNullException(nameof(header3));
        var htmlElement = new Header3(header3);
        HtmlDocument.AddHtmlElement(htmlElement);
        return new HtmlElementBuilder(htmlElement, this);
    }

    /// <summary>
    /// Adds a header of type 4 to the document
    /// </summary>
    /// <param name="header4">The value of the header</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></retu
    public IHtmlElementBuilder AddHeader4(string header4)
    {
        _ = header4 ?? throw new ArgumentNullException(nameof(header4));
        var htmlElement = new Header3(header4);
        HtmlDocument.AddHtmlElement(htmlElement);
        return new HtmlElementBuilder(htmlElement, this);
    }

    /// <summary>
    /// Adds a paragraph to the document
    /// </summary>
    /// <param name="paragraph">The value of the paragraph</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder AddParagraph(string paragraph)
    {
        _ = paragraph ?? throw new ArgumentNullException(nameof(paragraph));
        var htmlElement = new Paragraph(paragraph);
        HtmlDocument.AddHtmlElement(htmlElement);
        return new HtmlElementBuilder(htmlElement, this);
    }

    /// <summary>
    /// Adds an ordered list to the document
    /// </summary>
    /// <param name="orderedList">The value of the ordered list</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder AddOrderedList<T>(IEnumerable<T> orderedList)
    {
        _ = orderedList ?? throw new ArgumentNullException(nameof(orderedList));

        if (!EnumerableRenderingStrategy.ShouldRender(orderedList))
        {
            return new HtmlElementBuilder(null, this);
        }

        var htmlElement = new OrderedList<T>(orderedList);
        HtmlDocument.AddHtmlElement(htmlElement);
        return new HtmlElementBuilder(htmlElement, this);
    }

    /// <summary>
    /// Adds an unordered list to the document
    /// </summary>
    /// <param name="unorderedList">The value of the unordered list</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder AddUnorderedList<T>(IEnumerable<T> unorderedList)
    {
        _ = unorderedList ?? throw new ArgumentNullException(nameof(unorderedList));

        if (!EnumerableRenderingStrategy.ShouldRender(unorderedList))
        {
            return new HtmlElementBuilder(null, this);
        }

        var htmlElement = new OrderedList<T>(unorderedList);
        HtmlDocument.AddHtmlElement(htmlElement);
        return new HtmlElementBuilder(htmlElement, this);
    }

    /// <summary>
    /// Adds a table to the document
    /// </summary>
    /// <typeparam name="TRow">The type of the row</typeparam>
    /// <param name="tableRows">The values of the table rows</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder AddTable<T>(IEnumerable<T> tableRows)
    {
        return AddTableInternal(tableRows);
    }

    /// <summary>
    /// Adds a table to the document
    /// </summary>
    /// <typeparam name="TRow">The type of the row</typeparam>
    /// <param name="tableRow">The values of the table row</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder AddTable<T>(T tableRow)
    {
        var tableRows = new T[] { tableRow };
        return AddTableInternal(tableRows);
    }

    private IHtmlElementBuilder AddTableInternal<T>(IEnumerable<T> tableRows)
    {
        if (!EnumerableRenderingStrategy.ShouldRender(tableRows))
        {
            return new HtmlElementBuilder(null, this);
        }

        var htmlElement = new Table<T>(tableRows, Options);
        HtmlDocument.AddHtmlElement(htmlElement);
        return new HtmlElementBuilder(htmlElement, this);
    }

    /// <summary>
    /// Adds a figure to the document
    /// </summary>
    /// <param name="name">The name of the image</param>
    /// <param name="path">The path to the image</param>
    /// <param name="caption">The caption of the image</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder AddFigure(string name, string path, string? caption = null)
    {
        _ = name ?? throw new ArgumentNullException(nameof(name));
        _ = path ?? throw new ArgumentNullException(nameof(path));

        var htmlElement = new Figure(name, path, caption);
        HtmlDocument.AddHtmlElement(htmlElement);
        return new HtmlElementBuilder(htmlElement, this);
    }

    /// <summary>
    /// Adds the provided content directly into the document
    /// </summary>
    /// <param name="content">The content</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder AddRaw(string content)
    {
        _ = content ?? throw new ArgumentNullException(nameof(content));

        var htmlElement = new Raw(content);
        HtmlDocument.AddHtmlElement(htmlElement);
        return new HtmlElementBuilder(htmlElement, this);
    }
}
