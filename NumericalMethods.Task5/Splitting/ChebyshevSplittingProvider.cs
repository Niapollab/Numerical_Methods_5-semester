using System;
using System.Collections.Generic;

namespace NumericalMethods.Task5.Splitting
{
    public class ChebyshevSplittingProvider : ISplittingProvider
    {
        public IReadOnlyList<double> Split(int segmentsCount, double segmentStart, double segmentEnd)
        {
            _ = segmentsCount < 1 ? throw new ArgumentException("Segments count must be greater than zero.", nameof(segmentsCount)) : true;
            
            if (segmentStart > segmentEnd)
                (segmentStart, segmentEnd) = (segmentEnd, segmentStart);
            
            double aPlusBDiv2 = (segmentStart + segmentEnd) / 2;
            double bMinusADiv2 = (segmentEnd - segmentStart) / 2;
            double cosDivider = 2 * (segmentsCount + 1);

            var dots = new double[segmentsCount + 1];
            for (var i = 0; i < dots.Length; ++i)
                dots[i] = aPlusBDiv2 - bMinusADiv2 * Math.Cos((2 * i + 1) / cosDivider * Math.PI);

            return dots;
        }
    }
}