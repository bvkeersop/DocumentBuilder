namespace DocumentBuilder.Test.Unit.TestHelpers;

public static class ExampleProductsGenerator
{
    static readonly string _longestDescription = "Very long description with most characters";

    public static IEnumerable<ProductTableRowWithColumnAttribute> CreateTableRowsWithColumnAttribute()
    {
        var productOne = new ProductTableRowWithColumnAttribute
        {
            Id = "1",
            Description = "Description 1",
            Amount = "1",
            Price = "1,11"
        };
        var productTwo = new ProductTableRowWithColumnAttribute
        {
            Id = "2",
            Description = "Description 2",
            Amount = "2",
            Price = "2,22"
        };
        var productThree = new ProductTableRowWithColumnAttribute
        {
            Id = "3",
            Description = _longestDescription,
            Amount = "3",
            Price = "3,33"
        };

        return new List<ProductTableRowWithColumnAttribute>
        {
            productOne,
            productTwo,
            productThree
        };
    }

    public static IEnumerable<ProductTableRowWithoutAttributes> CreateTableRowsWithoutAttributes()
    {
        // Arrange
        var productOne = new ProductTableRowWithoutAttributes
        {
            Id = "1",
            Amount = "1",
            Price = "1,11",
            Description = "Description 1",
        };
        var productTwo = new ProductTableRowWithoutAttributes
        {
            Id = "2",
            Amount = "2",
            Price = "2,22",
            Description = "Description 2",
        };
        var productThree = new ProductTableRowWithoutAttributes
        {
            Id = "3",
            Amount = "3",
            Price = "3,33",
            Description = _longestDescription,
        };

        return new List<ProductTableRowWithoutAttributes>
        {
            productOne,
            productTwo,
            productThree
        };
    }

    public static IEnumerable<ProductTableRowWithIgnoreColumnAttribute> CreateTableRowsWithIgnoreColumnAttribute()
    {
        // Arrange
        var productOne = new ProductTableRowWithIgnoreColumnAttribute
        {
            Id = "1",
            Amount = "1",
            Price = "1,11",
            Description = "Description 1",
        };
        var productTwo = new ProductTableRowWithIgnoreColumnAttribute
        {
            Id = "2",
            Amount = "2",
            Price = "2,22",
            Description = "Description 2",
        };
        var productThree = new ProductTableRowWithIgnoreColumnAttribute
        {
            Id = "3",
            Amount = "3",
            Price = "3,33",
            Description = _longestDescription,
        };

        return new List<ProductTableRowWithIgnoreColumnAttribute>
        {
            productOne,
            productTwo,
            productThree
        };
    }
}
