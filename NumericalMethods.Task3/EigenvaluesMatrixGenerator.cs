using NumericalMethods.Core.Utils.Interfaces;
using NumericalMethods.Core.Utils.RandomProviders;
using System.Linq;
using NumericalMethods.Core.Extensions;
using System;
using System.Collections.Generic;

namespace NumericalMethods.Task3
{
    static class EigenValuesMatrixGenerator
    {
        public static double[,] Generate(IReadOnlyList<double> eigenvalues, IRandomProvider<double> random = default)
        {
            random = random ?? new DoubleRandomProvider();
            eigenvalues = eigenvalues ?? throw new ArgumentNullException(nameof(eigenvalues));
            
            double[,] matrix = eigenvalues.GenerateDiagonalMatrix();
            double[,] omegaVector = GenerateOmegaVector(random, eigenvalues.Count).ToVectorColumn();

            double[,] householderMatrix = BuildHouseholderMatrix(matrix, omegaVector);
            double[,] transposedHouseholderMatrix = householderMatrix.Transpose();

            return householderMatrix.Multiplicate(matrix).Multiplicate(transposedHouseholderMatrix);
        }

        private static double[,] BuildHouseholderMatrix(double[,] matrix, double[,] omegaVector)
        {
            double[,] unitMatrix = Enumerable.Repeat(1.0, omegaVector.Length).ToArray().GenerateDiagonalMatrix();
            double[,] transposedOmegaVector = omegaVector.Transpose();
            
            return unitMatrix.Subtract(omegaVector.Multiplicate(2).Multiplicate(transposedOmegaVector));
        }

        private static IReadOnlyList<double> GenerateOmegaVector(IRandomProvider<double> random, int length)
        {
            double[] randomVector = random.Repeat(length).ToArray();
            double currentLength = randomVector.GetMathLength();

            return randomVector.GetMathNormalized();
        }
    }
}