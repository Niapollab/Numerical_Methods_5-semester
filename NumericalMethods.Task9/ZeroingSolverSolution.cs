using System.Collections.Generic;

namespace NumericalMethods.Task9
{
    record ZeroingSolverSolution(IReadOnlyList<(double X, double Y, double Z)> Points, double Alpha);
}