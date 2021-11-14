using System;
using System.Collections.Generic;
using NumericalMethods.Core.Utils.Interfaces;

namespace NumericalMethods.Core.Utils.RandomProviders
{
    public class NotDefaultValueRandomProvider<T> : IRandomProvider<T>
    {
        private readonly IRandomProvider<T> _provider;

        private readonly IEqualityComparer<T> _comparer;

        internal NotDefaultValueRandomProvider(IRandomProvider<T> provider, IEqualityComparer<T> comparer = null)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }
        
        public T Next()
        {
            T value;
            do
                value = _provider.Next();
            while (_comparer.Equals(value, default));

            return value;
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