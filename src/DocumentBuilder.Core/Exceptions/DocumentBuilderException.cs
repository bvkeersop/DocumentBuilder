using System.Runtime.Serialization;

namespace DocumentBuilder.Exceptions
{
    [Serializable]
    public sealed class DocumentBuilderException : Exception
    {
        public DocumentBuilderErrorCode ErrorCode { get; init; }

        public DocumentBuilderException() : this(DocumentBuilderErrorCode.Unknown) { }

        public DocumentBuilderException(string message) : this(DocumentBuilderErrorCode.Unknown, message) { }

        public DocumentBuilderException(string message, Exception innerException) : this(DocumentBuilderErrorCode.Unknown, message, innerException) { }

        public DocumentBuilderException(DocumentBuilderErrorCode errorCode) : base($"Error code: {errorCode}")
        {
            ErrorCode = errorCode;
        }

        public DocumentBuilderException(Exception innerException, DocumentBuilderErrorCode errorCode) : base($"Error code: {errorCode}", innerException)
        {
            ErrorCode = errorCode;
        }

        public DocumentBuilderException(DocumentBuilderErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public DocumentBuilderException(DocumentBuilderErrorCode errorCode, string message, Exception innerException) : base(message, innerException)
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

        private DocumentBuilderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            var errorCode = info.GetValue(nameof(ErrorCode), typeof(DocumentBuilderErrorCode)) ?? DocumentBuilderErrorCode.Unknown;
            ErrorCode = (DocumentBuilderErrorCode) errorCode;
        }
    }
}
