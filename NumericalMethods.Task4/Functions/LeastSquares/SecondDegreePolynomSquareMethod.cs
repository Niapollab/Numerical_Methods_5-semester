using NumericalMethods.Task4.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;
using NumericalMethods.Core.Algorithms;

namespace NumericalMethods.Task4.Functions.LeastSquareMethod
{
    class SecondDegreePolynomSquareMethod : ILeastSquareMethod<double>
    {
        public IReadOnlyList<double> FindApproximationCoefficients(IReadOnlyCollection<(double X, double Y)> points)
        {
            _ = points ?? throw new ArgumentNullException(nameof(points));
            
            double xSum = points.Select(p => p.X).Sum();
            double ySum = points.Select(p => p.Y).Sum();
            double xSqrSum = points.Select(p => p.X * p.X).Sum();
            double xySum = points.Select(p => p.X * p.Y).Sum();

            double [,] leftSide = 
            {
                {xSqrSum, xSum},
                {xSum, points.Count}
            };
            IReadOnlyList<double> rightSide = new[]
            {
                xySum,
                ySum
            };

            return SweepAlgorithm.Calculate(leftSide, rightSide);
        }
    }
}