using DocumentBuilder.Constants;
using DocumentBuilder.Options;

namespace DocumentBuilder.Model.Generic
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

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
        {
            return CreateHtmlHeader(HtmlIndicators.Header1, options, indentationLevel);
        }
    }
}
