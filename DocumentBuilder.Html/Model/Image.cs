using DocumentBuilder.Constants;
using DocumentBuilder.Factories;
using DocumentBuilder.Options;

namespace DocumentBuilder.Html.Model;

public class Image : IHtmlElement
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

    public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
    {
        var newLine = NewLineProviderFactory.Create(options.LineEndings).GetNewLine();

        if (Caption is null)
        {
            var valueWithoutCaption = $"![{Name}]({Path})";
            return AddNewLine(valueWithoutCaption, options);
        }

        var valueWithCaption = $"![{Name}]({Path}){newLine}*{Caption}*";
        return AddNewLine(valueWithCaption, options);
    }

    public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
    {
        var newLine = NewLineProviderFactory.Create(options.LineEndings).GetNewLine();
        var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize, indentationLevel);
        var indentation0 = indentationProvider.GetIndentation(0);
        var indentation1 = indentationProvider.GetIndentation(1);
        var imageHtml = $"<{Indicators.Image} src=\"{Path}\" alt=\"{Name}\" />";
        var figureStartTag = GetHtmlStartTagWithAttributes(Indicators.Figure);
        var figureEndTag = Indicators.Figure.ToHtmlEndTag();

        if (Caption is null)
        {
            var valueWithoutCaption =
                $"{indentation0}{figureStartTag}{newLine}" +
                    $"{indentation1}{imageHtml}{newLine}" +
                $"{indentation0}{figureEndTag}{newLine}";

            return new ValueTask<string>(valueWithoutCaption);
        }

        var figCaptionStartTag = GetHtmlStartTagWithAttributes(Indicators.FigCaption);
        var figCaptionEndTag = Indicators.FigCaption.ToHtmlEndTag();

        var valueWithCaption =
            $"{indentation0}{figureStartTag}{newLine}" +
                $"{indentation1}{imageHtml}{newLine}" +
                $"{indentation1}{figCaptionStartTag}{Caption}{figCaptionEndTag}{newLine}" +
            $"{indentation0}{figureEndTag}{newLine}";

        return new ValueTask<string>(valueWithCaption);
    }
}
