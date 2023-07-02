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

    public string ToMarkdown(MarkdownDocumentOptions options)
    {
        var sb = new StringBuilder();

        sb.Append("![")
            .Append(Name)
            .Append("](")
            .Append(Path)
            .Append(')')
            .Append(options.NewLineProvider.GetNewLine());

        if (Caption != null)
        {
            sb.Append('*')
                .Append(Caption)
                .Append('*');
        }

        return sb.ToString();
    }
}
