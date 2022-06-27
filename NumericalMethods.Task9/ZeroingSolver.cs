using System;
using System.Collections.Generic;
using System.Linq;
using NumericalMethods.Task8;

using static System.Math;

namespace NumericalMethods.Task9
{
    class ZeroingSolver
    {
        private readonly BaseRungeSolver _rungeSolver;

        private readonly Func<ZeroingSolverParams, double, double> _y0Calculator;

        private readonly Func<ZeroingSolverParams, double, double> _z0Calculator;

        public ZeroingSolver(BaseRungeSolver rungeSolver, Func<ZeroingSolverParams, double, double> y0Calculator, Func<ZeroingSolverParams, double, double> z0Calculator)
        {
            _rungeSolver = rungeSolver ?? throw new ArgumentNullException(nameof(rungeSolver));
            _y0Calculator = y0Calculator ?? throw new ArgumentNullException(nameof(y0Calculator));
            _z0Calculator = z0Calculator ?? throw new ArgumentNullException(nameof(z0Calculator));
        }

        public ZeroingSolverSolution Solve(ZeroingSolverParams solverParams)
        {
            try
            {
                IReadOnlyList<(double X, double Y, double Z)> rungeSolution = GetRungeSolution(solverParams, solverParams.Alpha0);
                ThrowIfEndSolution(solverParams, rungeSolution, solverParams.Alpha0);

                double delta = FindDelta(solverParams, rungeSolution);

                (double alpha1, double alpha2) = FindAlphas(solverParams, delta);

                ApplyHalfDivisionMethod(solverParams, alpha1, alpha2);
            }
            catch (FindSolutionException ex)
            {
                return ex.Solution;
            }

            throw new Exception();
        }

        private void ApplyHalfDivisionMethod(ZeroingSolverParams solverParams, double alpha1, double alpha2)
        {
            IReadOnlyList<(double X, double Y, double Z)> firstSolution = GetRungeSolution(solverParams, alpha1);
            ThrowIfEndSolution(solverParams, firstSolution, alpha1);

            IReadOnlyList<(double X, double Y, double Z)> secondSolution = GetRungeSolution(solverParams, alpha2);
            ThrowIfEndSolution(solverParams, secondSolution, alpha2);

            for (var i = 0; i < solverParams.MaxIterationsCount; ++i)
            {
                double alphaMid = (alpha1 + alpha2) / 2;

                IReadOnlyList<(double X, double Y, double Z)> midSolution = GetRungeSolution(solverParams, alphaMid);
                ThrowIfEndSolution(solverParams, midSolution, alphaMid);

                if (Sign(CalculateFi(solverParams, firstSolution)) != Sign(CalculateFi(solverParams, midSolution)))
                {
                    alpha2 = alphaMid;
                    secondSolution = midSolution;
                }
                else
                {
                    alpha1 = alphaMid;
                    firstSolution = midSolution;
                }
            }
        }

        private (double Alpha1, double Alpha2) FindAlphas(ZeroingSolverParams solverParams, double delta)
        {
            double alpha1 = solverParams.Alpha0;
            double alpha2 = solverParams.Alpha0 + delta;

            IReadOnlyList<(double X, double Y, double Z)> firstSolution = GetRungeSolution(solverParams, alpha1);
            ThrowIfEndSolution(solverParams, firstSolution, alpha1);

            IReadOnlyList<(double X, double Y, double Z)> secondSolution = GetRungeSolution(solverParams, alpha2);
            ThrowIfEndSolution(solverParams, secondSolution, alpha2);

            while (Sign(CalculateFi(solverParams, firstSolution)) == Sign(CalculateFi(solverParams, secondSolution)))
            {
                alpha1 = alpha2;
                alpha2 += delta;

                firstSolution = GetRungeSolution(solverParams, alpha1);
                ThrowIfEndSolution(solverParams, firstSolution, alpha1);

                secondSolution = GetRungeSolution(solverParams, alpha2);
                ThrowIfEndSolution(solverParams, secondSolution, alpha2);
            }

            return (alpha1, alpha2);
        }

        private double FindDelta(ZeroingSolverParams solverParams, IReadOnlyList<(double X, double Y, double Z)> firstSolution)
        {
            const double Delta = 1.0;
            double alpha2 = solverParams.Alpha0 + Delta;

            IReadOnlyList<(double X, double Y, double Z)> secondSolution = GetRungeSolution(solverParams, alpha2);
            ThrowIfEndSolution(solverParams, secondSolution, alpha2);

            return Abs(CalculateFi(solverParams, secondSolution)) <= Abs(CalculateFi(solverParams, firstSolution))
                ? Delta
                : -Delta;
        }

        private IReadOnlyList<(double X, double Y, double Z)> GetRungeSolution(ZeroingSolverParams solverParams, double alpha)
            => _rungeSolver.EnumerateSolutions(_y0Calculator(solverParams, alpha), _z0Calculator(solverParams, alpha), solverParams.SegmentStart, solverParams.SegmentEnd, solverParams.SegmentsCount, double.MaxValue).First();

        private static void ThrowIfEndSolution(ZeroingSolverParams solverParams, IReadOnlyList<(double X, double Y, double Z)> rungeSolution, double alpha)
            => _ = IsEndSolution(solverParams, rungeSolution) ? throw new FindSolutionException(new ZeroingSolverSolution(rungeSolution, alpha)): true;

        private static bool IsEndSolution(ZeroingSolverParams solverParams, IReadOnlyList<(double X, double Y, double Z)> rungeSolution)
            => Abs(CalculateFi(solverParams, rungeSolution)) <= solverParams.Eps;

        private static double CalculateFi(ZeroingSolverParams solverParams, IReadOnlyList<(double X, double Y, double Z)> rungeSolution)
            => solverParams.Mu[1] * rungeSolution[^1].Y + solverParams.Nu[1] * rungeSolution[^1].Z - solverParams.B;

        private class FindSolutionException : Exception
        {
            public ZeroingSolverSolution Solution { get; }

            public FindSolutionException(ZeroingSolverSolution solution)
                => Solution = solution;
        }
    }
}