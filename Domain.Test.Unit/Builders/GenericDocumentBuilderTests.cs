using FluentAssertions;
using NDocument.Domain.Builders;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Options;
using NDocument.Domain.Test.Unit.TestHelpers;

namespace NDocument.Domain.Test.Unit.Builders
{
    [TestClass]
    public class GenericDocumentBuilderTests : BuilderTestBase
    {
        [TestMethod]
        public async Task Build_CreatesHtmlDocument()
        {
            // Arrange
            var options = new HtmlDocumentOptions()
            {
                LineEndings = LineEndings.Environment,
                IndentationType = IndentationType.Spaces,
                IndentationSize = 2
            };

            var outputStream = new MemoryStream();

            var htmlDocumentBuilder = new GenericDocumentBuilder(options)
                .WithHeader1(_header1)
                .WithHeader2(_header2)
                .WithHeader3(_header3)
                .WithHeader4(_header4)
                .WithParagraph(_paragraph)
                .WithUnorderedList(_unorderedList)
                .WithOrderedList(_orderedList)
                .WithTable(_productTableRowsWithoutHeaders);

            // Act
            await htmlDocumentBuilder.WriteToStreamAsync(outputStream, DocumentType.Html);

            // Assert
            var expectedHtmlDocument = GetExpectedHtmlDocument(options);
            var htmlDocument = StreamHelper.GetStreamContents(outputStream);
            htmlDocument.Should().Be(expectedHtmlDocument);
        }

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

            var markdownDocumentBuilder = new GenericDocumentBuilder(options)
                .WithHeader1(_header1)
                .WithHeader2(_header2)
                .WithHeader3(_header3)
                .WithHeader4(_header4)
                .WithParagraph(_paragraph)
                .WithUnorderedList(_unorderedList)
                .WithOrderedList(_orderedList)
                .WithTable(_productTableRowsWithoutHeaders);

            // Act
            await markdownDocumentBuilder.WriteToStreamAsync(outputStream, DocumentType.Markdown);

            // Assert
            var expectedMarkdownDocument = GetExpectedMarkdownDocument(options);
            var markdownDocument = StreamHelper.GetStreamContents(outputStream);
            markdownDocument.Should().Be(expectedMarkdownDocument);
        }
    }
}
