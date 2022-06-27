using System.Collections.Generic;

namespace NumericalMethods.Task10
{
    record ZeroingSolverSolution(IReadOnlyList<(double X, double Y, double Z, double W)> Points, double Alpha);
}