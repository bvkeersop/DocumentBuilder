using DocumentBuilder.Factories;
using DocumentBuilder.Html.Options;
using DocumentBuilder.Interfaces;

namespace DocumentBuilder.Html
{
    internal static class HtmlStreamWriterFactory
    {
        public static IHtmlStreamWriter Create(Stream outputStream, HtmlDocumentOptions options)
        {
            var streamWriter = new StreamWriter(outputStream, leaveOpen: true);
            return new HtmlStreamWriter(streamWriter, options.NewLineProvider, options.IndentationProvider);
        }
    }
}
