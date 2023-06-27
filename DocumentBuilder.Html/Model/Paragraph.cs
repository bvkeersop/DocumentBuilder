using DocumentBuilder.Constants;
using DocumentBuilder.Extensions;
using DocumentBuilder.Options;
using System.Text;

namespace DocumentBuilder.Model.Generic
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
            return AddNewLine(Value, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(GetHtmlStartTagWithAttributes(Indicators.Paragraph));
            stringBuilder.Append(Value);
            stringBuilder.Append(Indicators.Paragraph.ToHtmlEndTag());
            var value = stringBuilder.ToString();
            return WrapWithIndentationAndNewLine(value, options, indentationLevel);
        }
    }
}
