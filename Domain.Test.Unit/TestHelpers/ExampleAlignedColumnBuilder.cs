﻿using NDocument.Domain.Enumerations;
using NDocument.Domain.Factories;
using NDocument.Domain.Options;

namespace NDocument.Domain.Test.Unit.TestHelpers
{
    internal static class ExampleAlignedColumnBuilder
    {

        public static string BuildExpectedAlignedColumn(Alignment alignment, MarkdownDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var dividerCellContent = CreateDividerCellContent(alignment);
            return
                 "| ColumnName  |" + newLineProvider.GetNewLine() +
                $"| {dividerCellContent} |" + newLineProvider.GetNewLine() +
                $"| ColumnValue |" + newLineProvider.GetNewLine();
        }

        public static string CreateDividerCellContent(Alignment alignment)
        {
            return alignment switch
            {
                Alignment.Center => ":---------:",
                Alignment.Left => ":----------",
                Alignment.Right => "----------:",
                Alignment.None => "-----------",
                _ => throw new NotSupportedException($"{alignment} is currently not supported")
            };
        }

    }
}
