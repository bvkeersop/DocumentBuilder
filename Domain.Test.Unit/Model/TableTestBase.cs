using DocumentBuilder.Model;
using DocumentBuilder.Test.Unit.TestHelpers;

namespace DocumentBuilder.Test.Unit.Model
{
    public class TableTestBase
    {
        protected const string _longestDescription = "Very long description with most characters";
        protected IEnumerable<ProductTableRowWithColumnAttribute> _productTableRowsWithColumnAttribute;
        protected IEnumerable<ProductTableRowWithoutAttributes> _productTableRowsWithoutAttributes;
        protected Table<ProductTableRowWithoutAttributes> _tableWithoutHeaderAttributes;

        [TestInitialize]
        public void TableTestBaseInitialize()
        {
            _productTableRowsWithColumnAttribute = ExampleProductsGenerator.CreateTableRowsWithColumnAttribute();
            _productTableRowsWithoutAttributes = ExampleProductsGenerator.CreateTableRowsWithoutAttributes();
            _tableWithoutHeaderAttributes = new Table<ProductTableRowWithoutAttributes>(_productTableRowsWithoutAttributes);
        }
    }
}
