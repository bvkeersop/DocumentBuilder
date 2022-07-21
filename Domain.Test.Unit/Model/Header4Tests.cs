using FluentAssertions;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Factories;
using NDocument.Domain.Model;
using NDocument.Domain.Options;

namespace NDocument.Domain.Test.Unit.Model
{
    [TestClass]
    public class Header4Tests : TestBase
    {
        private Header4 _header4;
        private const string _value = "Header4";

        [TestInitialize]
        public void TestInitialize()
        {
            _header4 = new Header4(_value);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdown_ReturnsMarkdownHeader4(LineEndings lineEndings)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            _markdownDocumentOptions = new MarkdownDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var markdownHeader4 = await _header4.ToMarkdownAsync(_markdownDocumentOptions);

            // Assert
            var expectedMarkdownHeader4 = $"#### {_value}" + newLineProvider.GetNewLine();
            markdownHeader4.Should().Be(expectedMarkdownHeader4);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToHtml_ReturnsHtmlHeader4(LineEndings lineEndings)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            _htmlDocumentOptions = new HtmlDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var htmlHeader4 = await _header4.ToHtmlAsync(_htmlDocumentOptions);

            // Assert
            var expectedHtmlHeader4 = $"<h4>{_value}</h4>" + newLineProvider.GetNewLine();
            htmlHeader4.Should().Be(expectedHtmlHeader4);
        }
    }
}
