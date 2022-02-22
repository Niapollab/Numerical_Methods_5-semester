using System;
using System.Collections.Generic;
using System.Linq;
using NumericalMethods.Core.Utils.RandomProviders;
using NumericalMethods.Core.Utils.Interfaces;
using NumericalMethods.Core.Extensions;

namespace NumericalMethods.Task4.Models
{
    class InputParams
    {
        public Func<double, double> Func { get; }
        
        public IReadOnlyList<double> XValues { get; }

        public IReadOnlyList<double> YRealValues { get; }

        public IReadOnlyList<double> YCorruptedValues { get; }

        public IReadOnlyList<(double X, double Y)> RealPoints { get; }

        public IReadOnlyList<(double X, double Y)> CorruptedPoints { get; } 
        
        public InputParams(Func<double, double> func, double a, double b, double h, IRandomProvider<double> randomProvider = default)
        {
            Func = func ?? throw new ArgumentNullException(nameof(func));
            randomProvider ??= new DoubleRandomProvider();

            if (b < a)
                (a, b) = (b, a);

            XValues = Enumerable
                .Range(0, int.MaxValue)
                .Select(i => a + h * i)
                .TakeWhile(x => x <= b)
                .ToArray();

            YRealValues = XValues
                .Select(x => Func(x))
                .ToArray();

            double d = YRealValues.Max() * 0.2;
            
            IReadOnlyList<double> corruption = randomProvider
                .Repeat(YRealValues.Count, -d / 2, d / 2)
                .ToArray();

            YCorruptedValues = YRealValues
                .Zip(corruption)
                .Select(p => p.First * p.Second)
                .ToArray();

            RealPoints = XValues
                .Zip(YRealValues)
                .ToArray();

            CorruptedPoints = XValues
                .Zip(YCorruptedValues)
                .ToArray();
        }
    }
}