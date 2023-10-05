using System.Runtime.Serialization;

namespace DocumentBuilder.Html.Exceptions;

[Serializable]
public sealed class HtmlDocumentBuilderException : Exception
{
    public HtmlDocumentBuilderErrorCode ErrorCode { get; init; }

    public HtmlDocumentBuilderException() : this(HtmlDocumentBuilderErrorCode.Unknown) { }

    public HtmlDocumentBuilderException(string message) : this(HtmlDocumentBuilderErrorCode.Unknown, message) { }

    public HtmlDocumentBuilderException(string message, Exception innerException) : this(HtmlDocumentBuilderErrorCode.Unknown, message, innerException) { }

    public HtmlDocumentBuilderException(HtmlDocumentBuilderErrorCode errorCode) : base($"Error code: {errorCode}")
    {
        ErrorCode = errorCode;
    }

    public HtmlDocumentBuilderException(Exception innerException, HtmlDocumentBuilderErrorCode errorCode) : base($"Error code: {errorCode}", innerException)
    {
        ErrorCode = errorCode;
    }

    public HtmlDocumentBuilderException(HtmlDocumentBuilderErrorCode errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }

    public HtmlDocumentBuilderException(HtmlDocumentBuilderErrorCode errorCode, string message, Exception innerException) : base(message, innerException)
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

    private HtmlDocumentBuilderException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        var errorCode = info.GetValue(nameof(ErrorCode), typeof(HtmlDocumentBuilderErrorCode)) ?? HtmlDocumentBuilderErrorCode.Unknown;
        ErrorCode = (HtmlDocumentBuilderErrorCode)errorCode;
    }
}
