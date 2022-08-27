using DocumentBuilder.Factories;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Options;

namespace DocumentBuilder.Model.Generic
{
    public abstract class GenericElement : IMarkdownConvertable, IHtmlConvertable
    {
        protected static ValueTask<string> ConvertToMarkdown(string value, MarkdownDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var markdown = $"{value}{newLineProvider.GetNewLine()}";
            return new ValueTask<string>(markdown);
        }

        protected static ValueTask<string> ConvertToHtml(string value, HtmlDocumentOptions options, int indentationLevel)
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
