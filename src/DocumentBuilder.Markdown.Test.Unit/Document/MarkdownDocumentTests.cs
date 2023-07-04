using DocumentBuilder.Core.Enumerations;
using DocumentBuilder.Markdown.Constants;
using DocumentBuilder.Markdown.Document;
using DocumentBuilder.Markdown.Options;
using DocumentBuilder.Test.Unit.TestHelpers;
using FluentAssertions;

namespace DocumentBuilder.Markdown.Test.Unit.Document;

[TestClass]
public class MarkdownDocumentTests
{
    private const string _header1 = "Header1";
    private const string _header2 = "Header2";
    private const string _header3 = "Header3";
    private const string _header4 = "Header4";
    private const string _paragraph = "An interesting paragraph";
    private const string _imageName = "imageName";
    private const string _imagePath = "./imagePath";
    private const string _imageCaption = "this is an image";
    private const string _codeblock = "codeblock";
    private const string _blockquote = "blockquote";
    private const string _language = "C#";
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
    [DataRow(IndentationType.Spaces, LineEndings.Environment, 1)]
    [DataRow(IndentationType.Spaces, LineEndings.Windows, 2)]
    [DataRow(IndentationType.Spaces, LineEndings.Linux, 4)]
    [DataRow(IndentationType.Tabs, LineEndings.Environment, 1)]
    [DataRow(IndentationType.Tabs, LineEndings.Windows, 2)]
    [DataRow(IndentationType.Tabs, LineEndings.Linux, 4)]
    public async Task Save_CreatesTextRepresentation(IndentationType indentationType, LineEndings lineEndings, int indentationSize)
    {
        // Arrange
        var options = new MarkdownDocumentOptionsBuilder()
            .WithIndentationProvider(indentationType, indentationSize)
            .WithNewLineProvider(lineEndings)
            .WithMarkdownTableOptions(new MarkdownTableOptions())
            .Build();

        var outputStream = new MemoryStream();

        var markdownDocumentBuilder = new MarkdownDocumentBuilder(options)
            .AddHeader1(_header1)
            .AddHeader2(_header2)
            .AddHeader3(_header3)
            .AddHeader4(_header4)
            .AddParagraph(_paragraph)
            .AddUnorderedList(_unorderedList)
            .AddOrderedList(_orderedList)
            .AddTable(_productTableRowsWithoutAttributes)
            .AddImage(_imageName, _imagePath, _imageCaption)
            .AddHorizontalRule()
            .AddBlockquote(_blockquote)
            .AddFencedCodeblock(_codeblock, _language)
            .AddRaw(_raw);


        // Act
        var markdownDocument = markdownDocumentBuilder.Build();
        await markdownDocument.SaveAsync(outputStream);

        // Assert
        var expectedMarkdownDocument = GetExpectedMarkdownDocument(options);
        var markdownDocumentAsTextRepresentation = StreamHelper.GetStreamContents(outputStream);
        markdownDocumentAsTextRepresentation.Should().Be(expectedMarkdownDocument);
    }

    private string GetExpectedMarkdownDocument(MarkdownDocumentOptions options)
    {
        var newLineProvider = options.NewLineProvider;

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
            ExampleProductMarkdownTableBuilder.BuildExpectedFormattedProductTable(options) + newLineProvider.GetNewLine() +
            "![imageName](./imagePath)" + newLineProvider.GetNewLine() +
            "*this is an image*" + newLineProvider.GetNewLine() +
            newLineProvider.GetNewLine() +
            Indicators.HorizontalRule + newLineProvider.GetNewLine() +
            newLineProvider.GetNewLine() +
            $"> {_blockquote}" + newLineProvider.GetNewLine() +
            newLineProvider.GetNewLine() +
            $"```{_language}" + newLineProvider.GetNewLine() +
            $"{_codeblock}" + newLineProvider.GetNewLine() +
            $"```" + newLineProvider.GetNewLine() +
            newLineProvider.GetNewLine() +
            $"{_raw}" + newLineProvider.GetNewLine();
    }
}
