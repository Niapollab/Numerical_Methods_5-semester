using System;
using System.Text;

namespace NumericalMethods.Task1.PoC
{
    public static class MatrixExtensions
    {
        public static double[,] DivideBy(this double[,] matrix, int lineIndex, double element)
        {
            if (element == 0)
                throw new DivideByZeroException("");

            if (lineIndex < 0 || lineIndex >= matrix.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(lineIndex), "");

            for (var j = 0; j < matrix.GetLength(1); ++j)
                matrix[lineIndex, j] /= element;
            
            return matrix;
        }

        public static float[,] DivideBy(this float[,] matrix, int lineIndex, float element)
        {
            if (element == 0)
                throw new DivideByZeroException("");

            if (lineIndex < 0 || lineIndex >= matrix.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(lineIndex), "");

            for (var j = 0; j < matrix.GetLength(1); ++j)
                matrix[lineIndex, j] /= element;
            
            return matrix;
        }

        public static decimal[,] DivideBy(this decimal[,] matrix, int lineIndex, decimal element)
        {
            if (element == 0)
                throw new DivideByZeroException("");

            if (lineIndex < 0 || lineIndex >= matrix.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(lineIndex), "");

            for (var j = 0; j < matrix.GetLength(1); ++j)
                matrix[lineIndex, j] /= element;
            
            return matrix;
        }

        public static double[,] SubLines(this double[,] matrix, int firstLineIndex, int secondLineIndex)
        {
            if (firstLineIndex < 0 || firstLineIndex >= matrix.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(firstLineIndex), "");
            
            if (secondLineIndex < 0 || secondLineIndex >= matrix.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(secondLineIndex), "");
            
            for (var j = 0; j < matrix.GetLength(1); ++j)
                matrix[firstLineIndex, j] -= matrix[secondLineIndex, j];

            return matrix;
        }

        public static float[,] SubLines(this float[,] matrix, int firstLineIndex, int secondLineIndex)
        {
            if (firstLineIndex < 0 || firstLineIndex >= matrix.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(firstLineIndex), "");
            
            if (secondLineIndex < 0 || secondLineIndex >= matrix.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(secondLineIndex), "");
            
            for (var j = 0; j < matrix.GetLength(1); ++j)
                matrix[firstLineIndex, j] -= matrix[secondLineIndex, j];

            return matrix;
        }

        public static decimal[,] SubLines(this decimal[,] matrix, int firstLineIndex, int secondLineIndex)
        {
            if (firstLineIndex < 0 || firstLineIndex >= matrix.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(firstLineIndex), "");
            
            if (secondLineIndex < 0 || secondLineIndex >= matrix.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(secondLineIndex), "");
            
            for (var j = 0; j < matrix.GetLength(1); ++j)
                matrix[firstLineIndex, j] -= matrix[secondLineIndex, j];

            return matrix;
        }

        public static string ToString(this double[,] matrix, int digitsAfterComma = 2, char space = '\t')
        {
            return matrix.ToString((i, j, value) => Math.Round(value, digitsAfterComma).ToString(), space);
        }

        public static string ToString(this float[,] matrix, int digitsAfterComma = 2, char space = '\t')
        {
            return matrix.ToString((i, j, value) => Math.Round(value, digitsAfterComma).ToString(), space);
        }

        public static string ToString(this decimal[,] matrix, int digitsAfterComma = 2, char space = '\t')
        {
            return matrix.ToString((i, j, value) => Math.Round(value, digitsAfterComma).ToString(), space);
        }

        public static string ToString<T>(this T[,] matrix, char space = '\t')
        {
            return matrix.ToString((i, j, value) => value.ToString(), space);
        }

        public static string ToString<T>(this T[,] matrix, Func<int, int, T, string> stringSelector, char space = '\t')
        {
            if (stringSelector is null)
                throw new ArgumentNullException(nameof(stringSelector));
            
            var builder = new StringBuilder();

            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                for (var j = 0; j < matrix.GetLength(1); ++j)
                    builder.Append(stringSelector(i, j, matrix[i, j])).Append(space);
                builder.Remove(builder.Length - 1, 1);
                builder.Append(Environment.NewLine);
            }
            builder.Remove(builder.Length - Environment.NewLine.Length, Environment.NewLine.Length);

            return builder.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var matrix = new double[,]
            {
                {-9, -6, 0, 0, 0, -4, -7, 0, 0, 0, -37},
                {-5, 2, 10, 0, 0, 1, -2, 0, 0, 0, 5},
                {0, -7, -7, -2, 0, -7, -6, 0, 0, 0, -42},
                {0, 0, 6, -1, -1, -1, -2, 0, 0, 0, -2},
                {0, 0, 0, -4, 1, 1, 0, 0, 0, 0, -1},
                {0, 0, 0, 0, 9, -7, 8, 0, 0, 0, 11},
                {0, 0, 0, 0, 0, 5, 0, -8, 0, 0, -6},
                {0, 0, 0, 0, 0, 10, 2, 3, 8, 0, 46},
                {0, 0, 0, 0, 0, -2, 6, 8, 6, 8, 52},
                {0, 0, 0, 0, 0, 6, -1, 0, -6, -8, -18}
            };

            // First stage
            for (var i = 0; i < 6; ++i)
            {
                if (matrix[i, i] == 0)
                    continue;

                matrix.DivideBy(i, matrix[i, i]);

                if (matrix[i + 1, i] == 0)
                    continue;
                
                matrix.DivideBy(i + 1, matrix[i + 1, i]);

                matrix.SubLines(i + 1, i);
            }

            // Second stage
            for (var i = matrix.GetLength(0) - 1; i > 6; --i)
            {
                if (matrix[i, i] == 0)
                    continue;
                
                matrix.DivideBy(i, matrix[i, i]);

                if (matrix[i - 1, i] == 0)
                    continue;

                matrix.DivideBy(i - 1, matrix[i - 1, i]);

                matrix.SubLines(i - 1, i);
            }
            if (matrix[6, 6] != 0)
                matrix.DivideBy(6, matrix[6, 6]);

            // Third phase
            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                if (i != 5 && matrix[i, 5] != 0)
                {
                    matrix.DivideBy(i, matrix[i, 5]);
                    matrix.SubLines(i, 5);
                }
            }

            Console.WriteLine(matrix.ToString(2));
            Console.ReadKey(true);
        }
    }
}
