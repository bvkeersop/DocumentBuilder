using FluentAssertions;
using NDocument.Domain.Helpers;
using NDocument.Domain.Test.Unit.TestHelpers;

namespace NDocument.Domain.Test.Unit.Helpers
{
    [TestClass]
    public class ReflectionHelperTests
    {
        [TestMethod]
        public void GetOrderedTableRowPropertyInfos_NoHeaders_ReturnedInOrderOfProperties()
        {
            // Arrange
            var productTableRowsWithHeaders = ExampleProductsGenerator.CreateTableRowsWithHeaders();

            // Act
            var orderedTableRowPropertyInfos = ReflectionHelper<ProductTableRowWithHeaders>.GetOrderedTableRowPropertyInfos(productTableRowsWithHeaders);

            // Assert
            orderedTableRowPropertyInfos.ElementAt(0).Name.Should().Be("Id");
            orderedTableRowPropertyInfos.ElementAt(1).Name.Should().Be("Amount");
            orderedTableRowPropertyInfos.ElementAt(2).Name.Should().Be("Price");
            orderedTableRowPropertyInfos.ElementAt(3).Name.Should().Be("Description");
        }

        [TestMethod]
        public void GetOrderedTableRowPropertyInfos_Headers_ReturnedInOrderOfCountPropertyOnHeader()
        {
            // Arrange
            var productTableRowsWithoutHeaders = ExampleProductsGenerator.CreateTableRowsWithoutHeaders();

            // Act
            var orderedTableRowPropertyInfos = ReflectionHelper<ProductTableRowWithoutHeaders>.GetOrderedTableRowPropertyInfos(productTableRowsWithoutHeaders);

            // Assert
            orderedTableRowPropertyInfos.ElementAt(0).Name.Should().Be("Id");
            orderedTableRowPropertyInfos.ElementAt(1).Name.Should().Be("Amount");
            orderedTableRowPropertyInfos.ElementAt(2).Name.Should().Be("Price");
            orderedTableRowPropertyInfos.ElementAt(3).Name.Should().Be("Description");
        }

        [TestMethod]
        public void GetOrderedColumnNames_NoHeaders_ReturnedInOrderOfProperties()
        {
            // Arrange
            var productTableRowsWithHeaders = ExampleProductsGenerator.CreateTableRowsWithHeaders();

            // Act
            var orderedTableRowPropertyInfos = ReflectionHelper<ProductTableRowWithHeaders>.GetOrderedTableRowPropertyInfos(productTableRowsWithHeaders);

            // Assert
            orderedTableRowPropertyInfos.ElementAt(0).Name.Should().Be("Id");
            orderedTableRowPropertyInfos.ElementAt(1).Name.Should().Be("Amount");
            orderedTableRowPropertyInfos.ElementAt(2).Name.Should().Be("Price");
            orderedTableRowPropertyInfos.ElementAt(3).Name.Should().Be("Description");
        }

        [TestMethod]
        public void GetOrderedColumnNames_Headers_ReturnedInOrderOfCountPropertyOnHeader()
        {
            // Arrange
            var productTableRowsWithoutHeaders = ExampleProductsGenerator.CreateTableRowsWithoutHeaders();

            // Act
            var orderedTableRowPropertyInfos = ReflectionHelper<ProductTableRowWithoutHeaders>.GetOrderedTableRowPropertyInfos(productTableRowsWithoutHeaders);

            // Assert
            orderedTableRowPropertyInfos.ElementAt(0).Name.Should().Be("Id");
            orderedTableRowPropertyInfos.ElementAt(1).Name.Should().Be("Amount");
            orderedTableRowPropertyInfos.ElementAt(2).Name.Should().Be("Price");
            orderedTableRowPropertyInfos.ElementAt(3).Name.Should().Be("Description");
        }
    }
}
