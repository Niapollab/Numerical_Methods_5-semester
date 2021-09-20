using System;
using System.Text;
using NumericalMethods.Core.Interfaces;

namespace NumericalMethods.Core.Extensions
{
    public static class SystemOfEquationsExtensions
    {
        public static ILeakyMatrix<double> Invert(this ILeakyMatrix<double> matrix)
        {
            matrix.MultiplyBy(-1);
            return matrix;
        }

        public static ILeakyMatrix<double> Invert(this ILeakyMatrix<double> matrix, int lineIndex)
        {
            matrix.MultiplyBy(lineIndex, -1);
            return matrix;
        }

        public static ILeakyMatrix<double> SumLines(this ILeakyMatrix<double> matrix, int firstLineIndex, int secondLineIndex)
        {
            if (firstLineIndex < 0 || firstLineIndex >= matrix.RowsCount)
                throw new ArgumentOutOfRangeException(nameof(firstLineIndex), "");
            
            if (secondLineIndex < 0 || secondLineIndex >= matrix.RowsCount)
                throw new ArgumentOutOfRangeException(nameof(secondLineIndex), "");
            
            for (var j = 0; j < matrix.ColumnsCount; ++j)
                matrix[secondLineIndex, j] += matrix[firstLineIndex, j];

            return matrix;
        }

        public static ILeakyMatrix<double> SubLines(this ILeakyMatrix<double> matrix, int firstLineIndex, int secondLineIndex)
        {
            if (firstLineIndex < 0 || firstLineIndex >= matrix.RowsCount)
                throw new ArgumentOutOfRangeException(nameof(firstLineIndex), "");
            
            if (secondLineIndex < 0 || secondLineIndex >= matrix.RowsCount)
                throw new ArgumentOutOfRangeException(nameof(secondLineIndex), "");
            
            for (var j = 0; j < matrix.ColumnsCount; ++j)
                matrix[firstLineIndex, j] -= matrix[secondLineIndex, j];

            return matrix;
        }

        public static ILeakyMatrix<double> SaveLine(this ILeakyMatrix<double> matrix, int lineIndex, out double[] line)
        {
            if (lineIndex < 0 || lineIndex >= matrix.RowsCount)
                throw new ArgumentOutOfRangeException(nameof(lineIndex), "");

            line = new double[matrix.ColumnsCount];
            
            for (var j = 0; j < matrix.ColumnsCount; ++j)
                line[j] = matrix[lineIndex, j];

            return matrix;
        }

        public static ILeakyMatrix<double> LoadLine(this ILeakyMatrix<double> matrix, int lineIndex, double[] line)
        {
            if (line is null)
                throw new ArgumentNullException(nameof(line));

            if (line.Length != matrix.ColumnsCount)
                throw new ArgumentOutOfRangeException(nameof(lineIndex), "");

            for (var j = 0; j < matrix.ColumnsCount; ++j)
                matrix[lineIndex, j] = line[j];

            return matrix;
        }
        
        public static ILeakyMatrix<double> MultiplyBy(this ILeakyMatrix<double> matrix, int lineIndex, double element)
        {
            if (lineIndex < 0 || lineIndex >= matrix.RowsCount)
                throw new ArgumentOutOfRangeException(nameof(lineIndex), "");

            for (var j = 0; j < matrix.ColumnsCount; ++j)
                matrix[lineIndex, j] *= element;
            
            return matrix;
        }

        public static ILeakyMatrix<double> MultiplyBy(this ILeakyMatrix<double> matrix, double element)
        {
            for (var i = 0; i < matrix.RowsCount; ++i)
                matrix.DivideBy(i, element);
            
            return matrix;
        }

        public static ILeakyMatrix<double> DivideBy(this ILeakyMatrix<double> matrix, int lineIndex, double element)
        {
            if (element == 0)
                throw new DivideByZeroException("");

            if (lineIndex < 0 || lineIndex >= matrix.RowsCount)
                throw new ArgumentOutOfRangeException(nameof(lineIndex), "");

            for (var j = 0; j < matrix.ColumnsCount; ++j)
                matrix[lineIndex, j] /= element;
            
            return matrix;
        }

        public static ILeakyMatrix<double> DivideBy(this ILeakyMatrix<double> matrix, double element)
        {
            for (var i = 0; i < matrix.RowsCount; ++i)
                matrix.DivideBy(i, element);
            
            return matrix;
        }

        public static ILeakyMatrix<double> SafeDivideBy(this ILeakyMatrix<double> matrix, int lineIndex, double element)
        {
            try
            {
                matrix.DivideBy(lineIndex, element);
            }
            catch (DivideByZeroException)
            {

            }
            return matrix;
        }

        public static ILeakyMatrix<double> SafeDivideBy(this ILeakyMatrix<double> matrix, double element)
        {
            try
            {
                matrix.DivideBy(element);
            }
            catch
            {

            }
            return matrix;
        }

        public static string ToMatrixString(this ILeakyMatrix<double> matrix, int digitsAfterComma = 2, char space = '\t')
        {            
            var builder = new StringBuilder();
            
            for (var i = 0; i < matrix.RowsCount; ++i)
            {
                for (var j = 0; j < matrix.ColumnsCount; ++j)
                {
                    builder.Append(Math.Round(matrix[i, j], digitsAfterComma)).Append(space);
                }
                builder.Remove(builder.Length - 1, 1);
                builder.Append(Environment.NewLine);
            }
            builder.Remove(builder.Length - Environment.NewLine.Length, Environment.NewLine.Length);
            
            return builder.ToString();
        }

        public static double[][] ToArrayOfArray(this ILeakyMatrix<double> matrix)
        { 
            double[][] result = new double[matrix.RowsCount][];
            for (int i = 0; i < result.Length; ++i)
                result[i] = new double[matrix.ColumnsCount];
                
            for (var i = 0; i < matrix.RowsCount; ++i)
            {
                for (var j = 0; j < matrix.ColumnsCount; ++j)
                {
                    result[i][j] = matrix[i, j];
                }
            }

            return result;
        }
    }
}