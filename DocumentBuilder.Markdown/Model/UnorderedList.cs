using DocumentBuilder.Constants;
using System.Text;
using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown.Model;

public class UnorderedList<TValue> : IMarkdownElement
{
    public IEnumerable<TValue> Value { get; }

    public UnorderedList(IEnumerable<TValue> value)
    {
        Value = value;
    }

    public ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions args)
    {
        var sb = new StringBuilder();
        var newLine = args.NewLineProvider.GetNewLine();

        foreach (var item in Value)
        {
            sb.Append(Indicators.UnorderedListItem)
                .Append(' ')
                .Append(item)
                .Append(newLine);
        }

        var list = sb.ToString();
        return new ValueTask<string>(list);
    }
}
