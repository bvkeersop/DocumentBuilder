using DocumentBuilder.Constants;
using DocumentBuilder.Html.Extensions;
using DocumentBuilder.Html.Options;
using System.Text;

namespace DocumentBuilder.Html.Model;

public class Image : IHtmlElement
{
    public string Indicator { get; }
    public string Name { get; }
    public string Path { get; }
    public string? Caption { get; }

    public Attributes Attributes { get; } = new Attributes();

    public Image(string name, string path, string? caption = null)
    {
        Indicator = Indicators.Image;
        Name = name;
        Path = path;
        Caption = caption;
    }

    public ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
    {
        var newLine = options.NewLineProvider.GetNewLine();
        var indentationProvider = options.IndentationProvider;

        var indentation0 = indentationProvider.GetIndentation(0);
        var indentation1 = indentationProvider.GetIndentation(1);
        var imageHtml = $"<{Indicators.Image} src=\"{Path}\" alt=\"{Name}\" />";
        var figureStartTag = GetHtmlStartTagWithAttributes();
        var figureEndTag = Indicators.Figure.ToHtmlEndTag();

        if (Caption is null)
        {
            var valueWithoutCaption =
                $"{indentation0}{figureStartTag}{newLine}" +
                    $"{indentation1}{imageHtml}{newLine}" +
                $"{indentation0}{figureEndTag}{newLine}";

            return new ValueTask<string>(valueWithoutCaption);
        }

        var figCaptionStartTag = GetHtmlStartTagWithAttributes();
        var figCaptionEndTag = Indicators.FigCaption.ToHtmlEndTag();

        var valueWithCaption =
            $"{indentation0}{figureStartTag}{newLine}" +
                $"{indentation1}{imageHtml}{newLine}" +
                $"{indentation1}{figCaptionStartTag}{Caption}{figCaptionEndTag}{newLine}" +
            $"{indentation0}{figureEndTag}{newLine}";

        return new ValueTask<string>(valueWithCaption);
    }

    protected string GetHtmlStartTagWithAttributes()
    {
        if (Attributes.IsEmpty)
        {
            return Indicator.ToHtmlStartTag();
        }

        var sb = new StringBuilder();
        sb.Append(Indicator)
            .Append(' ')
            .Append(Attributes);
        var element = sb.ToString();
        return element.ToHtmlStartTag();
    }

    protected string GetHtmlEndTag() => Indicator.ToHtmlEndTag();
}
