using NDocument.Domain.Attributes;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Exceptions;
using NDocument.Domain.Extensions;
using NDocument.Domain.Factories;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Model.Generic;
using NDocument.Domain.Options;
using NDocument.Domain.Utilities;
using System.Text;

namespace NDocument.Domain.Model
{
    public partial class Table<TValue>
    {
        private const char _columnDivider = '|';
        private const char _rowDivider = '-';
        private const char _whiteSpace = ' ';
        private const char _alignmentChar = ':';
        private const int _minimumNumberOfDividerCharacters = 3;

        private async Task<string> CreateMarkdownTableAsync(MarkdownDocumentOptions options)
        {
            var outputStream = new MemoryStream();
            await CreateMarkdownTableAsync(outputStream, options);
            outputStream.Seek(0, SeekOrigin.Begin);
            using var streamReader = new StreamReader(outputStream);
            return await streamReader.ReadToEndAsync();
        }

        private async Task CreateMarkdownTableAsync(Stream outputStream, MarkdownDocumentOptions options)
        {
            if (!outputStream.CanWrite)
            {
                throw new NDocumentException(NDocumentErrorCode.StreamIsNotWriteable, nameof(outputStream));
            }

            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            using var markdownStreamWriter = MarkdownStreamWriterFactory.Create(outputStream, options);
            await CreateMarkdownTableHeaderAsync(markdownStreamWriter, newLineProvider, options).ConfigureAwait(false);
            await CreateMarkdownTableDividerAsync(markdownStreamWriter, newLineProvider, options).ConfigureAwait(false);
            await CreateMarkdownTableRowsAsync(markdownStreamWriter, newLineProvider, options).ConfigureAwait(false);
            await markdownStreamWriter.FlushAsync().ConfigureAwait(false);
        }

        private async Task CreateMarkdownTableHeaderAsync(IMarkdownStreamWriter markdownStreamWriter, INewLineProvider newLineProvider, MarkdownDocumentOptions options)
        {
            await markdownStreamWriter.WriteAsync(_columnDivider).ConfigureAwait(false);
            var numberOfColumns = TableValues.NumberOfColumns;

            for (var i = 0; i < numberOfColumns; i++)
            {
                var columnName = GetColumnName(i, options.MarkdownTableOptions.BoldColumnNames);
                var amountOfWhiteSpace = DetermineAmountOfWhiteSpace(columnName, i, options);
                await CreateMarkdownTableCellAsync(markdownStreamWriter, columnName, amountOfWhiteSpace);
            }

            await markdownStreamWriter.WriteNewLineAsync();
        }

        private async Task CreateMarkdownTableDividerAsync(IMarkdownStreamWriter markdownStreamWriter, INewLineProvider newLineProvider, MarkdownDocumentOptions options)
        {
            await markdownStreamWriter.WriteAsync(_columnDivider).ConfigureAwait(false);
            var numberOfColumns = TableValues.NumberOfColumns;

            for (var i = 0; i < numberOfColumns; i++)
            {
                var columnAttribute = OrderedColumnAttributes.ElementAt(i);
                var numberOfDividerCellCharacters = GetNumberOfCharactersForDividerCell(i, options);
                var divider = new string(_rowDivider, numberOfDividerCellCharacters);
                var alignment = GetAlignment(columnAttribute, options);
                var alignedDivider = AddAlignment(divider, alignment);
                await CreateMarkdownTableCellAsync(markdownStreamWriter, alignedDivider, 0, whiteSpaceCharacter: _rowDivider);
            }

            await markdownStreamWriter.WriteNewLineAsync();
        }

        private int GetNumberOfCharactersForDividerCell(int columnIndex, MarkdownDocumentOptions options)
        {
            var numberOfCharacters = GetLongestCellSizeForColumn(columnIndex, options.MarkdownTableOptions.BoldColumnNames);
            if (options.MarkdownTableOptions.Formatting == Formatting.None || numberOfCharacters < _minimumNumberOfDividerCharacters)
            {
                return _minimumNumberOfDividerCharacters;
            }
            return numberOfCharacters;
        }

        private static Alignment GetAlignment(ColumnAttribute columnAttribute, MarkdownDocumentOptions options)
        {
            if (columnAttribute.Alignment == Alignment.Default)
            {
                return options.MarkdownTableOptions.DefaultAlignment;
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

        private async Task CreateMarkdownTableRowsAsync(IMarkdownStreamWriter markdownStreamWriter, INewLineProvider newLineProvider, MarkdownDocumentOptions options)
        {
            var numberOfRows = TableValues.NumberOfRows;

            for (var i = 0; i < numberOfRows; i++)
            {
                var currentRow = TableValues.GetRow(i);
                await CreateMarkdownTableRowAsync(markdownStreamWriter, currentRow, options);
                await markdownStreamWriter.WriteNewLineAsync();
            }
        }

        private async Task CreateMarkdownTableRowAsync(IMarkdownStreamWriter markdownStreamWriter, TableCell[] tableRow, MarkdownDocumentOptions options)
        {
            await markdownStreamWriter.WriteAsync(_columnDivider).ConfigureAwait(false);
            for (var i = 0; i < tableRow.Length; i++)
            {
                var cellValue = tableRow[i].Value;
                var amountOfWhiteSpace = DetermineAmountOfWhiteSpace(cellValue, i, options);
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

        private int DetermineAmountOfWhiteSpace(string value, int currentColumnIndex, MarkdownDocumentOptions options)
        {
            var formatting = options.MarkdownTableOptions.Formatting;

            if (formatting == Formatting.AlignColumns)
            {
                var longestColumnCellSize = GetLongestCellSizeForColumn(currentColumnIndex, options);
                var amountOfCharactersToAdd = CorrectCellSizeBasedOnBoldOption(longestColumnCellSize, value.Length, options);

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

            throw new NotSupportedException($"Formatting {options.MarkdownTableOptions.Formatting} is not supported");
        }

        private int GetLongestCellSizeForColumn(int columnIndex, MarkdownDocumentOptions options)
        {
            if (!options.MarkdownTableOptions.BoldColumnNames)
            {
                return GetLongestCellSizeForColumn(columnIndex, options.MarkdownTableOptions.BoldColumnNames);
            }

            var longestCellSizeForColumnValue = GetLongestCellSizeForColumn(columnIndex, options.MarkdownTableOptions.BoldColumnNames);
            var columnName = OrderedColumnAttributes.ElementAt(columnIndex);
            var boldColumnNameSize = columnName.Name.Value.Length + 4;
            return Math.Max(longestCellSizeForColumnValue, boldColumnNameSize);
        }

        private static int CorrectCellSizeBasedOnBoldOption(int cellSize, int longestCellSize, MarkdownDocumentOptions options)
        {
            // When bold column names is enabled, and the cellsize is shorter than the longest cellsize, we need to align it.
            if (options.MarkdownTableOptions.BoldColumnNames && cellSize < longestCellSize)
            {
                return 4;
            }

            // In case of still having the longest cell size, we don't need to add extra space to align it.
            return 0;
        }
    }
}