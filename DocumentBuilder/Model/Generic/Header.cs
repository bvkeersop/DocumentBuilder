using DocumentBuilder.Extensions;
using DocumentBuilder.Options;

namespace DocumentBuilder.Model.Generic
{
    public abstract class Header : GenericElement
    {
        public string Value { get; }

        protected Header(string value)
        {
            Value = value;
        }

        protected ValueTask<string> CreateMarkdownHeader(string headerIndicator, MarkdownDocumentOptions options)
        {
            var markdown = $"{headerIndicator} {Value}";
            return AddNewLine(markdown, options);
        }

        protected ValueTask<string> CreateHtmlHeader(string headerIndicator, HtmlDocumentOptions options, int indentationLevel = 0)
        {
            var html = $"{GetHtmlStartTagWithAttributes(headerIndicator)}{Value}{headerIndicator.ToHtmlEndTag()}";
            return WrapWithIndentationAndNewLine(html, options, indentationLevel);
        }
    }
}
