using DocumentBuilder.Domain.Constants;
using DocumentBuilder.Domain.Options;

namespace DocumentBuilder.Domain.Model.Generic
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

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
        {
            return CreateHtmlHeader(HtmlIndicators.Header3, options, indentationLevel);
        }
    }
}
