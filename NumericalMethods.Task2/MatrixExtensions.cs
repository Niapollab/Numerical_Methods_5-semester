using System;

namespace NumericalMethods.Task2
{
    static class MatrixExtensions
    {
        public static double[,] ToPoorlyConditionedMatrix(this double[,] matrix, double eps = 1e-5)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));

            for (var i = 0; i < Math.Min(matrix.GetLength(0), matrix.GetLength(1)); ++i)
                matrix[i, i] *= eps;

            return matrix;
        }
    }
}