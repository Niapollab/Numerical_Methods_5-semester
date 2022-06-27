using System;
using System.Collections.Generic;

namespace NumericalMethods.Task9
{
    class ZeroingSolverParams
    {
        public int MaxIterationsCount { get; }

        public double SegmentStart { get; }

        public double SegmentEnd { get; }

        public int SegmentsCount { get; }

        public IReadOnlyList<double> Mu { get; }

        public IReadOnlyList<double> Nu { get; }

        public double A { get; }

        public double B { get; }

        public double Alpha0 { get; }

        public double Eps { get; }

        public ZeroingSolverParams(int maxIterationsCount, double segmentStart, double segmentEnd, int segmentsCount, IReadOnlyList<double> mu, IReadOnlyList<double> nu, double alpha0, double eps, Func<double, double> originalFunc, Func<double, double> derivativeFunc)
        {
            _ = originalFunc ?? throw new ArgumentNullException(nameof(originalFunc));
            _ = derivativeFunc ?? throw new ArgumentNullException(nameof(derivativeFunc));
            Mu = mu ?? throw new ArgumentNullException(nameof(mu));
            Nu = nu ?? throw new ArgumentNullException(nameof(nu));

            _ = mu.Count != 2 ? throw new ArgumentException("", nameof(mu)) : true;
            _ = nu.Count != 2 ? throw new ArgumentException("", nameof(nu)) : true;
            _ = segmentsCount < 1 ? throw new ArgumentException("", nameof(segmentsCount)) : true;
            _ = eps < 0 ? throw new ArgumentException("", nameof(eps)) : true;
            _ = maxIterationsCount < 1 ? throw new ArgumentException("", nameof(maxIterationsCount)) : true;

            MaxIterationsCount = maxIterationsCount;
            SegmentStart = segmentStart;
            SegmentEnd = segmentEnd;
            SegmentsCount = segmentsCount;
            Alpha0 = alpha0;
            Eps = eps;

            A = mu[0] * originalFunc(segmentStart) + nu[0] * derivativeFunc(segmentStart);
            B = mu[1] * originalFunc(segmentEnd) + nu[1] * derivativeFunc(segmentEnd);
        }
    }
}