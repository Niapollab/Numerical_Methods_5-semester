using System;
using System.Collections.Generic;
using NumericalMethods.Task4.Interfaces;

namespace NumericalMethods.Task4.Functions
{
    class PolynomFuncBuilder : IFuncBuilder<double>
    {
        public Func<double, double> Build(params double[] coefficients)
            => Build((IReadOnlyList<double>)coefficients);

        public Func<double, double> Build(IReadOnlyList<double> coefficients)
        {
            _ = coefficients ?? throw new ArgumentNullException(nameof(coefficients));
            _ = coefficients.Count < 1 ? throw new ArgumentException("Coefficients must have greater than 0 elements.", nameof(coefficients)) : true;

            return x =>
            {
                double xPower = 1;
                double result = 0.0;

                for (var i = coefficients.Count - 1; i >= 0; --i)
                {
                    result += coefficients[i] * xPower;
                    xPower *= x;
                }
                
                return result;
            };
        }
    }
}