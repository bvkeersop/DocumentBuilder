using NDocument.Domain.Constants;
using NDocument.Domain.Options;

namespace NDocument.Domain.Model.Generic
{
    public class Header3 : Header
    {
        public Header3(string value) : base(value)
        {
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return CreateMarkdownHeader(MarkdownIndicators.Header3, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel)
        {
            return CreateHtmlHeader(HtmlIndicators.Header3, options, indentationLevel);
        }
    }
}
