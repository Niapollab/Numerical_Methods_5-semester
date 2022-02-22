using System;
using System.Collections.Generic;
using NumericalMethods.Task4.Interfaces;

namespace NumericalMethods.Task4.Functions
{
    class HyperbolaFuncBuilder : IFuncBuilder<double>
    {
        public Func<double, double> Build(params double[] coefficients)
            => Build((IReadOnlyList<double>)coefficients);

        public Func<double, double> Build(IReadOnlyList<double> coefficients)
        {
            _ = coefficients ?? throw new ArgumentNullException(nameof(coefficients));
            _ = coefficients.Count != 2 ? throw new ArgumentException("Coefficients must have only 2 elements.", nameof(coefficients)) : true;
            
            return x => coefficients[0] + coefficients[1] / x;
        }
    }
}