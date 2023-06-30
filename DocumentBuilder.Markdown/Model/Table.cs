using DocumentBuilder.Exceptions;
using DocumentBuilder.Helpers;
using DocumentBuilder.Markdown.Extensions;
using DocumentBuilder.Markdown.Options;
using DocumentBuilder.Core.Attributes;
using DocumentBuilder.Core.Enumerations;
using DocumentBuilder.Core.Extensions;
using DocumentBuilder.Core.Model;
using DocumentBuilder.Utilities;
using System.Text;
using Alignment = DocumentBuilder.Core.Enumerations.Alignment;

namespace DocumentBuilder.Markdown.Model;

public class Table<TRow> : IMarkdownElement
{
    private const char _columnDivider = '|';
    private const char _rowDivider = '-';
    private const char _whiteSpace = ' ';
    private const char _alignmentChar = ':';
    private const int _minimumNumberOfDividerCharacters = 3;

    public INewLineProvider NewLineProvider { get; }
    public MarkdownTableOptions Options { get; }
    public IEnumerable<ColumnAttribute> OrderedColumnAttributes { get; }
    public Matrix<TRow> TableValues { get; }
    public IEnumerable<TableCell> TableCells { get; }

    public Table(IEnumerable<TRow> tableRows, MarkdownDocumentOptions options)
    {
        ValidateRows(tableRows);
        NewLineProvider = options.NewLineProvider;
        Options = options.MarkdownTableOptions;
        TableValues = new Matrix<TRow>(tableRows);
        OrderedColumnAttributes = ReflectionHelper<TRow>.GetOrderedColumnAttributes();
        TableCells = CreateEnumerableOfTableCells();
    }

    public string ToMarkdown(MarkdownDocumentOptions options)
    {
        var sb = new StringBuilder();
        CreateMarkdownTableHeaderAsync(sb);
        CreateMarkdownTableDividerAsync(sb);
        CreateMarkdownTableRowsAsync(sb);
        return sb.ToString();
    }

    private void CreateMarkdownTableHeaderAsync(StringBuilder sb)
    {
        sb.Append(_columnDivider);
        var numberOfColumns = TableValues.NumberOfColumns;

        for (var i = 0; i < numberOfColumns; i++)
        {
            var columnName = GetColumnName(i, Options.BoldColumnNames);
            var amountOfWhiteSpace = DetermineAmountOfWhiteSpace(columnName, i);
            CreateMarkdownTableCellAsync(sb, columnName, amountOfWhiteSpace);
        }

        sb.Append(NewLineProvider.GetNewLine());
    }

    private void CreateMarkdownTableDividerAsync(StringBuilder sb)
    {
        sb.Append(_columnDivider);
        var numberOfColumns = TableValues.NumberOfColumns;

        for (var i = 0; i < numberOfColumns; i++)
        {
            var columnAttribute = OrderedColumnAttributes.ElementAt(i);
            var numberOfDividerCellCharacters = GetNumberOfCharactersForDividerCell(i);
            var divider = new string(_rowDivider, numberOfDividerCellCharacters);
            var alignment = GetAlignment(columnAttribute);
            var alignedDivider = AddAlignment(divider, alignment);
            CreateMarkdownTableCellAsync(sb, alignedDivider, 0, whiteSpaceCharacter: _rowDivider);
        }

        sb.Append(NewLineProvider.GetNewLine());
    }

    private int GetNumberOfCharactersForDividerCell(int columnIndex)
    {
        var numberOfCharacters = GetLongestCellSizeForColumn(columnIndex, Options.BoldColumnNames);
        if (Options.Formatting == Formatting.None || numberOfCharacters < _minimumNumberOfDividerCharacters)
        {
            return _minimumNumberOfDividerCharacters;
        }
        return numberOfCharacters;
    }

    private Alignment GetAlignment(ColumnAttribute columnAttribute)
    {
        if (columnAttribute.Alignment == Alignment.Default)
        {
            return Options.DefaultAlignment;
        }

        return columnAttribute.Alignment;
    }

    private static string AddAlignment(string cellDividerValue, Alignment alignment)
    {
        if (alignment == Alignment.Left)
        {
            return cellDividerValue.ReplaceAt(0, _alignmentChar);
        }

        if (alignment == Alignment.Right)
        {
            return cellDividerValue.ReplaceAt(cellDividerValue.Length - 1, _alignmentChar);
        }

        if (alignment == Alignment.Center)
        {
            var alignedDivider = cellDividerValue.ReplaceAt(0, _alignmentChar);
            return alignedDivider.ReplaceAt(cellDividerValue.Length - 1, _alignmentChar);
        }

        return cellDividerValue;
    }

    private void CreateMarkdownTableRowsAsync(StringBuilder sb)
    {
        var numberOfRows = TableValues.NumberOfRows;

        for (var i = 0; i < numberOfRows; i++)
        {
            var currentRow = TableValues.GetRow(i);
            CreateMarkdownTableRowAsync(sb, currentRow);
            sb.Append(NewLineProvider.GetNewLine());
        }
    }

    private void CreateMarkdownTableRowAsync(StringBuilder sb, TableCell[] tableRow)
    {
        sb.Append(_columnDivider);
        for (var i = 0; i < tableRow.Length; i++)
        {
            var cellValue = tableRow[i].Value;
            var amountOfWhiteSpace = DetermineAmountOfWhiteSpace(cellValue, i);
            CreateMarkdownTableCellAsync(sb, cellValue, amountOfWhiteSpace);
        }
    }

    private void CreateMarkdownTableCellAsync(StringBuilder sb, string cellValue, int amountOfWhiteSpace, char whiteSpaceCharacter = ' ')
    {
        var whiteSpace = CreateRequiredWhiteSpace(amountOfWhiteSpace, whiteSpaceCharacter);
        sb.Append(_whiteSpace)
            .Append(cellValue)
            .Append(whiteSpace)
            .Append(_whiteSpace)
            .Append(_columnDivider);
    }

    private static string CreateRequiredWhiteSpace(int amountOfWhiteSpace, char whiteSpaceCharacter) => new(whiteSpaceCharacter, amountOfWhiteSpace);

    private int DetermineAmountOfWhiteSpace(string value, int currentColumnIndex)
    {
        var formatting = Options.Formatting;

        if (formatting == Formatting.AlignColumns)
        {
            var longestColumnCellSize = GetLongestCellSizeForColumn(currentColumnIndex);
            var amountOfCharactersToAdd = CorrectCellSizeBasedOnBoldOption(longestColumnCellSize, value.Length);

            if (longestColumnCellSize < _minimumNumberOfDividerCharacters)
            {
                return _minimumNumberOfDividerCharacters - value.Length;
            }

            return longestColumnCellSize + amountOfCharactersToAdd - value.Length;
        }

        if (formatting == Formatting.None)
        {
            return 0;
        }

        throw new NotSupportedException($"Formatting {Options.Formatting} is not supported");
    }

    private int GetLongestCellSizeForColumn(int columnIndex)
    {
        if (!Options.BoldColumnNames)
        {
            return GetLongestCellSizeForColumn(columnIndex, Options.BoldColumnNames);
        }

        var longestCellSizeForColumnValue = GetLongestCellSizeForColumn(columnIndex, Options.BoldColumnNames);
        var columnName = OrderedColumnAttributes.ElementAt(columnIndex);
        var boldColumnNameSize = columnName.Name.Value.Length + 4;
        return Math.Max(longestCellSizeForColumnValue, boldColumnNameSize);
    }

    private int CorrectCellSizeBasedOnBoldOption(int cellSize, int longestCellSize)
    {
        // When bold column names is enabled, and the cellsize is shorter than the longest cellsize, we need to align it.
        if (Options.BoldColumnNames && cellSize < longestCellSize)
        {
            return 4;
        }

        // In case of still having the longest cell size, we don't need to add extra space to align it.
        return 0;
    }

    private int GetLongestCellSizeForColumn(int columnIndex, bool isBold)
    {
        var longestTableValue = TableValues.GetLongestCellSizeOfColumn(columnIndex);
        var columnNameLength = OrderedColumnAttributes.ElementAt(columnIndex).Name.Value.Length;

        if (isBold)
        {
            columnNameLength += 4;
        }

        return Math.Max(longestTableValue, columnNameLength);
    }

    private string GetColumnName(int index, bool isBold)
    {
        var columnName = OrderedColumnAttributes.ElementAt(index).Name.Value;

        if (isBold)
        {
            return $"**{columnName}**";
        }

        return columnName;
    }

    private IEnumerable<TableCell> CreateEnumerableOfTableCells()
    {
        var columnTableCells = Enumerable.Empty<TableCell>();
        for (var i = 0; i < OrderedColumnAttributes.Count(); i++)
        {
            var currentOrderedColumnAttribute = OrderedColumnAttributes.ElementAt(i);
            var tableCell = new TableCell(
                currentOrderedColumnAttribute.Name.Value,
                currentOrderedColumnAttribute.Name.GetType(),
                0,
                i);
            columnTableCells = columnTableCells.Append(tableCell);
        }

        var shiftedTableCells = TableValues.TableCells.Select(t => t.ShiftRow());

        return columnTableCells.Concat(shiftedTableCells);
    }

    private static void ValidateRows(IEnumerable<TRow> tableRows)
    {
        var genericType = typeof(TRow);
        foreach (var tableRow in tableRows)
        {
            var tableRowType = tableRow?.GetType();
            if (tableRowType != genericType)
            {
                var message = $"The type {tableRowType} does not equal the provided generic parameter {genericType}, base types are not supported";
                throw new DocumentBuilderException(DocumentBuilderErrorCode.ProvidedGenericTypeForTableDoesNotEqualRunTimeType, message);
            }
        }
    }
}