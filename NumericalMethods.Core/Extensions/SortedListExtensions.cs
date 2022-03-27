using System;
using System.Collections.Generic;

namespace NumericalMethods.Core.Extensions
{
    public static class SortedListExtensions
    {
        public static KeyValuePair<TKey, TValue>? FirstGreater<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key)
        {
            _ = list ?? throw new ArgumentNullException(nameof(list));

            int index = list.Keys.RightBoundSafetyBinarySearch(key, (x, y) => list.Comparer.Compare(x, y) > 0);
            if (index < 0)
                return null;

            return new KeyValuePair<TKey, TValue>(list.Keys[index], list[list.Keys[index]]);
        }

        public static KeyValuePair<TKey, TValue>? FirstGreaterOrEqual<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key)
        {
            _ = list ?? throw new ArgumentNullException(nameof(list));

            int index = list.Keys.RightBoundSafetyBinarySearch(key, (x, y) => list.Comparer.Compare(x, y) >= 0);
            if (index < 0)
                return null;

            return new KeyValuePair<TKey, TValue>(list.Keys[index], list[list.Keys[index]]);
        }

        public static KeyValuePair<TKey, TValue>? LastLower<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key)
        {
            _ = list ?? throw new ArgumentNullException(nameof(list));

            int index = list.Keys.RightBoundSafetyBinarySearch(key, (x, y) => list.Comparer.Compare(x, y) >= 0) - 1;
            if (index < 0)
                return null;

            return new KeyValuePair<TKey, TValue>(list.Keys[index], list[list.Keys[index]]);
        }

        public static KeyValuePair<TKey, TValue>? LastLowerOrEqual<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key)
        {
            _ = list ?? throw new ArgumentNullException(nameof(list));

            int index = list.Keys.RightBoundSafetyBinarySearch(key, (x, y) => list.Comparer.Compare(x, y) > 0) - 1;
            if (index < 0)
                return null;

            return new KeyValuePair<TKey, TValue>(list.Keys[index], list[list.Keys[index]]);
        }
    }
}