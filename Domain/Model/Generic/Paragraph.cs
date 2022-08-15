using DocumentBuilder.Domain.Constants;
using DocumentBuilder.Domain.Extensions;
using DocumentBuilder.Domain.Options;
using System.Text;

namespace DocumentBuilder.Domain.Model.Generic
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

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
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
