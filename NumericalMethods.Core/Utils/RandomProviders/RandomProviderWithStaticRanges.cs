using System;
using System.Collections.Generic;
using NumericalMethods.Core.Utils.Interfaces;

namespace NumericalMethods.Core.Utils.RandomProviders
{
    public class RandomProviderWithStaticRanges<T> : IRandomProvider<T>
    {
        private readonly IRandomProvider<T> _randomProvider;

        private readonly IComparer<T> _comparer;

        private readonly T _minValue;

        private readonly T _maxValue;

        internal RandomProviderWithStaticRanges(IRandomProvider<T> randomProvider, T minValue, T maxValue, IComparer<T> comparer = null)
        {
            _randomProvider = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
            _minValue = minValue;
            _maxValue = maxValue;
            _comparer = comparer ?? Comparer<T>.Default;
        }

        internal RandomProviderWithStaticRanges(IRandomProvider<T> randomProvider, T minValue, T maxValue, Comparison<T> comparison)
            : this(randomProvider, minValue, maxValue, comparison is not null ? Comparer<T>.Create(comparison) : Comparer<T>.Default)
        {
        }

        public T Next()
        {
            
            T value;
            do
                value = _randomProvider.Next();
            while (_comparer.Compare(value, _minValue) < 0 && _comparer.Compare(value, _maxValue) >= 0);

            return value;
        }
    }
}