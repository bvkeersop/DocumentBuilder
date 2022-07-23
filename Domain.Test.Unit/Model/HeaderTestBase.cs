using FluentAssertions;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Extensions;
using NDocument.Domain.Factories;
using NDocument.Domain.Model;
using NDocument.Domain.Options;

namespace NDocument.Domain.Test.Unit.Model
{
    public class HeaderTestBase : TestBase
    {
        protected private const string _value = "Header";

        public async Task AssertToMarkdownReturnsCorrectMarkdownHeader(
            Header header,
            string headerIndicator,
            LineEndings lineEndings)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            var options = new MarkdownDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var markdownHeader = await header.ToMarkdownAsync(options);

            // Assert
            var expectedMarkdownHeader = $"{headerIndicator} {header.Value}" + newLineProvider.GetNewLine();
            markdownHeader.Should().Be(expectedMarkdownHeader);
        }

        public async Task AssertToHtmlReturnsCorrectHtmlHeader(
          Header header,
          string headerIndicator,
          LineEndings lineEndings,
          IndentationType indentationType,
          int indentationSize,
          int indentationLevel)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);
            var indenationProvider = IndentationProviderFactory.Create(indentationType, indentationSize);

            var options = new HtmlDocumentOptions
            {
                LineEndings = lineEndings,
                IndentationType = indentationType,
                IndentationSize = indentationSize,
            };

            // Act
            var htmlHeader = await header.ToHtmlAsync(options, indentationLevel);

            // Assert
            var expectedIndentation = indenationProvider.GetIndentation(indentationLevel);
            var expectedHtmlHeader1 = $"{expectedIndentation}{headerIndicator.ToHtmlStartTag()}{header.Value}{headerIndicator.ToHtmlEndTag()}" + newLineProvider.GetNewLine();
            htmlHeader.Should().Be(expectedHtmlHeader1);
        }
    }
}
