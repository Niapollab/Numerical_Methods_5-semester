using System;
using System.Collections.Generic;

namespace NumericalMethods.Core.Extensions
{
    public static class BinarySearchExtensions
    {
        public static int RightBoundSafetyBinarySearch<T>(this IReadOnlyList<T> list, T value, Func<T, T, bool> predicate)
        {
            _ = list ?? throw new ArgumentNullException(nameof(list));
            _ = predicate ?? throw new ArgumentNullException(nameof(predicate));

            if (list.Count < 1)
                return -1;

            int left = 0;
            int right = list.Count - 1;

            while (right - left + 1 > 1)
            {
                int middle = left + (right - left) / 2;

                if (predicate(list[middle], value))
                    right = middle;
                else
                    left = middle + 1;
            }

            int index = left + (right - left) / 2;
            if (predicate(list[index], value))
                return index;

            return -1;
        }

        public static int RightBoundSafetyBinarySearch<T>(this IList<T> list, T value, Func<T, T, bool> predicate)
        {
            _ = list ?? throw new ArgumentNullException(nameof(list));
            _ = predicate ?? throw new ArgumentNullException(nameof(predicate));

            if (list.Count < 1)
                return -1;

            int left = 0;
            int right = list.Count - 1;

            while (right - left + 1 > 1)
            {
                int middle = left + (right - left) / 2;

                if (predicate(list[middle], value))
                    right = middle;
                else
                    left = middle + 1;
            }

            int index = left + (right - left) / 2;
            if (predicate(list[index], value))
                return index;

            return -1;
        }
    }
}