using FluentAssertions;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Factories;
using NDocument.Domain.Model;
using NDocument.Domain.Options;

namespace NDocument.Domain.Test.Unit.Model
{
    [TestClass]
    public class RawTextTests : TestBase
    {
        private RawText _rawText;
        private const string _value = "RawText";

        [TestInitialize]
        public void TestInitialize()
        {
            _rawText = new RawText(_value);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdown_ReturnsRawText(LineEndings lineEndings)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            _markdownDocumentOptions = new MarkdownDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var markdownRawText = await _rawText.ToMarkdownAsync(_markdownDocumentOptions);

            // Assert
            var expectedMarkdownRawText = _value + newLineProvider.GetNewLine();
            markdownRawText.Should().Be(expectedMarkdownRawText);
        }

        [DataRow(LineEndings.Linux)]
        public async Task ToHtml_ReturnsRawText(LineEndings lineEndings)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            _htmlDocumentOptions = new HtmlDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var markdownRawText = await _rawText.ToHtmlAsync(_htmlDocumentOptions);

            // Assert
            var expectedMarkdownRawText = _value + newLineProvider.GetNewLine();
            markdownRawText.Should().Be(expectedMarkdownRawText);
        }
    }
}
