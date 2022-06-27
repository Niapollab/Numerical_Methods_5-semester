using System;
using System.Collections.Generic;
using NumericalMethods.Task10.Exceptions;

namespace NumericalMethods.Task10
{
    public abstract class BaseRungeSolver
    {
        protected const double MaxSegmentsCount = 64e6;

        protected readonly Func<(double X, double Y, double Z, double W), double> _yDerivative;

        protected readonly Func<(double X, double Y, double Z, double W), double> _zDerivative;

        protected readonly Func<(double X, double Y, double Z, double W), double> _wDerivative;

        public BaseRungeSolver( Func<(double X, double Y, double Z, double W), double> yDerivative, Func<(double X, double Y, double Z, double W), double> zDerivative, Func<(double X, double Y, double Z, double W), double> wDerivative)
        {
            _yDerivative = yDerivative ?? throw new ArgumentNullException(nameof(yDerivative));
            _zDerivative = zDerivative ?? throw new ArgumentNullException(nameof(zDerivative));
            _wDerivative = wDerivative ?? throw new ArgumentNullException(nameof(wDerivative));
        }

        public virtual IEnumerable<IReadOnlyList<(double X, double Y, double Z, double W)>> EnumerateSolutions(double yn, double zn, double wn, double segmentStart, double segmentEnd, int segmentsCount, double eps)
        {
            if (segmentStart > segmentEnd)
                (segmentStart, segmentEnd) = (segmentEnd, segmentStart);

            if (segmentsCount > MaxSegmentsCount)
                throw new RungeSolverException(0, "The number of segments has reached its maximum value. Step too small.");

            (double X, double Y, double Z, double W) endPoint = (segmentEnd, yn, zn, wn);

            double step = GetSegmentDeltaStep(segmentStart, segmentEnd, segmentsCount);
            IReadOnlyList<(double X, double Y, double Z, double W)> currentSolution = FindSolutions(endPoint, segmentsCount, step);
            yield return currentSolution;

            double prevZError = 0;
            for (var i = 1; true; ++i)
            {
                segmentsCount *= 2;

                if (segmentsCount > MaxSegmentsCount)
                    throw new RungeSolverException(i, "The number of segments has reached its maximum value. Step too small.");

                step = GetSegmentDeltaStep(segmentStart, segmentEnd, segmentsCount);
                IReadOnlyList<(double X, double Y, double Z, double W)> nextSolution = FindSolutions(endPoint, segmentsCount, step);

                double currentWError = CalculateError(currentSolution[^1].W, nextSolution[^1].W);
                double currentZError = CalculateError(currentSolution[^1].Z, nextSolution[^1].Z);
                double currentYError = CalculateError(currentSolution[^1].Y, nextSolution[^1].Y);

                if (Math.Abs(currentZError) - Math.Abs(prevZError) == 0)
                    throw new RungeSolverException(i, "Reducing the step does not affect the size of the error.");

                yield return nextSolution;

                if (currentZError <= eps && currentYError <= eps)
                    yield break;

                prevZError = currentZError;
                currentSolution = nextSolution;
            }
        }

        protected abstract IReadOnlyList<(double X, double Y, double Z, double W)> FindSolutions((double X, double Y, double Z, double W) endPoint, int segmentsCount, double step);

        protected abstract double CalculateError(double prevValue, double currentValue);

        protected static double GetSegmentDeltaStep(double segmentStart, double segmentEnd, int segmentsCount)
            => -(segmentEnd - segmentStart) / segmentsCount;

        protected static double GetDotByIndex(double startValue, double index, double step)
            => startValue + index * step;
    }
}