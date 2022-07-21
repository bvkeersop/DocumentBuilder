using NDocument.Domain.Options;

namespace NDocument.Domain.Model
{
    public class RawText : GenericElement
    {
        public string Value { get; }

        public RawText(string value)
        {
            Value = value;
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return ConvertToMarkdown(Value, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options)
        {
            return ConvertToHtml(Value, options);
        }
    }
}
