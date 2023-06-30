using DocumentBuilder.Utilities;

namespace DocumentBuilder.Html
{
    public interface IHtmlStreamWriter : IDisposable
    {
        Task WriteLineAsync(string value, int indenationLevel = 0);
        Task WriteAsync(string value, int indenationLevel = 0);
        Task WriteNewLineAsync();
        Task FlushAsync();
    }

    internal class HtmlStreamWriter : IHtmlStreamWriter
    {
        private bool _disposedValue;

        public StreamWriter StreamWriter { get; }
        public INewLineProvider NewLineProvider { get; }
        public IIndentationProvider IndentationProvider { get; }

        public HtmlStreamWriter(StreamWriter streamWriter, INewLineProvider newLineProvider, IIndentationProvider indentationProvider)
        {
            StreamWriter = streamWriter;
            NewLineProvider = newLineProvider;
            IndentationProvider = indentationProvider;
        }

        public async Task WriteLineAsync(string value, int indenationLevel = 0)
        {
            await StreamWriter.WriteAsync(IndentationProvider.GetIndentation(indenationLevel));
            await StreamWriter.WriteAsync(value).ConfigureAwait(false);
            await WriteNewLineAsync().ConfigureAwait(false);
        }

        public async Task WriteAsync(string value, int indenationLevel = 0)
        {
            await StreamWriter.WriteAsync(IndentationProvider.GetIndentation(indenationLevel));
            await StreamWriter.WriteAsync(value).ConfigureAwait(false);
        }

        public async Task WriteNewLineAsync()
        {
            await StreamWriter.WriteAsync(NewLineProvider.GetNewLine()).ConfigureAwait(false);
        }

        public async Task FlushAsync()
        {
            await StreamWriter.FlushAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    StreamWriter.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~HtmlStreamWriter()
        {
            Dispose(disposing: false);
        }
    }
}
