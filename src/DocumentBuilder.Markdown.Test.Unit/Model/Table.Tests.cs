using DocumentBuilder.Core.Enumerations;
using DocumentBuilder.Factories;
using DocumentBuilder.Markdown.Model;
using DocumentBuilder.Markdown.Options;
using DocumentBuilder.Test.Unit.TestHelpers;
using FluentAssertions;

namespace DocumentBuilder.Test.Unit.Model.Markdown
{
    [TestClass]
    public class TableMarkdownTests
    {
        [TestMethod]
        public void ToMarkdownAsync_FormattingAligned_CreatesFormattedTable(Formatting formatting)
        {
            // Arrange
            var options = new MarkdownDocumentOptions();
            var productTableRowsWithoutAttributes = ExampleProductsGenerator.CreateTableRowsWithoutAttributes();
            var tableWithoutHeaderAttributes = new Table<ProductTableRowWithoutAttributes>(productTableRowsWithoutAttributes, options);


            // Act
            var markdownTable = tableWithoutHeaderAttributes.ToMarkdown(options);

            // Assert
            var nl = options.NewLineProvider.GetNewLine();
            var expectedTable =
                "| Id  | Amount | Price | Description                                |" + nl +
                "| --- | ------ | ----- | ------------------------------------------ |" + nl +
                "| 1   | 1      | 1,11  | Description 1                              |" + nl +
                "| 2   | 2      | 2,22  | Description 2                              |" + nl +
                "| 3   | 3      | 3,33  | Very long description with most characters |" + nl;

            markdownTable.Should().Be(expectedTable);
        }

        [TestMethod]
        public static async Task ToMarkdownAsync_ProvideAlignmentLeftInColumnAttribute_CreatesAlignedColumn()
        {
            // Arrange
            var options = new MarkdownDocumentOptions();
            var alignedColumn = new List<AlignedLeftColumn> { new AlignedLeftColumn() };
            var alignedTable = new Table<AlignedLeftColumn>(alignedColumn);

            // Act
            var table = await alignedTable.ToMarkdownAsync(options);

            // Assert
            var expectedTable = ExampleAlignedColumnBuilder.BuildExpectedAlignedColumn(Alignment.Left, options);
            table.Should().Be(expectedTable);
        }


        [TestMethod]
        public static async Task ToMarkdownAsync_ProvideAlignmentRightInColumnAttribute_CreatesAlignedColumn()
        {
            // Arrange
            var options = new MarkdownDocumentOptions();
            var alignedColumn = new List<AlignedRightColumn> { new AlignedRightColumn() };
            var alignedTable = new Table<AlignedRightColumn>(alignedColumn);

            // Act
            var table = await alignedTable.ToMarkdownAsync(options);

            // Assert
            var expectedTable = ExampleAlignedColumnBuilder.BuildExpectedAlignedColumn(Alignment.Right, options);
            table.Should().Be(expectedTable);
        }


        [TestMethod]
        public static async Task ToMarkdownAsync_ProvideAlignmentCenterInColumnAttribute_CreatesAlignedColumn()
        {
            // Arrange
            var options = new MarkdownDocumentOptions();
            var alignedColumn = new List<AlignedCenterColumn> { new AlignedCenterColumn() };
            var alignedTable = new Table<AlignedCenterColumn>(alignedColumn);

            // Act
            var table = await alignedTable.ToMarkdownAsync(options);

            // Assert
            var expectedTable = ExampleAlignedColumnBuilder.BuildExpectedAlignedColumn(Alignment.Center, options);
            table.Should().Be(expectedTable);
        }


        [TestMethod]
        public static async Task ToMarkdownAsync_ProvideAlignmentNoneInColumnAttribute_CreatesAlignedColumn()
        {
            // Arrange
            var options = new MarkdownDocumentOptions();
            var alignedColumn = new List<AlignedNoneColumn> { new AlignedNoneColumn() };
            var alignedTable = new Table<AlignedNoneColumn>(alignedColumn);

            // Act
            var table = await alignedTable.ToMarkdownAsync(options);

            // Assert
            var expectedTable = ExampleAlignedColumnBuilder.BuildExpectedAlignedColumn(Alignment.None, options);
            table.Should().Be(expectedTable);
        }



        [TestMethod]
        public static async Task ToMarkdownAsync_ProvideAlignmentLeftInOptions_CreatesAlignedColumn()
        {
            // Arrange
            var alignedColumn = new List<AlignedDefaultColumn> { new AlignedDefaultColumn() };
            var alignedTable = new Table<AlignedDefaultColumn>(alignedColumn);
            var options = new MarkdownDocumentOptions
            {
                MarkdownTableOptions = new MarkdownTableOptions
                {
                    DefaultAlignment = Alignment.Left,
                }
            };

            // Act
            var table = await alignedTable.ToMarkdownAsync(options);

            // Assert
            var expectedTable = ExampleAlignedColumnBuilder.BuildExpectedAlignedColumn(Alignment.Left, options);
            table.Should().Be(expectedTable);
        }


        [TestMethod]
        public static async Task ToMarkdownAsync_ProvideAlignmentRightInOptions_CreatesAlignedColumn(Alignment alignment)
        {
            // Arrange
            var alignedColumn = new List<AlignedDefaultColumn> { new AlignedDefaultColumn() };
            var alignedTable = new Table<AlignedDefaultColumn>(alignedColumn);
            var options = new MarkdownDocumentOptions
            {
                MarkdownTableOptions = new MarkdownTableOptions
                {
                    DefaultAlignment = Alignment.Right,
                }
            };

            // Act
            var table = await alignedTable.ToMarkdownAsync(options);

            // Assert
            var expectedTable = ExampleAlignedColumnBuilder.BuildExpectedAlignedColumn(Alignment.Right, options);
            table.Should().Be(expectedTable);
        }



        [TestMethod]
        public static async Task ToMarkdownAsync_ProvideAlignmentCenterInOptions_CreatesAlignedColumn(Alignment alignment)
        {
            // Arrange
            var alignedColumn = new List<AlignedDefaultColumn> { new AlignedDefaultColumn() };
            var alignedTable = new Table<AlignedDefaultColumn>(alignedColumn);
            var options = new MarkdownDocumentOptions
            {
                MarkdownTableOptions = new MarkdownTableOptions
                {
                    DefaultAlignment = Alignment.Center,
                }
            };

            // Act
            var table = await alignedTable.ToMarkdownAsync(options);

            // Assert
            var expectedTable = ExampleAlignedColumnBuilder.BuildExpectedAlignedColumn(Alignment.Center, options);
            table.Should().Be(expectedTable);
        }



        [TestMethod]
        public static async Task ToMarkdownAsync_ProvideAlignmentNoneInOptions_CreatesAlignedColumn()
        {
            // Arrange
            var alignedColumn = new List<AlignedDefaultColumn> { new AlignedDefaultColumn() };
            var alignedTable = new Table<AlignedDefaultColumn>(alignedColumn);
            var options = new MarkdownDocumentOptions
            {
                MarkdownTableOptions = new MarkdownTableOptions
                {
                    DefaultAlignment = Alignment.None,
                }
            };

            // Act
            var table = await alignedTable.ToMarkdownAsync(options);

            // Assert
            var expectedTable = ExampleAlignedColumnBuilder.BuildExpectedAlignedColumn(Alignment.None, options);
            table.Should().Be(expectedTable);
        }

        [TestMethod]
        public async Task ToMarkdownAsync_BoldColumnNames_CreatesTableWithBoldColumnNames()
        {
            // Arrange
            var alignedColumn = new List<AlignedDefaultColumn> { new AlignedDefaultColumn() };
            var alignedTable = new Table<AlignedDefaultColumn>(alignedColumn);
            var options = new MarkdownDocumentOptions
            {
                MarkdownTableOptions = new MarkdownTableOptions
                {
                    BoldColumnNames = true,
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
    }
}