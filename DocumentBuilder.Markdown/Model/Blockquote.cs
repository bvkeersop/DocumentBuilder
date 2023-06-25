using DocumentBuilder.Constants;

namespace DocumentBuilder.Markdown.Model;

internal class Blockquote : IMarkdownElement
{
    private string Value { get; }

    public Blockquote(string value)
    {
        Value = value;
    }

    public ValueTask<string> ToMarkdownAsync(MarkdownConversionArgs args)
    {
        var markdown = $"{Indicators.Blockquote} {Value}{args.NewLineProvider.GetNewLine()}";
        return new ValueTask<string>(markdown);
    }
}
