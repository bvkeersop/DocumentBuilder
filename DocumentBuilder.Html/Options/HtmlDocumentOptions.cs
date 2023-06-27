using DocumentBuilder.Core.Enumerations;
using DocumentBuilder.Factories;
using DocumentBuilder.Utilities;

namespace DocumentBuilder.Html.Options
{
    public class HtmlDocumentOptions
    {
        /// <summary>
        /// How an list or table will be rendered when it's empty
        /// </summary>
        public NullOrEmptyEnumerableRenderingStrategy NullOrEmptyEnumerableRenderingStrategy { get; set; }

        /// <summary>
        /// A provider for the new line character
        /// </summary>
        public INewLineProvider NewLineProvider { get; set; } = NewLineProviderFactory.Create(LineEndings.Environment);

        /// <summary>
        /// A provider for indentation
        /// </summary>
        public IIndentationProvider IndentationProvider { get; set; } = IndentationProviderFactory.Create(IndentationType.Spaces, 4);
    }
}