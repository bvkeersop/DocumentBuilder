using DocumentBuilder.Core.Enumerations;
using DocumentBuilder.Html.Document;
using DocumentBuilder.Html.Options;
using DocumentBuilder.Html.Test.Unit.TestHelpers;
using DocumentBuilder.Test.Unit.Base;
using DocumentBuilder.Test.Unit.TestHelpers;
using FluentAssertions;

namespace DocumentBuilder.Html.Test.Unit.Document;

[TestClass]
public class HtmlDocumentTests : TestBase
{
    private const string _header1 = "Header1";
    private const string _header2 = "Header2";
    private const string _header3 = "Header3";
    private const string _header4 = "Header4";
    private const string _paragraph = "An interesting paragraph";
    private const string _imageName = "imageName";
    private const string _imagePath = "./imagePath";
    private const string _imageCaption = "this is an image";
    private const string _raw = "raw";
    private IEnumerable<ProductTableRowWithColumnAttribute> _productTableRowsWithColumnAttribute;
    private IEnumerable<ProductTableRowWithoutAttributes> _productTableRowsWithoutAttributes;
    private List<string> _orderedList;
    private List<string> _unorderedList;

    [TestInitialize]
    public void TestInitialize()
    {
        _productTableRowsWithColumnAttribute = ExampleProductsGenerator.CreateTableRowsWithColumnAttribute();
        _productTableRowsWithoutAttributes = ExampleProductsGenerator.CreateTableRowsWithoutAttributes();
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

    [TestMethod]
    public async Task Build_CreatesHtmlDocument(LineEndings lineEndings, IndentationType indentationType, int indentationSize)
    {
        // Arrange
        var options = new HtmlDocumentOptionsBuilder()
            .WithNewLineProvider(lineEndings)
            .WithIndentationProvider(indentationType, indentationSize)
            .Build();

        var outputStream = new MemoryStream();

        var htmlDocumentBuilder = new HtmlDocumentBuilder(options)
            .AddHeader1(_header1)
            .AddHeader2(_header2)
            .AddHeader3(_header3)
            .AddHeader4(_header4)
            .AddParagraph(_paragraph)
            .AddUnorderedList(_unorderedList)
            .AddOrderedList(_orderedList)
            .AddTable(_productTableRowsWithoutAttributes)
            .AddFigure(_imageName, _imagePath, _imageCaption)
            .AddRaw(_raw);

        // Act
        var htmlDocument = htmlDocumentBuilder.Build();
        await htmlDocument.SaveAsync(outputStream);

        // Assert
        var expectedHtmlDocument = GetExpectedHtmlDocument(options);
        var htmlDocumentAsTextRepresentation = StreamHelper.GetStreamContents(outputStream);
        htmlDocumentAsTextRepresentation.Should().Be(expectedHtmlDocument);
    }

    protected string GetExpectedHtmlDocument(HtmlDocumentOptions options)
    {
        var newLineProvider = options.NewLineProvider;
        var indentationProvider = options.IndentationProvider;

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
                    ExampleProductHtmlTableBuilder.BuildExpectedProductTable(options) + GetIndentation(indentationProvider, 2) +
                    "<figure>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 3) +
                        $"<img src=\"{_imagePath}\" alt=\"{_imageName}\" />" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 3) +
                        $"<figcaption>{_imageCaption}</figcaption>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                    $"</figure>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                    $"{_raw}" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 1) +
                "</body>" + GetNewLine(newLineProvider) +
            "</html>" + GetNewLine(newLineProvider);
    }
}
