using DocumentBuilder.Domain.Interfaces;
using DocumentBuilder.Domain.Options;

namespace DocumentBuilder.Domain.DocumentWriters
{
    internal class MarkdownDocumentWriter
    {
        private readonly Func<Stream, MarkdownDocumentOptions, IMarkdownStreamWriter> _markdownStreamWriterFactory;
        private readonly MarkdownDocumentOptions _options;

        public MarkdownDocumentWriter(Func<Stream, MarkdownDocumentOptions, IMarkdownStreamWriter> markdownStreamWriterFactory, MarkdownDocumentOptions options)
        {
            _markdownStreamWriterFactory = markdownStreamWriterFactory;
            _options = options;
        }

        public async Task WriteToStreamAsync(Stream outputStream, IEnumerable<IMarkdownConvertable> markdownConvertables)
        {
            using var markdownStreamWriter = _markdownStreamWriterFactory(outputStream, _options);

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

            await markdownStreamWriter.FlushAsync().ConfigureAwait(false);
        }
    }
}