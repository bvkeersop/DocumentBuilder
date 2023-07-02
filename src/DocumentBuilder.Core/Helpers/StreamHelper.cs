namespace DocumentBuilder.Helpers;

public static class StreamHelper
{
    public static string GetStreamContents(Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);
        using var streamReader = new StreamReader(stream);
        return streamReader.ReadToEnd();
    }
}
