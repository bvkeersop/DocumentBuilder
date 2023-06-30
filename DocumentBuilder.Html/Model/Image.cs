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

    private void AppendImageHtml(StringBuilder sb, string indentation, string newline) =>
        sb.Append(indentation)
        .Append('<')
        .Append(Indicators.Image)
        .Append("src=")
        .Append('"').Append(Path).Append('"')
        .Append('"').Append(Name).Append('"')
        .Append("alt=").Append(Name).Append(" />")
        .Append(newline);

    private void AppendFigCaption(StringBuilder sb, string indentation, string newline) =>
        sb.Append(indentation)
        .Append(Indicators.FigCaption.ToHtmlStartTag())
        .Append(Caption)
        .Append(Indicators.FigCaption.ToHtmlEndTag())
        .Append(newline);

    public string ToHtml(HtmlDocumentOptions options, int indentationLevel = 0)
    {
        var newLine = options.NewLineProvider.GetNewLine();
        var indentationProvider = options.IndentationProvider;
        var indentation0 = indentationProvider.GetIndentation(indentationLevel);
        var indentation1 = indentationProvider.GetIndentation(indentationLevel + 1);

        var sb = new StringBuilder();

        sb.Append(indentation0)
          .Append(Indicator.ToHtmlStartTag());

        AppendImageHtml(sb, indentation1, newLine);

        if (Caption is null)
        {
            AppendFigCaption(sb, indentation1, newLine);
        }

        sb.Append(indentation0)
          .Append(Indicator.ToHtmlEndTag());

        return sb.ToString();
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
