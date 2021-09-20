using System;
using System.Collections.Generic;
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
        
        static void Main(string[] args)
        {
            ILeakyMatrix<double> matrix = GenerateMatrix();

            ReduceAdditionalDiagonale(matrix);
            RemoveBadArray(matrix, 5);
            RemoveBadArray(matrix, 6);
            SimplifyMainDiagonale(matrix);

            Console.WriteLine(matrix.ToMatrixString());
            Console.ReadKey(true);
        }

        private static void SimplifyMainDiagonale(ILeakyMatrix<double> matrix)
        {
            for (int rowIndex = 0; rowIndex < 12; ++rowIndex)
                matrix.DivideBy(rowIndex, matrix[rowIndex, rowIndex]);
        }

        private static void ReduceAdditionalDiagonale(ILeakyMatrix<double> matrix)
        {
            for (int rowIndex = 0; rowIndex < 6; ++rowIndex)
            {
                int nextRow = rowIndex + 1;
                matrix
                    .DivideBy(rowIndex, matrix[rowIndex, rowIndex])
                    .DivideBy(nextRow, matrix[nextRow, rowIndex])
                    .SubLines(nextRow, rowIndex);
            }

            for (int rowIndex = 11; rowIndex > 6; --rowIndex)
            {
                int prevRow = rowIndex - 1;
                matrix
                    .DivideBy(rowIndex, matrix[rowIndex, rowIndex])
                    .DivideBy(prevRow, matrix[prevRow, rowIndex])
                    .SubLines(prevRow, rowIndex);
            }
        }

        private static void RemoveBadArray(ILeakyMatrix<double> matrix, int badIndex)
        {
            matrix.DivideBy(6, matrix[6, 6]);

            for (int rowIndex = 0; rowIndex < 12; ++rowIndex)
            {
                if (rowIndex == badIndex)
                    continue;
                matrix
                    .DivideBy(rowIndex, matrix[rowIndex, badIndex])
                    .SubLines(rowIndex, badIndex);
            }
        }
    }
}
