using DocumentBuilder.Html.Extensions;
using DocumentBuilder.Html.Options;
using System.Text;

namespace DocumentBuilder.Html.Model;
public abstract class HtmlElement : IHtmlElement
{
    public string Indicator { get; }
    public string Value { get; }
    public Attributes Attributes { get; } = new();
    public InlineStyles InlineStyles { get; } = new();

    public HtmlElement(string indicator, string value)
    {
        Indicator = indicator;
        Value = value;
    }

    public string ToHtml(HtmlDocumentOptions options, int indentationLevel = 0) 
        => new StringBuilder()
            .Append(options.IndentationProvider.GetIndentation(indentationLevel))
            .Append(GetHtmlStartTagWithAttributes())
            .Append(Value)
            .Append(GetHtmlEndTag())
            .ToString();

    protected string GetHtmlStartTagWithAttributes()
    {
        if (Attributes.IsEmpty)
        {
            return Indicator.ToHtmlStartTag();
        }

        var sb = new StringBuilder();
        sb.Append(Indicator)
            .Append(' ')
            .Append(InlineStyles)
            .Append(Attributes);
        var element = sb.ToString();
        return element.ToHtmlStartTag();
    }

    protected string GetHtmlEndTag() => Indicator.ToHtmlEndTag();
}
