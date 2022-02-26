using System;
using System.Linq;
using System.Collections.Generic;
using NumericalMethods.Core.Collections;
using NumericalMethods.Core.Utils.Interfaces;
using NumericalMethods.Core.Utils.RandomProviders;

namespace NumericalMethods.Task4.Collections
{
    class PointsAndCorruptedCollection
    {
        private readonly PointCollection _pointCollection;

        private readonly Dictionary<double, double> _corruptedDict;

        private readonly IRangedRandomProvider<double> _randomProvider;

        public IReadOnlyList<(double X, double Y)> RealPoints { get; private set; }

        public IReadOnlyList<(double X, double Y)> CorruptedPoints { get; private set; }

        public PointsAndCorruptedCollection(Func<double, double> func, double start, double end, IRangedRandomProvider<double> randomProvider = null)
        {
            _ = func ?? throw new ArgumentNullException(nameof(func));
            _pointCollection = new PointCollection(func, start, end);
            _randomProvider = randomProvider ?? new DoubleRandomProvider();
            _corruptedDict = new Dictionary<double, double>();

            (double minValue, double maxValue) = GetRandomRange();
            foreach ((double X, double Y) point in _pointCollection)
                _corruptedDict.Add(point.X, point.Y + _randomProvider.Next(minValue, maxValue));

            RealPoints = _pointCollection.ToArray();

            CorruptedPoints = _pointCollection
                .Select(p => (p.X, _corruptedDict[p.X]))
                .ToArray();
        }

        public void AddPoints(int count)
        {
            _ = count < 1 ? throw new ArgumentOutOfRangeException(nameof(count), "Points count must be greater than one.") : true;

            _pointCollection.AddPoints(count);

            (double minValue, double maxValue) = GetRandomRange();
            foreach ((double X, double Y) point in _pointCollection)
            {
                var correptedYValue = point.Y + _randomProvider.Next(minValue, maxValue);
                if (_corruptedDict.ContainsKey(point.X))
                    _corruptedDict[point.X] = correptedYValue;
                else
                    _corruptedDict.Add(point.X, correptedYValue);
            }

            RealPoints = _pointCollection.ToArray();

            CorruptedPoints = _pointCollection
                .Select(p => (p.X, _corruptedDict[p.X]))
                .ToArray();
        }

        private (double MinValue, double MaxValue) GetRandomRange()
        {
            double maxYValue = Math.Abs(_pointCollection.Select(p => p.Y).Max());
            return (-maxYValue / 2, maxYValue / 2);
        }
    }
}