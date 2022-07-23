using NDocument.Domain.Options;

namespace NDocument.Domain.Model
{
    public class Header3 : Header
    {
        private const string _markdownHeader3Indicator = "###";
        private const string _htmlHeader3Indicator = "h3";

        public Header3(string value) : base(value)
        {
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return CreateMarkdownHeader(_markdownHeader3Indicator, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel)
        {
            return CreateHtmlHeader(_htmlHeader3Indicator, options, indentationLevel);
        }
    }
}
