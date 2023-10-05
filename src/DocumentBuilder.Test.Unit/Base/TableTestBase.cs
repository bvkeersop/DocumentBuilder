using DocumentBuilder.Core.Model;
using DocumentBuilder.Test.Unit.TestHelpers;

namespace DocumentBuilder.Test.Unit.Base
{
    public class TableTestBase<TTable>
    {
        protected const string _longestDescription = "Very long description with most characters";
        protected IEnumerable<ProductTableRowWithColumnAttribute> _productTableRowsWithColumnAttribute;
        protected IEnumerable<ProductTableRowWithoutAttributes> _productTableRowsWithoutAttributes;
        protected TableBase<ProductTableRowWithoutAttributes> _tableWithoutHeaderAttributes;

        [TestInitialize]
        public void TableTestBaseInitialize()
        {
            _productTableRowsWithColumnAttribute = ExampleProductsGenerator.CreateTableRowsWithColumnAttribute();
            _productTableRowsWithoutAttributes = ExampleProductsGenerator.CreateTableRowsWithoutAttributes();
            _tableWithoutHeaderAttributes = new TableBase<ProductTableRowWithoutAttributes>(_productTableRowsWithoutAttributes);
        }
    }
}
