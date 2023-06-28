using DocumentBuilder.Html.Options;

namespace DocumentBuilder.Html.Model;

internal class Raw : IHtmlElement
{
    public Attributes Attributes => throw new NotImplementedException();

    public ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
    {
        throw new NotImplementedException();
    }
}