using System.Text;
using DocumentBuilder.Markdown.Options;
using DocumentBuilder.Markdown.Constants;

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

    public string ToMarkdown(MarkdownDocumentOptions options)
        => new StringBuilder()
        .Append(Indicators.Codeblock)
        .Append(_language)
        .Append(options.NewLineProvider.GetNewLine())
        .Append(_codeblock)
        .Append(options.NewLineProvider.GetNewLine())
        .Append(Indicators.Codeblock)
        .ToString();
}
