using DocumentBuilder.Exceptions;
using DocumentBuilder.Helpers;
using DocumentBuilder.Markdown.Extensions;
using DocumentBuilder.Markdown.Options;
using DocumentBuilder.Shared.Attributes;
using DocumentBuilder.Shared.Enumerations;
using DocumentBuilder.Shared.Extensions;
using DocumentBuilder.Shared.Model;
using DocumentBuilder.Utilities;
using System.Text;
using Alignment = DocumentBuilder.Shared.Enumerations.Alignment;

namespace DocumentBuilder.Markdown.Model;

public class Table<TRow> : IMarkdownElement
{
    private const char _columnDivider = '|';
    private const char _rowDivider = '-';
    private const char _whiteSpace = ' ';
    private const char _alignmentChar = ':';
    private const int _minimumNumberOfDividerCharacters = 3;

    private INewLineProvider _newLineProvider;
    private MarkdownTableOptions _options;

    public IEnumerable<ColumnAttribute> OrderedColumnAttributes { get; }
    public Matrix<TRow> TableValues { get; }
    public IEnumerable<TableCell> TableCells { get; }

    public Table(IEnumerable<TRow> tableRows)
    {
        ValidateRows(tableRows);
        TableValues = new Matrix<TRow>(tableRows);
        OrderedColumnAttributes = ReflectionHelper<TRow>.GetOrderedColumnAttributes();
        TableCells = CreateEnumerableOfTableCells();
    }

    public async ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
    {
        _newLineProvider = options.NewLineProvider;
        _options = options.MarkdownTableOptions;
        return await CreateMarkdownTableAsync().ConfigureAwait(false);
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

    private async Task<string> CreateMarkdownTableAsync()
    {
        var outputStream = new MemoryStream();
        await CreateMarkdownTableAsync(outputStream);
        return StreamHelper.GetStreamContents(outputStream);
    }

    private async Task CreateMarkdownTableAsync(Stream outputStream)
    {
        if (!outputStream.CanWrite)
        {
            throw new DocumentBuilderException(DocumentBuilderErrorCode.StreamIsNotWriteable, nameof(outputStream));
        }

        var streamWriter = new StreamWriter(outputStream, leaveOpen: true);
        using var markdownStreamWriter = new MarkdownStreamWriter(streamWriter, _newLineProvider);
        await CreateMarkdownTableHeaderAsync(markdownStreamWriter).ConfigureAwait(false);
        await CreateMarkdownTableDividerAsync(markdownStreamWriter).ConfigureAwait(false);
        await CreateMarkdownTableRowsAsync(markdownStreamWriter).ConfigureAwait(false);
        await markdownStreamWriter.FlushAsync().ConfigureAwait(false);
    }

    private async Task CreateMarkdownTableHeaderAsync(IMarkdownStreamWriter markdownStreamWriter)
    {
        await markdownStreamWriter.WriteAsync(_columnDivider).ConfigureAwait(false);
        var numberOfColumns = TableValues.NumberOfColumns;

        for (var i = 0; i < numberOfColumns; i++)
        {
            var columnName = GetColumnName(i, _options.BoldColumnNames);
            var amountOfWhiteSpace = DetermineAmountOfWhiteSpace(columnName, i);
            await CreateMarkdownTableCellAsync(markdownStreamWriter, columnName, amountOfWhiteSpace);
        }

        await markdownStreamWriter.WriteNewLineAsync();
    }

    private async Task CreateMarkdownTableDividerAsync(IMarkdownStreamWriter markdownStreamWriter)
    {
        await markdownStreamWriter.WriteAsync(_columnDivider).ConfigureAwait(false);
        var numberOfColumns = TableValues.NumberOfColumns;

        for (var i = 0; i < numberOfColumns; i++)
        {
            var columnAttribute = OrderedColumnAttributes.ElementAt(i);
            var numberOfDividerCellCharacters = GetNumberOfCharactersForDividerCell(i);
            var divider = new string(_rowDivider, numberOfDividerCellCharacters);
            var alignment = GetAlignment(columnAttribute);
            var alignedDivider = AddAlignment(divider, alignment);
            await CreateMarkdownTableCellAsync(markdownStreamWriter, alignedDivider, 0, whiteSpaceCharacter: _rowDivider);
        }

        await markdownStreamWriter.WriteNewLineAsync();
    }

    private int GetNumberOfCharactersForDividerCell(int columnIndex)
    {
        var numberOfCharacters = GetLongestCellSizeForColumn(columnIndex, _options.BoldColumnNames);
        if (_options.Formatting == Formatting.None || numberOfCharacters < _minimumNumberOfDividerCharacters)
        {
            return _minimumNumberOfDividerCharacters;
        }
        return numberOfCharacters;
    }

    private Alignment GetAlignment(ColumnAttribute columnAttribute)
    {
        if (columnAttribute.Alignment == Alignment.Default)
        {
            return _options.DefaultAlignment;
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

    private async Task CreateMarkdownTableRowsAsync(IMarkdownStreamWriter markdownStreamWriter)
    {
        var numberOfRows = TableValues.NumberOfRows;

        for (var i = 0; i < numberOfRows; i++)
        {
            var currentRow = TableValues.GetRow(i);
            await CreateMarkdownTableRowAsync(markdownStreamWriter, currentRow);
            await markdownStreamWriter.WriteNewLineAsync();
        }
    }

    private async Task CreateMarkdownTableRowAsync(IMarkdownStreamWriter markdownStreamWriter, TableCell[] tableRow)
    {
        await markdownStreamWriter.WriteAsync(_columnDivider).ConfigureAwait(false);
        for (var i = 0; i < tableRow.Length; i++)
        {
            var cellValue = tableRow[i].Value;
            var amountOfWhiteSpace = DetermineAmountOfWhiteSpace(cellValue, i);
            await CreateMarkdownTableCellAsync(markdownStreamWriter, cellValue, amountOfWhiteSpace);
        }
    }

    private static async Task CreateMarkdownTableCellAsync(IMarkdownStreamWriter markdownStreamWriter, string cellValue, int amountOfWhiteSpace, char whiteSpaceCharacter = ' ')
    {
        var whiteSpace = CreateRequiredWhiteSpace(amountOfWhiteSpace, whiteSpaceCharacter);
        var stringBuilder = new StringBuilder();
        stringBuilder
            .Append(_whiteSpace)
            .Append(cellValue)
            .Append(whiteSpace)
            .Append(_whiteSpace)
            .Append(_columnDivider);
        await markdownStreamWriter.WriteAsync(stringBuilder.ToString());
    }

    private static string CreateRequiredWhiteSpace(int amountOfWhiteSpace, char whiteSpaceCharacter)
    {
        return new string(whiteSpaceCharacter, amountOfWhiteSpace);
    }

    private int DetermineAmountOfWhiteSpace(string value, int currentColumnIndex)
    {
        var formatting = _options.Formatting;

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

        throw new NotSupportedException($"Formatting {_options.Formatting} is not supported");
    }

    private int GetLongestCellSizeForColumn(int columnIndex)
    {
        if (!_options.BoldColumnNames)
        {
            return GetLongestCellSizeForColumn(columnIndex, _options.BoldColumnNames);
        }

        var longestCellSizeForColumnValue = GetLongestCellSizeForColumn(columnIndex, _options.BoldColumnNames);
        var columnName = OrderedColumnAttributes.ElementAt(columnIndex);
        var boldColumnNameSize = columnName.Name.Value.Length + 4;
        return Math.Max(longestCellSizeForColumnValue, boldColumnNameSize);
    }

    private int CorrectCellSizeBasedOnBoldOption(int cellSize, int longestCellSize)
    {
        // When bold column names is enabled, and the cellsize is shorter than the longest cellsize, we need to align it.
        if (_options.BoldColumnNames && cellSize < longestCellSize)
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
}