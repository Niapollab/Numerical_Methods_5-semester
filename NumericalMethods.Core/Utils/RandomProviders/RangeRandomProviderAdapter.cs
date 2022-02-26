using System;
using NumericalMethods.Core.Utils.Interfaces;

namespace NumericalMethods.Core.Utils.RandomProviders
{
    public class RangeRandomProviderAdapter<T> : IRandomProvider<T>
    {
        private readonly IRangedRandomProvider<T> _randomProvider;

        private readonly T _minValue;

        private readonly T _maxValue;

        internal RangeRandomProviderAdapter(IRangedRandomProvider<T> randomProvider, T minValue, T maxValue)
        {
            _randomProvider = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public T Next()
            => _randomProvider.Next(_minValue, _maxValue);
    }
}