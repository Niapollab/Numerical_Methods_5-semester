using NumericalMethods.Core.Utils.Interfaces;
using System;

namespace NumericalMethods.Task3
{
    static class RandomProviderExtensions
    {
        public static T[,] GenerateSymmetricMatrix<T>(this IRandomProvider<T> random, int rowsCount, int columnsCount, T minValue, T maxValue)
        {
            _ = random ?? throw new ArgumentNullException(nameof(random));
            _ = rowsCount < 0 ? throw new ArgumentOutOfRangeException(nameof(rowsCount), "The number of rows must not be negative.") : true;
            _ = columnsCount < 0 ? throw new ArgumentOutOfRangeException(nameof(columnsCount), "The number of columns must not be negative.") : true;

            var matrix = new T[rowsCount, columnsCount];
            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                for (var j = 0; j < i; ++j)
                    SetSymmetric(i, j, random.Next(minValue, maxValue));

                matrix[i, i] = random.Next(minValue, maxValue);
            }

            return matrix;

            void SetSymmetric(int rowIndex, int columnIndex, T value)
            {
                matrix[rowIndex, columnIndex] = random.Next(minValue, maxValue);
                matrix[columnIndex, rowIndex] = random.Next(minValue, maxValue);
            }
        }
    }
}
