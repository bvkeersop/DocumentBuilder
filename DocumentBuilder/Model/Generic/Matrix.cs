﻿using DocumentBuilder.Exceptions;
using DocumentBuilder.Helpers;
using System.Reflection;

namespace DocumentBuilder.Model.Generic
{
    public class Matrix<TValue>
    {
        private TableCell[][] Values { get; }
        public int NumberOfRows { get; }
        public int NumberOfColumns { get; }
        public IEnumerable<TableCell> TableCells
        {
            get { return _tableCells; }
        }

        private readonly Dictionary<int, int> _longestCellSizeOfColumn = new();
        private readonly IEnumerable<TableCell> _tableCells = Enumerable.Empty<TableCell>();

        public Matrix(IEnumerable<TValue> tableRows)
        {
            var orderedPropertyInfos = ReflectionHelper<TValue>.GetOrderedTableRowPropertyInfos();

            NumberOfRows = tableRows.Count();
            NumberOfColumns = orderedPropertyInfos.Count();

            var matrix = new TableCell[NumberOfRows][];

            for (var i = 0; i < NumberOfRows; i++)
            {
                matrix[i] = new TableCell[NumberOfColumns];
                for (var j = 0; j < NumberOfColumns; j++)
                {
                    var currentProperty = orderedPropertyInfos.ElementAt(j);
                    var currentTableRow = tableRows.ElementAt(i);

                    if (currentTableRow == null)
                    {
                        throw new DocumentBuilderException(DocumentBuilderErrorCode.CouldNotFindTableRowAtIndex, $"Could not find table row at index {i}");
                    }

                    var cellValue = GetTableCellValue(currentProperty, currentTableRow);
                    var cellType = GetTableCellType(currentProperty);
                    var tableCell = new TableCell(cellValue, cellType, i, j);
                    matrix[i][j] = tableCell;
                    _tableCells = _tableCells.Append(tableCell);
                    CreateOrUpdateLongestCellSizeOfColumn(cellValue, j);
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

        private static string GetTableCellValue(PropertyInfo properyInfo, TValue tableRow)
        {
            return properyInfo.GetValue(tableRow)?.ToString() ?? string.Empty;
        }

        private static Type GetTableCellType(PropertyInfo properyInfo)
        {
            return properyInfo.PropertyType;
        }

        public TableCell[] GetColumn(int index)
        {
            return Values.Select(v => v[index]).ToArray();
        }

        public TableCell[] GetRow(int index)
        {
            return Values[index];
        }

        public TableCell GetValue(int rowIndex, int columnIndex)
        {
            return Values[rowIndex][columnIndex];
        }

        public int GetLongestCellSizeOfColumn(int columnIndex)
        {
            // If there's no longest size cell size, there's no rows, thus the longest size is 0
            if (_longestCellSizeOfColumn.Count == 0)
            {
                return 0;
            }

            if (columnIndex < 0 || columnIndex > NumberOfColumns)
            {
                throw new DocumentBuilderException(DocumentBuilderErrorCode.CouldNotFindColumnAtIndex);
            }

            return _longestCellSizeOfColumn[columnIndex];
        }
    }
}
