using DocumentBuilder.Core.Enumerations;
using DocumentBuilder.Factories;

namespace DocumentBuilder.Html.Options;

public class HtmlDocumentOptionsBuilder
{
    private readonly HtmlDocumentOptions _options = new();

    public HtmlDocumentOptionsBuilder WithNewLineProvider(LineEndings lineEndings)
    {
        var newLineProvider = NewLineProviderFactory.Create(lineEndings);
        _options.NewLineProvider = newLineProvider;
        return this;
    }

    public HtmlDocumentOptionsBuilder WithIndentationProvider(IndentationType indentationType, int indentationSize)
    {
        var indentationProvider = IndentationProviderFactory.Create(indentationType, indentationSize);
        _options.IndentationProvider = indentationProvider;
        return this;
    }

    public HtmlDocumentOptions Build() => _options;
}
