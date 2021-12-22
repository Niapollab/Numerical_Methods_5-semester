using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethods.Core.Extensions
{
    public static class MatrixExtensions
    {
        #region ToString Methods
        public static string ToString(this double[,] matrix, int digitsAfterComma = 2, string separator = "\t")
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = digitsAfterComma < 0 ? throw new ArgumentOutOfRangeException(nameof(digitsAfterComma), "The numbers after the decimal point must be positive.") : true;
            _ = separator ?? throw new ArgumentNullException(nameof(separator));

            return matrix.ToString((i, j, value) => Math.Round(value, digitsAfterComma).ToString(), separator);
        }

        public static string ToString(this float[,] matrix, int digitsAfterComma = 2, string separator = "\t")
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = digitsAfterComma < 0 ? throw new ArgumentOutOfRangeException(nameof(digitsAfterComma), "The numbers after the decimal point must be positive.") : true;
            _ = separator ?? throw new ArgumentNullException(nameof(separator));

            return matrix.ToString((i, j, value) => Math.Round(value, digitsAfterComma).ToString(), separator);
        }

        public static string ToString(this decimal[,] matrix, int digitsAfterComma = 2, string separator = "\t")
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = digitsAfterComma < 0 ? throw new ArgumentOutOfRangeException(nameof(digitsAfterComma), "The numbers after the decimal point must be positive.") : true;
            _ = separator ?? throw new ArgumentNullException(nameof(separator));

            return matrix.ToString((i, j, value) => Math.Round(value, digitsAfterComma).ToString(), separator);
        }

        public static string ToString<T>(this T[,] matrix, string separator = "\t")
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = separator ?? throw new ArgumentNullException(nameof(separator));

            return matrix.ToString((i, j, value) => value.ToString(), separator);
        }

        public static string ToString<T>(this T[,] matrix, Func<int, int, T, string> stringSelector, string separator = "\t")
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = stringSelector ?? throw new ArgumentNullException(nameof(stringSelector));
            _ = separator ?? throw new ArgumentNullException(nameof(separator));
            
            var builder = new StringBuilder();

            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                for (var j = 0; j < matrix.GetLength(1); ++j)
                    builder.Append(stringSelector(i, j, matrix[i, j])).Append(separator);
                builder.Remove(builder.Length - separator.Length, separator.Length);
                builder.Append(Environment.NewLine);
            }
            builder.Remove(builder.Length - Environment.NewLine.Length, Environment.NewLine.Length);

            return builder.ToString();
        }
        #endregion
        
        #region Sum Methods
        public static double[,] Sum(this double[,] matrix, double[,] other)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = other ?? throw new ArgumentNullException(nameof(other));

            return matrix.Sum(other, (x, y) => x + y);
        }
        
        public static float[,] Sum(this float[,] matrix, float[,] other)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = other ?? throw new ArgumentNullException(nameof(other));

            return matrix.Sum(other, (x, y) => x + y);
        }
        
        public static decimal[,] Sum(this decimal[,] matrix, decimal[,] other)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = other ?? throw new ArgumentNullException(nameof(other));

            return matrix.Sum(other, (x, y) => x + y);
        }

        public static T[,] Sum<T>(this T[,] matrix, T[,] other, Func<T, T, T> summator)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = other ?? throw new ArgumentNullException(nameof(other));
            _ = summator ?? throw new ArgumentNullException(nameof(summator));
            _ = matrix.GetLength(0) != other.GetLength(0) || matrix.GetLength(1) != other.GetLength(1) ? throw new ArgumentException("Matrix sizes must be equals.", nameof(matrix)) : true;

            var result = new T[matrix.GetLength(0), matrix.GetLength(1)];
            for (var i = 0; i < matrix.GetLength(0); ++i)
                for (var j = 0; j < matrix.GetLength(1); ++j)
                    result[i, j] = summator(matrix[i, j], other[i, j]);

            return result;
        }
        #endregion

        #region Subtract Methods
        public static double[,] Subtract(this double[,] matrix, double[,] other)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = other ?? throw new ArgumentNullException(nameof(other));

            return matrix.Subtract(other, (x, y) => x - y);
        }
        
        public static float[,] Subtract(this float[,] matrix, float[,] other)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = other ?? throw new ArgumentNullException(nameof(other));

            return matrix.Subtract(other, (x, y) => x - y);
        }
        
        public static decimal[,] Subtract(this decimal[,] matrix, decimal[,] other)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = other ?? throw new ArgumentNullException(nameof(other));

            return matrix.Subtract(other, (x, y) => x - y);
        }

        public static T[,] Subtract<T>(this T[,] matrix, T[,] other, Func<T, T, T> subtractor)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = other ?? throw new ArgumentNullException(nameof(other));
            _ = subtractor ?? throw new ArgumentNullException(nameof(subtractor));
            _ = matrix.GetLength(0) != other.GetLength(0) || matrix.GetLength(1) != other.GetLength(1) ? throw new ArgumentException("Matrix sizes must be equals.", nameof(matrix)) : true;

            var result = new T[matrix.GetLength(0), matrix.GetLength(1)];
            for (var i = 0; i < matrix.GetLength(0); ++i)
                for (var j = 0; j < matrix.GetLength(1); ++j)
                    result[i, j] = subtractor(matrix[i, j], other[i, j]);

            return result;
        }
        #endregion

        #region Multiplicate Methods
        public static double[,] Multiplicate(this double[,] matrix, double other)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));

            return matrix.Multiplicate(other, (x, y) => x * y);
        }
        
        public static float[,] Multiplicate(this float[,] matrix, float other)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));

            return matrix.Multiplicate(other, (x, y) => x * y);
        }
        
        public static decimal[,] Multiplicate(this decimal[,] matrix, decimal other)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));

            return matrix.Multiplicate(other, (x, y) => x * y);
        }

        public static double[,] Multiplicate(this double[,] matrix, double[,] other)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = other ?? throw new ArgumentNullException(nameof(other));

            return matrix.Multiplicate(other, (x, y) => x + y, (x, y) => x * y);
        }
        
        public static float[,] Multiplicate(this float[,] matrix, float[,] other)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = other ?? throw new ArgumentNullException(nameof(other));

            return matrix.Multiplicate(other, (x, y) => x + y, (x, y) => x * y);
        }
        
        public static decimal[,] Multiplicate(this decimal[,] matrix, decimal[,] other)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = other ?? throw new ArgumentNullException(nameof(other));

            return matrix.Multiplicate(other, (x, y) => x + y, (x, y) => x * y);
        }

        public static T[,] Multiplicate<T>(this T[,] matrix, T other, Func<T, T, T> multiplicator)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = multiplicator ?? throw new ArgumentNullException(nameof(multiplicator));

            var result = new T[matrix.GetLength(0), matrix.GetLength(1)];
            for (var i = 0; i < matrix.GetLength(0); ++i)
                for (var j = 0; j < matrix.GetLength(1); ++j)
                    result[i, j] = multiplicator(matrix[i, j], other);

            return result;
        }

        public static T[,] Multiplicate<T>(this T[,] matrix, T[,] other, Func<T, T, T> summator, Func<T, T, T> multiplicator)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = other ?? throw new ArgumentNullException(nameof(other));
            _ = summator ?? throw new ArgumentNullException(nameof(summator));
            _ = multiplicator ?? throw new ArgumentNullException(nameof(multiplicator));
            _ = matrix.GetLength(1) != other.GetLength(0) ? throw new ArgumentException("The number of columns in the first matrix must be equal to the number of rows in the second.", nameof(matrix)) : true;

            var result = new T[matrix.GetLength(0), other.GetLength(1)];
            for (var i = 0; i < matrix.GetLength(0); ++i)
                for (var j = 0; j < other.GetLength(1); ++j)
                    for (var k = 0; k < other.GetLength(0); ++k)
                        result[i, j] = summator(result[i, j], multiplicator(matrix[i, k], other[k, j]));

            return result;
        }
        #endregion

        public static T[,] Transpose<T>(this T[,] matrix)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));

            var result = new T[matrix.GetLength(1), matrix.GetLength(0)];
            for (var i = 0; i < result.GetLength(0); ++i)
                for (var j = 0; j < result.GetLength(1); ++j)
                    result[i, j] = matrix[j, i];

            return result;
        }

        public static T[,] InsertColumn<T>(this T[,] matrix, IReadOnlyList<T> column, int index = -1)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = column ?? throw new ArgumentNullException(nameof(column));
            _ = matrix.GetLength(0) != column.Count ? throw new ArgumentException("Column size is larger than the original matrix column size.") : true;

            index = index > -1 && index < matrix.GetLength(1) ? index : matrix.GetLength(1);

            var newMatrix = new T[matrix.GetLength(0), matrix.GetLength(1) + 1];
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

        public static T[,] InsertRow<T>(this T[,] matrix, IReadOnlyList<T> row, int index = -1)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = row ?? throw new ArgumentNullException(nameof(row));
            _ = matrix.GetLength(1) != row.Count ? throw new ArgumentException("Row size is larger than the original matrix row size.") : true;

            index = index > -1 && index < matrix.GetLength(0) ? index : matrix.GetLength(0);

            var newMatrix = new T[matrix.GetLength(0) + 1, matrix.GetLength(1)];
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
