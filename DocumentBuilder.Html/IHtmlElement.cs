using DocumentBuilder.Model.Html;
using DocumentBuilder.Options;

namespace DocumentBuilder.Html;

public interface IHtmlElement
{
    IList<IHtmlElement> Elements { get; }
    HtmlAttributes Attributes { get; }
    ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0);
}
