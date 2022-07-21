using NDocument.Domain.Options;

namespace NDocument.Domain.Interfaces
{
    public interface IMarkdownConvertable
    {
        ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options);
    }
}
