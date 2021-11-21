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
            const int MatrixLength = 5;
            const int HalfRibbonLength = 2;
            const double MinValue = 1;
            const double MaxValue = 10;

            var random = new WholeDoubleRandomProvider().NotDefault();
            
            double[,] matrixWithoutRightSide = random.GenerateBandedSymmetricMatrix(MatrixLength, MatrixLength, HalfRibbonLength, MinValue, MaxValue);

            double[,] applyToRawMatrix = CholeskyAlgorithms.Decompose(matrixWithoutRightSide);
            double[,] temp = CholeskyAlgorithms.DecomposeBandedMatrix(matrixWithoutRightSide, HalfRibbonLength);

            Console.WriteLine(applyToRawMatrix.ToString(2));
            Console.WriteLine("-----------");
            Console.WriteLine(temp.ToString(2));

            Console.ReadKey(true);
        }
    }
}