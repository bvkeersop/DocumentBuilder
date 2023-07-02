using DocumentBuilder.Markdown.Options;
using System.Text;

namespace DocumentBuilder.Markdown.Model;

public class OrderedList<TValue> : List<TValue>, IMarkdownElement
{
    public IEnumerable<TValue> Value { get; }

    public OrderedList(IEnumerable<TValue> value)
    {
        Value = value;
    }

    public string ToMarkdown(MarkdownDocumentOptions options)
    {
        var sb = new StringBuilder();
        var newLine = options.NewLineProvider.GetNewLine();

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

        return sb.ToString();
    }
}
