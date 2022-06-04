using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using NumericalMethods.Task8.Exceptions;
using NumericalMethods.UI;

namespace NumericalMethods.Task8
{
    class Program
    {
        static void Main()
        {
            const double SegmentStart = 1;
            const double SegmentEnd = 2;
            const int SegmentsCount = 10;
            const double Y0 = Math.E;
            const double Z0 = 2 * Math.E;
            const double Eps = 0.01;

            var solver = new RungeSolver(Y0, Z0, YDerivative, ZDerivative);
            try
            {
                IReadOnlyList<IReadOnlyList<(double X, double Y, double Z)>> solutions = solver
                    .EnumerateSolutions(SegmentStart, SegmentEnd, SegmentsCount, Eps)
                    .ToArray();

                IReadOnlyList<IReadOnlyList<(double X, double Y)>> ySolutions = solutions.Select(l => l.Select(p => (p.X, p.Y)).ToArray()).ToArray();
                IReadOnlyList<IReadOnlyList<(double X, double Z)>> zSolutions = solutions.Select(l => l.Select(p => (p.X, p.Z)).ToArray()).ToArray();

                IReadOnlyList<(double X, double Y)> yOriginalSolution = ySolutions[0];
                IReadOnlyList<(double X, double Y)> yRealSolution = ySolutions[^1];
                IReadOnlyList<(double X, double Y)> yPrevSolution = ySolutions[^2];

                IReadOnlyList<(double X, double Y)> zOriginalSolution = zSolutions[0];
                IReadOnlyList<(double X, double Y)> zRealSolution = zSolutions[^1];
                IReadOnlyList<(double X, double Y)> zPrevSolution = zSolutions[^2];

                var formsTasks = new[]
                {
                    ChartVisualiser.RunAsync
                    (
                        new ChartFunctionInfo(yPrevSolution)
                        {
                            Name = "Решение предудыщего шага Y(x)",
                            Color = Color.Blue
                        },
                        new ChartFunctionInfo(zPrevSolution)
                        {
                            Name = "Решение предудыщего шага Z(x)",
                            Color = Color.Green
                        }
                    ),
                    ChartVisualiser.RunAsync
                    (
                        new ChartFunctionInfo(yOriginalSolution)
                        {
                            Name = "Решение первого шага Y(x)",
                            Color = Color.Blue
                        },
                        new ChartFunctionInfo(zOriginalSolution)
                        {
                            Name = "Решение первого шага Z(x)",
                            Color = Color.Green
                        }
                    ),
                    ChartVisualiser.RunAsync
                    (
                        new ChartFunctionInfo(yRealSolution)
                        {
                            Name = "Решение текущего шага Y(x)",
                            Color = Color.Blue
                        },
                        new ChartFunctionInfo(zRealSolution)
                        {
                            Name = "Решение текущего шага Z(x)",
                            Color = Color.Green
                        }
                    )
                };

                Task.WaitAll(formsTasks);
            }
            catch (RungeSolverException ex)
            {
                Console.WriteLine($"Exception at {ex.IterationNumber} iteration. {ex.Message}");
            }
        }

        private static double YDerivative((double X, double Y, double Z) point)
            => point.Z;

        private static double ZDerivative((double X, double Y, double Z) point)
            => point.Z * point.Z / point.Y + point.Z / point.X;
    }
}