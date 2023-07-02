using System.Runtime.Serialization;

namespace DocumentBuilder.Exceptions;

[Serializable]
public sealed class MarkdownDocumentBuilderException : Exception
{
    public MarkdownDocumentBuilderErrorCode ErrorCode { get; init; }

    public MarkdownDocumentBuilderException() : this(MarkdownDocumentBuilderErrorCode.Unknown) { }

    public MarkdownDocumentBuilderException(string message) : this(MarkdownDocumentBuilderErrorCode.Unknown, message) { }

    public MarkdownDocumentBuilderException(string message, Exception innerException) : this(MarkdownDocumentBuilderErrorCode.Unknown, message, innerException) { }

    public MarkdownDocumentBuilderException(MarkdownDocumentBuilderErrorCode errorCode) : base($"Error code: {errorCode}")
    {
        ErrorCode = errorCode;
    }

    public MarkdownDocumentBuilderException(Exception innerException, MarkdownDocumentBuilderErrorCode errorCode) : base($"Error code: {errorCode}", innerException)
    {
        ErrorCode = errorCode;
    }

    public MarkdownDocumentBuilderException(MarkdownDocumentBuilderErrorCode errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }

    public MarkdownDocumentBuilderException(MarkdownDocumentBuilderErrorCode errorCode, string message, Exception innerException) : base(message, innerException)
    {
        ErrorCode = errorCode;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
        {
            throw new ArgumentNullException(nameof(info));
        }

        base.GetObjectData(info, context);
        info.AddValue(nameof(ErrorCode), ErrorCode);
    }

    private MarkdownDocumentBuilderException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        var errorCode = info.GetValue(nameof(ErrorCode), typeof(MarkdownDocumentBuilderErrorCode)) ?? MarkdownDocumentBuilderErrorCode.Unknown;
        ErrorCode = (MarkdownDocumentBuilderErrorCode)errorCode;
    }
}
