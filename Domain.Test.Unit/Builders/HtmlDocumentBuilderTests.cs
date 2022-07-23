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
                .WithHeader1(_header1)
                .WithHeader2(_header2)
                .WithHeader3(_header3)
                .WithHeader4(_header4)
                .WithParagraph(_paragraph)
                .WithUnorderedList(_unorderedList)
                .WithOrderedList(_orderedList)
                .WithTable(_productTableRowsWithoutHeaders);

            // Act
            await htmlDocumentBuilder.WriteToOutputStreamAsync(outputStream);

            // Assert
            var expectedHtmlDocument = GetExpectedHtmlDocument(options);
            var htmlDocument = StreamHelper.GetStreamContents(outputStream);
            htmlDocument.Should().Be(expectedHtmlDocument);
        }

        private string GetExpectedHtmlDocument(HtmlDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize);

            return
                "<!DOCTYPE html>" + GetNewLine(newLineProvider) +
                "<html>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 1) +
                    "<body>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        $"<h1>{_header1}</h1>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        $"<h2>{_header2}</h2>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        $"<h3>{_header3}</h3>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        $"<h4>{_header4}</h4>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        $"<p>{_paragraph}</p>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<ul>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 3) +
                            $"<li>{_unorderedList.ElementAt(0)}</li>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 3) +
                            $"<li>{_unorderedList.ElementAt(1)}</li>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 3) +
                            $"<li>{_unorderedList.ElementAt(2)}</li>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "</ul>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<ol>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 3) +
                            $"<li>{_orderedList.ElementAt(0)}</li>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 3) +
                            $"<li>{_orderedList.ElementAt(1)}</li>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 3) +
                            $"<li>{_orderedList.ElementAt(2)}</li>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "</ol>" + GetNewLine(newLineProvider) +
                        ExampleProductHtmlTableBuilder.BuildExpectedProductTable(options, 2) + GetIndentation(indentationProvider, 1) +
                    "</body>" + GetNewLine(newLineProvider) +
                "</html>" + GetNewLine(newLineProvider);
        }
    }
}
