using DocumentBuilder.Domain.Enumerations;
using DocumentBuilder.Domain.Test.Unit.Model;
using DocumentBuilder.Domain.Test.Unit.TestHelpers;
using FluentAssertions;
using DocumentBuilder.Domain.Model;
using DocumentBuilder.Domain.Options;

namespace DocumentBuilder.Domain.Test.Unit.Model.Html
{
    [TestClass]
    public class TableHtmlTests : TableTestBase
    {
        private Table<ProductTableRowWithoutHeaders> _tableWithoutHeaderAttributes;

        [TestInitialize]
        public void TestInitialize()
        {
            _tableWithoutHeaderAttributes = new Table<ProductTableRowWithoutHeaders>(_productTableRowsWithoutHeaders);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Linux, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Windows, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 4, 2)]
        [DataRow(LineEndings.Environment, IndentationType.Tabs, 2, 0)]
        public async Task ToHtmlAsync_CreatesFormattedTable(LineEndings LineEndings, IndentationType indentationType, int indenationSize, int indentationLevel)
        {
            // Arrange
            var options = new HtmlDocumentOptions
            {
                LineEndings = LineEndings,
                IndentationType = indentationType,
                IndentationSize = indenationSize
            };

            // Act
            var htmlTable = await _tableWithoutHeaderAttributes.ToHtmlAsync(options, indentationLevel);

            // Assert
            var expectedTable = ExampleProductHtmlTableBuilder.BuildExpectedProductTable(options, indentationLevel);
            htmlTable.Should().Be(expectedTable);
        }
    }
}
