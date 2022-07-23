using NDocument.Domain.Constants;
using NDocument.Domain.Options;

namespace NDocument.Domain.Model
{
    public class Header4 : Header
    {
        public Header4(string value) : base(value)
        {
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return CreateMarkdownHeader(MarkdownIndicators.Header4, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel)
        {
            return CreateHtmlHeader(HtmlIndicators.Header4, options, indentationLevel);
        }
    }
}
