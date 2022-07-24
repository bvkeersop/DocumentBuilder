namespace NDocument.Domain.Model
{
    public class TableCell
    {
        public string Value { get; }
        public Type Type { get; }

        public TableCell(string value, Type propertyInfo)
        {
            Value = value;
            Type = propertyInfo;
        }
    }
}
