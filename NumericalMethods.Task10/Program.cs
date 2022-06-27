using System;

namespace NumericalMethods.Task10
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

            var solverParams = new ZeroingSolverParams(MaxIterationsCount, SegmentStart, SegmentEnd, SegmentsCount, Alpha0, Eps, originalFunc, derivativeFunc);
            var solver = new ZeroingSolver(new RungeSolver(YDerivative, ZDerivative, WDerivative), CalculateYN, CalculateZN, CalculateWN);

            var solution = solver.Solve(solverParams);

            Console.ReadKey(true);
        }

        private static double CalculateYN(ZeroingSolverParams solverParams, double alpha)
            => solverParams.B;

        private static double CalculateZN(ZeroingSolverParams solverParams, double alpha)
            => solverParams.C;

        private static double CalculateWN(ZeroingSolverParams solverParams, double alpha)
            => alpha;

        private static double YDerivative((double X, double Y, double Z, double W) point)
            => point.Z;

        private static double ZDerivative((double X, double Y, double Z, double W) point)
            => point.W;

        private static double WDerivative((double X, double Y, double Z, double W) point)
            => point.Y / (point.X * point.X * point.X) + point.Z / (3 * point.X * point.X) - 2 * point.W / (6 * point.X) + 6;
    }
}