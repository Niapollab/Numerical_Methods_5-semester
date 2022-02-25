using NumericalMethods.Task4.Readers;
using NumericalMethods.Task4.Models;
using NumericalMethods.Task4.Functions;
using System.Linq;
using NumericalMethods.Task4.Interfaces;
using NumericalMethods.Task4.Functions.LeastSquareMethod;
using System.Collections.Generic;
using System;
using NumericalMethods.UI;

namespace NumericalMethods.Task4
{
    class Program
    {
        static double FindRootMeanSquareError(IReadOnlyList<double> expectedSolution, IReadOnlyList<double> actualSolution)
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
            IFuncBuilder<double> funcBuilder = new HyperbolaFuncBuilder();
            ILeastSquareMethod<double> approximationFunctionCoefficientsFinder = new HyperbolaFuncLeastSquareMethod();
            IInputParamsReader inputReader = new ConsoleInputReader(funcBuilder);

            Console.CursorVisible = false;
            InputParams inputParams = inputReader.Read();
            Console.WriteLine();

            Console.WriteLine("Исходные точки: ");
            Console.WriteLine(string.Join(Environment.NewLine, inputParams.RealPoints.Select(p => $"X = {p.X}; Y = {p.Y}")));
            Console.WriteLine();

            Console.WriteLine("Поврежденные точки:");
            Console.WriteLine(string.Join(Environment.NewLine, inputParams.CorruptedPoints.Select(p => $"X = {p.X}; Y = {p.Y}")));
            Console.WriteLine();

            IReadOnlyList<double> approximationFunctionCoefficients = approximationFunctionCoefficientsFinder.FindApproximationCoefficients(inputParams.CorruptedPoints);
            Func<double, double> approximationFunction = funcBuilder.Build(approximationFunctionCoefficients);

            Console.WriteLine("Коэффициенты аппроксимации: ");
            Console.WriteLine(string.Join(Environment.NewLine, approximationFunctionCoefficients.Select((c, i) => $"c{i + 1} = {c}")));
            Console.WriteLine();

            IReadOnlyList<double> approximationYValues = inputParams.XValues.Select(x => approximationFunction(x)).ToArray();
            IReadOnlyList<(double X, double Y)> approximationPoints = inputParams.XValues
                .Zip(approximationYValues)
                .ToArray();

            Console.WriteLine("Аппроксимированные точки:");
            Console.WriteLine(string.Join(Environment.NewLine, approximationPoints.Select(p => $"X = {p.X}; Y = {p.Y}")));
            Console.WriteLine();
            
            double rootMeanSquareError = FindRootMeanSquareError(inputParams.YRealValues, approximationYValues);

            Console.Write($"Среднеквадратичная погрешность аппроксимации: {rootMeanSquareError}");

            ChartVisualiser.Run(
                new ChartFunctionInfo(inputParams.RealPoints)
                {
                    Name = "Исходный график",
                    Color = System.Drawing.Color.Green,
                },
                new ChartFunctionInfo(inputParams.CorruptedPoints)
                {
                    Name = "Испорченный график",
                    Color = System.Drawing.Color.Red,
                },
                new ChartFunctionInfo(approximationPoints)
                {
                    Name = "Апроксимированный график",
                    Color = System.Drawing.Color.Blue,
                }
            );

            Console.ReadKey(true);
        }
    }
}