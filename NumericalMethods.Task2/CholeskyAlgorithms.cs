using System;

namespace NumericalMethods.Task2
{
    static class CholeskyAlgorithms
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

        public static double[,] DecomposeBandedMatrix(double[,] matrix, int halfRibbonLength)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            
            var resultMatrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
            var lMatrix = resultMatrix;
            var uMatrix = resultMatrix;

            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                int columnStartsIndex = BottomBandedMatrixUtils.GetColumnStartsIndex(i, halfRibbonLength);
                int columnEndsIndex = GetColumnEndsIndex(i);
            
                for (var j = columnStartsIndex; j <= i; ++j)
                {
                    double sum = matrix[i, j];

                    for (var k = 0; k < j; ++k)
                        sum -= lMatrix[i, k] * uMatrix[k, j];

                    lMatrix[i, j] = sum;
                }

                for (var j = i + 1; j <= columnEndsIndex; ++j)
                {
                    double sum = matrix[i, j];

                    for (var k = 0; k < i; ++k)
                        sum -= lMatrix[i, k] * uMatrix[k, j];

                    uMatrix[i, j] = sum / lMatrix[i, i];
                }
            }

            return resultMatrix;

            int GetColumnEndsIndex(int rowIndex)
                => Math.Min(rowIndex + halfRibbonLength, matrix.GetLength(1) - 1);
        }
    }
}