using DocumentBuilder.Constants;
using DocumentBuilder.Html.Extensions;
using DocumentBuilder.Html.Options;
using System.Text;

namespace DocumentBuilder.Html.Model;

internal class DivEnd : IHtmlElement
{
    public Attributes Attributes { get; } = new Attributes();

    public string ToHtml(HtmlDocumentOptions options, int indentationLevel = 0) 
        => new StringBuilder()
            .Append(options.IndentationProvider.GetIndentation(indentationLevel))
            .Append(Indicators.Div.ToHtmlEndTag())
            .ToString();
}
