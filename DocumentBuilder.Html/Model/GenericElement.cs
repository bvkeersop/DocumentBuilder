using DocumentBuilder.Extensions;
using DocumentBuilder.Factories;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Model.Html;
using DocumentBuilder.Options;

namespace DocumentBuilder.Model.Generic
{
    public abstract class GenericElement : IMarkdownConvertable, IHtmlConvertable
    {
        public HtmlAttributes Attributes { get; } = new();

        public string GetHtmlStartTagWithAttributes(string htmlIndicator)
        {
            if (Attributes.IsEmpty)
            {
                return htmlIndicator.ToHtmlStartTag();
            }

            return $"{htmlIndicator} {Attributes}".ToHtmlStartTag();
        }

        protected static ValueTask<string> AddNewLine(string value, MarkdownDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var markdown = $"{value}{newLineProvider.GetNewLine()}";
            return new ValueTask<string>(markdown);
        }

        protected static ValueTask<string> WrapWithIndentationAndNewLine(string value, HtmlDocumentOptions options, int indentationLevel)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var indenationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize, indentationLevel);
            var html = $"{indenationProvider.GetIndentation(0)}{value}{newLineProvider.GetNewLine()}";
            return new ValueTask<string>(html);
        }

        public abstract ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options);

        public abstract ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0);
    }
}
