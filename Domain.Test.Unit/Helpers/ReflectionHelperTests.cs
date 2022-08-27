using DocumentBuilder.Domain.Helpers;
using DocumentBuilder.Domain.Test.Unit.TestHelpers;
using FluentAssertions;

namespace DocumentBuilder.Domain.Test.Unit.Helpers
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
            orderedTableRowPropertyInfos.Count().Should().Be(4);
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
            orderedTableRowPropertyInfos.Count().Should().Be(4);
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
            orderedTableRowPropertyInfos.Count().Should().Be(4);
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
            orderedTableRowPropertyInfos.Count().Should().Be(4);
            orderedTableRowPropertyInfos.ElementAt(0).Name.Should().Be("Id");
            orderedTableRowPropertyInfos.ElementAt(1).Name.Should().Be("Amount");
            orderedTableRowPropertyInfos.ElementAt(2).Name.Should().Be("Price");
            orderedTableRowPropertyInfos.ElementAt(3).Name.Should().Be("Description");
        }

        [TestMethod]
        public void GetOrderedTableRowPropertyInfos_IgnoreColumnAttributesPresent_DoesntIncludeIgnoredProperties()
        {
            // Arrange
            var productTableRowsWithHeaders = ExampleProductsGenerator.CreateTableRowsWithIgnoreColumnAttribute();

            // Act
            var orderedTableRowPropertyInfos = ReflectionHelper<ProductTableRowWithIgnoreColumnAttribute>.GetOrderedTableRowPropertyInfos(productTableRowsWithHeaders);

            // Assert
            orderedTableRowPropertyInfos.Count().Should().Be(2);
            orderedTableRowPropertyInfos.ElementAt(0).Name.Should().Be("Id");
            orderedTableRowPropertyInfos.ElementAt(1).Name.Should().Be("Price");
        }
    }
}
