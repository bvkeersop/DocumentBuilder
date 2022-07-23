using NDocument.Domain.Constants;
using NDocument.Domain.Extensions;
using NDocument.Domain.Options;
using System.Text;

namespace NDocument.Domain.Model
{
    public class Paragraph : GenericElement
    {
        public string Value { get; }

        public Paragraph(string value)
        {
            Value = value;
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return ConvertToMarkdown(Value, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(HtmlIndicators.Paragraph.ToHtmlStartTag());
            stringBuilder.Append(Value);
            stringBuilder.Append(HtmlIndicators.Paragraph.ToHtmlEndTag());
            var value = stringBuilder.ToString();
            return ConvertToHtml(value, options, indentationLevel);
        }
    }
}
