using System;
using System.Collections.Generic;

namespace NumericalMethods.Task5.Interpolating
{
    class NewtonPolynomialInterpolateFuncBuilder : IPolynomialInterpolateFuncBuilder
    {
        public Func<double, double> Build(IReadOnlyList<(double X, double Y)> points)
        {
            _ = points ?? throw new ArgumentNullException(nameof(points));

            return value => 
            {
                double result = points[0].Y;

                for (var i = 1; i < points.Count; ++i)
                {
                    double f = 0;
                    for (var j = 0; j <= i; ++j)
                    {
                        double den = 1;

                        for (var k = 0; k <= i; ++k)
                            if (k != j)
                                den *= (points[j].X - points[k].X);

                        f += points[j].Y / den;
                    }

                    for (var k = 0; k < i; ++k)
                        f *= (value - points[k].X);

                    result += f;
                }

                return result;
            };
        }

        public Func<double, double> Build(params (double X, double Y)[] points)
            => Build((IReadOnlyList<(double X, double Y)>)points);
    }
}