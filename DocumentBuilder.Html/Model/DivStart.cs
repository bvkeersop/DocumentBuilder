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
            .Append(GetHtmlStartTagWithAttributes())
            .ToString();

    protected string GetHtmlStartTagWithAttributes()
    {
        if (Attributes.IsEmpty)
        {
            return Indicators.Div.ToHtmlStartTag();
        }

        return new StringBuilder()
            .Append(Indicators.Div)
            .Append(' ')
            .Append(Attributes)
            .ToString()
            .ToHtmlStartTag();
    }
}
