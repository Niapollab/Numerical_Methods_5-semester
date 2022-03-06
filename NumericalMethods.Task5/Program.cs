using System;
using System.Collections.Generic;
using System.Linq;
using NumericalMethods.Core.Collections;
using NumericalMethods.Task5.Interpolating;
using NumericalMethods.Task5.Models;
using NumericalMethods.Task5.Splitting;
using NumericalMethods.Core.Extensions;
using System.Threading.Tasks;
using NumericalMethods.UI;
using System.Drawing;
using NumericalMethods.Task5.Readers;

namespace NumericalMethods.Task5
{
    class Program
    {
        private static readonly Func<double, double> ConvergentFunction = x => x * x;

        private static readonly Func<double, double> NonConvergentFunction = x => Math.Abs(x);

        private static readonly IPolynomialInterpolateFuncBuilder PolynomialInterpolateFuncBuilder = new AitkenSchemaPolynomialInterpolateFuncBuilder();

        static Func<double, double> BuildInterpolationErrorFunc(Func<double, double> originalFunction, Func<double, double> interpolatedFunction)
        {
            _ = originalFunction ?? throw new ArgumentNullException(nameof(originalFunction));
            _ = interpolatedFunction ?? throw new ArgumentNullException(nameof(interpolatedFunction));

            return value => Math.Abs(originalFunction(value) - interpolatedFunction(value));
        }

        static void RunTest(Func<double, double> originalFunc, InputParams inputParams)
        {
            Console.Clear();

            var tasks = new List<Task>();
            IEnumerable<ISplittingProvider> splittingProviders = new ISplittingProvider[] { new UniformSplittingProvider(), new ChebyshevSplittingProvider() };
            List<double> maxsErrorTestPoints = new List<double>();

            var testDots = new SegmentCollection(inputParams.SegmentStart, inputParams.SegmentEnd);
            testDots.AddDots(inputParams.SegmentCount - 1);

            for (var i = 0; i < 3; ++i)
            {
                Console.WriteLine($"{testDots.Count}: ");

                foreach (ISplittingProvider splittingProvider in splittingProviders)
                {
                    IReadOnlyList<(double X, double Y)> interpolatePoints = splittingProvider
                        .Split(testDots.Count, inputParams.SegmentStart, inputParams.SegmentEnd)
                        .ToPoints(originalFunc);

                    Func<double, double> interpolatedFunc = PolynomialInterpolateFuncBuilder.Build(interpolatePoints);
                    Func<double, double> interpolationErrorFunc = BuildInterpolationErrorFunc(originalFunc, interpolatedFunc);

                    IReadOnlyList<(double X, double Y)> originalTestPoints = testDots.Dots.ToPoints(originalFunc);
                    IReadOnlyList<(double X, double Y)> interpolatedTestPoints = testDots.Dots.ToPoints(interpolatedFunc);
                    IReadOnlyList<(double X, double Y)> errorTestPoints = testDots.Dots.ToPoints(interpolationErrorFunc);

                    maxsErrorTestPoints.Add(errorTestPoints.Max(p => p.Y));

                    tasks.Add(ChartVisualiser.RunAsync(
                        new ChartFunctionInfo(originalTestPoints)
                        {
                            Name = "Исходная функция",
                            Color = System.Drawing.Color.Blue
                        },
                        new ChartFunctionInfo(interpolatedTestPoints)
                        {
                            Name = "Интерполяционный полином",
                            Color = Color.Green,
                        },
                        new ChartFunctionInfo(errorTestPoints)
                        {
                            Name = "Погрешность интерполяции",
                            Color = Color.Red,
                        }
                    ));
                }

                Console.WriteLine($"Максимальная погрешность при равномерном разбиении: {maxsErrorTestPoints[0]}");
                Console.WriteLine($"Максимальная погрешность при разбиении Чебышева: {maxsErrorTestPoints[1]}");

                maxsErrorTestPoints.Clear();

                Task.WaitAll(tasks.ToArray());

                testDots.AddDots(25);
            }
        }

        static void Main()
        {
            IInputParamsReader inputReader = new ConsoleInputReader();

            InputParams inputParams = inputReader.Read();
            
            RunTest(ConvergentFunction, inputParams);
            RunTest(NonConvergentFunction, inputParams);

            Console.ReadKey(true);
        }
    }
}