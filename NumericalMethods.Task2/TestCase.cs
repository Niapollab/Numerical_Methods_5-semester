using NumericalMethods.Core.Utils.Interfaces;

namespace NumericalMethods.Task2
{
    class TestCase
    {
        public int MatrixLength { get; }

        public double MinValue { get; }

        public double MaxValue { get; }

        public int HalfRibbonLength { get; }

        public TestCase(IRandomProvider<int> random, int matrixLength, double minValue, double maxValue)
        {
            MatrixLength = matrixLength;
            MinValue = minValue;
            MaxValue = maxValue;
            HalfRibbonLength = random.Next(MatrixLength / 10, MatrixLength);
        }
    }
}