using DocumentBuilder.Constants;
using DocumentBuilder.Html.Options;

namespace DocumentBuilder.Html.Model;

public class Link
{
    public string Rel { get; }
    public string Href { get; }
    public string Type { get; }

    public Link(string rel, string href, string type)
    {
        Rel = rel;
        Href = href;
        Type = type;
    }

    public string ToHtml(HtmlDocumentOptions options, int indentationLevel = 0)
    {
        return $"<{Indicators.Link} rel=\"{Rel}\" type=\"{Type}\" href=\"{Href}\" />";
    }
}