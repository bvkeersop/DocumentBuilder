using DocumentBuilder.Constants;
using System.Text;
using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown.Model;

internal class FencedCodeblock : IMarkdownElement
{
    private readonly string _codeblock;
    private readonly string? _language;

    public FencedCodeblock(string codeblock, string? language = null)
    {
        _codeblock = codeblock;
        _language = language;
    }

    public ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions args)
    {
        var sb = new StringBuilder();
        sb.Append(Indicators.Codeblock);
        sb.Append(_language);
        sb.Append(args.NewLineProvider.GetNewLine());
        sb.Append(_codeblock);
        sb.Append(args.NewLineProvider.GetNewLine());
        sb.Append(Indicators.Codeblock);
        sb.Append(args.NewLineProvider.GetNewLine());
        var markdown = sb.ToString();
        return new ValueTask<string>(markdown);
    }
}
