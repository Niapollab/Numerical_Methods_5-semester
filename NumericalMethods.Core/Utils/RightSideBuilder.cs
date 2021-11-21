using System;
using System.Collections.Generic;

namespace NumericalMethods.Core.Utils
{
    public class RightSideBuilder
    {
        private readonly double[,] _matrix;

        public RightSideBuilder(double[,] matrix)
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));

            _matrix = (double[,])matrix.Clone();
        }

        public IReadOnlyList<double> Build(IReadOnlyList<double> expectedSolution)
        {
            _ = expectedSolution ?? throw new ArgumentNullException(nameof(expectedSolution));
            _ = expectedSolution.Count != _matrix.GetLength(1) ? throw new ArgumentException("The length of the expected solution is not equal to the length of the matrix.", nameof(expectedSolution)) : true;

            var side = new double[_matrix.GetLength(0)];

            for (var i = 0; i < _matrix.GetLength(0); ++i)
                for (var j = 0; j < _matrix.GetLength(1); ++j)
                    side[i] += _matrix[i, j] * expectedSolution[j];

            return side;
        }
    }
}
