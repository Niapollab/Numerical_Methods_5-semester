using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NumericalMethods.Core.Models;

namespace NumericalMethods.Core.Collections
{
    public class SegmentCollection : IReadOnlyCollection<Segment>, ICloneable
    {
        private readonly SortedSet<double> _xAxis;

        public IReadOnlyCollection<double> Dots => _xAxis;

        public int Count => _xAxis.Count - 1;

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

        public IReadOnlyList<double> AddDots(int count)
        {
            _ = count < 1 ? throw new ArgumentOutOfRangeException(nameof(count), "Dots count must be greater than one.") : true;

            double[] addedDots = new double[count];
            for (var i = 0; i < count; ++i)
                addedDots[i] = HalfSplitMaxSegment();

            return addedDots;
        }

        public IEnumerator<Segment> GetEnumerator()
        {
            double? prevDot = null;
            foreach (double dot in _xAxis)
            {
                if (prevDot.HasValue)
                    yield return new Segment(prevDot.Value, dot);

                prevDot = dot;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private double HalfSplitMaxSegment()
        {
            Segment maxSegment = this.MaxBy(s => s.Length);

            double newDot = (maxSegment.Start + maxSegment.End) / 2;
            _xAxis.Add(newDot);

            return newDot;
        }

        public object Clone()
            => new SegmentCollection(_xAxis);
    }
}