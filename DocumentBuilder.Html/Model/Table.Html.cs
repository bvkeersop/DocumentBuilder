using DocumentBuilder.Constants;
using DocumentBuilder.Exceptions;
using DocumentBuilder.Extensions;
using DocumentBuilder.Factories;
using DocumentBuilder.Helpers;
using DocumentBuilder.Html;
using DocumentBuilder.Model.Generic;
using DocumentBuilder.Options;
using System.Text;

namespace DocumentBuilder.Model
{
    public partial class Table<TRow>
    {
        private async Task<string> CreateHtmlTableAsync(HtmlDocumentOptions options, int indentationLevel)
        {
            var outputStream = new MemoryStream();
            await CreateHtmlTableAsync(outputStream, options, indentationLevel);
            return StreamHelper.GetStreamContents(outputStream);
        }

        private async Task CreateHtmlTableAsync(Stream outputStream, HtmlDocumentOptions options, int indentationLevel)
        {
            if (!outputStream.CanWrite)
            {
                throw new DocumentBuilderException(DocumentBuilderErrorCode.StreamIsNotWriteable, nameof(outputStream));
            }

            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize, indentationLevel);

            var streamWriter = new StreamWriter(outputStream, leaveOpen: true);
            using var htmlStreamWriter = new HtmlStreamWriter(streamWriter, newLineProvider, indentationProvider);

            await htmlStreamWriter.WriteLineAsync(HtmlIndicators.Table.ToHtmlStartTag()).ConfigureAwait(false);

            await CreateHtmlTableHeaderAsync(htmlStreamWriter).ConfigureAwait(false);
            await CreateHtmlTableRowsAsync(htmlStreamWriter).ConfigureAwait(false);

            await htmlStreamWriter.WriteLineAsync(HtmlIndicators.Table.ToHtmlEndTag()).ConfigureAwait(false);
            await htmlStreamWriter.FlushAsync().ConfigureAwait(false);
        }

        private async Task CreateHtmlTableHeaderAsync(HtmlStreamWriter htmlStreamWriter)
        {
            var numberOfColumns = TableValues.NumberOfColumns;

            await htmlStreamWriter.WriteLineAsync(HtmlIndicators.TableRow.ToHtmlStartTag(), 1).ConfigureAwait(false);

            for (var i = 0; i < numberOfColumns; i++)
            {
                var columnName = OrderedColumnAttributes.ElementAt(i).Name.Value;
                await CreateHtmlTableCellAsync(htmlStreamWriter, columnName, HtmlIndicators.TableHeader, 2).ConfigureAwait(false);
            }

            await htmlStreamWriter.WriteLineAsync(HtmlIndicators.TableRow.ToHtmlEndTag(), 1).ConfigureAwait(false);
        }

        private async Task CreateHtmlTableRowsAsync(HtmlStreamWriter htmlStreamWriter)
        {
            var numberOfRows = TableValues.NumberOfRows;

            for (var i = 0; i < numberOfRows; i++)
            {
                var currentRow = TableValues.GetRow(i);
                await CreateHtmlTableRowAsync(htmlStreamWriter, currentRow).ConfigureAwait(false);
                await htmlStreamWriter.WriteNewLineAsync().ConfigureAwait(false);
            }
        }

        private static async Task CreateHtmlTableRowAsync(HtmlStreamWriter htmlStreamWriter, TableCell[] tableRow)
        {
            await htmlStreamWriter.WriteLineAsync(HtmlIndicators.TableRow.ToHtmlStartTag(), 1).ConfigureAwait(false);

            for (var i = 0; i < tableRow.Length; i++)
            {
                var cellValue = tableRow[i].Value;
                await CreateHtmlTableCellAsync(htmlStreamWriter, cellValue, HtmlIndicators.TableData, 2).ConfigureAwait(false);
            }

            await htmlStreamWriter.WriteAsync(HtmlIndicators.TableRow.ToHtmlEndTag(), 1).ConfigureAwait(false);
        }

        private static async Task CreateHtmlTableCellAsync(
            HtmlStreamWriter htmlStreamWriter,
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