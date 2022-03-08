using System;
using System.Collections.Generic;
using System.Linq;

namespace NumericalMethods.Task5.Interpolating
{
    class AitkenSchemaPolynomialInterpolateFuncBuilder : IPolynomialInterpolateFuncBuilder
    {
        public Func<double, double> Build(IReadOnlyList<(double X, double Y)> points)
        {
            _ = points ?? throw new ArgumentNullException(nameof(points));

            return value =>
            {
                double[] result = points.Select(p => p.Y).ToArray();

                for (var i = 0; i < result.Length; ++i)
                    for (var j = result.Length - 1; j > i; --j)
                    {
                        double determinant = (result[j - 1] * (points[j].X - value)) - (result[j] * (points[j - i - 1].X - value));
                        result[j] = determinant / (points[j].X - points[j - i - 1].X);
                    }

                return result[^1];
            };
        }

        public Func<double, double> Build(params (double X, double Y)[] points)
            => Build((IReadOnlyList<(double X, double Y)>)points);
    }
}