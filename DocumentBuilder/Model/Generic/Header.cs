using DocumentBuilder.Constants;
using DocumentBuilder.Enumerations;
using DocumentBuilder.Extensions;
using DocumentBuilder.Factories;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Options;

namespace DocumentBuilder.Model.Generic
{
    public abstract class Header : GenericElement, IMarkdownHeader
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
            var html = $"{headerIndicator.ToHtmlStartTag()}{Value}{headerIndicator.ToHtmlEndTag()}";
            return WrapWithIndentationAndNewLine(html, options, indentationLevel);
        }

        public abstract ValueTask<string> ToMarkdownTableOfContentsEntry(bool isNumbered, MarkdownDocumentOptions options);

        protected ValueTask<string> CreateMarkdownTableOfContentsEntry(bool isNumbered, int headerNumber, MarkdownDocumentOptions options)
        {
            var prefix = MarkdownIndicators.UnorderedListItem;

            if (isNumbered)
            {
                prefix = MarkdownIndicators.OrderedListItem;
            }

            var indentation = IndentationProviderFactory.Create(IndentationType.Tabs, 1).GetIndentation(headerNumber - 1);
            var markdown = $"{indentation}{prefix} {Value.ToMarkdownLink()}";
            return AddNewLine(markdown, options);
        }
    }
}
