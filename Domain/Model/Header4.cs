using NDocument.Domain.Options;

namespace NDocument.Domain.Model
{
    public class Header4 : Header
    {
        private const string _markdownHeader4Indicator = "####";
        private const string _htmlHeader4Indicator = "h4";

        public Header4(string value) : base(value)
        {
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return CreateMarkdownHeader(_markdownHeader4Indicator, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options)
        {
            return CreateHtmlHeader(_htmlHeader4Indicator, options);
        }
    }
}
