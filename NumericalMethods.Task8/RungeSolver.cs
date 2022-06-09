using System;
using System.Collections.Generic;

namespace NumericalMethods.Task8
{
    public class RungeSolver : BaseRungeSolver
    {
        public const int Power = 4;

        public RungeSolver(Func<(double X, double Y, double Z), double> yDerivative, Func<(double X, double Y, double Z), double> zDerivative) : base(yDerivative, zDerivative)
        {
        }

        protected override IReadOnlyList<(double X, double Y, double Z)> FindSolutions((double X, double Y, double Z) point, int segmentsCount, double step)
        {
            int pointsCount = segmentsCount + 1;
            List<(double X, double Y, double Z)> solutions = new List<(double X, double Y, double Z)>();

            solutions.Add((point.X, point.Y, point.Z));

            for (var i = 1; i < pointsCount; i++)
            {
                (double X, double Y, double Z) prevSolution = solutions[^1];

                double x = GetDotByIndex(point.X, i - 1, step);

                double k1 = step * _yDerivative((x, prevSolution.Y, prevSolution.Z));
                double l1 = step * _zDerivative((x, prevSolution.Y, prevSolution.Z));

                double k2 = step * _yDerivative((x + step / 2, prevSolution.Y + k1 / 2, prevSolution.Z + l1 / 2));
                double l2 = step * _zDerivative((x + step / 2, prevSolution.Y + k1 / 2, prevSolution.Z + l1 / 2));

                double k3 = step * _yDerivative((x + step / 2, prevSolution.Y + k2 / 2, prevSolution.Z + l2 / 2));
                double l3 = step * _zDerivative((x + step / 2, prevSolution.Y + k2 / 2, prevSolution.Z + l2 / 2));

                double k4 = step * _yDerivative((x + step, prevSolution.Y + k3, prevSolution.Z + l3));
                double l4 = step * _zDerivative((x + step, prevSolution.Y + k3, prevSolution.Z + l3));

                double y = prevSolution.Y + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                double z = prevSolution.Z + (l1 + 2 * l2 + 2 * l3 + l4) / 6;

                solutions.Add((GetDotByIndex(point.X, i, step), y, z));
            }

            return solutions;
        }

        protected override double CalculateError(double prevValue, double currentValue)
            => Math.Abs(prevValue - currentValue) / (Math.Pow(2, Power) - 1);
    }
}