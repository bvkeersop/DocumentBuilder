using NDocument.Domain.Constants;
using NDocument.Domain.Options;

namespace NDocument.Domain.Model.Generic
{
    public class Header2 : Header
    {
        public Header2(string value) : base(value)
        {
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return CreateMarkdownHeader(MarkdownIndicators.Header2, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
        {
            return CreateHtmlHeader(HtmlIndicators.Header2, options, indentationLevel);
        }
    }
}
