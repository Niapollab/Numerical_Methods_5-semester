using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethods.Task1
{
    public static class MatrixExtensions
    {
        public static string ToString(this double[,] matrix, int digitsAfterComma = 2, char space = '\t')
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = digitsAfterComma < 0 ? throw new ArgumentOutOfRangeException(nameof(digitsAfterComma), "The numbers after the decimal point must be positive.") : true;

            return matrix.ToString((i, j, value) => Math.Round(value, digitsAfterComma).ToString(), space);
        }

        public static string ToString(this float[,] matrix, int digitsAfterComma = 2, char space = '\t')
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = digitsAfterComma < 0 ? throw new ArgumentOutOfRangeException(nameof(digitsAfterComma), "The numbers after the decimal point must be positive.") : true;

            return matrix.ToString((i, j, value) => Math.Round(value, digitsAfterComma).ToString(), space);
        }

        public static string ToString(this decimal[,] matrix, int digitsAfterComma = 2, char space = '\t')
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = digitsAfterComma < 0 ? throw new ArgumentOutOfRangeException(nameof(digitsAfterComma), "The numbers after the decimal point must be positive.") : true;

            return matrix.ToString((i, j, value) => Math.Round(value, digitsAfterComma).ToString(), space);
        }

        public static string ToString<T>(this T[,] matrix, char space = '\t')
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));

            return matrix.ToString((i, j, value) => value.ToString(), space);
        }

        public static string ToString<T>(this T[,] matrix, Func<int, int, T, string> stringSelector, char space = '\t')
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = stringSelector ?? throw new ArgumentNullException(nameof(stringSelector));
            
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

        public static double[,] InsertColumn(this double[,] matrix, IReadOnlyList<double> column, int index = -1)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = column ?? throw new ArgumentNullException(nameof(column));
            _ = matrix.GetLength(0) != column.Count ? throw new ArgumentException("Column size is larger than the original matrix column size.") : true;
            
            index = index > -1 && index < matrix.GetLength(1) ? index : matrix.GetLength(1);
        
            var newMatrix = new double[matrix.GetLength(0), matrix.GetLength(1) + 1];
            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                for (var j = 0; j < index; ++j)
                    newMatrix[i, j] = matrix[i, j];

                newMatrix[i, index] = column[i];

                for (var j = index; j < matrix.GetLength(1); ++j)
                    newMatrix[i, j + 1] = matrix[i, j];
            }

            return newMatrix;
        }

        public static double[,] InsertRow(this double[,] matrix, IReadOnlyList<double> row, int index = -1)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = row ?? throw new ArgumentNullException(nameof(row));
            _ = matrix.GetLength(1) != row.Count ? throw new ArgumentException("Row size is larger than the original matrix row size.") : true;
            
            index = index > -1 && index < matrix.GetLength(0) ? index : matrix.GetLength(0);
        
            var newMatrix = new double[matrix.GetLength(0) + 1, matrix.GetLength(1)];
            for (var j = 0; j < matrix.GetLength(1); ++j)
            {
                for (var i = 0; i < index; ++i)
                    newMatrix[i, j] = matrix[i, j];

                newMatrix[index, j] = row[j];

                for (var i = index; i < matrix.GetLength(0); ++i)
                    newMatrix[i + 1, j] = matrix[i, j];
            }

            return newMatrix;
        }
    }
}
