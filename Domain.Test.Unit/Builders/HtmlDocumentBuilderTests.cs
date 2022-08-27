using DocumentBuilder.Enumerations;
using DocumentBuilder.Test.Unit.TestHelpers;
using FluentAssertions;
using DocumentBuilder.DocumentBuilders;
using DocumentBuilder.Options;

namespace DocumentBuilder.Test.Unit.Builders
{
    [TestClass]
    public class HtmlDocumentBuilderTests : BuilderTestBase
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

            var htmlDocumentBuilder = new HtmlDocumentBuilder(options)
                .AddHeader1(_header1)
                .AddHeader2(_header2)
                .AddHeader3(_header3)
                .AddHeader4(_header4)
                .AddParagraph(_paragraph)
                .AddUnorderedList(_unorderedList)
                .AddOrderedList(_orderedList)
                .AddTable(_productTableRowsWithoutHeaders)
                .AddImage(_imageName, _imagePath, _imageCaption);

            // Act
            await htmlDocumentBuilder.WriteToStreamAsync(outputStream);

            // Assert
            var expectedHtmlDocument = GetExpectedHtmlDocument(options);
            var htmlDocument = StreamHelper.GetStreamContents(outputStream);
            htmlDocument.Should().Be(expectedHtmlDocument);
        }
    }
}
