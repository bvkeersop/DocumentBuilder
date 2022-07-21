using NDocument.Domain.Attributes;
using NDocument.Domain.Enumerations;

namespace NDocument.Domain.Test.Unit.TestHelpers
{
    internal class AlignedLeftColumn : AlignedColumn
    {
        [Column(alignment: Alignment.Left)]
        public string ColumnName { get; set; } = "ColumnValue";
    }
}
