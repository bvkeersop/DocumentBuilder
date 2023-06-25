using DocumentBuilder.Constants;
using System.Text;
using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown.Model;

internal class HorizontalRule : IMarkdownElement
{
    public ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions args)
    {
        var sb = new StringBuilder();
        sb.Append(Indicators.HorizontalRule);
        sb.Append(args.NewLineProvider.GetNewLine());
        var markdown = sb.ToString();
        return new ValueTask<string>(markdown);
    }
}
