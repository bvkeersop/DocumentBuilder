using NDocument.Domain.Constants;
using NDocument.Domain.Options;

namespace NDocument.Domain.Model.Generic
{
    public class Header1 : Header
    {
        public Header1(string value) : base(value)
        {
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return CreateMarkdownHeader(MarkdownIndicators.Header1, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel)
        {
            return CreateHtmlHeader(HtmlIndicators.Header1, options, indentationLevel);
        }
    }
}
