﻿using DocumentBuilder.Utilities;
using System.Text;

namespace DocumentBuilder.Markdown.Model;

public class OrderedList<TValue> : List<TValue>
{
    public IEnumerable<TValue> Value { get; }

    public OrderedList(IEnumerable<TValue> value)
    {
        Value = value;
    }

    public ValueTask<string> ToMarkdownAsync(INewLineProvider newLineProvider, IIndentationProvider indentationProvider)
    {
        var sb = new StringBuilder();
        var newLine = args.NewLineProvider.GetNewLine();

        var index = 1;
        foreach (var item in Value)
        {
            sb.Append(index)
                .Append('.')
                .Append(' ')
                .Append(item)
                .Append(newLine);
            index++;
        }

        var list = sb.ToString();
        return new ValueTask<string>(list);
    }
}