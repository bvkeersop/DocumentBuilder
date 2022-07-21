using NDocument.Domain.Options;

namespace NDocument.Domain.Model
{
    public class Header1 : Header
    {
        private const string _markdownHeader1Indicator = "#";
        private const string _htmlHeader1Indicator = "h1";

        public Header1(string value) : base(value)
        {
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return CreateMarkdownHeader(_markdownHeader1Indicator, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options)
        {
            return CreateHtmlHeader(_htmlHeader1Indicator, options);
        }
    }
}
