using DocumentBuilder.Helpers;
using DocumentBuilder.Test.Unit.TestHelpers;
using FluentAssertions;

namespace DocumentBuilder.Test.Unit.Helpers
{
    [TestClass]
    public class ReflectionHelperTests
    {
        [TestMethod]
        public void GetOrderedTableRowPropertyInfos_NoHeaders_ReturnedInOrderOfProperties()
        {
            // Arrange
            var productTableRowsWithColumnAttribute = ExampleProductsGenerator.CreateTableRowsWithColumnAttribute();

            // Act
            var orderedTableRowPropertyInfos = ReflectionHelper<ProductTableRowWithColumnAttribute>.GetOrderedTableRowPropertyInfos(productTableRowsWithColumnAttribute);

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
            var productTableRowsWithoutAttributes = ExampleProductsGenerator.CreateTableRowsWithoutAttributes();

            // Act
            var orderedTableRowPropertyInfos = ReflectionHelper<ProductTableRowWithoutAttributes>.GetOrderedTableRowPropertyInfos(productTableRowsWithoutAttributes);

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
            var productTableRowsWithColumnAttribute = ExampleProductsGenerator.CreateTableRowsWithColumnAttribute();

            // Act
            var orderedTableRowPropertyInfos = ReflectionHelper<ProductTableRowWithColumnAttribute>.GetOrderedTableRowPropertyInfos(productTableRowsWithColumnAttribute);

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
            var productTableRowsWithoutAttributes = ExampleProductsGenerator.CreateTableRowsWithoutAttributes();

            // Act
            var orderedTableRowPropertyInfos = ReflectionHelper<ProductTableRowWithoutAttributes>.GetOrderedTableRowPropertyInfos(productTableRowsWithoutAttributes);

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
            var productTableRowsWithIgnoreAttribute = ExampleProductsGenerator.CreateTableRowsWithIgnoreColumnAttribute();

            // Act
            var orderedTableRowPropertyInfos = ReflectionHelper<ProductTableRowWithIgnoreColumnAttribute>.GetOrderedTableRowPropertyInfos(productTableRowsWithIgnoreAttribute);

            // Assert
            orderedTableRowPropertyInfos.Count().Should().Be(2);
            orderedTableRowPropertyInfos.ElementAt(0).Name.Should().Be("Id");
            orderedTableRowPropertyInfos.ElementAt(1).Name.Should().Be("Price");
        }
    }
}
