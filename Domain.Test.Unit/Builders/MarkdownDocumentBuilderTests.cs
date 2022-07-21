using FluentAssertions;
using NDocument.Domain.Builders;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Factories;
using NDocument.Domain.Options;
using NDocument.Domain.Test.Unit.TestHelpers;

namespace NDocument.Domain.Test.Unit.Builders
{
    [TestClass]
    public class MarkdownDocumentBuilderTests : BuilderTestBase
    {
        [TestMethod]
        public async Task Build_CreatesMarkdownDocument()
        {
            // Arrange
            var options = new MarkdownDocumentOptions()
            {
                LineEndings = LineEndings.Environment,
                MarkdownTableOptions = new MarkdownTableOptions
                {
                    BoldColumnNames = false,
                    Formatting = Formatting.AlignColumns
                }
            };

            var outputStream = new MemoryStream();

            var markdownDocumentBuilder = new MarkdownDocumentBuilder(options)
                .WithHeader1(_header1)
                .WithHeader2(_header2)
                .WithHeader3(_header3)
                .WithHeader4(_header4)
                .WithParagraph(_paragraph)
                .WithUnorderedList(_unorderedList)
                .WithOrderedList(_orderedList)
                .WithTable(_productTableRowsWithoutHeaders);

            // Act
            await markdownDocumentBuilder.WriteToOutputStreamAsync(outputStream);

            // Assert
            var expectedMarkdownDocument = GetExpectedMarkdownDocument(options);
            var markdownDocument = StreamHelper.GetStreamContents(outputStream);
            markdownDocument.Should().Be(expectedMarkdownDocument);
        }

        private string GetExpectedMarkdownDocument(MarkdownDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);

            return
                $"# {_header1}" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                $"## {_header2}" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                $"### {_header3}" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                $"#### {_header4}" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                _paragraph + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                $"- {_unorderedList.ElementAt(0)}" + newLineProvider.GetNewLine() +
                $"- {_unorderedList.ElementAt(1)}" + newLineProvider.GetNewLine() +
                $"- {_unorderedList.ElementAt(2)}" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                $"1. {_orderedList.ElementAt(0)}" + newLineProvider.GetNewLine() +
                $"1. {_orderedList.ElementAt(1)}" + newLineProvider.GetNewLine() +
                $"1. {_orderedList.ElementAt(2)}" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                ExampleProductMarkdownTableBuilder.BuildExpectedFormattedProductTable(options);
        }
    }
}
