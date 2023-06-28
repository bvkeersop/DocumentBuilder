using DocumentBuilder.Constants;
using DocumentBuilder.Factories;
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

    public ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
    {
        var value = $"<{Indicators.Link} rel=\"{Rel}\" type=\"{Type}\" href=\"{Href}\" />";
    }
}