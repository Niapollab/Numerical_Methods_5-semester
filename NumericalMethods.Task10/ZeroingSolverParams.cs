using System;

namespace NumericalMethods.Task10
{
    public class ZeroingSolverParams
    {
        public int MaxIterationsCount { get; }

        public double SegmentStart { get; }

        public double SegmentEnd { get; }

        public int SegmentsCount { get; }

        public double A { get; }

        public double B { get; }

        public double C { get; }

        public double Alpha0 { get; }

        public double Eps { get; }

        public ZeroingSolverParams(int maxIterationsCount, double segmentStart, double segmentEnd, int segmentsCount, double alpha0, double eps, Func<double, double> originalFunc, Func<double, double> derivativeFunc)
        {
            _ = originalFunc ?? throw new ArgumentNullException(nameof(originalFunc));
            _ = derivativeFunc ?? throw new ArgumentNullException(nameof(derivativeFunc));
            _ = segmentsCount < 1 ? throw new ArgumentException("", nameof(segmentsCount)) : true;
            _ = eps < 0 ? throw new ArgumentException("", nameof(eps)) : true;
            _ = maxIterationsCount < 1 ? throw new ArgumentException("", nameof(maxIterationsCount)) : true;

            MaxIterationsCount = maxIterationsCount;
            SegmentStart = segmentStart;
            SegmentEnd = segmentEnd;
            SegmentsCount = segmentsCount;

            Alpha0 = alpha0;
            Eps = eps;

            A = originalFunc(segmentStart);
            B = originalFunc(segmentEnd);
            C = derivativeFunc(segmentEnd);
        }
    }
}