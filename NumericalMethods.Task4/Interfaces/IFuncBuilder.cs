using System;
using System.Collections.Generic;

namespace NumericalMethods.Task4.Interfaces
{
    interface IFuncBuilder<T>
    {
        Func<T, T> Build(IReadOnlyList<T> coefficients);

        Func<T, T> Build(params T[] coefficients);
    }
}