using System;
using NumericalMethods.Core.Utils.Interfaces;

namespace NumericalMethods.Task1
{
    static class RandomProviderExtensions
    {
        public static T[,] GenerateMatrix<T>(this IRangedRandomProvider<T> random, int rowsCount, int columnsCount, T minValue, T maxValue)
        {
            _ = random ?? throw new ArgumentNullException(nameof(random));
            _ = rowsCount < 0 ? throw new ArgumentOutOfRangeException(nameof(rowsCount), "The number of rows must not be negative.") : true;
            _ = columnsCount < 0 ? throw new ArgumentOutOfRangeException(nameof(columnsCount), "The number of columns must not be negative.") : true;

            var matrix = new T[rowsCount, columnsCount];
            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                matrix[i, i] = random.Next(minValue, maxValue);

                if (i + 1 < matrix.GetLength(1))
                    matrix[i, i + 1] = random.Next(minValue, maxValue);

                if (i + 1 < matrix.GetLength(0))
                    matrix[i + 1, i] = random.Next(minValue, maxValue);

                matrix[i, 5] = random.Next(minValue, maxValue);

                matrix[i, 6] = random.Next(minValue, maxValue);
            }
            return matrix;
        }
    }
}
