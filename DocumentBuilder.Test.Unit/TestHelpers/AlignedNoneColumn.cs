using DocumentBuilder.Enumerations;
using DocumentBuilder.Attributes;

namespace DocumentBuilder.Test.Unit.TestHelpers
{
    internal class AlignedNoneColumn
    {
        [Column(alignment: Alignment.None)]
        public string ColumnName { get; set; } = "ColumnValue";
    }
}
