using System;
using System.Collections.Generic;
using NumericalMethods.Core.Algorithms;
using NumericalMethods.Core.Extensions;
using NumericalMethods.Task5.Interpolating;

namespace NumericalMethods.Task6
{
    class SplineSecondProductionFuncBuilder : IPolynomialInterpolateFuncBuilder
    {
        public Func<double, double> Build(IReadOnlyList<(double X, double Y)> points)
        {
            _ = points ?? throw new ArgumentNullException(nameof(points));

            SortedList<double, SplineSegmentInfo> segments = BuildSplineSegments(points);

            return value =>
            {
                KeyValuePair<double, SplineSegmentInfo> nearestSegment = segments.FirstGreaterOrEqual(value)
                    ?? new KeyValuePair<double, SplineSegmentInfo>(segments.Keys[^1], segments[segments.Keys[^1]]);

                (double segmentEnd, SplineSegmentInfo info) = nearestSegment;

                double deltaValue = value - segmentEnd;
                return info.OriginalFunctionValue + (info.FirstProductionValue + (info.SecondProductionValue / 2 + info.ThirdProductionValue * deltaValue / 6) * deltaValue) * deltaValue;
            };
        }

        public Func<double, double> Build(params (double X, double Y)[] points)
            => Build((IReadOnlyList<(double X, double Y)>)points);

        private SortedList<double, SplineSegmentInfo> BuildSplineSegments(IReadOnlyList<(double X, double Y)> points)
        {
            IReadOnlyList<double> secondProductionCoefficients = CalculateSecondProductionCoefficients(points, p =>
            {
                double[] row = new double[p.Count + 1];
                double h0 = p[1].X - p[0].X;

                row[0] = 2 * h0;
                row[1] = 1 * h0;
                row[^1] = 6 * ((p[1].Y - p[0].Y) / h0 - p[0].X);

                return row;
            }, p =>
            {
                double[] row = new double[p.Count + 1];
                double h0 = p[1].X - p[0].X;

                row[^2] = 1;
                row[^1] = p[^1].X;

                return row;
            });

            var result = new SortedList<double, SplineSegmentInfo>()
            {
                [points[0].X] = new SplineSegmentInfo(points[0].Y, 0, secondProductionCoefficients[0], 0)
            };

            for (var i = points.Count - 1; i > 0; --i)
            {
                double hi = points[i].X - points[i - 1].X;
                double thirdProductionCoefficient = (secondProductionCoefficients[i] - secondProductionCoefficients[i - 1]) / hi;
                double firstProductionCoefficient = hi * (2 * secondProductionCoefficients[i] + secondProductionCoefficients[i - 1]) / 6 + (points[i].Y - points[i - 1].Y) / hi;

                result.Add(points[i].X, new SplineSegmentInfo(points[i].Y, firstProductionCoefficient, secondProductionCoefficients[i], thirdProductionCoefficient));
            }

            return result;
        }

        private IReadOnlyList<double> CalculateSecondProductionCoefficients(IReadOnlyList<(double X, double Y)> points, Func<IReadOnlyList<(double X, double Y)>, IReadOnlyList<double>> leftBoundTermBuilder, Func<IReadOnlyList<(double X, double Y)>, IReadOnlyList<double>> rightBoundTermBuilder)
        {
            double[,] leftSide = new double[points.Count, points.Count];
            for (var i = 1; i < points.Count - 1; ++i)
            {
                leftSide[i, i - 1] = points[i].X - points[i - 1].X;
                leftSide[i, i + 1] = points[i + 1].X - points[i].X;
                leftSide[i, i] = 2 * (leftSide[i, i - 1] + leftSide[i, i + 1]);
            }

            double[] rightSide = new double[points.Count];
            for (var i = 1; i < points.Count - 1; ++i)
                rightSide[i] = 6 * ((points[i + 1].Y - points[i].Y) / (points[i + 1].X - points[i].X) - (points[i].Y - points[i - 1].Y) / (points[i].X - points[i - 1].X));

            foreach (var element in new (int Index, IReadOnlyList<double> BoundTerm)[] { (0, leftBoundTermBuilder(points)), (leftSide.GetLength(0) - 1, rightBoundTermBuilder(points)) })
            {
                for (var i = 0; i < leftSide.GetLength(1); ++i)
                    leftSide[element.Index, i] = element.BoundTerm[i];

                rightSide[element.Index] = element.BoundTerm[^1];
            }

            return SweepAlgorithm.Calculate(leftSide, rightSide);
        }

        private struct SplineSegmentInfo
        {
            public double OriginalFunctionValue { get; }

            public double FirstProductionValue { get; }

            public double SecondProductionValue { get; }

            public double ThirdProductionValue { get; }

            public SplineSegmentInfo(double originalFunctionValue, double firstProductionValue, double secondProductionValue, double thirdProductionValue)
            {
                OriginalFunctionValue = originalFunctionValue;
                FirstProductionValue = firstProductionValue;
                SecondProductionValue = secondProductionValue;
                ThirdProductionValue = thirdProductionValue;
            }
        }
    }
}