using DocumentBuilder.Utilities;
using DocumentBuilder.Factories;
using DocumentBuilder.Options;

namespace DocumentBuilder.Test.Unit.TestHelpers
{
    internal class ExampleProductHtmlTableBuilder
    {

        public static string BuildExpectedProductTable(HtmlDocumentOptions options, int baseIndentation)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize, baseIndentation);

            return
                indentationProvider.GetIndentation(0) +
                "<table>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 1) +
                    "<tr>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<th>Id</th>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<th>Amount</th>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<th>Price</th>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<th>Description</th>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 1) +
                    "</tr>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 1) +
                    "<tr>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<td>1</td>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<td>1</td>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<td>1,11</td>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<td>Description 1</td>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 1) +
                    "</tr>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 1) +
                    "<tr>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<td>2</td>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<td>2</td>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<td>2,22</td>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<td>Description 2</td>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 1) +
                    "</tr>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 1) +
                    "<tr>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<td>3</td>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<td>3</td>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                        "<td>3,33</td>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 2) +
                    "<td>Very long description with most characters</td>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 1) +
                    "</tr>" + GetNewLineAndIndentation(newLineProvider, indentationProvider) +
                "</table>" + newLineProvider.GetNewLine();
        }

        private static string GetNewLineAndIndentation(INewLineProvider newLineProvider, IIndentationProvider indentationProvider, int level = 0)
        {
            var indenation = newLineProvider.GetNewLine() + indentationProvider.GetIndentation(level);
            return indenation;
        }
    }
}
