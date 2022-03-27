using System;
using System.Collections.Generic;
using NumericalMethods.Core.Extensions;

namespace NumericalMethods.Core.Algorithms
{
    public static class SweepAlgorithm
    {
        public static IReadOnlyList<double> Calculate(double[,] leftSide, IReadOnlyList<double> rightSide)
        {
            _ = leftSide ?? throw new ArgumentNullException(nameof(leftSide));
            _ = rightSide ?? throw new ArgumentNullException(nameof(rightSide));
            _ = leftSide.GetLength(0) != rightSide.Count ? throw new ArgumentException("Left side length must be equals rightSide.") : true;
            _ = leftSide.GetLength(0) <= 1 && leftSide.GetLength(1) <= 1 ? throw new ArgumentNullException("Left side must be greater than 1.", nameof(leftSide)) : true;

            int rowsCount = leftSide.GetLength(0);
            var forwardTurnResult = new (double Y, double Alpha, double Beta)[rowsCount];

            for (int i = 0; i < rowsCount; ++i)
            {
                double lowerDiagonalValue = leftSide.GetValueOrDefault(i, i - 1, 0.0).Value;
                double mainDiagonalValue = leftSide[i, i];
                double upperDiagonalValue = leftSide.GetValueOrDefault(i, i + 1, 0.0).Value;
                double rightSideValue = rightSide[i];
                (double Y, double Alpha, double Beta) prevForwardTurnResult = forwardTurnResult.GetValueOrDefault(i - 1, (0, 0, 0)).Value;

                double y = mainDiagonalValue + lowerDiagonalValue * prevForwardTurnResult.Alpha;
                double alpha = -upperDiagonalValue / y;
                double beta = (rightSideValue - lowerDiagonalValue * prevForwardTurnResult.Beta) / y;

                forwardTurnResult[i] = (y, alpha, beta);
            }
        
            double[] result = new double[rowsCount];
            result[^1] = forwardTurnResult[^1].Beta;

            for (int i = rowsCount - 2; i >= 0; --i)
                result[i] = forwardTurnResult[i].Alpha * result[i + 1] + forwardTurnResult[i].Beta;

            return result;
        }
    }
}