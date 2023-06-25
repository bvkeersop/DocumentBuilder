using DocumentBuilder.Factories;
using DocumentBuilder.Shared.Enumerations;

namespace DocumentBuilder.Markdown.Options
{
    public class MarkdownDocumentOptionsBuilder
    {
        private readonly MarkdownDocumentOptions _options = new();

        public MarkdownDocumentOptionsBuilder WithNewLineProvider(LineEndings lineEndings)
        {
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);
            _options.NewLineProvider = newLineProvider;
            return this;
        }

        public MarkdownDocumentOptionsBuilder WithIndentationProvider(IndentationType indentationType, int indentationSize)
        {
            var indentationProvider = IndentationProviderFactory.Create(indentationType, indentationSize);
            _options.IndentationProvider = indentationProvider;
            return this;
        }

        public MarkdownDocumentOptionsBuilder WithMarkdownTableOptions(MarkdownTableOptions tableOptions)
        {
            _options.MarkdownTableOptions = tableOptions;
            return this;
        }

        public MarkdownDocumentOptions Build() => _options;
    }
}
