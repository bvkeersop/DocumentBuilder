using DocumentBuilder.Domain.Options;

namespace DocumentBuilder.Domain.Interfaces
{
    public interface IHtmlConvertable
    {
        ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0);
    }
}
