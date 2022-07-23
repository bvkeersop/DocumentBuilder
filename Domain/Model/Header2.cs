using NDocument.Domain.Options;

namespace NDocument.Domain.Model
{
    public class Header2 : Header
    {
        private const string _markdownHeader2Indicator = "##";
        private const string _htmlHeader2Indicator = "h2";

        public Header2(string value) : base(value)
        {
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return CreateMarkdownHeader(_markdownHeader2Indicator, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel)
        {
            return CreateHtmlHeader(_htmlHeader2Indicator, options, indentationLevel);
        }
    }
}
