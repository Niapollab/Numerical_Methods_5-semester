using System;

namespace NumericalMethods.Core.Extensions
{
    public static class FloatNumberExtensions
    {
        public static int CompareTo(this double first, double second, double eps)
        {
            return Math.Abs(first - second) <= eps
                ? 0
                : (first - second > eps ? 1 : -1);
        }

        public static int CompareTo(this float first, float second, float eps)
        {
            return Math.Abs(first - second) <= eps
                ? 0
                : (first - second > eps ? 1 : -1);
        }

        public static int CompareTo(this decimal first, decimal second, decimal eps)
        {
            return Math.Abs(first - second) <= eps
                ? 0
                : (first - second > eps ? 1 : -1);
        }
    }
}
