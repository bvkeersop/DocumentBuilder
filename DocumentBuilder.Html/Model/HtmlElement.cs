using DocumentBuilder.Html.Extensions;
using DocumentBuilder.Html.Options;
using System.Text;

namespace DocumentBuilder.Html.Model;
public abstract class HtmlElement : IHtmlElement
{
    public string Indicator { get; }
    public string Value { get; }

    public Attributes Attributes { get; } = new();

    public HtmlElement(string indicator, string value)
    {
        Indicator = indicator;
        Value = value;
    }

    public ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
    {
        var htmlStartTag = GetHtmlStartTagWithAttributes();
        var htmlEndTag = GetHtmlEndTag();
        var html = new StringBuilder().Append(htmlStartTag)
            .Append(Value)
            .Append(htmlEndTag)
            .ToString();
        return new ValueTask<string>(html);
    }

    protected string GetHtmlStartTagWithAttributes()
    {
        if (Attributes.IsEmpty)
        {
            return Indicator.ToHtmlStartTag();
        }

        var sb = new StringBuilder();
        sb.Append(Indicator)
            .Append(' ')
            .Append(Attributes);
        var element = sb.ToString();
        return element.ToHtmlStartTag();
    }

    protected string GetHtmlEndTag() => Indicator.ToHtmlEndTag();
}
