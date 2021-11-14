using System;
using NumericalMethods.Core.Utils.Interfaces;

namespace NumericalMethods.Core.Utils.RandomProviders
{
    public class WholeDoubleRandomProvider : IRandomProvider<double>
    {
        private readonly DoubleRandomProvider _provider;

        public WholeDoubleRandomProvider(DoubleRandomProvider provider = null)
        {
            _provider = provider ?? new DoubleRandomProvider();
        }

        public WholeDoubleRandomProvider(double eps)
            : this(new DoubleRandomProvider(eps))
        {
        }

        public double Next()
        {
            return Math.Truncate(_provider.Next());
        }

        public double Next(double minValue, double maxValue)
        {
            return Math.Truncate(_provider.Next(minValue, maxValue));
        }
    }
}