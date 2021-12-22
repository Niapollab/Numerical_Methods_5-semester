using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NumericalMethods.Core.Algorithms;
using NumericalMethods.Core.Extensions;
using NumericalMethods.Task3.Utils;

namespace NumericalMethods.Task3
{
    class ReverseIterationMethodEigenFinder : IEnumerable<(IReadOnlyList<double> EigenVector, double MinEigenValue)>
    {
        private readonly double[,] _matrix;

        private readonly Lazy<double[,]> _unitMatrix;

        private readonly Lazy<double[]> _eigenvector;

        public ReverseIterationMethodEigenFinder(double[,] matrix)
        {
            _matrix = (double[,])matrix?.Clone() ?? throw new ArgumentNullException(nameof(matrix));

            int minSize = Math.Min(matrix.GetLength(0), matrix.GetLength(1));

            _unitMatrix = new Lazy<double[,]>(() => Enumerable.Repeat(1.0, minSize).ToArray().GenerateDiagonalMatrix());
            _eigenvector = new Lazy<double[]>(() => Enumerable.Repeat(1.0, minSize).ToArray());
        }

        public IEnumerator<(IReadOnlyList<double> EigenVector, double MinEigenValue)> GetEnumerator()
        {
            IReadOnlyList<double> eigenvector = (IReadOnlyList<double>)_eigenvector.Value.Clone();
            double[,] matrixDecomposition = CholeskyAlgorithm.Decompose(_matrix);
            double eigenvalue = 0;

            while (true)
            {
                IReadOnlyList<double> solution = MatrixDecompositionUtils.Solve(matrixDecomposition, eigenvector);
                eigenvector = solution.GetMathNormalized();

                var numeratorMatrix = eigenvector.ToVectorRow().Multiplicate(_matrix).Multiplicate(eigenvector.ToVectorColumn());
                var denumeratorMatrix = eigenvector.ToVectorRow().Multiplicate(eigenvector.ToVectorColumn());

                eigenvalue = numeratorMatrix[0, 0] / denumeratorMatrix[0, 0];

                yield return (eigenvector, eigenvalue);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}