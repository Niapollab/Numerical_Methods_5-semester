using System;

namespace NumericalMethods.Task1
{
    static class RandomExtensions
    {
        public static double NextNotZero(this Random random, int minValue, int maxValue)
        {
            double answer;
            do
                answer = random.Next(minValue, maxValue) + random.NextDouble();
            while (answer == 0);

            return answer;
        }
    }
}
