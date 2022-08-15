using DocumentBuilder.Domain.Extensions;
using DocumentBuilder.Domain.Options;
using FluentAssertions;

namespace DocumentBuilder.Domain.Test.Unit.Model.Excel
{
    [TestClass]
    public class TableExcelTests : TableTestBase
    {
        [TestMethod]
        public void ToExcel_CreatesExcelTableCells()
        {
            // Arrange
            var options = new ExcelDocumentOptions();

            // Act
            var excelTableCells = _tableWithoutHeaderAttributes.ToExcel(options);

            // Assert
            var expectedExcelTableCells = _tableWithoutHeaderAttributes.TableCells.Select(t => t.ToExcelTableCell());

            //TODO: fix test
            excelTableCells.SequenceEqual(expectedExcelTableCells).Should().BeTrue();
        }
    }
}