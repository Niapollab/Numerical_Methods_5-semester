using System;
using System.Collections.Generic;

namespace NumericalMethods.Task5.Interpolating
{
    class LagrangePolynomialInterpolateFuncBuilder : IPolynomialInterpolateFuncBuilder
    {
        public Func<double, double> Build(IReadOnlyList<(double X, double Y)> points)
        {
            _ = points ?? throw new ArgumentNullException(nameof(points));

            return value =>
            {
                double result = 0;
                for (var i = 0; i < points.Count; ++i)
                {
                        double basicsPol = 1;

                        for (var j = 0; j < points.Count; ++j)
                            if (j != i)
                                basicsPol *= (value - points[j].X) / (points[i].X - points[j].X);

                        result += basicsPol * points[i].Y;
                }

                return result;
            };
        }

        public Func<double, double> Build(params (double X, double Y)[] points)
            => Build((IReadOnlyList<(double X, double Y)>)points);
    }
}