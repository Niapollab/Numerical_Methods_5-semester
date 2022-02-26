using System;
using System.Collections.Generic;
using System.Linq;
using NumericalMethods.Core.Utils.Interfaces;
using NumericalMethods.Core.Utils.RandomProviders;

namespace NumericalMethods.Core.Extensions
{
    public static class RandomProviderExtensions
    {
        public static IRandomProvider<T> NotDefault<T>(this IRandomProvider<T> randomProvider, IEqualityComparer<T> comparer = null)
        {
            _ = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
            comparer = comparer ?? EqualityComparer<T>.Default;
            
            return new NotDefaultValueRandomProvider<T>(randomProvider);
        }

        public static IRangedRandomProvider<T> NotDefault<T>(this IRangedRandomProvider<T> randomProvider, IEqualityComparer<T> comparer = null)
        {
            _ = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
            comparer = comparer ?? EqualityComparer<T>.Default;
            
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
