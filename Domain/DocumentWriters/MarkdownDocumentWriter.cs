using NDocument.Domain.Enumerations;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Options;
using NDocument.Domain.Utilities;

namespace NDocument.Domain.Writers
{
    internal class MarkdownDocumentWriter
    {
        private readonly Func<Stream, MarkdownDocumentOptions, MarkdownStreamWriter> _markdownStreamWriterFactory;
        private readonly MarkdownDocumentOptions _options;

        public MarkdownDocumentWriter(Func<Stream, MarkdownDocumentOptions, MarkdownStreamWriter> markdownStreamWriterFactory, MarkdownDocumentOptions options)
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