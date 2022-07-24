namespace NDocument.Domain.Options
{
    public class ExcelDocumentOptions
    {
        /// <summary>
        /// The name of the excel worksheet
        /// </summary>
        public string? WorksheetName { get; set; }

        /// <summary>
        /// The path where the excel document should be written to, use the .xlsx extension
        /// </summary>
        public string? FilePath { get; set; }
    }
}
