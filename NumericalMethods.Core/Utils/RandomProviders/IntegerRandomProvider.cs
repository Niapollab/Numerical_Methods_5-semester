using System;
using NumericalMethods.Core.Utils.Interfaces;

namespace NumericalMethods.Core.Utils.RandomProviders
{
    public class IntegerRandomProvider : IRandomProvider<int>, IRangedRandomProvider<int>
    {
        private readonly Random _random;

        public IntegerRandomProvider(Random random = null)
        {
            _random = random ?? new Random();
        }

        public int Next()
        {
            return _random.Next();
        }

        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}