using DocumentBuilder.Enumerations;
using DocumentBuilder.Factories;
using DocumentBuilder.Test.Unit.TestHelpers;
using FluentAssertions;
using DocumentBuilder.Model;
using DocumentBuilder.Options;

namespace DocumentBuilder.Test.Unit.Model.Markdown
{
    [TestClass]
    public class TableMarkdownTests : TableTestBase
    {
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