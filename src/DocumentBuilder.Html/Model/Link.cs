using DocumentBuilder.Html.Constants;

namespace DocumentBuilder.Html.Model;

public class Link : IHtmlHeaderElement
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

    public string ToHtml() => $"<{Indicators.Link} rel=\"{Rel}\" type=\"{Type}\" href=\"{Href}\" />";
}