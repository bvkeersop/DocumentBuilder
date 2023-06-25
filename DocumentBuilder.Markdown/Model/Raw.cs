using System.Text;
using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown.Model;

internal class Raw : IMarkdownElement
{
    public string Value { get; }

    public Raw(string value)
    {
        Value = value;
    }

    public ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions args)
    {
        var sb = new StringBuilder();
        sb.Append(Value);
        sb.Append(args.NewLineProvider.GetNewLine());
        var paragraph = sb.ToString();
        return new ValueTask<string>(paragraph);
    }
}
