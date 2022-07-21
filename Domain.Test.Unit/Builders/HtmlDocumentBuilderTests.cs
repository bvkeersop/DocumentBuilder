using FluentAssertions;
using NDocument.Domain.Builders;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Factories;
using NDocument.Domain.Options;
using NDocument.Domain.Test.Unit.TestHelpers;

namespace NDocument.Domain.Test.Unit.Builders
{
    [TestClass]
    public class HtmlDocumentBuilderTests : BuilderTestBase
    {
        [TestMethod]
        public async Task Build_CreatesMarkdownDocument()
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
                .WithHeader1(_header1)
                .WithHeader2(_header2)
                .WithHeader3(_header3)
                .WithHeader4(_header4)
                .WithParagraph(_paragraph)
                .WithUnorderedList(_unorderedList)
                .WithOrderedList(_orderedList)
                .WithTable(_productTableRowsWithHeaders);

            // Act
            await htmlDocumentBuilder.WriteToOutputStreamAsync(outputStream);

            // Assert
            var expectedMarkdownDocument = GetExpectedHtmlDocument(options);
            var markdownDocument = StreamHelper.GetStreamContents(outputStream);
            markdownDocument.Should().Be(expectedMarkdownDocument);
        }

        private string GetExpectedHtmlDocument(HtmlDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize);

            return
                $"<h1>{_header1}</h1>" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                $"<h2>{_header2}</h2>" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                $"<h3>{_header3}</h3>" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                $"<h4>{_header4}</h4>" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                $"<p>{_paragraph}</p>" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                "<ul>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(1) +
                $"<li>{_unorderedList.ElementAt(0)}</li>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(1) +
                $"<li>{_unorderedList.ElementAt(1)}</li>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(1) +
                $"<li>{_unorderedList.ElementAt(2)}</li>" + newLineProvider.GetNewLine() +
                "</ul>" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                "<ol>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(1) +
                $"<li>{_orderedList.ElementAt(0)}</li>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(1) +
                $"<li>{_orderedList.ElementAt(1)}</li>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(1) +
                $"<li>{_orderedList.ElementAt(2)}</li>" + newLineProvider.GetNewLine() +
                "</ol>" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                ExampleProductHtmlTableBuilder.BuildExpectedProductTable(options);
        }
    }
}
