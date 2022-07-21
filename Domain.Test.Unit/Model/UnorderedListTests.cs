using FluentAssertions;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Factories;
using NDocument.Domain.Model;
using NDocument.Domain.Options;
using NDocument.Domain.Test.Unit.TestHelpers;
using NDocument.Domain.Utilities;

namespace NDocument.Domain.Test.Unit.Model
{
    [TestClass]
    public class UnorderedListTests : TestBase
    {
        private INewLineProvider _newLineProvider;
        private IIndentationProvider _indentationProvider;
        private IEnumerable<ProductTableRowWithoutHeaders> _exampleProducts;
        private UnorderedList<ProductTableRowWithoutHeaders> _orderedList;

        [TestInitialize]
        public void TestInitialize()
        {
            _exampleProducts = ExampleProductsGenerator.CreateTableRowsWithoutHeaders();
            _orderedList = new UnorderedList<ProductTableRowWithoutHeaders>(_exampleProducts);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdown_ReturnsMarkdownOrderedList(LineEndings lineEndings)
        {
            // Arrange
            _markdownDocumentOptions = new MarkdownDocumentOptions
            {
                LineEndings = lineEndings
            };

            _newLineProvider = NewLineProviderFactory.Create(_markdownDocumentOptions.LineEndings);

            // Act
            var markdownOrderedList = await _orderedList.ToMarkdownAsync(_markdownDocumentOptions);

            // Assert
            var expectedMarkdownOrderedList = CreateExpectedMarkdownOrderedList();
            markdownOrderedList.Should().Be(expectedMarkdownOrderedList);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 2)]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 4)]
        [DataRow(LineEndings.Environment, IndentationType.Tabs, 1)]
        [DataRow(LineEndings.Environment, IndentationType.Tabs, 2)]
        [DataRow(LineEndings.Windows, IndentationType.Spaces, 2)]
        [DataRow(LineEndings.Windows, IndentationType.Spaces, 4)]
        [DataRow(LineEndings.Windows, IndentationType.Tabs, 1)]
        [DataRow(LineEndings.Windows, IndentationType.Tabs, 2)]
        [DataRow(LineEndings.Linux, IndentationType.Spaces, 2)]
        [DataRow(LineEndings.Linux, IndentationType.Spaces, 4)]
        [DataRow(LineEndings.Linux, IndentationType.Tabs, 1)]
        [DataRow(LineEndings.Linux, IndentationType.Tabs, 2)]
        public async Task ToHtml_ReturnsHtmlOrderedList(LineEndings lineEndings, IndentationType indentationType, int indentationSize)
        {
            // Arrange

            _htmlDocumentOptions = new HtmlDocumentOptions
            {
                LineEndings = lineEndings,
                IndentationType = IndentationType.Spaces,
                IndentationSize = 2
            };

            _newLineProvider = NewLineProviderFactory.Create(_htmlDocumentOptions.LineEndings);
            _indentationProvider = IndentationProviderFactory.Create(_htmlDocumentOptions.IndentationType, _htmlDocumentOptions.IndentationSize);

            // Act
            var htmlOrderedList = await _orderedList.ToHtmlAsync(_htmlDocumentOptions);

            // Assert
            var expectedHtmlOrderedList = CreateExpectedHtmlOrderedList();
            htmlOrderedList.Should().Be(expectedHtmlOrderedList);
        }

        private string CreateExpectedMarkdownOrderedList()
        {
            var string1 = _exampleProducts.ElementAt(0).ToString();
            var string2 = _exampleProducts.ElementAt(1).ToString();
            var string3 = _exampleProducts.ElementAt(2).ToString();

            return
                $"- {string1}" + _newLineProvider.GetNewLine() +
                $"- {string2}" + _newLineProvider.GetNewLine() +
                $"- {string3}" + _newLineProvider.GetNewLine();
        }

        private string CreateExpectedHtmlOrderedList()
        {
            var string1 = _exampleProducts.ElementAt(0).ToString();
            var string2 = _exampleProducts.ElementAt(1).ToString();
            var string3 = _exampleProducts.ElementAt(2).ToString();

            return
                "<ul>" + _newLineProvider.GetNewLine() + _indentationProvider.GetIndentation(1) +
                $"<li>{string1}</li>" + _newLineProvider.GetNewLine() + _indentationProvider.GetIndentation(1) +
                $"<li>{string2}</li>" + _newLineProvider.GetNewLine() + _indentationProvider.GetIndentation(1) +
                $"<li>{string3}</li>" + _newLineProvider.GetNewLine() +
                "</ul>" + _newLineProvider.GetNewLine();
        }
    }
}
