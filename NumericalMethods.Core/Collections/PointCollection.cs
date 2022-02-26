using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NumericalMethods.Core.Collections
{
    public class PointCollection : IReadOnlyCollection<(double X, double Y)>
    {
        private readonly Func<double, double> _func;
        
        private readonly SegmentCollection _xAxis;

        private readonly Dictionary<double, double> _yAxis;

        public int Count => _yAxis.Count;

        public PointCollection(Func<double, double> func, double start, double end)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));

            _xAxis = new SegmentCollection(start, end);
            _yAxis = new Dictionary<double, double>();
            
            foreach (double x in _xAxis.Dots)
                _yAxis.Add(x, _func(x));
        }

        public IReadOnlyList<(double X, double Y)> AddPoints(int count)
        {
            _ = count < 1 ? throw new ArgumentOutOfRangeException(nameof(count), "Points count must be greater than one.") : true;

            IReadOnlyList<double> addedDots = _xAxis.AddDots(count);
            (double X, double Y)[] addedPoints = new (double X, double Y)[count];

            for (var i = 0; i < count; ++i)
            {
                double x = addedDots[i];

                if (_yAxis.ContainsKey(x))
                    _yAxis[x] = _func(x);
                else
                    _yAxis.Add(x, _func(x));
                
                addedPoints[i] = (x, _yAxis[x]);
            }

            return addedPoints;
        }

        public IEnumerator<(double X, double Y)> GetEnumerator()
            => _xAxis.Dots.Select(x => (x, _yAxis[x])).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}