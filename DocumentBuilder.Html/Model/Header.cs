using DocumentBuilder.Options;
using System.Text;

namespace DocumentBuilder.Html.Model;
public abstract class Header : HtmlElement
{
    protected Header(string indicator, string value) : base(indicator, value)
    {
    }

    public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
    {
        var header = new StringBuilder()
            .Append(GetHtmlStartTagWithAttributes())
            .Append(Value)
            .Append(GetHtmlEndTag())
            .ToString();
        return new ValueTask<string>(header);
    }
}