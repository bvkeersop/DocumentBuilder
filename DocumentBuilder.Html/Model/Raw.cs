using DocumentBuilder.Factories;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Model.Html;
using DocumentBuilder.Options;

namespace DocumentBuilder.Core.Model
{
    internal class Raw : IMarkdownConvertable, IHtmlElement
    {
        private readonly string _value;

        public DocumentBuilder.Model.Html.Attributes Attributes { get; } = new();

        public Raw(string value)
        {
            _value = value;
        }

        public ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
        {
            return WrapWithIndentationAndNewLine(_value, options, indentationLevel);
        }

        public ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            var newlineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var markdown = $"{_value}{newlineProvider.GetNewLine()}";
            return new ValueTask<string>(markdown);
        }

        protected static ValueTask<string> WrapWithIndentationAndNewLine(string value, HtmlDocumentOptions options, int indentationLevel)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var indenationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize, indentationLevel);
            var html = $"{indenationProvider.GetIndentation(0)}{value}{newLineProvider.GetNewLine()}";
            return new ValueTask<string>(html);
        }
    }
}
