using System;

namespace NumericalMethods.Core.Algorithms
{
    public static class CholeskyAlgorithm
    {
        public static double[,] Decompose(double[,] matrix)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            
            var resultMatrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
            var lMatrix = resultMatrix;
            var uMatrix = resultMatrix;

            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                for (var j = 0; j <= i; ++j)
                {
                    double sum = matrix[i, j];

                    for (var k = 0; k < j; ++k)
                        sum -= lMatrix[i, k] * uMatrix[k, j];

                    lMatrix[i, j] = sum;
                }

                for (var j = i + 1; j < matrix.GetLength(1); ++j)
                {
                    double sum = matrix[i, j];

                    for (var k = 0; k < i; ++k)
                        sum -= lMatrix[i, k] * uMatrix[k, j];

                    uMatrix[i, j] = sum / lMatrix[i, i];
                }
            }

            return resultMatrix;
        } 
    }
}