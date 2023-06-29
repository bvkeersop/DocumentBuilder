using DocumentBuilder.Constants;
using DocumentBuilder.Html.Extensions;
using DocumentBuilder.Html.Options;
using System.Text;

namespace DocumentBuilder.Html.Model;

public abstract class ListBase<TValue> : IHtmlElement
{
    public string Indicator { get; }
    public Attributes Attributes { get; } = new Attributes();
    protected IEnumerable<TValue> Value { get; }

    protected ListBase(string indicator, IEnumerable<TValue> value)
    {
        Indicator = indicator;
        Value = value;
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

    public ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
    {
        var indentationProvider = options.IndentationProvider;
        var newLineProvider = options.NewLineProvider;

        var sb = new StringBuilder()
            .Append(indentationProvider.GetIndentation(0))
            .Append(GetHtmlStartTagWithAttributes())
            .Append(newLineProvider.GetNewLine());

        foreach (var item in Value)
        {
            sb.Append(Indicators.ListItem.ToHtmlStartTag())
                .Append(item)
                .Append(Indicators.ListItem.ToHtmlEndTag());
        }

        sb.Append(indentationProvider.GetIndentation(0))
            .Append(GetHtmlEndTag())
            .Append(newLineProvider.GetNewLine());

        var element = sb.ToString();
        return new ValueTask<string>(element);
    }
}
