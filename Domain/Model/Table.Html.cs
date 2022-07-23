using NDocument.Domain.Constants;
using NDocument.Domain.Exceptions;
using NDocument.Domain.Extensions;
using NDocument.Domain.Factories;
using NDocument.Domain.Options;
using NDocument.Domain.Utilities;
using System.Text;

namespace NDocument.Domain.Model
{
    public partial class Table<T>
    {
        private async Task<string> CreateHtmlTableAsync(HtmlDocumentOptions options, int indentationLevel)
        {
            var outputStream = new MemoryStream();
            await CreateHtmlTableAsync(outputStream, options, indentationLevel);
            outputStream.Seek(0, SeekOrigin.Begin);
            using var streamReader = new StreamReader(outputStream);
            return await streamReader.ReadToEndAsync();
        }

        private async Task CreateHtmlTableAsync(Stream outputStream, HtmlDocumentOptions options, int indentationLevel)
        {
            if (!outputStream.CanWrite)
            {
                throw new NDocumentException(NDocumentErrorCode.StreamIsNotWriteable, nameof(outputStream));
            }

            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize, indentationLevel);

            var streamWriter = new StreamWriter(outputStream, leaveOpen: true);
            using var htmlStreamWriter = new HtmlStreamWriter(streamWriter, newLineProvider, indentationProvider);

            await htmlStreamWriter.WriteLineAsync(HtmlIndicators.Table.ToHtmlStartTag()).ConfigureAwait(false);

            await CreateHtmlTableHeaderAsync(htmlStreamWriter, newLineProvider, indentationProvider).ConfigureAwait(false);
            await CreateHtmlTableRowsAsync(htmlStreamWriter, newLineProvider, indentationProvider).ConfigureAwait(false);

            await htmlStreamWriter.WriteLineAsync(HtmlIndicators.Table.ToHtmlEndTag()).ConfigureAwait(false);
            await htmlStreamWriter.FlushAsync().ConfigureAwait(false);
        }

        private async Task CreateHtmlTableHeaderAsync(
            HtmlStreamWriter htmlStreamWriter,
            INewLineProvider newLineProvider,
            IIndentationProvider indentationProvider)
        {
            var numberOfColumns = TableValues.NumberOfColumns;

            await htmlStreamWriter.WriteLineAsync(HtmlIndicators.TableRow.ToHtmlStartTag(), 1).ConfigureAwait(false);

            for (var i = 0; i < numberOfColumns; i++)
            {
                var columnName = OrderedColumnAttributes.ElementAt(i).Name;
                await CreateHtmlTableCellAsync(htmlStreamWriter, newLineProvider, indentationProvider, columnName, HtmlIndicators.TableHeader, 2).ConfigureAwait(false);
            }

            await htmlStreamWriter.WriteLineAsync(HtmlIndicators.TableRow.ToHtmlEndTag(), 1).ConfigureAwait(false);
        }

        private async Task CreateHtmlTableRowsAsync(
            HtmlStreamWriter htmlStreamWriter,
            INewLineProvider newLineProvider,
            IIndentationProvider indentationProvider)
        {
            var numberOfRows = TableValues.NumberOfRows;

            for (var i = 0; i < numberOfRows; i++)
            {
                var currentRow = TableValues.GetRow(i);
                await CreateHtmlTableRowAsync(htmlStreamWriter, currentRow, newLineProvider, indentationProvider).ConfigureAwait(false);
                await htmlStreamWriter.WriteNewLineAsync().ConfigureAwait(false);
            }
        }

        private static async Task CreateHtmlTableRowAsync(
            HtmlStreamWriter htmlStreamWriter,
            string[] tableRow,
            INewLineProvider newLineProvider,
            IIndentationProvider indentationProvider)
        {
            await htmlStreamWriter.WriteLineAsync(HtmlIndicators.TableRow.ToHtmlStartTag(), 1).ConfigureAwait(false);

            for (var i = 0; i < tableRow.Length; i++)
            {
                var cellValue = tableRow[i];
                await CreateHtmlTableCellAsync(htmlStreamWriter, newLineProvider, indentationProvider, cellValue, HtmlIndicators.TableData, 2).ConfigureAwait(false);
            }

            await htmlStreamWriter.WriteAsync(HtmlIndicators.TableRow.ToHtmlEndTag(), 1).ConfigureAwait(false);
        }

        private static async Task CreateHtmlTableCellAsync(
            HtmlStreamWriter htmlStreamWriter,
            INewLineProvider newLineProvider,
            IIndentationProvider indentationProvider,
            string cellValue,
            string htmlIndicator,
            int indentation)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder
                .Append(htmlIndicator.ToHtmlStartTag())
                .Append(cellValue)
                .Append(htmlIndicator.ToHtmlEndTag());
            await htmlStreamWriter.WriteLineAsync(stringBuilder.ToString(), indentation).ConfigureAwait(false);
        }
    }
}