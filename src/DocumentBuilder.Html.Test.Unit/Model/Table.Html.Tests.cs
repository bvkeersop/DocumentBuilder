using DocumentBuilder.Enumerations;
using DocumentBuilder.Html.Test.Unit.TestHelpers;
using DocumentBuilder.Options;
using FluentAssertions;

namespace DocumentBuilder.Test.Unit.Model.Html
{
    [TestClass]
    public class TableHtmlTests : TableTestBase
    {
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
