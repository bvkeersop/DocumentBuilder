using NDocument.Domain.Interfaces;
using NDocument.Domain.Options;
using NDocument.Domain.StreamWriters;

namespace NDocument.Domain.Factories
{
    internal static class MarkdownStreamWriterFactory
    {
        public static IMarkdownStreamWriter Create(Stream outputStream, MarkdownDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var streamWriter = new StreamWriter(outputStream, leaveOpen: true);
            return new MarkdownStreamWriter(streamWriter, newLineProvider);
        }
    }
}
