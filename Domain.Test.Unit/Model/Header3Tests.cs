using FluentAssertions;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Factories;
using NDocument.Domain.Model;
using NDocument.Domain.Options;

namespace NDocument.Domain.Test.Unit.Model
{
    [TestClass]
    public class Header3Tests : TestBase
    {
        private Header3 _header3;
        private const string _value = "Header3";

        [TestInitialize]
        public void TestInitialize()
        {
            _header3 = new Header3(_value);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdown_ReturnsMarkdownHeader3(LineEndings lineEndings)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            _markdownDocumentOptions = new MarkdownDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var markdownHeader3 = await _header3.ToMarkdownAsync(_markdownDocumentOptions);

            // Assert
            var expectedMarkdownHeader3 = $"### {_value}" + newLineProvider.GetNewLine();
            markdownHeader3.Should().Be(expectedMarkdownHeader3);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToHtml_ReturnsHtmlHeader3(LineEndings lineEndings)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            _htmlDocumentOptions = new HtmlDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var htmlHeader3 = await _header3.ToHtmlAsync(_htmlDocumentOptions);

            // Assert
            var expectedHtmlHeader3 = $"<h3>{_value}</h3>" + newLineProvider.GetNewLine();
            htmlHeader3.Should().Be(expectedHtmlHeader3);
        }
    }
}
