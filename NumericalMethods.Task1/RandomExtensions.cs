using System;
using System.Collections.Generic;
using System.Linq;

namespace NumericalMethods.Task1
{
    public static class RandomExtensions
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

        public static IReadOnlyList<double> GenerateVector(this Random random, int count, double minValue, double maxValue, double eps)
        {
            _ = random ?? throw new ArgumentNullException(nameof(random));
            _ = count < 0 ? throw new ArgumentOutOfRangeException(nameof(count), "The number of elements must not be negative.") : true;
            eps = Math.Abs(eps);

            return Enumerable.Range(1, count).Select(x => random.NextDoubleNotZero(minValue, maxValue, eps)).ToArray();
        }
        
        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            _ = random ?? throw new ArgumentNullException(nameof(random));
            _ = double.IsFinite(minValue) ? true : throw new NotFiniteNumberException(minValue);
            _ = double.IsFinite(maxValue) ? true : throw new NotFiniteNumberException(maxValue);
            (minValue, maxValue) = minValue > maxValue ? (maxValue, minValue) : (minValue, maxValue);

            return random.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static int NextNotZero(this Random random, int minValue, int maxValue)
        {
            _ = random ?? throw new ArgumentNullException(nameof(random));
            (minValue, maxValue) = minValue > maxValue ? (maxValue, minValue) : (minValue, maxValue);

            int answer;
            do
                answer = random.Next(minValue, maxValue);
            while (answer == 0);

            return answer;
        }

        public static double NextDoubleNotZero(this Random random, double minValue, double maxValue, double eps)
        {
            _ = random ?? throw new ArgumentNullException(nameof(random));
            (minValue, maxValue) = minValue > maxValue ? (maxValue, minValue) : (minValue, maxValue);
            eps = Math.Abs(eps);

            double answer;
            do
                answer = random.NextDouble(minValue, maxValue);
            while (answer.CompareTo(0, eps) == 0);

            return answer;
        }
    }
}
