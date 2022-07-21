using NDocument.Domain.Attributes;
using NDocument.Domain.Enumerations;

namespace NDocument.Domain.Test.Unit.TestHelpers
{
    internal class AlignedRightColumn : AlignedColumn
    {
        [Column(alignment: Alignment.Right)]
        public string ColumnName { get; set; } = "ColumnValue";
    }
}
