using NDocument.Domain.Enumerations;
using NDocument.Domain.Extensions;
using NDocument.Domain.Factories;
using NDocument.Domain.Model;
using NDocument.Domain.Options;
using NDocument.Domain.Utilities;

namespace NDocument.Domain.Builders
{
    internal class DocumentBuilder
    {
        public IEnumerable<GenericElement> Convertables { get; private set; } = new List<GenericElement>();

        private readonly INewLineProvider _newLineProvider;
        private readonly DocumentOptions _options;

        public DocumentBuilder(DocumentOptions options)
        {
            _newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            _options = options;
        }

        private Func<GenericElement, Task<string>> GetCreateContentFunction(DocumentType type)
        {
            return type switch
            {
                DocumentType.Markdown => async (GenericElement element) => await element.ToMarkdownAsync(_options.ToMarkdownDocumentOptions()),
                DocumentType.Html => async (GenericElement element) => await element.ToHtmlAsync(_options.ToHtmlDocumentOptions()),
                _ => throw new NotSupportedException($"{type} is currently not supported")
            };
        }

        public async Task WriteToOutputStreamAsync(Stream outputStream, DocumentType documentType)
        {
            var createContentFunction = GetCreateContentFunction(documentType);
            await WriteToOutputStreamInternalAsync(outputStream, createContentFunction);
        }

        public async Task WriteToOutputStreamInternalAsync(Stream outputStream, Func<GenericElement, Task<string>> getContentToWrite)
        {
            using var streamWriter = new StreamWriter(outputStream, leaveOpen: true);

            for (var i = 0; i < Convertables.Count(); i++)
            {
                var currentConvertable = Convertables.ElementAt(i);
                var content = await getContentToWrite(currentConvertable);
                await streamWriter.WriteAsync(content).ConfigureAwait(false);

                if (i < Convertables.Count() - 1)
                {
                    await streamWriter.WriteAsync(_newLineProvider.GetNewLine()).ConfigureAwait(false);
                }
            }

            await streamWriter.FlushAsync().ConfigureAwait(false);
        }

        public DocumentBuilder WithHeader1(string header1)
        {
            Convertables = Convertables.Append(new Header1(header1));
            return this;
        }

        public DocumentBuilder WithHeader2(string header2)
        {
            Convertables = Convertables.Append(new Header2(header2));
            return this;
        }

        public DocumentBuilder WithHeader3(string header3)
        {
            Convertables = Convertables.Append(new Header3(header3));
            return this;
        }

        public DocumentBuilder WithHeader4(string header4)
        {
            Convertables = Convertables.Append(new Header4(header4));
            return this;
        }

        public DocumentBuilder WithParagraph(string paragraph)
        {
            Convertables = Convertables.Append(new Paragraph(paragraph));
            return this;
        }

        public DocumentBuilder WithOrderedList<T>(IEnumerable<T> orderedList)
        {
            Convertables = Convertables.Append(new OrderedList<T>(orderedList));
            return this;
        }

        public DocumentBuilder WithUnorderedList<T>(IEnumerable<T> unorderedList)
        {
            Convertables = Convertables.Append(new UnorderedList<T>(unorderedList));
            return this;
        }

        public DocumentBuilder WithTable<T>(IEnumerable<T> tableRows)
        {
            Convertables = Convertables.Append(new Table<T>(tableRows));
            return this;
        }
    }
}
