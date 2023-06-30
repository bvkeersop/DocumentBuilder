namespace DocumentBuilder.Exceptions
{
    public enum DocumentBuilderErrorCode
    {
        Unknown,
        ProvidedTableIsEmpty,
        StreamIsNotWriteable,
        CouldNotFindColumnAtIndex,
        CouldNotFindTableRowAtIndex,
        ColumnHasNoName,
        IdentifierMustBeGreaterThanZero,
        NoWorksheetInstantiated,
        ProvidedEnumerableIsEmpty,
        ProvidedGenericTypeForTableDoesNotEqualRunTimeType,
        AttemptedToAddDuplicateUniqueHtmlAttribute,
        NoHtmlElementAdded,
        MultipleWorksheetsFound,
        WorksheetNotFound,
        WorksheetNameAlreadyExists,
        NoDivElementToClose,
    }
}
