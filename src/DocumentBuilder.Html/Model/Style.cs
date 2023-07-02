using DocumentBuilder.Html.Constants;
using DocumentBuilder.Html.Extensions;
using DocumentBuilder.Html.Options;
using System.Text;

namespace DocumentBuilder.Html.Model;

public class Style : IHtmlHeaderElement
{
    public string Value { get; }
    public HtmlDocumentOptions Options { get; }

    public Style(string value, HtmlDocumentOptions options)
    {
        Value = value;
        Options = options;
    }

    public string ToHtml()
        => new StringBuilder()
        .Append(Indicators.Style.ToHtmlStartTag())
        .Append(Options.NewLineProvider.GetNewLine())
        .Append(Options.NewLineProvider.GetNewLine())
        .Append(Value)
        .Append(Indicators.Style.ToHtmlEndTag())
        .ToString();
}
