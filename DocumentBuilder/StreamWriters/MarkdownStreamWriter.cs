﻿using DocumentBuilder.Interfaces;
using DocumentBuilder.Utilities;

namespace DocumentBuilder.StreamWriters
{
    internal class MarkdownStreamWriter : IMarkdownStreamWriter
    {
        private bool _disposedValue;

        public StreamWriter StreamWriter { get; }
        public INewLineProvider NewLineProvider { get; }

        public MarkdownStreamWriter(StreamWriter streamWriter, INewLineProvider newLineProvider)
        {
            StreamWriter = streamWriter;
            NewLineProvider = newLineProvider;
        }

        public async Task WriteAsync(char value)
        {
            await StreamWriter.WriteAsync(value).ConfigureAwait(false);
        }

        public async Task WriteAsync(string value)
        {
            await StreamWriter.WriteAsync(value).ConfigureAwait(false);
        }

        public async Task WriteLineAsync(string value)
        {
            await StreamWriter.WriteAsync(value).ConfigureAwait(false);
            await WriteNewLineAsync().ConfigureAwait(false);
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

        ~MarkdownStreamWriter()
        {
            Dispose(disposing: false);
        }
    }
}
