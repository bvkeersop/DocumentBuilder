using DocumentBuilder.Constants;
using DocumentBuilder.Html.Extensions;
using DocumentBuilder.Html.Options;

namespace DocumentBuilder.Html.Model;

public class DivStart : IHtmlElement
{
    public Attributes Attributes => new();

    public ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0) => new(Indicators.Div.ToHtmlStartTag());
}
