using DocumentBuilder.Constants;
using DocumentBuilder.Factories;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Options;

namespace DocumentBuilder.Model.Markdown
{
    internal class Blockquote : IMarkdownConvertable
    {
        private readonly string _value;

        public Blockquote(string value)
        {
            _value = value;
        }

        public ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var markdown = $"{MarkdownIndicators.Blockquote} {_value}{newLineProvider.GetNewLine()}";
            return new ValueTask<string>(markdown);
        }
    }
}
