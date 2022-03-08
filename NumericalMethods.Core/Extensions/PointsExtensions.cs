using System;
using System.Collections.Generic;
using System.Linq;

namespace NumericalMethods.Core.Extensions
{
    public static class PointsExtensions
    {
        public static IReadOnlyList<(T X, G Y)> ToPoints<T, G>(this IEnumerable<T> axisDots, Func<T, G> func)
        {
            _ = axisDots ?? throw new ArgumentNullException(nameof(axisDots));
            _ = func ?? throw new ArgumentNullException(nameof(func));

            IEnumerable<G> yDots = axisDots.Select(x => func(x));
            IReadOnlyList<(T X, G Y)> points = axisDots.Zip(yDots).ToArray();

            return points;
        }
    }
}
