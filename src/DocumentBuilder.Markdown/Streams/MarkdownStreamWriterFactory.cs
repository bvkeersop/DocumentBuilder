using DocumentBuilder.Utilities;

namespace DocumentBuilder.Markdown.Streams;

internal static class MarkdownStreamWriterFactory
{
    public static IMarkdownStreamWriter Create(Stream outputStream, INewLineProvider newLineProvider)
    {
        var streamWriter = new StreamWriter(outputStream, leaveOpen: true);
        return new MarkdownStreamWriter(streamWriter, newLineProvider);
    }
}
