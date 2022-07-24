using FluentAssertions;
using NDocument.Domain.Model;
using NDocument.Domain.Test.Unit.TestHelpers;

namespace NDocument.Domain.Test.Unit.Model
{
    [TestClass]
    public class MatrixTests : TableTestBase
    {
        private Matrix<ProductTableRowWithHeaders> _matrix;

        [TestInitialize]
        public void TestInitialize()
        {
            _matrix = new Matrix<ProductTableRowWithHeaders>(_productTableRowsWithHeaders);
        }

        [DataTestMethod]
        [DataRow(0, 1)]
        [DataRow(1, 1)]
        [DataRow(2, 4)]
        [DataRow(3, 42)]
        public void GetLongestCellSize_ReturnsLongestCellSize(int columnIndex, int expectedLength)
        {
            // Act
            var longestCellSize = _matrix.GetLongestCellSizeOfColumn(columnIndex);

            // Assert
            longestCellSize.Should().Be(expectedLength);
        }

        [TestMethod]
        public void GetRow_ReturnsRow()
        {
            // Act
            var row = _matrix.GetRow(0);

            // Assert
            var expectedRow = _productTableRowsWithHeaders.ElementAt(0);
            row.Length.Should().Be(4); // amount of properties
            row[0].Value.Should().Be(expectedRow.Id);
            row[0].Type.Should().Be(expectedRow.Id.GetType());
            row[1].Value.Should().Be(expectedRow.Amount);
            row[1].Type.Should().Be(expectedRow.Amount.GetType());
            row[2].Value.Should().Be(expectedRow.Price);
            row[2].Type.Should().Be(expectedRow.Price.GetType());
            row[3].Value.Should().Be(expectedRow.Description);
            row[3].Type.Should().Be(expectedRow.Description.GetType());
        }

        [TestMethod]
        public void GetColumn_ReturnsColumn()
        {
            // Act
            var column = _matrix.GetColumn(0);

            // Assert
            var rowOne = _productTableRowsWithHeaders.ElementAt(0);
            var rowTwo = _productTableRowsWithHeaders.ElementAt(1);
            var rowThree = _productTableRowsWithHeaders.ElementAt(2);

            column.Length.Should().Be(_productTableRowsWithHeaders.Count());
            column[0].Value.Should().Be(rowOne.Id);
            column[0].Type.Should().Be(rowOne.Id.GetType());
            column[1].Value.Should().Be(rowTwo.Id);
            column[1].Type.Should().Be(rowTwo.Id.GetType());
            column[2].Value.Should().Be(rowThree.Id);
            column[2].Type.Should().Be(rowThree.Id.GetType());
        }

        [TestMethod]
        public void NumberOfColums_ReturnsNumberOfColumns()
        {
            // Act
            var numberOfColums = _matrix.NumberOfColumns;

            // Assert
            numberOfColums.Should().Be(4);
        }

        [TestMethod]
        public void NumberOfRows_ReturnsNumberOfRows()
        {
            // Act
            var numberOfRows = _matrix.NumberOfRows;

            // Assert
            numberOfRows.Should().Be(3);
        }
    }
}
