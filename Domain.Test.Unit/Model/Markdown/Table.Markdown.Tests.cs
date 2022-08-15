using DocumentBuilder.Domain.Enumerations;
using DocumentBuilder.Domain.Factories;
using DocumentBuilder.Domain.Test.Unit.TestHelpers;
using FluentAssertions;
using DocumentBuilder.Domain.Model;
using DocumentBuilder.Domain.Options;

namespace DocumentBuilder.Domain.Test.Unit.Model.Markdown
{
    [TestClass]
    public class TableMarkdownTests : TableTestBase
    {
        private Table<ProductTableRowWithoutHeaders> _tableWithoutHeaderAttributes;

        [TestInitialize]
        public void TestInitialize()
        {
            _tableWithoutHeaderAttributes = new Table<ProductTableRowWithoutHeaders>(_productTableRowsWithoutHeaders);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment, Formatting.AlignColumns)]
        [DataRow(LineEndings.Linux, Formatting.AlignColumns)]
        [DataRow(LineEndings.Windows, Formatting.AlignColumns)]
        [DataRow(LineEndings.Environment, Formatting.None)]
        [DataRow(LineEndings.Linux, Formatting.None)]
        [DataRow(LineEndings.Windows, Formatting.None)]
        public async Task ToMarkdownAsync_CreatesFormattedTable(LineEndings LineEndings, Formatting formatting)
        {
            // Arrange
            var options = new MarkdownDocumentOptions
            {
                LineEndings = LineEndings,
                MarkdownTableOptions = new MarkdownTableOptions
                {
                    Formatting = formatting
                }
            };

            // Act
            var markdownTable = await _tableWithoutHeaderAttributes.ToMarkdownAsync(options);

            // Assert
            var expectedTable = ExampleProductMarkdownTableBuilder.BuildExpectedFormattedProductTable(options);
            markdownTable.Should().Be(expectedTable);
        }

        [DataTestMethod]
        [DataRow(Alignment.Left)]
        [DataRow(Alignment.Right)]
        [DataRow(Alignment.Center)]
        [DataRow(Alignment.None)]
        public async Task ToMarkdownAsync_ProvideAlignmentInColumnAttribute_CreatesAlignedColumn(Alignment alignment)
        {
            // Arrange
            var options = new MarkdownDocumentOptions();
            var alignedColumn = GetAlignedColumn(alignment);
            var alignedTable = new Table<AlignedColumn>(alignedColumn);

            // Act
            var table = await alignedTable.ToMarkdownAsync(options);

            // Assert
            var expectedTable = ExampleAlignedColumnBuilder.BuildExpectedAlignedColumn(alignment, options);
            table.Should().Be(expectedTable);
        }

        [DataTestMethod]
        [DataRow(Alignment.Left)]
        [DataRow(Alignment.Right)]
        [DataRow(Alignment.Center)]
        [DataRow(Alignment.None)]
        public async Task ToMarkdownAsync_ProvideAlignmentInOptions_CreatesAlignedColumn(Alignment alignment)
        {
            // Arrange
            var alignedColumn = GetAlignedColumn(Alignment.Default);
            var alignedTable = new Table<AlignedColumn>(alignedColumn);
            var options = new MarkdownDocumentOptions
            {
                MarkdownTableOptions = new MarkdownTableOptions
                {
                    DefaultAlignment = alignment
                }
            };

            // Act
            var table = await alignedTable.ToMarkdownAsync(options);

            // Assert
            var expectedTable = ExampleAlignedColumnBuilder.BuildExpectedAlignedColumn(alignment, options);
            table.Should().Be(expectedTable);
        }

        [TestMethod]
        public async Task ToMarkdownAsync_BoldColumnNames_CreatesTableWithBoldColumnNames()
        {
            // Arrange
            var alignedColumn = GetAlignedColumn(Alignment.Default);
            var alignedTable = new Table<AlignedColumn>(alignedColumn);
            var options = new MarkdownDocumentOptions
            {
                MarkdownTableOptions = new MarkdownTableOptions
                {
                    BoldColumnNames = true
                }
            };

            // Act
            var table = await alignedTable.ToMarkdownAsync(options);

            // Assert
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var expectedTable =
                 "| **ColumnName** |" + newLineProvider.GetNewLine() +
                $"| -------------- |" + newLineProvider.GetNewLine() +
                $"| ColumnValue    |" + newLineProvider.GetNewLine();
            table.Should().Be(expectedTable);
        }

        private static IEnumerable<AlignedColumn> GetAlignedColumn(Alignment alignment)
        {
            return alignment switch
            {
                Alignment.Center => new List<AlignedColumn>() { new AlignedCenterColumn() },
                Alignment.Left => new List<AlignedColumn>() { new AlignedLeftColumn() },
                Alignment.Right => new List<AlignedColumn>() { new AlignedRightColumn() },
                Alignment.None => new List<AlignedColumn>() { new AlignedNoneColumn() },
                Alignment.Default => new List<AlignedColumn>() { new AlignedDefaultColumn() },
                _ => throw new NotSupportedException($"{alignment} is currently not supported")
            };
        }
    }
}