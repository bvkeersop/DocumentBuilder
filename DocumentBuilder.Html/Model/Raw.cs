using DocumentBuilder.Html.Options;
using System.Text;

namespace DocumentBuilder.Html.Model;

internal class Raw : IHtmlElement
{
    public string Value { get; }
    public Attributes Attributes => throw new NotImplementedException();

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