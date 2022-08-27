using DocumentBuilder.Interfaces;
using DocumentBuilder.Options;
using DocumentBuilder.StreamWriters;

namespace DocumentBuilder.Factories
{
    internal static class HtmlStreamWriterFactory
    {
        public static IHtmlStreamWriter Create(Stream outputStream, HtmlDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize);
            var streamWriter = new StreamWriter(outputStream, leaveOpen: true);
            return new HtmlStreamWriter(streamWriter, newLineProvider, indentationProvider);
        }
    }
}
