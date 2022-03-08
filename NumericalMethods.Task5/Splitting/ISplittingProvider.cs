using System.Collections.Generic;

namespace NumericalMethods.Task5.Splitting
{
    interface ISplittingProvider
    {
        IReadOnlyList<double> Split(int segmentsCount, double segmentStart, double segmentEnd);
    }
}