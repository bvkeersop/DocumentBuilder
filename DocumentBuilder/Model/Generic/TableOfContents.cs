using DocumentBuilder.Factories;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Options;
using System.Text;

namespace DocumentBuilder.Model.Generic
{
    internal class TableOfContents : IMarkdownConvertable
    {
        private readonly IEnumerable<Header> _chapters;
        private readonly bool _isNumbered;

        public TableOfContents(IEnumerable<Header> chapters, bool isNumbered)
        {
            _chapters = chapters;
            _isNumbered = isNumbered;
        }

        public async ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            var stringBuilder = new StringBuilder();
            foreach (var chapter in _chapters)
            {
                var tableOfContentsEntry = await chapter.ToMarkdownTableOfContentsEntry(_isNumbered, options);
                stringBuilder.Append(tableOfContentsEntry);
            }
            var markdown = stringBuilder.ToString();
            return markdown;
        }
    }
}
