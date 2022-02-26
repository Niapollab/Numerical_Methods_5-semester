using System;
using System.Linq;
using NumericalMethods.Task4.Readers;
using NumericalMethods.Task4.Models;
using NumericalMethods.Task4.Functions;
using NumericalMethods.Task4.Interfaces;
using NumericalMethods.Task4.Functions.LeastSquareMethod;
using System.Collections.Generic;
using NumericalMethods.UI;
using NumericalMethods.Task4.Collections;
using NumericalMethods.Core.Utils.Interfaces;
using NumericalMethods.Core.Utils.RandomProviders;
using NumericalMethods.Core.Collections;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumericalMethods.Task4
{
    class Program
    {
        private readonly static IRangedRandomProvider<double> RandomProvider = new DoubleRandomProvider();

        private readonly static ILeastSquareMethod<double> ApproximationFunctionCoefficientsFinder = new HyperbolaFuncLeastSquareMethod();

        static void ExperimentWithApproximatingInputFunction(InputParams inputParams)
        {
            Console.Clear();

            IFuncBuilder<double> funcBuilder = new HyperbolaFuncBuilder();

            var pointCollection = new PointCollection(funcBuilder.Build(inputParams.Coefficients), inputParams.SegmentStart, inputParams.SegmentEnd);
            int segmentCount = pointCollection.Count - 1;
            pointCollection.AddPoints(inputParams.SegmentCount - segmentCount);

            Console.WriteLine($"Число разбиений: {pointCollection.Count - 1}");

            IReadOnlyList<double> approximationFunctionCoefficients = ApproximationFunctionCoefficientsFinder.FindApproximationCoefficients(pointCollection);
            Func<double, double> approximationFunction = funcBuilder.Build(approximationFunctionCoefficients);

            IReadOnlyList<(double X, double Y)> approximationPoints = pointCollection.Select(p => (p.X, approximationFunction(p.X))).ToArray();

            double rootMeanSquareError = FindRootMeanSquareError(pointCollection.Select(p => p.Y).ToArray(), approximationPoints.Select(p => p.Y).ToArray());

            Console.WriteLine($"Среднеквадратичная погрешность аппроксимации: {rootMeanSquareError}");

            ChartVisualiser.Run(
                new ChartFunctionInfo(approximationPoints)
                {
                    Name = "Апроксимированный график",
                    Color = System.Drawing.Color.Blue
                },
                new ChartFunctionInfo(pointCollection)
                {
                    Name = "Исходный график",
                    Color = Color.Green,
                    ChartDashStyle = ChartDashStyle.DashDot
                }
            );
        }

        static void ExperimentWithApproximatingWhenSegmentsCountGrowing(InputParams inputParams, IFuncBuilder<double> funcBuilder)
        {
            Console.Clear();

            List<Task<DialogResult>> tasks = new List<Task<DialogResult>>();
            var pointsAndCorruptedCollection = new PointsAndCorruptedCollection(funcBuilder.Build(inputParams.Coefficients), inputParams.SegmentStart, inputParams.SegmentEnd, RandomProvider);
            for (int i = 0; i < 3; ++i)
            {
                int segmentCount = pointsAndCorruptedCollection.CorruptedPoints.Count - 1;
                pointsAndCorruptedCollection.AddPoints((inputParams.SegmentCount * (int)Math.Pow(10, i)) - segmentCount);

                Console.WriteLine($"Число разбиений: {pointsAndCorruptedCollection.CorruptedPoints.Count - 1}");

                IReadOnlyList<double> approximationFunctionCoefficients = ApproximationFunctionCoefficientsFinder.FindApproximationCoefficients(pointsAndCorruptedCollection.CorruptedPoints);
                Func<double, double> approximationFunction = funcBuilder.Build(approximationFunctionCoefficients);

                IReadOnlyList<(double X, double Y)> approximationPoints = pointsAndCorruptedCollection.CorruptedPoints.Select(p => (p.X, approximationFunction(p.X))).ToArray();

                double rootMeanSquareError = FindRootMeanSquareError(pointsAndCorruptedCollection.CorruptedPoints.Select(p => p.Y).ToArray(), approximationPoints.Select(p => p.Y).ToArray());

                Console.WriteLine($"Среднеквадратичная погрешность аппроксимации: {rootMeanSquareError}");

                tasks.Add(ChartVisualiser.RunAsync(
                    new ChartFunctionInfo(approximationPoints)
                    {
                        Name = "Апроксимированный график",
                        Color = System.Drawing.Color.Blue
                    },
                    new ChartFunctionInfo(pointsAndCorruptedCollection.CorruptedPoints)
                    {
                        Name = "Исходный график",
                        Color = Color.Green,
                        ChartDashStyle = ChartDashStyle.DashDot
                    }
                ));
            }

            Task.WaitAll(tasks.ToArray());
        }

        static void ExperimentWithApproximatingSameFuncTypeWhenSegmentsCountGrowing(InputParams inputParams)
        {
            IFuncBuilder<double> funcBuilder = new HyperbolaFuncBuilder();
            ExperimentWithApproximatingWhenSegmentsCountGrowing(inputParams, funcBuilder);
        }

        static void ExperimentWithApproximatingDifferentFuncTypeWhenSegmentsCountGrowing(InputParams inputParams)
        {
            IFuncBuilder<double> funcBuilder = new ExhibitorFuncBuilder();
            ExperimentWithApproximatingWhenSegmentsCountGrowing(inputParams, funcBuilder);
        }

        static double FindRootMeanSquareError(IReadOnlyCollection<double> expectedSolution, IReadOnlyCollection<double> actualSolution)
        {
            _ = expectedSolution ?? throw new ArgumentNullException(nameof(expectedSolution));
            _ = actualSolution ?? throw new ArgumentNullException(nameof(actualSolution));
            _ = expectedSolution.Count != actualSolution.Count ? throw new ArgumentException("List sizes are not equal.") : true;

            return expectedSolution
                .Zip(actualSolution)
                .Select(p => (p.First - p.Second) * (p.First - p.Second))
                .Max();
        }

        static void Main()
        {
            IInputParamsReader inputReader = new ConsoleInputReader();

            InputParams inputParams = inputReader.Read();
            Console.CursorVisible = false;
            Console.WriteLine();

            IEnumerable<Action<InputParams>> exprements = new[] {
                ExperimentWithApproximatingInputFunction,
                ExperimentWithApproximatingSameFuncTypeWhenSegmentsCountGrowing,
                ExperimentWithApproximatingDifferentFuncTypeWhenSegmentsCountGrowing
            };

            foreach (Action<InputParams> exprement in exprements)
                exprement(inputParams);

            Console.ReadKey(true);
        }
    }
}