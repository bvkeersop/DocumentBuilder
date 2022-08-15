using DocumentBuilder.Domain.Interfaces;
using DocumentBuilder.Domain.Options;
using DocumentBuilder.Domain.StreamWriters;

namespace DocumentBuilder.Domain.Factories
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
