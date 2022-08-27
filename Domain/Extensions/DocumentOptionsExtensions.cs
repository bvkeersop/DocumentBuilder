using DocumentBuilder.Options;

namespace DocumentBuilder.Extensions
{
    internal static class DocumentOptionsExtensions
    {
        public static MarkdownDocumentOptions ToMarkdownDocumentOptions(this DocumentOptions options)
        {
            return new MarkdownDocumentOptions
            {
                LineEndings = options.LineEndings
            };
        }

        public static HtmlDocumentOptions ToHtmlDocumentOptions(this DocumentOptions options)
        {
            return new HtmlDocumentOptions
            {
                LineEndings = options.LineEndings
            };
        }
    }
}
