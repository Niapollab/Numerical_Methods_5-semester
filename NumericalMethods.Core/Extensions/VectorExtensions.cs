using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static IReadOnlyList<double> GetMathNormalized(this IReadOnlyCollection<double> vector)
        {
            _ = vector ?? throw new ArgumentNullException(nameof(vector));

            return vector.GetMathNormalized(x => x);
        }

        public static IReadOnlyList<double> GetMathNormalized<T>(this IReadOnlyCollection<T> vector, Func<T, double> selector)
        {
            _ = vector ?? throw new ArgumentNullException(nameof(vector));
            _ = selector ?? throw new ArgumentNullException(nameof(selector));

            double vectorMathLength = vector.GetMathLength(selector);

            return vector.Select(x => selector(x) / vectorMathLength).ToArray();
        }
        
        public static double GetMathLength(this IReadOnlyCollection<double> vector)
        {
            _ = vector ?? throw new ArgumentNullException(nameof(vector));

            return vector.GetMathLength(x => x);
        }

        public static double GetMathLength<T>(this IReadOnlyCollection<T> vector, Func<T, double> selector)
        {
            _ = vector ?? throw new ArgumentNullException(nameof(vector));
            _ = selector ?? throw new ArgumentNullException(nameof(selector));

            return Math.Sqrt(vector.Select(x => selector(x) * selector(x)).Sum());
        }

        public static T[,] GenerateDiagonalMatrix<T>(this IReadOnlyCollection<T> diagonal)
        {
            _ = diagonal ?? throw new ArgumentNullException(nameof(diagonal));
            
            var matrix = new T[diagonal.Count, diagonal.Count];
            
            int currentIndex = 0;
            foreach (T element in diagonal)
            {
                matrix[currentIndex, currentIndex] = element;
                ++currentIndex;
            }

            return matrix;
        }

        public static string ToString(this IReadOnlyList<double> vector, int digitsAfterComma = 2, string separator = ", ")
        {
            _ = vector ?? throw new ArgumentNullException(nameof(vector));
            _ = digitsAfterComma < 0 ? throw new ArgumentOutOfRangeException(nameof(digitsAfterComma), "The numbers after the decimal point must be positive.") : true;
            _ = separator ?? throw new ArgumentNullException(nameof(separator));

            return vector.ToString((i, value) => Math.Round(value, digitsAfterComma).ToString(), separator);
        }

        public static string ToString(this IReadOnlyList<float> vector, int digitsAfterComma = 2, string separator = ", ")
        {
            _ = vector ?? throw new ArgumentNullException(nameof(vector));
            _ = digitsAfterComma < 0 ? throw new ArgumentOutOfRangeException(nameof(digitsAfterComma), "The numbers after the decimal point must be positive.") : true;
            _ = separator ?? throw new ArgumentNullException(nameof(separator));

            return vector.ToString((i, value) => Math.Round(value, digitsAfterComma).ToString(), separator);
        }

        public static string ToString(this IReadOnlyList<decimal> vector, int digitsAfterComma = 2, string separator = ", ")
        {
            _ = vector ?? throw new ArgumentNullException(nameof(vector));
            _ = digitsAfterComma < 0 ? throw new ArgumentOutOfRangeException(nameof(digitsAfterComma), "The numbers after the decimal point must be positive.") : true;
            _ = separator ?? throw new ArgumentNullException(nameof(separator));

            return vector.ToString((i, value) => Math.Round(value, digitsAfterComma).ToString(), separator);
        }

        public static string ToString<T>(this IReadOnlyList<T> vector, string separator = ", ")
        {
            _ = vector ?? throw new ArgumentNullException(nameof(vector));
            _ = separator ?? throw new ArgumentNullException(nameof(separator));

            return vector.ToString((i, value) => value.ToString(), separator);
        }

        public static string ToString<T>(this IReadOnlyList<T> vector, Func<int, T, string> stringSelector, string separator = ", ")
        {
            _ = vector ?? throw new ArgumentNullException(nameof(vector));
            _ = stringSelector ?? throw new ArgumentNullException(nameof(stringSelector));
            _ = separator ?? throw new ArgumentNullException(nameof(separator));

            var builder = new StringBuilder();

            for (var i = 0; i < vector.Count; ++i)
            {
                builder.Append(stringSelector(i, vector[i])).Append(separator);
            }
            builder.Remove(builder.Length - separator.Length, separator.Length);

            return builder.ToString();
        }
    }
}
