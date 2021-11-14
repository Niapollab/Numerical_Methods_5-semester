using System;

namespace NumericalMethods.Task2
{
    static class BottomBandedMatrixUtils
    {
        public static void SetSemetric<T>(T[,] matrix, T value, int rowIndex, int columnIndex)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = rowIndex < 0 || rowIndex >= matrix.GetLength(0) ? throw new ArgumentOutOfRangeException(nameof(rowIndex), "Row index must be within the matrix.") : true;
            _ = columnIndex < 0 || columnIndex >= matrix.GetLength(1) ? throw new ArgumentOutOfRangeException(nameof(columnIndex), "Column index must be within the matrix.") : true;

            matrix[rowIndex, columnIndex] = matrix[columnIndex, rowIndex] = value;
        }

        public static int GetColumnStartsIndex(int rowIndex, int halfRibbonLength)
        {
            _ = rowIndex < 0 ? throw new ArgumentOutOfRangeException(nameof(rowIndex), "Row index must be positive.") : true;
            _ = halfRibbonLength < 0 ? throw new ArgumentOutOfRangeException(nameof(halfRibbonLength), "Half Ribbon Length must be greater than zero.") : true;
            
            return Math.Max(rowIndex - halfRibbonLength, 0);
        }

        public static int GetRowLength(int rowIndex, int halfRibbonLength)
        {
            _ = rowIndex < 0 ? throw new ArgumentOutOfRangeException(nameof(rowIndex), "Row index must be positive.") : true;
            _ = halfRibbonLength < 0 ? throw new ArgumentOutOfRangeException(nameof(halfRibbonLength), "Half Ribbon Length must be greater than zero.") : true;
            
            return Math.Min(halfRibbonLength, rowIndex); 
        }

        public static double[,] ToRectangularMatrix(double[,] matrix, int halfRibbonLength)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = halfRibbonLength < 0 || halfRibbonLength > matrix.GetLength(0) || halfRibbonLength > matrix.GetLength(1) ? throw new ArgumentOutOfRangeException(nameof(halfRibbonLength), "Half Ribbon Length must be greater than zero and less or equal than rows and columns count.") : halfRibbonLength;

            // The main diagonal is not considered a ribbon element
            int rectangularMatrixColumsCount = halfRibbonLength + 1;

            var rectangularMatrix = new double[matrix.GetLength(0), rectangularMatrixColumsCount];

            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                int columnStartsIndex = BottomBandedMatrixUtils.GetColumnStartsIndex(i, halfRibbonLength);
                int columnEndsIndex = columnStartsIndex + BottomBandedMatrixUtils.GetRowLength(i, halfRibbonLength);

                for (var j = columnStartsIndex; j <= columnEndsIndex; ++j)
                {
                    int mappedColumnIndex = MapColumnIndexForRectangularMatrix(i, j, halfRibbonLength);
                    rectangularMatrix[i, mappedColumnIndex] = matrix[i, j];
                }
            }

            return rectangularMatrix;
        }

        public static int MapColumnIndexForRectangularMatrix(int rowIndex, int columnIndex, int halfRibbonLength)
        {
            _ = rowIndex < 0 ? throw new ArgumentOutOfRangeException(nameof(rowIndex), "Row index must be within the matrix.") : true;
            _ = columnIndex < 0 ? throw new ArgumentOutOfRangeException(nameof(columnIndex), "Column index must be positive.") : true;
            _ = halfRibbonLength < 0 ? throw new ArgumentOutOfRangeException(nameof(halfRibbonLength), "Half Ribbon Length must be greater than zero.") : halfRibbonLength;
            
            return columnIndex - rowIndex + halfRibbonLength;
        }
    }
}