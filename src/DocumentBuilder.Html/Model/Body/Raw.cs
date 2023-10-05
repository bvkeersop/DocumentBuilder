using DocumentBuilder.Html.Options;
using System.Text;

namespace DocumentBuilder.Html.Model.Body;

internal class Raw : IHtmlElement
{
    public string Value { get; }

    // Raw means raw, does not support any calls on the element for styling as they should be present in the raw html
    public Attributes Attributes => new();
    public InlineStyles InlineStyles => new();

    public Raw(string value)
    {
        Value = value;
    }

    public string ToHtml(HtmlDocumentOptions options, int indentationLevel = 0)
        => new StringBuilder()
            .Append(options.IndentationProvider.GetIndentation(indentationLevel))
            .Append(Value)
            .ToString();
}