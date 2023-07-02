namespace DocumentBuilder.Exceptions;

public enum ExcelDocumentBuilderErrorCode
{
    Unknown,
    NoWorksheetInstantiated,
    WorksheetNotFound,
    MultipleWorksheetsFound,
    WorksheetNameAlreadyExists,
    IdentifierMustBeGreaterThanZero,
}
