using DocumentBuilder.Domain.Constants;
using DocumentBuilder.Domain.Enumerations;
using DocumentBuilder.Domain.Model.Generic;
using DocumentBuilder.Domain.Test.Unit.TestHelpers;
using DocumentBuilder.Domain.Options;

namespace DocumentBuilder.Domain.Test.Unit.Model.Generic
{
    [TestClass]
    public class OrderedListTests : ListTestBase
    {
        private OrderedList<ProductTableRowWithoutHeaders> _orderedList;

        [TestInitialize]
        public void TestInitialize()
        {
            _orderedList = new OrderedList<ProductTableRowWithoutHeaders>(_exampleProducts);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdown_ReturnsMarkdownOrderedList(LineEndings lineEndings)
        {
            // Arrange
            var options = new MarkdownDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act & Assert
            await AssertToMarkdownReturnsCorrectMarkdownlList(_orderedList, MarkdownIndicators.OrderedListItem, options);
        }


        [DataTestMethod]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Windows, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Linux, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 4, 2)]
        public async Task ToHtml_ReturnsHtmlOrderedList(LineEndings lineEndings, IndentationType indentationType, int indentationSize, int indentationLevel)
        {
            // Arrange
            var options = new HtmlDocumentOptions
            {
                LineEndings = lineEndings,
                IndentationType = indentationType,
                IndentationSize = indentationSize,
            };

            // Act & Assert
            await AssertToHtmlReturnsCorrectHtmlList(_orderedList, HtmlIndicators.OrderedList, options, indentationLevel);
        }
    }
}
