using DocumentBuilder.Constants;
using DocumentBuilder.Factories;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Options;

namespace DocumentBuilder.Model.Markdown
{
    internal class HorizontalRule : IMarkdownConvertable
    {
        public ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var markdown = $"{MarkdownIndicators.HorizontalRule}{newLineProvider.GetNewLine()}";
            return new ValueTask<string>(markdown);
        }
    }
}
