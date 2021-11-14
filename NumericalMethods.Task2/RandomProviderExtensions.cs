using System;
using NumericalMethods.Core.Utils.Interfaces;

namespace NumericalMethods.Task2
{
    static class RandomProviderExtensions
    {
        public static T[,] GenerateBandedSymmetricMatrix<T>(this IRandomProvider<T> random, int rowsCount, int columnsCount, int halfRibbonLength, T minValue, T maxValue)
        {
            _ = random ?? throw new ArgumentNullException(nameof(random));
            _ = rowsCount < 0 ? throw new ArgumentOutOfRangeException(nameof(rowsCount), "The number of rows must not be negative.") : true;
            _ = columnsCount < 0 ? throw new ArgumentOutOfRangeException(nameof(columnsCount), "The number of columns must not be negative.") : true;
            _ = halfRibbonLength < 0 || halfRibbonLength > columnsCount || halfRibbonLength > rowsCount ? throw new ArgumentOutOfRangeException(nameof(columnsCount), "Half Ribbon Length must be greater than zero and less or equal than rows and columns count.") : true;

            var matrix = new T[rowsCount, columnsCount];
            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                int columnStartsIndex = GetColumnStartsIndex(i);
                int columnEndsIndex = columnStartsIndex + GetRowLength(i);

                for (var j = columnStartsIndex; j <= columnEndsIndex; ++j)
                    SetSemetric(matrix, random.Next(minValue, maxValue), i, j);
            }

            return matrix;

            void SetSemetric<G>(G[,] matrix, G value, int rowIndex, int columnIndex)
                => matrix[rowIndex, columnIndex] = matrix[columnIndex, rowIndex] = value;

            int GetColumnStartsIndex(int rowIndex)
                => Math.Max(rowIndex - halfRibbonLength, 0);

            int GetRowLength(int rowIndex)
                => Math.Min(halfRibbonLength, rowIndex);
        }
    }
}