using System.Text;
using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown.Model;

internal class Image : IMarkdownElement
{
    public string Name { get; }
    public string Path { get; }
    public string? Caption { get; }

    public Image(string name, string path, string? caption = null)
    {
        Name = name;
        Path = path;
        Caption = caption;
    }

    public ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions args)
    {
        var sb = new StringBuilder();
        var newLine = args.NewLineProvider.GetNewLine();

        sb.Append("![")
            .Append(Name)
            .Append("](")
            .Append(Path)
            .Append(')')
            .Append(newLine);

        if (Caption != null)
        {
            sb.Append('*')
                .Append(Caption)
                .Append('*');
        }

        var markdown = sb.ToString();
        return new ValueTask<string>(markdown);
    }
}
