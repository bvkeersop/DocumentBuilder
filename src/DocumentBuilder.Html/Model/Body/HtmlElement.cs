using DocumentBuilder.Html.Extensions;
using DocumentBuilder.Html.Options;
using System.Text;

namespace DocumentBuilder.Html.Model.Body;
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
        var sb = new StringBuilder();
        sb.Append(Indicator);

        if (!InlineStyles.IsEmpty)
        {
            sb.Append(' ')
                .Append(InlineStyles);
        }

        if (!Attributes.IsEmpty)
        {
            sb.Append(' ')
                .Append(Attributes);
        }

        return sb.ToString().ToHtmlStartTag();
    }

    protected string GetHtmlEndTag() => Indicator.ToHtmlEndTag();
}
