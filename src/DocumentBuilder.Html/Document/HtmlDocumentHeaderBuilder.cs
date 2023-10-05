using DocumentBuilder.Html.Constants;
using DocumentBuilder.Html.Model.Head;
using DocumentBuilder.Html.Options;

namespace DocumentBuilder.Html.Document;

public interface IHtmlDocumentHeaderBuilder
{
    public IHtmlDocumentHeaderBuilder AddStylesheet(string stylesheetAsString);
    public IHtmlDocumentHeaderBuilder AddStylesheetByRef(string href, string type = "text/css");
}


public class HtmlDocumentHeaderBuilder : IHtmlDocumentHeaderBuilder
{
    public HtmlDocumentOptions _options;
    private readonly HtmlDocumentHeader _htmlDocumentHeader;

    public HtmlDocumentHeaderBuilder(HtmlDocumentOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public IHtmlDocumentHeaderBuilder Head() => new HtmlDocumentHeaderBuilder(_options);

    /// <summary>
    /// Directly adds a stylesheet by in the header
    /// </summary>
    /// <param name="href">The complete stylesheet</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlDocumentHeaderBuilder AddStylesheet(string stylesheetAsString)
    {
        var style = new Style(stylesheetAsString, _options);
        _htmlDocumentHeader.AddHtmlHeaderElement(style);
        return this;
    }

    /// <summary>
    /// Adds a stylesheet by reference in the header
    /// </summary>
    /// <param name="href">The file path of the style sheet</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlDocumentHeaderBuilder AddStylesheetByRef(string href, string type = "text/css")
    {
        var link = new Link(Links.Stylesheet, href, type);
        _htmlDocumentHeader.AddHtmlHeaderElement(link);
        return this;
    }

    public HtmlDocumentHeader Build() => _htmlDocumentHeader;
}
