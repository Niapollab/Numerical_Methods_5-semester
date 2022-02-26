using System;
using NumericalMethods.Core.Extensions;
using NumericalMethods.Core.Utils.Interfaces;

namespace NumericalMethods.Core.Utils.RandomProviders
{
    public class DoubleRandomProvider : IRandomProvider<double>, IRangedRandomProvider<double>
    {
        private readonly Random _random;

        private readonly double _eps;

        public DoubleRandomProvider(double eps = 1e-5)
            : this(new Random(), eps)
        {
        }

        public DoubleRandomProvider(Random random, double eps = 1e-5)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _eps = Math.Abs(eps);
        }

        public double Next()
        {
            double value = _random.Next() + _random.NextDouble();
            return value.CompareTo(default, _eps) == 0 ? default : value;
        }

        public double Next(double minValue, double maxValue)
        {
            _ = double.IsFinite(minValue) ? true : throw new NotFiniteNumberException(minValue);
            _ = double.IsFinite(maxValue) ? true : throw new NotFiniteNumberException(maxValue);
            (minValue, maxValue) = minValue > maxValue ? (maxValue, minValue) : (minValue, maxValue);

            double value = _random.NextDouble() * (maxValue - minValue) + minValue;
            return value.CompareTo(default, _eps) == 0 ? default : value;
        }
    }
}