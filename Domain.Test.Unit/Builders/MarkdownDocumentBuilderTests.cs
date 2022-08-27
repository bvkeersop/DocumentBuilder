using DocumentBuilder.Enumerations;
using DocumentBuilder.Test.Unit.TestHelpers;
using FluentAssertions;
using DocumentBuilder.DocumentBuilders;
using DocumentBuilder.Options;

namespace DocumentBuilder.Test.Unit.Builders
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
            await markdownDocumentBuilder.WriteToStreamAsync(outputStream);

            // Assert
            var expectedMarkdownDocument = GetExpectedMarkdownDocument(options);
            var markdownDocument = StreamHelper.GetStreamContents(outputStream);
            markdownDocument.Should().Be(expectedMarkdownDocument);
        }
    }
}
