using NumericalMethods.Core.Extensions;
using NumericalMethods.Core.Utils.Interfaces;
using NumericalMethods.Core.Utils.RandomProviders;
using NumericalMethods.Task3.Interfaces;
using NumericalMethods.Task3.Models;
using NumericalMethods.Task3.Readers;
using System;

namespace NumericalMethods.Task3
{
    class Program
    {
        static void Main()
        {
            const int MatrixLength = 10;
            IRandomProvider<double> randomProvider = new WholeDoubleRandomProvider();
            
            IInputParamsReader reader = new MockInputParamsReader(new InputParams(randomProvider.GenerateSymmetricMatrix(MatrixLength, MatrixLength, 1, 10), 0e-5, 0e-5, 10));

            InputParams inputParams = reader.Read();

            Console.ReadKey(true);
        }
    }
}