using System.Collections.Generic;

namespace NumericalMethods.Task5.Splitting
{
    public interface ISplittingProvider
    {
        IReadOnlyList<double> Split(int segmentsCount, double segmentStart, double segmentEnd);
    }
}