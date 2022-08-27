using DocumentBuilder.Options;

namespace DocumentBuilder.Interfaces
{
    public interface IHtmlConvertable
    {
        ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0);
    }
}
