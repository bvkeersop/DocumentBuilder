namespace DocumentBuilder.Interfaces
{
    public interface IHtmlStreamWriter : IDisposable
    {
        Task WriteLineAsync(string value, int indenationLevel = 0);

        Task WriteAsync(string value, int indenationLevel = 0);

        Task WriteNewLineAsync();

        Task FlushAsync();
    }
}
