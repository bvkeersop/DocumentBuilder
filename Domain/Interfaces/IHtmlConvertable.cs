using NDocument.Domain.Options;

namespace NDocument.Domain.Interfaces
{
    public interface IHtmlConvertable
    {
        ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options);
    }
}
