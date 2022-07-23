namespace NDocument.Domain.Utilities
{
    public class HtmlStreamWriter : IDisposable
    {
        private bool _disposedValue;

        public StreamWriter StreamWriter { get; init; }
        public INewLineProvider NewLineProvider { get; init; }
        public IIndentationProvider IndentationProvider { get; init; }

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
