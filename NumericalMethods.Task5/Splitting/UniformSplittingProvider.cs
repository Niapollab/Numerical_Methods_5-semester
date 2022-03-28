using System;
using System.Collections.Generic;

namespace NumericalMethods.Task5.Splitting
{
    public class UniformSplittingProvider : ISplittingProvider
    {
        public IReadOnlyList<double> Split(int segmentsCount, double segmentStart, double segmentEnd)
        {
            _ = segmentsCount < 1 ? throw new ArgumentException("Segments count must be greater than zero.", nameof(segmentsCount)) : true;
            
            if (segmentStart > segmentEnd)
                (segmentStart, segmentEnd) = (segmentEnd, segmentStart);
            
            double step = (segmentEnd - segmentStart) / segmentsCount;

            var dots = new double[segmentsCount + 1];
            for (var i = 0; i < dots.Length; ++i)
                dots[i] = segmentStart + step * i;

            return dots;
        }
    }
}