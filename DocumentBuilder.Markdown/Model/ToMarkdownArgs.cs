using DocumentBuilder.Factories;
using DocumentBuilder.Markdown.Options;
using DocumentBuilder.Shared.Enumerations;
using DocumentBuilder.Utilities;

namespace DocumentBuilder.Markdown.Model
{
    public record ToMarkdownArgs
    {
        public INewLineProvider NewLineProvider { get; init; } = NewLineProviderFactory.Create(LineEndings.Environment);
        public IIndentationProvider IndentationProvider { get; init; } = IndentationProviderFactory.Create(IndentationType.Spaces, 4);
        public MarkdownDocumentBuilderOptions MarkdownDocumentOptions { get; init; } = new MarkdownDocumentBuilderOptions();
    }
}
