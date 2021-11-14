using System;
using NumericalMethods.Core.Extensions;

namespace NumericalMethods.Task2
{
    static class RandomExtensions
    {
        public static double[,] GenerateBandedSymmetricMatrix(this Random random, int rowsCount, int columnsCount, int halfRibbonLength, double minValue, double maxValue, double eps)
        {
            _ = random ?? throw new ArgumentNullException(nameof(random));
            _ = rowsCount < 0 ? throw new ArgumentOutOfRangeException(nameof(rowsCount), "The number of rows must not be negative.") : true;
            _ = columnsCount < 0 ? throw new ArgumentOutOfRangeException(nameof(columnsCount), "The number of columns must not be negative.") : true;
            _ = halfRibbonLength < 0 || halfRibbonLength > columnsCount || halfRibbonLength > rowsCount ? throw new ArgumentOutOfRangeException(nameof(columnsCount), "Half Ribbon Length must be greater than zero and less or equal than rows and columns count.") : true;
            eps = Math.Abs(eps);

            var matrix = new double[rowsCount, columnsCount];
            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                int columnStartsIndex = GetColumnStartsIndex(i);
                int columnEndsIndex = columnStartsIndex + GetRowLength(i);

                for (var j = columnStartsIndex; j <= columnEndsIndex; ++j)
                    SetSemetric(matrix, random.NextDoubleNotZero(minValue, maxValue, eps), i, j);
            }

            return matrix;

            void SetSemetric<T>(T[,] matrix, T value, int rowIndex, int columnIndex)
                => matrix[rowIndex, columnIndex] = matrix[columnIndex, rowIndex] = value;

            int GetColumnStartsIndex(int rowIndex)
                => Math.Max(rowIndex - halfRibbonLength, 0);

            int GetRowLength(int rowIndex)
                => Math.Min(halfRibbonLength, rowIndex);
        }
    }
}