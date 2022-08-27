using DocumentBuilder.Extensions;
using DocumentBuilder.Model.Excel;
using DocumentBuilder.Model.Generic;
using DocumentBuilder.Options;
using DocumentBuilder.Utilities;
using FluentAssertions;

namespace DocumentBuilder.Test.Unit.Model.Excel
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
            var tableCells = _tableWithoutHeaderAttributes.TableCells.OrderBy(e => e.Value);
            excelTableCells = excelTableCells.OrderBy(e => e.Value);
            excelTableCells.Count().Should().Be(tableCells.Count());

            for (var i = 0; i < excelTableCells.Count(); i++)
            {
                var tableCell = tableCells.ElementAt(i);
                var excelTableCell = excelTableCells.ElementAt(i);
                AssertAreEqual(excelTableCell, tableCell);
            }
        }

        private static void AssertAreEqual(ExcelTableCell excelTableCell, TableCell tableCell)
        {
            tableCell.Value.Should().Be(excelTableCell.Value);
            tableCell.RowPosition.Should().Be(excelTableCell.ExcelRowIdentifier - 1);
            ExcelColumnIdentifierGenerator.GenerateColumnIdentifier(tableCell.ColumnPosition + 1).Should().Be(excelTableCell.ExcelColumnIdentifier);
        }
    }
}