using System;
using NumericalMethods.Core.Extensions;
using NumericalMethods.Core.Utils;

namespace NumericalMethods.Task2
{
    class Program
    {
        const double NonZeroEps = 1e-5;
        
        static void Main()
        {
            var random = new Random();
            double[,] matrixWithoutRightSide = random.GenerateBandedSymmetricMatrix(10, 10, 3, 1, 10, 1e-5);
            Console.WriteLine(matrixWithoutRightSide.ToString(2));
            Console.ReadKey(true);
        }
    }
}