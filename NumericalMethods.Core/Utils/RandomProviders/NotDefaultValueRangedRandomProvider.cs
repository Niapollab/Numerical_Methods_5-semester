using System;
using System.Collections.Generic;
using NumericalMethods.Core.Utils.Interfaces;

namespace NumericalMethods.Core.Utils.RandomProviders
{
    public class NotDefaultValueRangedRandomProvider<T> : IRangedRandomProvider<T>
    {
        private readonly IRangedRandomProvider<T> _randomProvider;

        private readonly IEqualityComparer<T> _comparer;

        internal NotDefaultValueRangedRandomProvider(IRangedRandomProvider<T> randomProvider, IEqualityComparer<T> comparer = null)
        {
            _randomProvider = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        public T Next(T minValue, T maxValue)
        {
            T value;
            do
                value = _randomProvider.Next(minValue, maxValue);
            while (_comparer.Equals(value, default));

            return value;
        }
    }
}