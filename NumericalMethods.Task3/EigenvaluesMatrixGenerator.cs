using NumericalMethods.Core.Utils.Interfaces;
using NumericalMethods.Core.Utils.RandomProviders;
using System.Linq;
using NumericalMethods.Core.Extensions;
using System;
using System.Collections.Generic;

namespace NumericalMethods.Task3
{
    static class EigenvaluesMatrixGenerator
    {
        public static double[,] Generate(IReadOnlyList<double> eigenvalues, IRandomProvider<double> random = default)
        {
            random = random ?? new DoubleRandomProvider();
            eigenvalues = eigenvalues ?? throw new ArgumentNullException(nameof(eigenvalues));
            
            double[,] matrix = GenerateDiagonalMatrix(eigenvalues);
            double[,] omegaVector = GenerateOmegaVector(random, eigenvalues.Count).ToVectorColumn();

            double[,] householderMatrix = BuildHouseholderMatrix(matrix, omegaVector);
            double[,] transposedHouseholderMatrix = householderMatrix.Transpose();

            return householderMatrix.Multiplicate(matrix).Multiplicate(transposedHouseholderMatrix);
        }

        private static double[,] BuildHouseholderMatrix(double[,] matrix, double[,] omegaVector)
        {
            double[,] unitMatrix = GenerateDiagonalMatrix(Enumerable.Repeat(1.0, omegaVector.Length).ToArray());
            double[,] transposedOmegaVector = omegaVector.Transpose();
            
            return unitMatrix.Subtract(omegaVector.Multiplicate(2).Multiplicate(transposedOmegaVector));
        }

        private static double[] GenerateOmegaVector(IRandomProvider<double> random, int length)
        {
            double[] randomVector = random.Repeat(length).ToArray();
            double currentLength = GetVectorLength(randomVector);

            return randomVector.Select(x => x / currentLength).ToArray();
            
            double GetVectorLength(double[] vector)
                => Math.Sqrt(vector.Select(x => x * x).Sum());
        }

        private static T[,] GenerateDiagonalMatrix<T>(IReadOnlyCollection<T> diagonal)
        {
            var matrix = new T[diagonal.Count, diagonal.Count];
            
            int currentIndex = 0;
            foreach (T element in diagonal)
            {
                matrix[currentIndex, currentIndex] = element;
                ++currentIndex;
            }

            return matrix;
        }
    }
}