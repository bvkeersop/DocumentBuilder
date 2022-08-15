using DocumentBuilder.Domain.Factories;
using DocumentBuilder.Domain.Options;
using DocumentBuilder.Domain.Test.Unit.Model;
using DocumentBuilder.Domain.Test.Unit.TestHelpers;

namespace DocumentBuilder.Domain.Test.Unit.Builders
{
    [TestClass]
    public abstract class BuilderTestBase : TestBase
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

        protected string GetExpectedHtmlDocument(HtmlDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize);

            return
                "<!DOCTYPE html>" + GetNewLine(newLineProvider) +
                "<html>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 1) +
                    "<body>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        $"<h1>{_header1}</h1>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        $"<h2>{_header2}</h2>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        $"<h3>{_header3}</h3>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        $"<h4>{_header4}</h4>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        $"<p>{_paragraph}</p>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<ul>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 3) +
                            $"<li>{_unorderedList.ElementAt(0)}</li>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 3) +
                            $"<li>{_unorderedList.ElementAt(1)}</li>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 3) +
                            $"<li>{_unorderedList.ElementAt(2)}</li>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "</ul>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<ol>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 3) +
                            $"<li>{_orderedList.ElementAt(0)}</li>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 3) +
                            $"<li>{_orderedList.ElementAt(1)}</li>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 3) +
                            $"<li>{_orderedList.ElementAt(2)}</li>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "</ol>" + GetNewLine(newLineProvider) +
                        ExampleProductHtmlTableBuilder.BuildExpectedProductTable(options, 2) + GetIndentation(indentationProvider, 1) +
                    "</body>" + GetNewLine(newLineProvider) +
                "</html>" + GetNewLine(newLineProvider);
        }

        protected string GetExpectedMarkdownDocument(MarkdownDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);

            return
                $"# {_header1}" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                $"## {_header2}" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                $"### {_header3}" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                $"#### {_header4}" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                _paragraph + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                $"- {_unorderedList.ElementAt(0)}" + newLineProvider.GetNewLine() +
                $"- {_unorderedList.ElementAt(1)}" + newLineProvider.GetNewLine() +
                $"- {_unorderedList.ElementAt(2)}" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                $"1. {_orderedList.ElementAt(0)}" + newLineProvider.GetNewLine() +
                $"1. {_orderedList.ElementAt(1)}" + newLineProvider.GetNewLine() +
                $"1. {_orderedList.ElementAt(2)}" + newLineProvider.GetNewLine() +
                newLineProvider.GetNewLine() +
                ExampleProductMarkdownTableBuilder.BuildExpectedFormattedProductTable(options);
        }
    }
}
