using Domain.Enumerations;
using Domain.Model;
using Domain.Options;
using FluentAssertions;
using Markdown.Test.Unit.Builders;
using NTable.Markdown.Options;
using Test.Helpers;

namespace Domain.Test.Unit.Model
{
    [TestClass]
    public partial class TableHtmlTests : TestBase
    {
        private Table<ProductTableRowWithHeaders> _table;

        [TestInitialize]
        public void TestInitialize()
        {
            _table = new Table<ProductTableRowWithHeaders>(_productTableRowsWithHeaders);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment, Formatting.AlignColumns, false)]
        public async Task CreateTable_CreatesFormattedTable(LineEndings LineEndings, Formatting formatting, bool boldColumnNames)
        {
            // Arrange
            var options = new MarkdownDocumentOptions
            {
                LineEndings = LineEndings,
                MarkdownTableOptions = new MarkdownTableOptions
                {
                    Formatting = formatting,
                    BoldColumnNames = boldColumnNames
                }
            };

            // Act
            var markdownTable = await _table.ToMarkdownAsync(options);

            // Assert
            var expectedTable = ExampleProductMarkdownTableBuilder.BuildExpectedProductTable(options);
            markdownTable.Should().Be(expectedTable);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment, Formatting.AlignColumns, false)]
        public async Task CreateTable_ProvideOutputStream_CreatesFormattedTable(LineEndings LineEndings, Formatting formatting, bool boldColumnNames)
        {
            // Arrange
            var outputStream = new MemoryStream();

            var options = new MarkdownDocumentOptions
            {
                LineEndings = LineEndings,
                MarkdownTableOptions = new MarkdownTableOptions
                {
                    Formatting = formatting,
                    BoldColumnNames = boldColumnNames
                }
            };

            // Act
            await _table.WriteAsMarkdownToStreamAsync(outputStream, options);

            // Assert
            var table = StreamHelper.GetStreamContents(outputStream);
            var expectedTable = ExampleProductMarkdownTableBuilder.BuildExpectedProductTable(options);
            table.Should().Be(expectedTable);
        }
    }
}