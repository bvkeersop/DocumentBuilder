using System.Runtime.Serialization;

namespace NDocument.Domain.Exceptions
{
    [Serializable]
    public sealed class NDocumentException : Exception
    {
        public NDocumentErrorCode ErrorCode { get; init; }

        public NDocumentException() : this(NDocumentErrorCode.Unknown) { }

        public NDocumentException(string message) : this(NDocumentErrorCode.Unknown, message) { }

        public NDocumentException(string message, Exception innerException) : this(NDocumentErrorCode.Unknown, message, innerException) { }

        public NDocumentException(NDocumentErrorCode errorCode) : base($"Error code: {errorCode}")
        {
            ErrorCode = errorCode;
        }

        public NDocumentException(Exception innerException, NDocumentErrorCode errorCode) : base($"Error code: {errorCode}", innerException)
        {
            ErrorCode = errorCode;
        }

        public NDocumentException(NDocumentErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public NDocumentException(NDocumentErrorCode errorCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        private NDocumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ErrorCode = (NDocumentErrorCode)info.GetValue(nameof(ErrorCode), typeof(NDocumentErrorCode));
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
    }
}
