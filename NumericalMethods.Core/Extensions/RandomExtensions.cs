using System;
using System.Collections.Generic;
using System.Linq;

namespace NumericalMethods.Core.Extensions
{
    public static class RandomExtensions
    {
        public static IReadOnlyList<double> GenerateVector(this Random random, int length, double minValue, double maxValue, double eps)
        {
            _ = random ?? throw new ArgumentNullException(nameof(random));
            _ = length < 0 ? throw new ArgumentOutOfRangeException(nameof(length), "The number of elements must not be negative.") : true;
            eps = Math.Abs(eps);

            return Enumerable.Range(1, length).Select(x => random.NextDoubleNotZero(minValue, maxValue, eps)).ToArray();
        }

        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            _ = random ?? throw new ArgumentNullException(nameof(random));
            _ = double.IsFinite(minValue) ? true : throw new NotFiniteNumberException(minValue);
            _ = double.IsFinite(maxValue) ? true : throw new NotFiniteNumberException(maxValue);
            (minValue, maxValue) = minValue > maxValue ? (maxValue, minValue) : (minValue, maxValue);

            return random.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static int NextNotZero(this Random random, int minValue, int maxValue)
        {
            _ = random ?? throw new ArgumentNullException(nameof(random));
            (minValue, maxValue) = minValue > maxValue ? (maxValue, minValue) : (minValue, maxValue);

            int answer;
            do
                answer = random.Next(minValue, maxValue);
            while (answer == 0);

            return answer;
        }

        public static double NextDoubleNotZero(this Random random, double minValue, double maxValue, double eps)
        {
            _ = random ?? throw new ArgumentNullException(nameof(random));
            (minValue, maxValue) = minValue > maxValue ? (maxValue, minValue) : (minValue, maxValue);
            eps = Math.Abs(eps);

            double answer;
            do
                answer = random.NextDouble(minValue, maxValue);
            while (answer.CompareTo(0, eps) == 0);

            return answer;
        }
    }
}
