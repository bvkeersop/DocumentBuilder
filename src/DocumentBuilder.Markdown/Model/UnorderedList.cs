using System.Text;
using DocumentBuilder.Markdown.Options;
using DocumentBuilder.Markdown.Constants;

namespace DocumentBuilder.Markdown.Model;

public class UnorderedList<TValue> : IMarkdownElement
{
    public IEnumerable<TValue> Value { get; }

    public UnorderedList(IEnumerable<TValue> value)
    {
        Value = value;
    }

    public string ToMarkdown(MarkdownDocumentOptions options)
    {
        var sb = new StringBuilder();
        var newLine = options.NewLineProvider.GetNewLine();

        foreach (var item in Value)
        {
            sb.Append(Indicators.UnorderedListItem)
                .Append(' ')
                .Append(item)
                .Append(newLine);
        }

        return sb.ToString();
    }
}
