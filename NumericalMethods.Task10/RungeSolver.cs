using System;
using System.Collections.Generic;

namespace NumericalMethods.Task10
{
    public class RungeSolver : BaseRungeSolver
    {
        public const int Power = 4;

        public RungeSolver(Func<(double X, double Y, double Z, double W), double> yDerivative, Func<(double X, double Y, double Z, double W), double> zDerivative, Func<(double X, double Y, double Z, double W), double> wDerivative) : base(yDerivative, zDerivative, wDerivative)
        {
        }

        protected override IReadOnlyList<(double X, double Y, double Z, double W)> FindSolutions((double X, double Y, double Z, double W) endPoint, int segmentsCount, double step)
        {
            int pointsCount = segmentsCount + 1;
            var solutions = new List<(double X, double Y, double Z, double W)>();

            solutions.Add((endPoint.X, endPoint.Y, endPoint.Z, endPoint.W));

            for (var i = 1; i < pointsCount; ++i)
            {
                (double X, double Y, double Z, double W) prevSolution = solutions[^1];

                double x = GetDotByIndex(endPoint.X, i - 1, step);

                double k1 = step * _yDerivative((x, prevSolution.Y, prevSolution.Z, prevSolution.W));
                double l1 = step * _zDerivative((x, prevSolution.Y, prevSolution.Z, prevSolution.W));
                double p1 = step * _wDerivative((x, prevSolution.Y, prevSolution.Z, prevSolution.W));

                double k2 = step * _yDerivative((x + step / 2, prevSolution.Y + k1 / 2, prevSolution.Z + l1 / 2, prevSolution.W + p1 / 2));
                double l2 = step * _zDerivative((x + step / 2, prevSolution.Y + k1 / 2, prevSolution.Z + l1 / 2, prevSolution.W + p1 / 2));
                double p2 = step * _wDerivative((x + step / 2, prevSolution.Y + k1 / 2, prevSolution.Z + l1 / 2, prevSolution.W + p1 / 2));

                double k3 = step * _yDerivative((x + step / 2, prevSolution.Y + k2 / 2, prevSolution.Z + l2 / 2, prevSolution.W + p2 / 2));
                double l3 = step * _zDerivative((x + step / 2, prevSolution.Y + k2 / 2, prevSolution.Z + l2 / 2, prevSolution.W + p2 / 2));
                double p3 = step * _wDerivative((x + step / 2, prevSolution.Y + k2 / 2, prevSolution.Z + l2 / 2, prevSolution.W + p2 / 2));

                double k4 = step * _yDerivative((x + step, prevSolution.Y + k3, prevSolution.Z + l3, prevSolution.W + p3));
                double l4 = step * _zDerivative((x + step, prevSolution.Y + k3, prevSolution.Z + l3, prevSolution.W + p3));
                double p4 = step * _wDerivative((x + step, prevSolution.Y + k3, prevSolution.Z + l3, prevSolution.W + p3));

                double y = prevSolution.Y + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                double z = prevSolution.Z + (l1 + 2 * l2 + 2 * l3 + l4) / 6;
                double w = prevSolution.W + (p1 + 2 * p2 + 2 * p3 + p4) / 6;

                solutions.Add((GetDotByIndex(endPoint.X, i, step), y, z, w));
            }

            solutions.Reverse();

            return solutions;
        }

        protected override double CalculateError(double prevValue, double currentValue)
            => Math.Abs(prevValue - currentValue) / (Math.Pow(2, Power) - 1);
    }
}