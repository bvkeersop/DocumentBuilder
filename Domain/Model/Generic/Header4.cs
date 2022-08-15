using DocumentBuilder.Domain.Constants;
using DocumentBuilder.Domain.Options;

namespace DocumentBuilder.Domain.Model.Generic
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

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
        {
            return CreateHtmlHeader(HtmlIndicators.Header4, options, indentationLevel);
        }
    }
}
