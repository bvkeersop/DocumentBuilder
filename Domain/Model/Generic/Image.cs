using DocumentBuilder.Domain.Constants;
using DocumentBuilder.Domain.Extensions;
using DocumentBuilder.Domain.Factories;
using DocumentBuilder.Domain.Options;

namespace DocumentBuilder.Domain.Model.Generic
{
    public class Image : GenericElement
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
            var imageHtml = $"<{HtmlIndicators.Image} src=\"{Path}\" alt=\"{Name}\" />";
            var figureStartTag = HtmlIndicators.Figure.ToHtmlStartTag();
            var figureEndTag = HtmlIndicators.Figure.ToHtmlEndTag();

            if (Caption is null)
            {
                var valueWithoutCaption = 
                    $"{indentation0}{figureStartTag}{newLine}" +
                        $"{indentation1}{imageHtml}{newLine}" +
                    $"{indentation0}{figureEndTag}{newLine}";

                return new ValueTask<string>(valueWithoutCaption);
            }

            var figCaptionStartTag = HtmlIndicators.FigCaption.ToHtmlStartTag();
            var figCaptionEndTag = HtmlIndicators.FigCaption.ToHtmlEndTag();

            var valueWithCaption = 
                $"{indentation0}{figureStartTag}{newLine}" +
                    $"{indentation1}{imageHtml}{newLine}" +
                    $"{indentation1}{figCaptionStartTag}{Caption}{figCaptionEndTag}{newLine}" +
                $"{indentation0}{figureEndTag}{newLine}";

            return new ValueTask<string>(valueWithCaption);
        }
    }
}
