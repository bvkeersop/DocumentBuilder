using NDocument.Domain.Options;
using NDocument.Domain.Factories;

namespace NDocument.Domain.Test.Unit.TestHelpers
{
    internal class ExampleProductHtmlTableBuilder
    {

        public static string BuildExpectedProductTable(HtmlDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize);

            return
                "<table>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(1) +
                "<tr>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<th>ProductId</th>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<th>Amount</th>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<th>Price</th>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<th>Description</th>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(1) +
                "</tr>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(1) +
                "<tr>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<td>1</td>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<td>1</td>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<td>1,11</td>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<td>Description 1</td>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(1) +
                "</tr>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(1) +
                "<tr>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<td>2</td>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<td>2</td>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<td>2,22</td>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<td>Description 2</td>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(1) +
                "</tr>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(1) +
                "<tr>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<td>3</td>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<td>3</td>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<td>3,33</td>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(2) +
                "<td>Very long description with most characters</td>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(1) +
                "</tr>" + newLineProvider.GetNewLine() + indentationProvider.GetIndentation(0) +
                "</table>";
        }
    }
}
