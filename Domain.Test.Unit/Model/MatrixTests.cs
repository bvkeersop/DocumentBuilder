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

        [TestMethod]
        public void GetLongestCellSize_ReturnsLongestCellSize()
        {
            // Act
            var longestCellSize = _matrix.LongestCellSize;

            // Assert
            longestCellSize.Should().Be(_longestDescription.Length);
        }

        [DataTestMethod]
        [DataRow(0, 13)]
        [DataRow(1, 13)]
        [DataRow(2, 42)]
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
            row[0].Should().Be(expectedRow.Id);
            row[1].Should().Be(expectedRow.Amount);
            row[2].Should().Be(expectedRow.Price);
            row[3].Should().Be(expectedRow.Description);
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
            column[0].Should().Be(rowOne.Id);
            column[1].Should().Be(rowTwo.Id);
            column[2].Should().Be(rowThree.Id);
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
