using DocumentBuilder.Html.Options;

namespace DocumentBuilder.Html.Model;

public interface IHtmlElement
{
    InlineStyles InlineStyles { get; }
    Attributes Attributes { get; }
    string ToHtml(HtmlDocumentOptions options, int indentationLevel = 0);
}
