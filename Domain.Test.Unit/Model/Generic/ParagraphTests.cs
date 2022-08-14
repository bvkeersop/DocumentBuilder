using FluentAssertions;
using NDocument.Domain.Constants;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Extensions;
using NDocument.Domain.Factories;
using NDocument.Domain.Model.Generic;
using NDocument.Domain.Options;

namespace NDocument.Domain.Test.Unit.Model.Generic
{
    [TestClass]
    public class ParagraphTests
    {
        private Paragraph _paragraph;
        private const string _value = "paragraph";

        [TestInitialize]
        public void TestInitialize()
        {
            _paragraph = new Paragraph(_value);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdown_ReturnsParagraph(LineEndings lineEndings)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            var options = new MarkdownDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var markdownRawText = await _paragraph.ToMarkdownAsync(options);

            // Assert
            var expectedMarkdownRawText = _value + newLineProvider.GetNewLine();
            markdownRawText.Should().Be(expectedMarkdownRawText);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 2, 1)]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 4, 2)]
        [DataRow(LineEndings.Windows, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Linux, IndentationType.Spaces, 2, 0)]
        public async Task ToHtml_ReturnsParagraph(
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
            var htmlHeader = await _paragraph.ToHtmlAsync(options, indentationLevel);

            // Assert
            var expectedIndentation = indenationProvider.GetIndentation(indentationLevel);
            var expectedHtmlHeader1 = $"{expectedIndentation}{HtmlIndicators.Paragraph.ToHtmlStartTag()}{_paragraph.Value}{HtmlIndicators.Paragraph.ToHtmlEndTag()}" + newLineProvider.GetNewLine();
            htmlHeader.Should().Be(expectedHtmlHeader1);
        }
    }
}
