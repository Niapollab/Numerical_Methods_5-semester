using System;
using System.Collections.Generic;
using NumericalMethods.Core.Utils.Interfaces;

namespace NumericalMethods.Core.Utils.RandomProviders
{
    public class NotDefaultValueRangedRandomProvider<T> : IRangedRandomProvider<T>
    {
        private readonly IRangedRandomProvider<T> _provider;

        private readonly IEqualityComparer<T> _comparer;

        internal NotDefaultValueRangedRandomProvider(IRangedRandomProvider<T> provider, IEqualityComparer<T> comparer = null)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        public T Next(T minValue, T maxValue)
        {
            T value;
            do
                value = _provider.Next(minValue, maxValue);
            while (_comparer.Equals(value, default));

            return value;
        }
    }
}