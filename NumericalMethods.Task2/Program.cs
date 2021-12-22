using System;
using System.Collections.Generic;
using System.Linq;
using NumericalMethods.Core.Extensions;
using NumericalMethods.Core.Utils;
using NumericalMethods.Core.Utils.Interfaces;
using NumericalMethods.Core.Utils.RandomProviders;
using NumericalMethods.Task3.Utils;

namespace NumericalMethods.Task2
{
    class Program
    {
        private static readonly IRandomProvider<int> _intRandom = new IntegerRandomProvider();

        private static readonly IRandomProvider<double> _doubleRandom = new DoubleRandomProvider();
        
        const double NonZeroEps = 1e-5;

        static double FindAccuracies(int count, int halfRibbonLength, double minValue, double maxValue)
        {
            _ = count < 0 ? throw new ArgumentOutOfRangeException(nameof(count), "The number of elements must not be negative.") : true;

            double[,] matrixWithoutRightSide = _doubleRandom.GenerateBandedSymmetricMatrix(count, count, halfRibbonLength, minValue, maxValue).ToPoorlyConditionedMatrix();

            IReadOnlyList<double> expectRandomSolution = _doubleRandom.Repeat(count, minValue, maxValue).ToArray();
            
            var rightSideBuilder = new RightSideBuilder(matrixWithoutRightSide);
            IReadOnlyList<double> randomRightSide = rightSideBuilder.Build(expectRandomSolution);

            double[,] decomposition = CholeskyAlgorithm.DecomposeBandedMatrix(matrixWithoutRightSide, halfRibbonLength);

            IReadOnlyList<double> solution = MatrixDecompositionUtils.Solve(decomposition, randomRightSide);

            return AccuracyUtils.CalculateAccuracy(expectRandomSolution, solution, NonZeroEps);
        }

        static void Main()
        {
            const int TestCount = 2;
            var testCases = new TestCase[]
            {
                new TestCase(_intRandom, _intRandom.Next(10, 99), _doubleRandom.Next(10, 99), _doubleRandom.Next(10, 99)),
                new TestCase(_intRandom, _intRandom.Next(10, 99), _doubleRandom.Next(10, 99), _doubleRandom.Next(10, 99)),
                new TestCase(_intRandom, _intRandom.Next(100, 999), _doubleRandom.Next(100, 999), _doubleRandom.Next(100, 999)),
                new TestCase(_intRandom, _intRandom.Next(100, 999), _doubleRandom.Next(100, 999), _doubleRandom.Next(100, 999))
            };

            foreach (TestCase testCase in testCases)
            {
                IReadOnlyList<double> accuracies = Enumerable
                    .Range(1, TestCount)
                    .Select(_ => FindAccuracies(testCase.MatrixLength, testCase.HalfRibbonLength, testCase.MinValue, testCase.MaxValue))
                    .ToArray();

                Console.WriteLine($"N = {testCase.MatrixLength}; HalfRibbonLength = {testCase.HalfRibbonLength}; Min = {testCase.MinValue}; Max = {testCase.MaxValue};");
                Console.WriteLine($"Средняя относительная погрешность системы: {accuracies.Average()}");
            }

            Console.ReadKey(true);
        }
    }
}