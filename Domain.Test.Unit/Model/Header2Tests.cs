using FluentAssertions;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Factories;
using NDocument.Domain.Model;
using NDocument.Domain.Options;

namespace NDocument.Domain.Test.Unit.Model
{
    [TestClass]
    public class Header2Tests : TestBase
    {
        private Header2 _header2;
        private const string _value = "Header2";

        [TestInitialize]
        public void TestInitialize()
        {
            _header2 = new Header2(_value);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdown_ReturnsMarkdownHeader2(LineEndings lineEndings)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            _markdownDocumentOptions = new MarkdownDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var markdownHeader2 = await _header2.ToMarkdownAsync(_markdownDocumentOptions);

            // Assert
            var expectedMarkdownHeader2 = $"## {_value}" + newLineProvider.GetNewLine();
            markdownHeader2.Should().Be(expectedMarkdownHeader2);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToHtml_ReturnsHtmlHeader2(LineEndings lineEndings)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            _htmlDocumentOptions = new HtmlDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var htmlHeader2 = await _header2.ToHtmlAsync(_htmlDocumentOptions);

            // Assert
            var expectedHtmlHeader2 = $"<h2>{_value}</h2>" + newLineProvider.GetNewLine();
            htmlHeader2.Should().Be(expectedHtmlHeader2);
        }
    }
}
