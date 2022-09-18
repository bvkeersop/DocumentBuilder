using DocumentBuilder.Constants;
using DocumentBuilder.Factories;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Options;

namespace DocumentBuilder.Model.Markdown
{
    internal class FencedCodeblock : IMarkdownConvertable
    {
        private readonly string _codeblock;
        private readonly string? _language;

        public FencedCodeblock(string codeblock, string? language = null)
        {
            _codeblock = codeblock;
            _language = language;
        }

        public ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var markdown =
                $"{MarkdownIndicators.Codeblock}{_language}{newLineProvider.GetNewLine()}" +
                _codeblock + newLineProvider.GetNewLine() +
                MarkdownIndicators.Codeblock + newLineProvider.GetNewLine();
            return new ValueTask<string>(markdown);
        }
    }
}
