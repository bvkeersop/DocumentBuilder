namespace DocumentBuilder.Exceptions;

public enum HtmlDocumentBuilderErrorCode
{
    Unknown,
    AttributeNotAllowed,
    NoDivElementToClose,
    AttemptedToAddDuplicateUniqueHtmlAttribute,
}
