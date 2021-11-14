using System;
using NumericalMethods.Core.Extensions;
using NumericalMethods.Core.Utils.RandomProviders;

namespace NumericalMethods.Task2
{
    class Program
    {
        const double NonZeroEps = 1e-5;
        
        static void Main()
        {
            var random = new WholeDoubleRandomProvider().NotDefault();
            double[,] matrixWithoutRightSide = random.GenerateBandedSymmetricMatrix(10, 10, 3, 1, 10);
            Console.WriteLine(matrixWithoutRightSide.ToString(2));
            Console.ReadKey(true);
        }
    }
}