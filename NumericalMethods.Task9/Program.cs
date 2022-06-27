using System;
using NumericalMethods.Task8;

namespace NumericalMethods.Task9
{
    class Program
    {
        static void Main()
        {
            Func<double, double> originalFunc = x => x * x * x;
            Func<double, double> derivativeFunc = x => 3 * x * x;

            const int MaxIterationsCount = 20; // K

            const int SegmentsCount = 9; // N
            const double SegmentStart = 1.0; // a
            const double SegmentEnd = 2.0; // b

            const double Eps = 0.01;
            const double Alpha0 = 2;

            var mu = new double[]
            {
                1,
                1
            };

            var nu = new double[]
            {
                1,
                1
            };

            var solverParams = new ZeroingSolverParams(MaxIterationsCount, SegmentStart, SegmentEnd, SegmentsCount, mu, nu, Alpha0, Eps, originalFunc, derivativeFunc);
            var solver = new ZeroingSolver(new RungeSolver(YDerivative, ZDerivative), CalculateY0, CalculateZ0);

            var solution = solver.Solve(solverParams);

            Console.ReadKey(true);
        }

        private static double CalculateY0(ZeroingSolverParams solverParams, double alpha)
            => solverParams.Nu[0] != 0.0 ? (solverParams.A - solverParams.Nu[0] * alpha) / solverParams.Mu[0] : alpha;

        private static double CalculateZ0(ZeroingSolverParams solverParams, double alpha)
            => solverParams.Nu[0] != 0.0 ? alpha : solverParams.A / solverParams.Nu[0];

        private static double YDerivative((double X, double Y, double Z) point)
            => point.Z;

        private static double ZDerivative((double X, double Y, double Z) point)
            => point.Y / (point.X * point.X * point.X) - point.Z / (3 * point.X * point.X) + 6 * point.X;
    }
}