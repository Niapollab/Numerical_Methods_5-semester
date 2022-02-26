using System.Collections.Generic;

namespace NumericalMethods.Task4.Interfaces
{
    interface ILeastSquareMethod<T>
    {
        IReadOnlyList<T> FindApproximationCoefficients(IReadOnlyCollection<(T X, T Y)> points);
    }
}