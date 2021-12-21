using System;
using System.Collections.Generic;

namespace NumericalMethods.Core.Extensions
{
    public static class VectorExtensions
    {
        public static T[,] ToVectorRow<T>(this IReadOnlyCollection<T> vector)
        {
            _ = vector ?? throw new ArgumentNullException(nameof(vector));

            if (vector.Count < 1)
                return new T[0, 0];

            var result = new T[1, vector.Count];
            int currentIndex = 0;
            foreach (T element in vector)
                result[0, currentIndex++] = element;

            return result;
        }

        public static T[,] ToVectorColumn<T>(this IReadOnlyCollection<T> vector)
        {
            _ = vector ?? throw new ArgumentNullException(nameof(vector));

            if (vector.Count < 1)
                return new T[0, 0];

            var result = new T[vector.Count, 1];
            int currentIndex = 0;
            foreach (T element in vector)
                result[currentIndex++, 0] = element;

            return result;
        }
    }
}
