using NDocument.Domain.Exceptions;
using NDocument.Domain.Helpers;
using System.Reflection;

namespace NDocument.Domain.Model
{
    public class Matrix<T>
    {
        private string[][] Values { get; }
        public int NumberOfRows { get; }
        public int NumberOfColumns { get; }

        public int LongestCellSize { get; private set; }

        private readonly Dictionary<int, int> _longestCellSizeOfColumn = new Dictionary<int, int>();

        public Matrix(IEnumerable<T> tableRows)
        {
            var orderedPropertyInfos = ReflectionHelper<T>.GetOrderedTableRowPropertyInfos(tableRows);

            NumberOfRows = tableRows.Count();
            NumberOfColumns = orderedPropertyInfos.Count();

            var matrix = new string[NumberOfRows][];

            for (var i = 0; i < NumberOfRows; i++)
            {
                matrix[i] = new string[NumberOfColumns];
                for (var j = 0; j < NumberOfColumns; j++)
                {
                    var currentProperty = orderedPropertyInfos.ElementAt(j);
                    var currentTableRow = tableRows.ElementAt(i);

                    if (currentTableRow == null)
                    {
                        throw new NDocumentException(NDocumentErrorCode.CouldNotFindTableRowAtIndex, $"Could not find table row at index {i}");
                    }

                    var cellValue = GetTableCellValue(currentProperty, currentTableRow);
                    matrix[i][j] = cellValue;
                    CreateOrUpdateLongestCellSizeOfColumn(cellValue, j);
                    UpdateLongestCellSize(cellValue);
                }
            }

            Values = matrix;
        }

        private void CreateOrUpdateLongestCellSizeOfColumn(string cellValue, int columnIndex)
        {
            bool entryExists = _longestCellSizeOfColumn.TryGetValue(columnIndex, out var currentLongestSize);

            if (!entryExists)
            {
                _longestCellSizeOfColumn.Add(columnIndex, cellValue.Length);
                return;
            }

            if (cellValue.Length > currentLongestSize)
            {
                _longestCellSizeOfColumn[columnIndex] = cellValue.Length;
            }
        }

        private void UpdateLongestCellSize(string cellValue)
        {
            if (cellValue.Length > LongestCellSize)
            {
                LongestCellSize = cellValue.Length;
            }
        }

        private static string GetTableCellValue(PropertyInfo properyInfo, object tableRow)
        {
            var value = properyInfo?.GetValue(tableRow);

            if (value is null)
            {
                return string.Empty;
            }

            return value.ToString();
        }

        public string[] GetColumn(int index)
        {
            return Values.Select(v => v[index]).ToArray();
        }

        public string[] GetRow(int index)
        {
            return Values[index];
        }

        public string GetValue(int rowIndex, int columnIndex)
        {
            return Values[rowIndex][columnIndex];
        }

        public int GetLongestCellSizeOfColumn(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex > NumberOfColumns)
            {
                throw new NDocumentException(NDocumentErrorCode.CouldNotFindColumnAtIndex);
            }

            return _longestCellSizeOfColumn[columnIndex];
        }
    }
}
