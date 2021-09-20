using System;
using System.Collections.Generic;
using System.Linq;
using NumericalMethods.Core;
using NumericalMethods.Core.Extensions;
using NumericalMethods.Core.Interfaces;

namespace NumericalMethods.Task1
{
    /// <example>
    /// *	*				*	*					
    /// *	*	*			*	*					
    /// 	*	*	*		*	*					
    /// 		*	*	*	*	*					
    /// 			*	*	*	*					
    /// 				*	*	*					
    /// 					*	*	*				
    /// 					*	*	*	*			
    /// 					*	*	*	*	*		
    /// 					*	*		*	*	*	
    /// 					*	*			*	*	*
    /// 					*	*				*	*
    ///
    ///                     d   e                 a     b     c
    /// </example>
    public class FirstTaskMatrix : LeakyVectorMatrix<double>
    {
        public FirstTaskMatrix(double[,] matrix)
            : base(matrix.GetLength(0), matrix.GetLength(1), BuildLeakyMatrixVectors(matrix))
        {
        }

        private static IEnumerable<ILeakyMatrixVector<double>> BuildLeakyMatrixVectors(double[,] matrix)
        {
            return new ILeakyMatrixVector<double>[] 
            {
                new UnderTheMainDiagonal<double>(matrix),
                new MainDiagonalVector<double>(matrix),
                new UpperTheMainDiagonal<double>(matrix),
                new ColumnVector<double>(matrix, 5),
                new ColumnVector<double>(matrix, 6)
            };
        }
    }

    class Program
    {
        static ILeakyMatrix<double> GenerateMatrix()
        {
            var random = new Random();
            var matrix = new double[12,12];
            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                matrix[i, i] = random.Next(2, 10);

                if (i + 1 < matrix.GetLength(1))
                    matrix[i, i + 1] = random.Next(2, 10);

                if (i + 1 < matrix.GetLength(0))
                    matrix[i + 1, i] = random.Next(2, 10);
                
                matrix[i, 5] = random.Next(2, 10);

                matrix[i, 6] = random.Next(2, 10);

            }
            return new FirstTaskMatrix(matrix);
        }

        static ILeakyMatrix<double> ReadMatrixFromConsole(char separator = ' ')
        {
            var lines = ConsoleExtensions.EnumerateLines();
            double[][] matrix = lines.Select(line =>
            {
                return line
                    .Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(el => double.Parse(el))
                    .ToArray();
            }).ToArray();
            
            int minRowLength = matrix.Select(row => row.Length).Min();

            var result = new double[matrix.Length, minRowLength];

            for (int rowIndex = 0; rowIndex < result.GetLength(0); ++rowIndex)
                for (int columnIndex = 0; columnIndex < result.GetLength(1); ++columnIndex)
                    result[rowIndex, columnIndex] = matrix[rowIndex][columnIndex];

            return new FirstTaskMatrix(result);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите матрицу: ");
            ILeakyMatrix<double> matrix = ReadMatrixFromConsole();

            ReduceAdditionalDiagonale(matrix);
            RemoveBadArray(matrix, 5);
            RemoveBadArray(matrix, 6);
            SimplifyMainDiagonale(matrix);

            Console.WriteLine(matrix.ToMatrixString());
            Console.ReadKey(true);
        }

        private static void SimplifyMainDiagonale(ILeakyMatrix<double> matrix)
        {
            for (int rowIndex = 0; rowIndex < matrix.RowsCount; ++rowIndex)
                matrix.SafeDivideBy(rowIndex, matrix[rowIndex, rowIndex]);
        }

        private static void ReduceAdditionalDiagonale(ILeakyMatrix<double> matrix)
        {
            for (int rowIndex = 0; rowIndex < matrix.RowsCount / 2; ++rowIndex)
            {
                int nextRow = rowIndex + 1;
                matrix
                    .SafeDivideBy(rowIndex, matrix[rowIndex, rowIndex])
                    .SafeDivideBy(nextRow, matrix[nextRow, rowIndex])
                    .SubLines(nextRow, rowIndex);
            }

            for (int rowIndex = matrix.RowsCount - 1; rowIndex > matrix.RowsCount / 2; --rowIndex)
            {
                int prevRow = rowIndex - 1;
                matrix
                    .SafeDivideBy(rowIndex, matrix[rowIndex, rowIndex])
                    .SafeDivideBy(prevRow, matrix[prevRow, rowIndex])
                    .SubLines(prevRow, rowIndex);
            }
        }

        private static void RemoveBadArray(ILeakyMatrix<double> matrix, int badIndex)
        {
            matrix.SafeDivideBy(matrix.RowsCount / 2, matrix[matrix.RowsCount / 2, matrix.RowsCount / 2]);

            for (int rowIndex = 0; rowIndex < matrix.RowsCount; ++rowIndex)
            {
                if (rowIndex == badIndex)
                    continue;
                matrix
                    .SafeDivideBy(rowIndex, matrix[rowIndex, badIndex])
                    .SubLines(rowIndex, badIndex);
            }
        }
    }
}
