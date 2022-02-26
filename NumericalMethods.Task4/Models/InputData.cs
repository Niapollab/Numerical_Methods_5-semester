using System;
using System.Collections.Generic;

namespace NumericalMethods.Task4.Models
{
    class InputParams
    {
        public IReadOnlyList<double> Coefficients { get; }
        
        public double SegmentStart { get; } 

        public double SegmentEnd { get; } 

        public int SegmentCount{ get; } 
        
        public InputParams(double segmentStart, double segmentEnd, int segmentCount, IReadOnlyList<double> coefficients)
        {
            _ = segmentStart > segmentEnd ? throw new ArgumentException("End of segment must be greater than start.") : true;
            _ = segmentCount < 1 ? throw new ArgumentException("Segments count must be greater than zero.") : true;
            Coefficients = coefficients ?? throw new ArgumentNullException(nameof(coefficients));
            
            SegmentStart = segmentStart;
            SegmentEnd = segmentEnd;
            SegmentCount = segmentCount;
        }
    }
}