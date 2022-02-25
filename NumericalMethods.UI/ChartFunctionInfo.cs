using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace NumericalMethods.UI
{
    public class ChartFunctionInfo
    {
        private readonly int _borderWidth = 2;

        public IReadOnlyList<(double X, double Y)> Points { get; }

        private readonly Color _color = Color.Blue;
        public Color Color {
            get => _color;
            init
            {
                if (value == Color.Transparent)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value can't be transparent color.");
                
                _color = value;
            }
        }

        public int BorderWidth { 
            get => _borderWidth; 
            init 
            {
                if (value < 1)
                    throw new ArgumentException("Value must be greater than zero.", nameof(value));

                _borderWidth = value;
            } 
        }

        public string Name { get; init; }

        public ChartDashStyle ChartDashStyle { get; init; } = ChartDashStyle.Solid;

        public ChartFunctionInfo(Func<double, double> func, IReadOnlyList<double> xAxis)
        {
            _ = func ?? throw new ArgumentNullException(nameof(func));
            _ = xAxis ?? throw new ArgumentNullException(nameof(xAxis));

            IEnumerable<double> yAsix = xAxis.Select(x => func(x));
            Points = xAxis.Zip(yAsix).ToArray();
        }

        public ChartFunctionInfo(IReadOnlyList<(double X, double Y)> points)
        {
            Points = points ?? throw new ArgumentNullException(nameof(points));
        }
    }
}