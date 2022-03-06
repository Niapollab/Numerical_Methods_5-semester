using System;

namespace NumericalMethods.Task5.Models
{
    class InputParams
    {        
        public double SegmentStart { get; } 

        public double SegmentEnd { get; } 

        public int SegmentCount{ get; } 
        
        public InputParams(double segmentStart, double segmentEnd, int segmentCount)
        {
            _ = segmentStart > segmentEnd ? throw new ArgumentException("End of segment must be greater than start.") : true;
            _ = segmentCount < 1 ? throw new ArgumentException("Segments count must be greater than zero.") : true;
            
            SegmentStart = segmentStart;
            SegmentEnd = segmentEnd;
            SegmentCount = segmentCount;
        }
    }
}