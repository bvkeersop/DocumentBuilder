using NDocument.Domain.Test.Unit.TestHelpers;

namespace NDocument.Domain.Test.Unit.Model
{
    public class TableTestBase
    {
        protected const string _longestDescription = "Very long description with most characters";
        protected IEnumerable<ProductTableRowWithHeaders> _productTableRowsWithHeaders;
        protected IEnumerable<ProductTableRowWithoutHeaders> _productTableRowsWithoutHeaders;

        [TestInitialize]
        public void TableTestBaseInitialize()
        {
            _productTableRowsWithHeaders = ExampleProductsGenerator.CreateTableRowsWithHeaders();
            _productTableRowsWithoutHeaders = ExampleProductsGenerator.CreateTableRowsWithoutHeaders();
        }
    }
}
