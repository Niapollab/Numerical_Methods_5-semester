using System;

namespace NumericalMethods.Task2
{
    static class CholeskyAlgorithm
    {
        public static double[,] DecomposeBandedMatrix(double[,] matrix, int halfRibbonLength)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = halfRibbonLength > matrix.GetLength(0) || halfRibbonLength > matrix.GetLength(1) ? throw new ArgumentOutOfRangeException(nameof(halfRibbonLength), "Half Ribbon Length must be greater than zero.") : true;

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

        public static double[,] DecomposeSymmetricBandedMatrix(double[,] rectangularBottomMatrix, int halfRibbonLength)
        {
            _ = rectangularBottomMatrix ?? throw new ArgumentNullException(nameof(rectangularBottomMatrix));
            _ = halfRibbonLength > rectangularBottomMatrix.GetLength(0) || halfRibbonLength > rectangularBottomMatrix.GetLength(1) * 2 - 1 ? throw new ArgumentOutOfRangeException(nameof(halfRibbonLength), "Half Ribbon Length must be greater than zero.") : true;

            var resultMatrix = new double[rectangularBottomMatrix.GetLength(0), rectangularBottomMatrix.GetLength(1) * 2 - 1];
            var lMatrix = resultMatrix;
            var uMatrix = resultMatrix;

            for (var i = 0; i < rectangularBottomMatrix.GetLength(0); ++i)
            {
                int columnStartsIndex = BottomBandedMatrixUtils.GetColumnStartsIndex(i, halfRibbonLength);
                int columnEndsIndex = GetColumnEndsIndex(i);

                for (var j = columnStartsIndex; j <= i; ++j)
                {
                    double sum = rectangularBottomMatrix[i, BottomBandedMatrixUtils.MapColumnIndexForRectangularMatrix(i, j, halfRibbonLength)];

                    for (var k = 0; k < j; ++k)
                        sum -= lMatrix[i, k] * uMatrix[k, j];

                    lMatrix[i, j] = sum;
                }

                for (var j = i + 1; j <= columnEndsIndex; ++j)
                {
                    var mappedColumnIndex = MapSymmetricColumn(i, j);
                    var mappedRowIndex = MapSymmetricRow(i, mappedColumnIndex);
                    double sum = rectangularBottomMatrix[mappedRowIndex, mappedColumnIndex];

                    for (var k = 0; k < i; ++k)
                        sum -= lMatrix[i, k] * uMatrix[k, j];

                    uMatrix[i, j] = sum / lMatrix[i, i];
                }
            }

            return resultMatrix;

            int MapSymmetricColumn(int rowIndex, int columnIndex)
                => halfRibbonLength - (columnIndex - rowIndex);

            int MapSymmetricRow(int rowIndex, int mappedColumnIndex)
                => rowIndex + halfRibbonLength - mappedColumnIndex;

            int GetColumnEndsIndex(int rowIndex)
                => Math.Min(rowIndex + halfRibbonLength, rectangularBottomMatrix.GetLength(1) * 2 - 2);
        }
    }
}