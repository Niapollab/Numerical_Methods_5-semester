using NumericalMethods.Core.Extensions;
using NumericalMethods.Core.Utils.Interfaces;
using NumericalMethods.Core.Utils.RandomProviders;
using NumericalMethods.Task3.Interfaces;
using NumericalMethods.Task3.Models;
using NumericalMethods.Task3.Readers;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NumericalMethods.Task3
{
    class Program
    {
        static void Main()
        {
            const int MatrixLength = 3;
            IRangedRandomProvider<double> randomProvider = new WholeDoubleRandomProvider();
            
            IReadOnlyList<double> solutionEigenVector = randomProvider.Repeat(MatrixLength, 1, 10).ToArray();
            
            IInputParamsReader reader = new MockInputParamsReader(new InputParams(EigenValuesMatrixGenerator.Generate(solutionEigenVector), 1e-5, 1e-5, 100));

            InputParams inputParams = reader.Read();

            double solutionMinEigenValue = EigenUtils.GetEigenValueFromVector(inputParams.Matrix, solutionEigenVector);

            Console.WriteLine($"Matrix:");
            Console.WriteLine(inputParams.Matrix.ToString(2));
            Console.WriteLine($"SolutionEigenVector: [{solutionEigenVector.ToString(2, "; ")}]");
            Console.WriteLine($"SolutionMinEigenValue: {solutionMinEigenValue}");
            Console.WriteLine();

            var currentIteration = 0;
            var eigenFinder = new ReverseIterationMethodEigenFinder(inputParams.Matrix);
            foreach ((IReadOnlyList<double> eigenVector, double minEigenValue) in eigenFinder)
            {
                Console.WriteLine($"Iteration: {currentIteration + 1}");
                Console.WriteLine($"EigenVector: [{eigenVector.ToString(2, "; ")}]");
                Console.WriteLine($"MinEigenValue: {minEigenValue}");
                Console.WriteLine();

                ++currentIteration;

                if (currentIteration >= inputParams.MaxIterationsNumber
                    || Math.Abs(minEigenValue - solutionMinEigenValue) <= inputParams.EigenValueAccuracy)
                    break;
            }

            Console.ReadKey(true);
        }
    }
}