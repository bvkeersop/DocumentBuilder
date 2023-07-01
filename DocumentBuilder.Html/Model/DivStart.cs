using DocumentBuilder.Constants;
using DocumentBuilder.Html.Extensions;
using DocumentBuilder.Html.Options;
using System.Text;

namespace DocumentBuilder.Html.Model;

public class DivStart : IHtmlElement
{
    public Attributes Attributes { get; } = new Attributes();

    public string ToHtml(HtmlDocumentOptions options, int indentationLevel = 0)
        => new StringBuilder()
            .Append(options.IndentationProvider.GetIndentation(indentationLevel))
            .Append(Indicators.Div.ToHtmlStartTag())
            .ToString();
}
