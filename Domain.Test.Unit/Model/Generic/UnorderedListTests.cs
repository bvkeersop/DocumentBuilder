using DocumentBuilder.Domain.Constants;
using DocumentBuilder.Domain.Enumerations;
using DocumentBuilder.Domain.Model.Generic;
using DocumentBuilder.Domain.Test.Unit.TestHelpers;
using DocumentBuilder.Domain.Options;

namespace DocumentBuilder.Domain.Test.Unit.Model.Generic
{
    [TestClass]
    public class UnorderedListTests : ListTestBase
    {
        private UnorderedList<ProductTableRowWithoutHeaders> _unorderedList;

        [TestInitialize]
        public void TestInitialize()
        {
            _unorderedList = new UnorderedList<ProductTableRowWithoutHeaders>(_exampleProducts);
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
