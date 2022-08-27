using DocumentBuilder.Enumerations;

namespace DocumentBuilder.Options
{
    public class HtmlDocumentOptions : DocumentOptions
    {
        /// <summary>
        /// The type of indentation to use
        /// </summary>
        public IndentationType IndentationType { get; set; }

        /// <summary>
        /// The size of each indentation
        /// </summary>
        public int IndentationSize { get; set; } = 2;
    }
}
