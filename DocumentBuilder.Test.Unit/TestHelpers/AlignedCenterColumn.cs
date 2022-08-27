using DocumentBuilder.Enumerations;
using DocumentBuilder.Attributes;

namespace DocumentBuilder.Test.Unit.TestHelpers
{
    internal class AlignedCenterColumn : AlignedColumn
    {
        [Column(alignment: Alignment.Center)]
        public string ColumnName { get; set; } = "ColumnValue";
    }
}
