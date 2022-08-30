using DocumentBuilder.Enumerations;
using DocumentBuilder.Attributes;

namespace DocumentBuilder.Test.Unit.TestHelpers
{
    internal class AlignedCenterColumn
    {
        [Column(alignment: Alignment.Center)]
        public string ColumnName { get; set; } = "ColumnValue";
    }
}
