using System;
using System.Text;

namespace NumericalMethods.Task1
{
    public static class MatrixExtensions
    {
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
                    builder.Append(matrix[i, j].ToString()).Append(space);
                builder.Remove(builder.Length - 1, 1);
                builder.Append(Environment.NewLine);
            }
            builder.Remove(builder.Length - Environment.NewLine.Length, Environment.NewLine.Length);

            return builder.ToString();
        }
    }
}
