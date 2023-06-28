using DocumentBuilder.Html.Options;

namespace DocumentBuilder.Html.Model;

public interface IHtmlElement
{
    Attributes Attributes { get; }
    ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0);
}
