using DocumentBuilder.Enumerations;
using DocumentBuilder.Test.Unit.TestHelpers;
using FluentAssertions;
using DocumentBuilder.DocumentBuilders;
using DocumentBuilder.Options;

namespace DocumentBuilder.Test.Unit.Builders
{
    [TestClass]
    public class GenericDocumentBuilderTests : BuilderTestBase
    {
        [TestMethod]
        public async Task Build_CreatesHtmlDocument()
        {
            // Arrange
            var options = new GenericDocumentOptions()
            {
                LineEndings = LineEndings.Environment,
                DocumentType = DocumentType.Html
            };

            var outputStream = new MemoryStream();

            var htmlDocumentBuilder = new GenericDocumentBuilder(options)
                .AddHeader1(_header1)
                .AddHeader2(_header2)
                .AddHeader3(_header3)
                .AddHeader4(_header4)
                .AddParagraph(_paragraph)
                .AddUnorderedList(_unorderedList)
                .AddOrderedList(_orderedList)
                .AddTable(_productTableRowsWithoutAttributes)
                .AddImage(_imageName, _imagePath, _imageCaption);

            // Act
            await htmlDocumentBuilder.BuildAsync(outputStream, options.DocumentType);

            // Assert
            var expectedHtmlDocument = GetExpectedHtmlDocument(new HtmlDocumentOptions());
            var htmlDocument = StreamHelper.GetStreamContents(outputStream);
            htmlDocument.Should().Be(expectedHtmlDocument);
        }

        [TestMethod]
        public async Task Build_CreatesMarkdownDocument()
        {
            // Arrange
            var options = new GenericDocumentOptions()
            {
                LineEndings = LineEndings.Environment,
                DocumentType = DocumentType.Markdown
            };

            var outputStream = new MemoryStream();

            var markdownDocumentBuilder = new GenericDocumentBuilder(options)
                .AddHeader1(_header1)
                .AddHeader2(_header2)
                .AddHeader3(_header3)
                .AddHeader4(_header4)
                .AddParagraph(_paragraph)
                .AddUnorderedList(_unorderedList)
                .AddOrderedList(_orderedList)
                .AddTable(_productTableRowsWithoutAttributes)
                .AddImage(_imageName, _imagePath, _imageCaption);

            // Act
            await markdownDocumentBuilder.BuildAsync(outputStream, options.DocumentType);

            // Assert
            var expectedMarkdownDocument = GetExpectedMarkdownDocument(new MarkdownDocumentOptions());
            var markdownDocument = StreamHelper.GetStreamContents(outputStream);
            markdownDocument.Should().Be(expectedMarkdownDocument);
        }
    }
}
