using DocumentBuilder.Domain.Enumerations;
using DocumentBuilder.Domain.Extensions;
using DocumentBuilder.Domain.Factories;
using DocumentBuilder.Domain.Model.Generic;
using FluentAssertions;
using DocumentBuilder.Domain.Options;

namespace DocumentBuilder.Domain.Test.Unit.Model.Generic
{
    public class HeaderTestBase : TestBase
    {
        protected private const string _value = "Header";

        public static async Task AssertToMarkdownReturnsCorrectMarkdownHeader(
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

        public static async Task AssertToHtmlReturnsCorrectHtmlHeader(
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
