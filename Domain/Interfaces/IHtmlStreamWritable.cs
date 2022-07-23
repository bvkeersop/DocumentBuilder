using NDocument.Domain.Options;

namespace NDocument.Domain.Interfaces
{
    internal interface IHtmlStreamWritable
    {
        Task WriteAsHtmlToStreamAsync(Stream outputStream, HtmlDocumentOptions options, int indentationLevel);
    }
}
