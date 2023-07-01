using DocumentBuilder.Core.Utilities;
using DocumentBuilder.Exceptions;
using DocumentBuilder.Factories;
using DocumentBuilder.Html.Model;
using DocumentBuilder.Html.Options;

namespace DocumentBuilder.Html;

public class Metadata
{
    public int NrOfDivStartElements { get; set; } = 0;
    public int NrOfDivEndElements { get; set; } = 0;

    public bool DoesDivEndCloseADivStartTag() => NrOfDivEndElements >= NrOfDivStartElements;
}

public class HtmlDocumentBuilder : IHtmlDocumentBuilder
{
    public IHtmlDocumentWriter HtmlDocumentWriter { get; }
    public HtmlDocumentOptions Options { get; }
    public IEnumerableRenderingStrategy EnumerableRenderingStrategy { get; }
    public HtmlDocument HtmlDocument { get; }
    public Metadata Metadata { get; }

    protected HtmlDocumentBuilder(HtmlDocumentBuilder htmlDocumentBuilder)
    {
        Options = htmlDocumentBuilder.Options;
        HtmlDocument = htmlDocumentBuilder.HtmlDocument;
        HtmlDocumentWriter = htmlDocumentBuilder.HtmlDocumentWriter;
        EnumerableRenderingStrategy = htmlDocumentBuilder.EnumerableRenderingStrategy;
        Metadata = htmlDocumentBuilder.Metadata;
    }

    public HtmlDocumentBuilder() : this(new HtmlDocumentOptions()) { }

    public HtmlDocumentBuilder(HtmlDocumentOptions options)
    {
        Options = options ?? throw new ArgumentNullException(nameof(options));
        EnumerableRenderingStrategy = EnumerableValidatorFactory.Create(options.NullOrEmptyEnumerableRenderingStrategy);
        HtmlDocumentWriter = new HtmlDocumentWriter(HtmlStreamWriterFactory.Create, options);
        HtmlDocument = new HtmlDocument(options, HtmlDocumentWriter);
        Metadata = new Metadata();
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
        HtmlDocument.AddHtmlElement(htmlElement);
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
    /// Adds an image to the document
    /// </summary>
    /// <param name="name">The name of the image</param>
    /// <param name="path">The path to the image</param>
    /// <param name="caption">The caption of the image</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder AddImage(string name, string path, string? caption = null)
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

    /// <summary>
    /// Opens a div element
    /// </summary>
    /// <param name="content">The content</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder AddDivStart()
    {
        var htmlElement = new DivStart();
        HtmlDocument.AddHtmlElement(htmlElement);
        Metadata.NrOfDivStartElements++;
        return new HtmlElementBuilder(htmlElement, this);
    }

    /// <summary>
    /// Closes a div element
    /// </summary>
    /// <param name="content">The content</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder AddDivEnd()
    {
        var htmlElement = new DivEnd();
        HtmlDocument.AddHtmlElement(htmlElement);

        if (Metadata.DoesDivEndCloseADivStartTag())
        {
            throw new DocumentBuilderException(DocumentBuilderErrorCode.NoDivElementToClose);
        }

        Metadata.NrOfDivEndElements++;
        return new HtmlElementBuilder(htmlElement, this);
    }

    /// <summary>
    /// Adds a stylesheet by reference
    /// </summary>
    /// <param name="href">The file path of the style sheet</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlDocumentBuilder AddStylesheetByRef(string href, string type = "text/css")
    {
        var link = new Link(Links.Stylesheet, href, type);
        HtmlDocument.AddLink(link);
        return this;
    }

    /// <summary>
    /// Builds the html document
    /// </summary>
    /// <returns><see cref="HtmlDocument"/></returns>
    public HtmlDocument Build()
    {
        return HtmlDocument;
    }
}
