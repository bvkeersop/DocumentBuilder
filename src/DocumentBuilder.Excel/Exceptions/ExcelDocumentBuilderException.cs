using System.Runtime.Serialization;

namespace DocumentBuilder.Exceptions;

[Serializable]
public sealed class ExcelDocumentBuilderException : Exception
{
    public ExcelDocumentBuilderErrorCode ErrorCode { get; init; }

    public ExcelDocumentBuilderException() : this(ExcelDocumentBuilderErrorCode.Unknown) { }

    public ExcelDocumentBuilderException(string message) : this(ExcelDocumentBuilderErrorCode.Unknown, message) { }

    public ExcelDocumentBuilderException(string message, Exception innerException) : this(ExcelDocumentBuilderErrorCode.Unknown, message, innerException) { }

    public ExcelDocumentBuilderException(ExcelDocumentBuilderErrorCode errorCode) : base($"Error code: {errorCode}")
    {
        ErrorCode = errorCode;
    }

    public ExcelDocumentBuilderException(Exception innerException, ExcelDocumentBuilderErrorCode errorCode) : base($"Error code: {errorCode}", innerException)
    {
        ErrorCode = errorCode;
    }

    public ExcelDocumentBuilderException(ExcelDocumentBuilderErrorCode errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }

    public ExcelDocumentBuilderException(ExcelDocumentBuilderErrorCode errorCode, string message, Exception innerException) : base(message, innerException)
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

    private ExcelDocumentBuilderException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        var errorCode = info.GetValue(nameof(ErrorCode), typeof(ExcelDocumentBuilderErrorCode)) ?? ExcelDocumentBuilderErrorCode.Unknown;
        ErrorCode = (ExcelDocumentBuilderErrorCode)errorCode;
    }
}
