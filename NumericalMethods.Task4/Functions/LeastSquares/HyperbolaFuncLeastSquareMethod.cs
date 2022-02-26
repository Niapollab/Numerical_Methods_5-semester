using NumericalMethods.Task4.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;
using NumericalMethods.Core.Algorithms;

namespace NumericalMethods.Task4.Functions.LeastSquareMethod
{
    class HyperbolaFuncLeastSquareMethod : ILeastSquareMethod<double>
    {
        public IReadOnlyList<double> FindApproximationCoefficients(IReadOnlyCollection<(double X, double Y)> points)
        {
            _ = points ?? throw new ArgumentNullException(nameof(points));

            double oneDivXSum = points.Select(p => 1 / p.X).Sum();
            double ySum = points.Select(p => p.Y).Sum();
            double oneDivSqrXSum = points.Select(p => 1 / (p.X * p.X)).Sum();
            double yDivx = points.Select(p => p.Y / p.X).Sum();

            double [,] leftSide = 
            {
                {points.Count, oneDivXSum},
                {oneDivXSum, oneDivSqrXSum}
            };
            IReadOnlyList<double> rightSide = new[]
            {
                ySum,
                yDivx
            };

            return SweepAlgorithm.Calculate(leftSide, rightSide);
        }
    }
}