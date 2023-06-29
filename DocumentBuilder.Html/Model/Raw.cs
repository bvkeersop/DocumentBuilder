using DocumentBuilder.Html.Options;

namespace DocumentBuilder.Html.Model;

internal class Raw : IHtmlElement
{
    public string Value { get; }
    public Attributes Attributes => throw new NotImplementedException();

    public Raw(string value)
    {
        Value = value;
    }

    public ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
    {
        throw new NotImplementedException();
    }
}