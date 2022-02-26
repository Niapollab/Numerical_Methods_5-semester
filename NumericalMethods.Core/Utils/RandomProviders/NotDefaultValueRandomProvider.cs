using System;
using System.Collections.Generic;
using NumericalMethods.Core.Utils.Interfaces;

namespace NumericalMethods.Core.Utils.RandomProviders
{
    public class NotDefaultValueRandomProvider<T> : IRandomProvider<T>
    {
        private readonly IRandomProvider<T> _randomProvider;

        private readonly IEqualityComparer<T> _comparer;

        internal NotDefaultValueRandomProvider(IRandomProvider<T> randomProvider, IEqualityComparer<T> comparer = null)
        {
            _randomProvider = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }
        
        public T Next()
        {
            T value;
            do
                value = _randomProvider.Next();
            while (_comparer.Equals(value, default));

            return value;
        }
    }
}