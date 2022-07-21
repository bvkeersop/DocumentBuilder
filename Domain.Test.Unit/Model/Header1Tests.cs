using FluentAssertions;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Factories;
using NDocument.Domain.Model;
using NDocument.Domain.Options;

namespace NDocument.Domain.Test.Unit.Model
{
    [TestClass]
    public class Header1Tests : TestBase
    {
        private Header1 _header1;
        private const string _value = "Header1";

        [TestInitialize]
        public void TestInitialize()
        {
            _header1 = new Header1(_value);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdown_ReturnsMarkdownHeader1(LineEndings lineEndings)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            _markdownDocumentOptions = new MarkdownDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var markdownHeader1 = await _header1.ToMarkdownAsync(_markdownDocumentOptions);

            // Assert
            var expectedMarkdownHeader1 = $"# {_value}" + newLineProvider.GetNewLine();
            markdownHeader1.Should().Be(expectedMarkdownHeader1);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToHtml_ReturnsHtmlHeader1(LineEndings lineEndings)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            _htmlDocumentOptions = new HtmlDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var htmlHeader1 = await _header1.ToHtmlAsync(_htmlDocumentOptions);

            // Assert
            var expectedHtmlHeader1 = $"<h1>{_value}</h1>" + newLineProvider.GetNewLine();
            htmlHeader1.Should().Be(expectedHtmlHeader1);
        }
    }
}
