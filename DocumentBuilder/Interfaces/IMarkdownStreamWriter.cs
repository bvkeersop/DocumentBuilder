﻿namespace DocumentBuilder.Interfaces
{
    public interface IMarkdownStreamWriter : IDisposable
    {
        Task WriteAsync(char value);

        Task WriteAsync(string value);

        Task WriteLineAsync(string value);

        Task WriteNewLineAsync();

        Task FlushAsync();
    }
}
