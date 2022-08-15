namespace DocumentBuilder.Domain.Test.Unit.TestHelpers
{
    internal static class ExampleProductsGenerator
    {
        static string _longestDescription = "Very long description with most characters";

        public static IEnumerable<ProductTableRowWithHeaders> CreateTableRowsWithHeaders()
        {
            var productOne = new ProductTableRowWithHeaders
            {
                Id = "1",
                Description = "Description 1",
                Amount = "1",
                Price = "1,11"
            };
            var productTwo = new ProductTableRowWithHeaders
            {
                Id = "2",
                Description = "Description 2",
                Amount = "2",
                Price = "2,22"
            };
            var productThree = new ProductTableRowWithHeaders
            {
                Id = "3",
                Description = _longestDescription,
                Amount = "3",
                Price = "3,33"
            };

            return new List<ProductTableRowWithHeaders>
            {
                productOne,
                productTwo,
                productThree
            };
        }

        public static IEnumerable<ProductTableRowWithoutHeaders> CreateTableRowsWithoutHeaders()
        {
            // Arrange
            var productOne = new ProductTableRowWithoutHeaders
            {
                Id = "1",
                Amount = "1",
                Price = "1,11",
                Description = "Description 1",
            };
            var productTwo = new ProductTableRowWithoutHeaders
            {
                Id = "2",
                Amount = "2",
                Price = "2,22",
                Description = "Description 2",
            };
            var productThree = new ProductTableRowWithoutHeaders
            {
                Id = "3",
                Amount = "3",
                Price = "3,33",
                Description = _longestDescription,
            };

            return new List<ProductTableRowWithoutHeaders>
            {
                productOne,
                productTwo,
                productThree
            };
        }
    }
}
