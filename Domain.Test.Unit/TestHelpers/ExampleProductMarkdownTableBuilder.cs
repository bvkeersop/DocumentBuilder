using DocumentBuilder.Domain.Enumerations;
using DocumentBuilder.Domain.Factories;
using DocumentBuilder.Domain.Options;
using DocumentBuilder.Domain.Utilities;

namespace DocumentBuilder.Domain.Test.Unit.TestHelpers
{
    internal static class ExampleProductMarkdownTableBuilder
    {
        public static string BuildExpectedFormattedProductTable(MarkdownDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var formatting = options.MarkdownTableOptions.Formatting;


            if (formatting == Formatting.AlignColumns)
            {
                return CreateTableWithAlignColumnsFormatting(newLineProvider);
            }

            if (formatting == Formatting.None)
            {
                return CreateTableWithNoFormatting(newLineProvider);
            }

            throw new NotSupportedException();
        }

        private static string CreateTableWithNoFormatting(INewLineProvider newLineProvider)
        {
            return
            "| Id | Amount | Price | Description |" + newLineProvider.GetNewLine() +
            "| --- | --- | --- | --- |" + newLineProvider.GetNewLine() +
            "| 1 | 1 | 1,11 | Description 1 |" + newLineProvider.GetNewLine() +
            "| 2 | 2 | 2,22 | Description 2 |" + newLineProvider.GetNewLine() +
            "| 3 | 3 | 3,33 | Very long description with most characters |" + newLineProvider.GetNewLine();
        }

        private static string CreateTableWithAlignColumnsFormatting(INewLineProvider newLineProvider)
        {
            return
            "| Id  | Amount | Price | Description                                |" + newLineProvider.GetNewLine() +
            "| --- | ------ | ----- | ------------------------------------------ |" + newLineProvider.GetNewLine() +
            "| 1   | 1      | 1,11  | Description 1                              |" + newLineProvider.GetNewLine() +
            "| 2   | 2      | 2,22  | Description 2                              |" + newLineProvider.GetNewLine() +
            "| 3   | 3      | 3,33  | Very long description with most characters |" + newLineProvider.GetNewLine();
        }
    }
}
