using System;
using System.Collections.Generic;
using NumericalMethods.Task8.Exceptions;

namespace NumericalMethods.Task8
{
    public abstract class BaseRungeSolver
    {
        protected const double MaxSegmentsCount = 64e6;

        protected readonly Func<(double X, double Y, double Z), double> _yDerivative;

        protected readonly Func<(double X, double Y, double Z), double> _zDerivative;

        public BaseRungeSolver( Func<(double X, double Y, double Z), double> yDerivative, Func<(double X, double Y, double Z), double> zDerivative)
        {
            _yDerivative = yDerivative ?? throw new ArgumentNullException(nameof(yDerivative));
            _zDerivative = zDerivative ?? throw new ArgumentNullException(nameof(zDerivative));
        }

        public virtual IEnumerable<IReadOnlyList<(double X, double Y, double Z)>> EnumerateSolutions(double y0, double z0, double segmentStart, double segmentEnd, int segmentsCount, double eps)
        {
            if (segmentStart > segmentEnd)
                (segmentStart, segmentEnd) = (segmentEnd, segmentStart);

            if (segmentsCount > MaxSegmentsCount)
                throw new RungeSolverException(0, "The number of segments has reached its maximum value. Step too small.");

            (double X, double Y, double Z) startPoint = (segmentStart, y0, z0);

            double step = GetSegmentDeltaStep(segmentStart, segmentEnd, segmentsCount);
            IReadOnlyList<(double X, double Y, double Z)> currentSolution = FindSolutions(startPoint, segmentsCount, step);
            yield return currentSolution;

            double prevZError = 0;
            for (var i = 1; true; ++i)
            {
                segmentsCount *= 2;

                if (segmentsCount > MaxSegmentsCount)
                    throw new RungeSolverException(i, "The number of segments has reached its maximum value. Step too small.");

                step = GetSegmentDeltaStep(segmentStart, segmentEnd, segmentsCount);
                IReadOnlyList<(double X, double Y, double Z)> nextSolution = FindSolutions(startPoint, segmentsCount, step);

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

        protected abstract IReadOnlyList<(double X, double Y, double Z)> FindSolutions((double X, double Y, double Z) startPoint, int segmentsCount, double step);

        protected abstract double CalculateError(double prevValue, double currentValue);

        protected static double GetSegmentDeltaStep(double segmentStart, double segmentEnd, int segmentsCount)
            => (segmentEnd - segmentStart) / segmentsCount;

        protected static double GetDotByIndex(double startValue, double index, double step)
            => startValue + index * step;
    }
}