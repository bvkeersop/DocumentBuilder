using NDocument.Domain.Factories;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Options;
using NDocument.Domain.Utilities;

namespace NDocument.Domain.Writers
{
    internal class MarkdownDocumentWriter
    {
        private readonly MarkdownDocumentOptions _options;
        private readonly INewLineProvider _newLineProvider;

        public MarkdownDocumentWriter(MarkdownDocumentOptions options)
        {
            _newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            _options = options;
        }

        public async Task WriteToStreamAsync(Stream outputStream, IEnumerable<IMarkdownConvertable> markdownConvertables)
        {
            var streamWriter = new StreamWriter(outputStream, leaveOpen: true);
            using var markdownStreamWriter = new MarkdownStreamWriter(streamWriter, _newLineProvider);

            for (var i = 0; i < markdownConvertables.Count(); i++)
            {
                var markdown = await markdownConvertables.ElementAt(i).ToMarkdownAsync(_options);

                if (i < markdownConvertables.Count() - 1)
                {
                    await markdownStreamWriter.WriteLineAsync(markdown).ConfigureAwait(false);
                    continue;
                }

                await markdownStreamWriter.WriteAsync(markdown).ConfigureAwait(false);
            }

            await streamWriter.FlushAsync().ConfigureAwait(false);
        }
    }
}