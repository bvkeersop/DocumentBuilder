using NDocument.Domain.Factories;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Model;
using NDocument.Domain.Options;
using NDocument.Domain.Utilities;

namespace NDocument.Domain.Builders
{
    public class MarkdownDocumentBuilder
    {
        public IEnumerable<IMarkdownConvertable> MarkdownConvertables { get; private set; } = new List<IMarkdownConvertable>();

        private readonly INewLineProvider _newLineProvider;
        private readonly MarkdownDocumentOptions _options;

        public MarkdownDocumentBuilder(MarkdownDocumentOptions options)
        {
            _newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            _options = options;
        }

        public async Task WriteToOutputStreamAsync(Stream outputStream)
        {
            using var streamWriter = new StreamWriter(outputStream, leaveOpen: true);

            for (var i = 0; i < MarkdownConvertables.Count(); i++)
            {
                var markdown = await MarkdownConvertables.ElementAt(i).ToMarkdownAsync(_options);
                await streamWriter.WriteAsync(markdown).ConfigureAwait(false);

                if (i < MarkdownConvertables.Count() - 1)
                {
                    await streamWriter.WriteAsync(_newLineProvider.GetNewLine()).ConfigureAwait(false);
                }
            }

            await streamWriter.FlushAsync().ConfigureAwait(false);
        }

        public MarkdownDocumentBuilder WithHeader1(string header1)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new Header1(header1));
            return this;
        }

        public MarkdownDocumentBuilder WithHeader2(string header2)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new Header2(header2));
            return this;
        }

        public MarkdownDocumentBuilder WithHeader3(string header3)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new Header3(header3));
            return this;
        }

        public MarkdownDocumentBuilder WithHeader4(string header4)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new Header4(header4));
            return this;
        }

        public MarkdownDocumentBuilder WithParagraph(string paragraph)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new Paragraph(paragraph));
            return this;
        }

        public MarkdownDocumentBuilder WithOrderedList<T>(IEnumerable<T> orderedList)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new OrderedList<T>(orderedList));
            return this;
        }

        public MarkdownDocumentBuilder WithUnorderedList<T>(IEnumerable<T> unorderedList)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new UnorderedList<T>(unorderedList));
            return this;
        }

        public MarkdownDocumentBuilder WithTable<T>(IEnumerable<T> tableRows)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new Table<T>(tableRows));
            return this;
        }
    }
}
