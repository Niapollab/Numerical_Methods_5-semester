using System;
using NumericalMethods.Core.Extensions;

namespace NumericalMethods.Task1
{
    static class RandomExtensions
    {
        public static double[,] GenerateMatrix(this Random random, int rowsCount, int columnsCount, double minValue, double maxValue, double eps)
        {
            _ = random ?? throw new ArgumentNullException(nameof(random));
            _ = rowsCount < 0 ? throw new ArgumentOutOfRangeException(nameof(rowsCount), "The number of rows must not be negative.") : true;
            _ = columnsCount < 0 ? throw new ArgumentOutOfRangeException(nameof(columnsCount), "The number of columns must not be negative.") : true;
            eps = Math.Abs(eps);

            var matrix = new double[rowsCount, columnsCount];
            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                matrix[i, i] = random.NextDoubleNotZero(minValue, maxValue, eps);

                if (i + 1 < matrix.GetLength(1))
                    matrix[i, i + 1] = random.NextDoubleNotZero(minValue, maxValue + 1, eps);

                if (i + 1 < matrix.GetLength(0))
                    matrix[i + 1, i] = random.NextDoubleNotZero(minValue, maxValue + 1, eps);

                matrix[i, 5] = random.NextDoubleNotZero(minValue, maxValue + 1, eps);

                matrix[i, 6] = random.NextDoubleNotZero(minValue, maxValue + 1, eps);
            }
            return matrix;
        }
    }
}
