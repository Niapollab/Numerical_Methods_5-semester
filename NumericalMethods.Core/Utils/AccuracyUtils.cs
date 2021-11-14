using System;
using System.Collections.Generic;

namespace NumericalMethods.Core.Utils
{
    public static class AccuracyUtils
    {
        public static double CalculateAccuracy(IReadOnlyList<double> expectedSolution, IReadOnlyList<double> actualSolution, double eps)
        {
            _ = expectedSolution ?? throw new ArgumentNullException(nameof(expectedSolution));
            _ = actualSolution ?? throw new ArgumentNullException(nameof(actualSolution));
            _ = expectedSolution.Count != actualSolution.Count ? throw new ArgumentException("List sizes are not equal.") : true;
            eps = Math.Abs(eps);

            var accuracy = 0.0;

            for (var i = 0; i < expectedSolution.Count; ++i)
            {
                accuracy = Math.Max(accuracy, Math.Abs(
                    Math.Abs(expectedSolution[i]) > eps
                        ? (actualSolution[i] - expectedSolution[i]) / expectedSolution[i]
                        : actualSolution[i] - expectedSolution[i]
                ));
            }

            return accuracy;
        }
    }
}
