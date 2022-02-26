using System;
using NumericalMethods.Core.Utils.Interfaces;

namespace NumericalMethods.Core.Utils.RandomProviders
{
    public class WholeDoubleRandomProvider : IRandomProvider<double>, IRangedRandomProvider<double>
    {
        private readonly DoubleRandomProvider _randomProvider;

        public WholeDoubleRandomProvider(DoubleRandomProvider randomProvider = null)
        {
            _randomProvider = randomProvider ?? new DoubleRandomProvider();
        }

        public WholeDoubleRandomProvider(double eps)
            : this(new DoubleRandomProvider(eps))
        {
        }

        public double Next()
        {
            return Math.Truncate(_randomProvider.Next());
        }

        public double Next(double minValue, double maxValue)
        {
            return Math.Truncate(_randomProvider.Next(minValue, maxValue));
        }
    }
}