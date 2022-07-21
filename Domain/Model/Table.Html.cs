using NDocument.Domain.Exceptions;
using NDocument.Domain.Factories;
using NDocument.Domain.Options;
using NDocument.Domain.Utilities;
using System.Text;

namespace NDocument.Domain.Model
{
    public partial class Table<T>
    {
        private const string _tableStart = "<table>";
        private const string _tableEnd = "</table>";
        private const string _tableRowStart = "<tr>";
        private const string _tableRowEnd = "</tr>";
        private const string _tableHeaderStart = "<th>";
        private const string _tableHeaderEnd = "</th>";
        private const string _tableDataStart = "<td>";
        private const string _tableDataEnd = "</td>";

        private async Task<string> CreateHtmlTableAsync(HtmlDocumentOptions options)
        {
            var outputStream = new MemoryStream();
            await CreateHtmlTableAsync(outputStream, options);
            outputStream.Seek(0, SeekOrigin.Begin);
            using var streamReader = new StreamReader(outputStream);
            return await streamReader.ReadToEndAsync();
        }

        private async Task CreateHtmlTableAsync(Stream outputStream, HtmlDocumentOptions options)
        {
            if (!outputStream.CanWrite)
            {
                throw new NDocumentException(NDocumentErrorCode.StreamIsNotWriteable, nameof(outputStream));
            }

            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize);

            using var streamWriter = new StreamWriter(outputStream, leaveOpen: true);
            await streamWriter.WriteAsync(_tableStart).ConfigureAwait(false);
            await streamWriter.WriteAsync(newLineProvider.GetNewLine()).ConfigureAwait(false);

            await CreateHtmlTableHeaderAsync(streamWriter, newLineProvider, indentationProvider).ConfigureAwait(false);
            await CreateHtmlTableRowsAsync(streamWriter, newLineProvider, indentationProvider).ConfigureAwait(false);

            await streamWriter.WriteAsync(_tableEnd).ConfigureAwait(false);
            await streamWriter.FlushAsync().ConfigureAwait(false);
        }

        private async Task CreateHtmlTableHeaderAsync(
            StreamWriter streamWriter,
            INewLineProvider newLineProvider,
            IIndentationProvider indentationProvider)
        {
            var numberOfColumns = TableValues.NumberOfColumns;

            await streamWriter.WriteAsync(indentationProvider.GetIndentation(1)).ConfigureAwait(false);
            await streamWriter.WriteAsync(_tableRowStart).ConfigureAwait(false);
            await streamWriter.WriteAsync(newLineProvider.GetNewLine()).ConfigureAwait(false);

            for (var i = 0; i < numberOfColumns; i++)
            {
                var columnName = OrderedColumnAttributes.ElementAt(i).Name;
                await CreateHtmlTableCellAsync(streamWriter, newLineProvider, indentationProvider, columnName, _tableHeaderStart, _tableHeaderEnd, 2).ConfigureAwait(false);
            }

            await streamWriter.WriteAsync(indentationProvider.GetIndentation(1)).ConfigureAwait(false);
            await streamWriter.WriteAsync(_tableRowEnd).ConfigureAwait(false);
            await streamWriter.WriteAsync(newLineProvider.GetNewLine()).ConfigureAwait(false);
        }

        private async Task CreateHtmlTableRowsAsync(
            StreamWriter streamWriter,
            INewLineProvider newLineProvider,
            IIndentationProvider indentationProvider)
        {
            var numberOfRows = TableValues.NumberOfRows;

            for (var i = 0; i < numberOfRows; i++)
            {
                var currentRow = TableValues.GetRow(i);
                await CreateHtmlTableRowAsync(streamWriter, currentRow, newLineProvider, indentationProvider).ConfigureAwait(false);
                await streamWriter.WriteAsync(newLineProvider.GetNewLine()).ConfigureAwait(false);
            }
        }

        private static async Task CreateHtmlTableRowAsync(
            StreamWriter streamWriter,
            string[] tableRow,
            INewLineProvider newLineProvider,
            IIndentationProvider indentationProvider)
        {
            await streamWriter.WriteAsync(indentationProvider.GetIndentation(1)).ConfigureAwait(false);
            await streamWriter.WriteAsync(_tableRowStart).ConfigureAwait(false);
            await streamWriter.WriteAsync(newLineProvider.GetNewLine()).ConfigureAwait(false);

            for (var i = 0; i < tableRow.Length; i++)
            {
                var cellValue = tableRow[i];
                await CreateHtmlTableCellAsync(streamWriter, newLineProvider, indentationProvider, cellValue, _tableDataStart, _tableDataEnd, 2).ConfigureAwait(false);
            }

            await streamWriter.WriteAsync(indentationProvider.GetIndentation(1)).ConfigureAwait(false);
            await streamWriter.WriteAsync(_tableRowEnd).ConfigureAwait(false);
        }

        private static async Task CreateHtmlTableCellAsync(
            StreamWriter streamWriter,
            INewLineProvider newLineProvider,
            IIndentationProvider indentationProvider,
            string cellValue,
            string startHtml,
            string endHtml,
            int indentation)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder
                .Append(indentationProvider.GetIndentation(indentation))
                .Append(startHtml)
                .Append(cellValue)
                .Append(endHtml)
                .Append(newLineProvider.GetNewLine());
            await streamWriter.WriteAsync(stringBuilder).ConfigureAwait(false);
        }
    }
}