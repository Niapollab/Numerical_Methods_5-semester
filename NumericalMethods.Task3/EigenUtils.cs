using System;
using System.Collections.Generic;
using NumericalMethods.Core.Extensions;

namespace NumericalMethods.Task3
{
    static class EigenUtils
    {
        public static double GetEigenValueFromVector(double[,] matrix, IReadOnlyCollection<double> eigenVector)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _ = eigenVector ?? throw new ArgumentNullException(nameof(eigenVector));
            _ = Math.Min(matrix.GetLength(0), matrix.GetLength(1)) != eigenVector.Count ? throw new ArgumentException("Matrix and EigenVector must have same length.") : true;

            var numeratorMatrix = eigenVector.ToVectorRow().Multiplicate(matrix).Multiplicate(eigenVector.ToVectorColumn());
            var denumeratorMatrix = eigenVector.ToVectorRow().Multiplicate(eigenVector.ToVectorColumn());

            return numeratorMatrix[0, 0] / denumeratorMatrix[0, 0];
        }
    }
}