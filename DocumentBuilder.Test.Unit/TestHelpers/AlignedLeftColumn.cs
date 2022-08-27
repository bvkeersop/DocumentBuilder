using DocumentBuilder.Enumerations;
using DocumentBuilder.Attributes;

namespace DocumentBuilder.Test.Unit.TestHelpers
{
    internal class AlignedLeftColumn : AlignedColumn
    {
        [Column(alignment: Alignment.Left)]
        public string ColumnName { get; set; } = "ColumnValue";
    }
}
