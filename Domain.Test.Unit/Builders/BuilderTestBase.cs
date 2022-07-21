using NDocument.Domain.Test.Unit.TestHelpers;

namespace NDocument.Domain.Test.Unit.Builders
{
    [TestClass]
    public abstract class BuilderTestBase
    {
        protected const string _header1 = "Header1";
        protected const string _header2 = "Header2";
        protected const string _header3 = "Header3";
        protected const string _header4 = "Header4";
        protected const string _paragraph = "An interesting paragraph";
        protected IEnumerable<ProductTableRowWithHeaders> _productTableRowsWithHeaders;
        protected IEnumerable<ProductTableRowWithoutHeaders> _productTableRowsWithoutHeaders;
        protected List<string> _orderedList;
        protected List<string> _unorderedList;

        [TestInitialize]
        public void TestBaseInitialize()
        {
            _productTableRowsWithHeaders = ExampleProductsGenerator.CreateTableRowsWithHeaders();
            _productTableRowsWithoutHeaders = ExampleProductsGenerator.CreateTableRowsWithoutHeaders();

            _orderedList = new List<string>
            {
                "an",
                "ordered",
                "list"
            };

            _unorderedList = new List<string>
            {
                "an",
                "unordered",
                "list"
            };
        }
    }
}
