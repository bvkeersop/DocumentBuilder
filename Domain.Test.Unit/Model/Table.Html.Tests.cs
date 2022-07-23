using FluentAssertions;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Model;
using NDocument.Domain.Options;
using NDocument.Domain.Test.Unit.TestHelpers;

namespace NDocument.Domain.Test.Unit.Model
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

        [DataTestMethod]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Linux, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Windows, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 4, 2)]
        [DataRow(LineEndings.Environment, IndentationType.Tabs, 2, 0)]
        public async Task WriteAsHtmlToStreamAsync_CreatesFormattedTable(LineEndings LineEndings, IndentationType indentationType, int indenationSize, int indentationLevel)
        {
            // Arrange
            var outputStream = new MemoryStream();

            var options = new HtmlDocumentOptions
            {
                LineEndings = LineEndings,
                IndentationType = indentationType,
                IndentationSize = indenationSize
            };

            // Act
            await _tableWithoutHeaderAttributes.WriteAsHtmlToStreamAsync(outputStream, options, indentationLevel);

            // Assert
            var table = StreamHelper.GetStreamContents(outputStream);
            var expectedTable = ExampleProductHtmlTableBuilder.BuildExpectedProductTable(options, indentationLevel);
            table.Should().Be(expectedTable);
        }
    }
}
