using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NumericalMethods.Core.Models;

namespace NumericalMethods.Core.Utils
{
    public class SegmentCollection : IEnumerable<Segment>, ICloneable
    {
        private readonly SortedSet<double> _xAxis;

        public double Count => _xAxis.Count - 1;

        public IReadOnlyCollection<double> Points => _xAxis;

        public SegmentCollection(double start, double end)
        {
            _ = start == end ? throw new ArgumentException("Params must be different.") : true;

            _xAxis = new SortedSet<double>()
            {
                start,
                end
            };
        }

        private SegmentCollection(SortedSet<double> sortedSet)
            => _xAxis = new SortedSet<double>(sortedSet);

        public IEnumerable<double> AddPoints(int count)
        {
            _ = count < 1 ? throw new ArgumentOutOfRangeException(nameof(count), "Points count must be greater than one.") : true;

            for(var i = 0; i < count; ++i)
                yield return HalfSplitMaxSegment();
        }

        public IEnumerator<Segment> GetEnumerator()
        {
            double? prevPoint = null;
            foreach (double point in _xAxis)
            {
                if (prevPoint.HasValue)
                    yield return new Segment(prevPoint.Value, point);
                
                prevPoint = point;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private double HalfSplitMaxSegment()
        {
            Segment maxSegment = this.MaxBy(s => s.Length);
            
            double newPoint = (maxSegment.Start + maxSegment.End) / 2;
            _xAxis.Add(newPoint);

            return newPoint;
        }

        public object Clone()
            => new SegmentCollection(_xAxis);
    }
}