using System;
using System.Collections.Generic;
using System.Linq;
using NumericalMethods.Core.Utils.Interfaces;
using NumericalMethods.Core.Utils.RandomProviders;

namespace NumericalMethods.Core.Extensions
{
    public static class RandomProviderExtensions
    {
        public static RandomProviderWithStaticRanges<T> InRange<T>(this IRandomProvider<T> randomProvider, T minValue, T maxValue, IComparer<T> comparer = null)
        {
            _ = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));

            return new RandomProviderWithStaticRanges<T>(randomProvider, minValue, maxValue, comparer);
        }

        public static RandomProviderWithStaticRanges<T> InRange<T>(this IRandomProvider<T> randomProvider, T minValue, T maxValue, Comparison<T> comparison)
        {
            _ = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));

            return new RandomProviderWithStaticRanges<T>(randomProvider, minValue, maxValue, comparison); 
        }

        public static RangeRandomProviderAdapter<T> InRange<T>(this IRangedRandomProvider<T> randomProvider, T minValue, T maxValue)
        {
            _ = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));

            return new RangeRandomProviderAdapter<T>(randomProvider, minValue, maxValue);
        }

        public static NotDefaultValueRandomProvider<T> NotDefault<T>(this IRandomProvider<T> randomProvider, IEqualityComparer<T> comparer = null)
        {
            _ = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
            
            return new NotDefaultValueRandomProvider<T>(randomProvider);
        }

        public static NotDefaultValueRangedRandomProvider<T> NotDefault<T>(this IRangedRandomProvider<T> randomProvider, IEqualityComparer<T> comparer = null)
        {
            _ = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
            
            return new NotDefaultValueRangedRandomProvider<T>(randomProvider);
        }

        public static IEnumerable<T> Repeat<T>(this IRandomProvider<T> randomProvider, int count)
        {
            _ = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
            _ = count < 0 ? throw new ArgumentNullException("The number of repetitions must be positive.", nameof(count)) : true;

            return Enumerable.Range(0, count).Select(x => randomProvider.Next());
        }

        public static IEnumerable<T> Repeat<T>(this IRangedRandomProvider<T> randomProvider, int count, T minValue, T maxValue)
        {
            _ = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
            _ = count < 0 ? throw new ArgumentNullException("The number of repetitions must be positive.", nameof(count)) : true;

            return Enumerable.Range(0, count).Select(x => randomProvider.Next(minValue, maxValue));
        }
    }
}