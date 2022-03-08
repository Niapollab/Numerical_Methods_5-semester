using System;
using System.Collections.Generic;

namespace NumericalMethods.Task5.Interpolating
{
    interface IPolynomialInterpolateFuncBuilder
    {
        Func<double, double> Build(IReadOnlyList<(double X, double Y)> points);

        Func<double, double> Build(params(double X, double Y)[] points);
    }
}