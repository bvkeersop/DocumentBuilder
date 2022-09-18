using DocumentBuilder.Interfaces;
using DocumentBuilder.Options;

namespace DocumentBuilder.Model.Shared
{
    internal class Raw : IMarkdownConvertable
    {
        private readonly string _value;

        public Raw(string value)
        {
            _value = value;
        }

        public ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return new ValueTask<string>(_value);
        }
    }
}
