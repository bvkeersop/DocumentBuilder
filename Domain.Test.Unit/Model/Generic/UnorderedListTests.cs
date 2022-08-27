using DocumentBuilder.Constants;
using DocumentBuilder.Enumerations;
using DocumentBuilder.Model.Generic;
using DocumentBuilder.Test.Unit.TestHelpers;
using DocumentBuilder.Options;

namespace DocumentBuilder.Test.Unit.Model.Generic
{
    [TestClass]
    public class UnorderedListTests : ListTestBase
    {
        private UnorderedList<ProductTableRowWithoutAttributes> _unorderedList;

        [TestInitialize]
        public void TestInitialize()
        {
            _unorderedList = new UnorderedList<ProductTableRowWithoutAttributes>(_exampleProducts);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdown_ReturnsMarkdownUnorderedList(LineEndings lineEndings)
        {
            // Arrange
            var options = new MarkdownDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act & Assert
            await AssertToMarkdownReturnsCorrectMarkdownlList(_unorderedList, MarkdownIndicators.UnorderedListItem, options);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Windows, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Linux, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 4, 2)]
        public async Task ToHtml_ReturnsHtmlUnorderedList(LineEndings lineEndings, IndentationType indentationType, int indentationSize, int indentationLevel)
        {
            // Arrange
            var options = new HtmlDocumentOptions
            {
                LineEndings = lineEndings,
                IndentationType = indentationType,
                IndentationSize = indentationSize,
            };

            // Act & Assert
            await AssertToHtmlReturnsCorrectHtmlList(_unorderedList, HtmlIndicators.UnorderedList, options, indentationLevel);
        }
    }
}
