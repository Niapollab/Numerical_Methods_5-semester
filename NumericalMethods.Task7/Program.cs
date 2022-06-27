using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using NumericalMethods.Task7.Exceptions;
using NumericalMethods.UI;

namespace NumericalMethods.Task7
{
    interface IAccuracyCalculator
    {
        double? Calculate(IReadOnlyList<(double X, double Y)> prevPoints, IReadOnlyList<(double X, double Y)> points);
    }

    class AnalyticalAccuracyCalculator : IAccuracyCalculator
    {
        private readonly Func<double, double> _func;

        public AnalyticalAccuracyCalculator(Func<double, double> func)
            => _func = func ?? throw new ArgumentNullException(nameof(func));

        public double? Calculate(IReadOnlyList<(double X, double Y)> prevPoints, IReadOnlyList<(double X, double Y)> points)
            => points.Select(p => Math.Abs(p.Y - _func(p.X))).Max();
    }

    class NotAnalyticalAccuracyCalculator : IAccuracyCalculator
    {
        private const int Power = 2;

        public double? Calculate(IReadOnlyList<(double X, double Y)> prevPoints, IReadOnlyList<(double X, double Y)> points)
            => prevPoints is not null
                ? Math.Abs(prevPoints[^1].Y - points[^1].Y) / (Math.Pow(2, Power) - 1)
                : null;
    }

    class Solver
    {
        private const int MaxIterationsCount = 20;

        private readonly Func<double, double, double> _func;

        private readonly IAccuracyCalculator _accuracyCalculator;

        public Solver(Func<double, double, double> func, IAccuracyCalculator accuracyCalculator = default)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
            _accuracyCalculator = accuracyCalculator;
        }

        public IReadOnlyList<(double X, double Y)> Calculate((double X, double Y) point, double xEnd, double desiredAccuracy)
        {
            IReadOnlyList<double> dots = new double[]
            {
                point.X,
                xEnd
            };

            IReadOnlyList<(double X, double Y)> prevPoint = null;
            while (true)
            {
                IReadOnlyList<(double X, double Y)> points = Calculate(dots, point.Y);

                double? accuracy = _accuracyCalculator.Calculate(prevPoint, points);

                if (accuracy is not null && accuracy.Value <= desiredAccuracy)
                    return points;

                dots = Specify(dots);
                prevPoint = points;
            }
        }

        static IReadOnlyList<double> Specify(IReadOnlyList<double> dots)
        {
            _ = dots ?? throw new ArgumentNullException(nameof(dots));
            _ = dots.Count < 2 ? throw new ArgumentException(nameof(dots), "") : true;

            var answer = new List<double>(dots);

            for (var i = 0; i < dots.Count - 1; ++i)
                answer.Add((dots[i + 1] + dots[i]) / 2);

            answer.Sort();

            return answer;
        }

        private IReadOnlyList<(double X, double Y)> Calculate(IReadOnlyList<double> dots, double y0)
        {
            (double X, double Y) startPoint = (dots[0], y0);

            var answer = new List<(double X, double Y)>
            {
                startPoint
            };

            double step = dots[1] - dots[0];

            double yPlus = CalculateYPlus(startPoint,  step);
            double yMinus = CalculateYMinus(startPoint, step);

            for (var i = 1; i < dots.Count; ++i)
            {
                step = dots[i] - dots[i - 1];

                yPlus = CalculateYPlus((dots[i], yPlus),  step);
                yMinus = CalculateYMinus((dots[i], yMinus), step);

                answer.Add((dots[i], CalculateY(yPlus, yMinus)));
            }

            return answer;
        }

        private double CalculateY(double yPlus, double yMinus)
            => (yPlus + yMinus) / 2;

        private double CalculateYPlus((double X, double Y) point, double step)
            => point.Y + step * _func(point.X + step / 3, point.Y + step / 3 * _func(point.X, point.Y));

        private double CalculateYMinus((double X, double Y) point, double step)
            => point.Y + step * _func(point.X + 2 * step / 3, point.Y + 2 * step / 3 * _func(point.X, point.Y));
    }

    class Program
    {
        static void Main()
        {
            AnaliticalSolve();
            NonAnaliticalSolve();
            Console.ReadKey(true);
        }

        static void AnaliticalSolve()
        {
            const double A = 1.4;
            const double B = 2.2;
            Func<double, double> analyticalFunc = (x) => 12.6 * Math.Pow(Math.E, (7 - 5 * x) / 20) + 4 * x - 16;
            Func<double, double, double> originalFunc = (x, y) => x - y / 4;

            var analyticalAccuracy = new AnalyticalAccuracyCalculator(analyticalFunc);
            var calc = new Solver(originalFunc, analyticalAccuracy);

            var result = calc.Calculate((A, analyticalFunc(A)), B, 0.01);
            var xs = result.Select(p => p.X).ToArray();

            for (int i = 0; i < result.Count; ++i)
                Console.WriteLine($"X = {xs[i]}; Y_Real = {analyticalFunc(xs[i])}; Y_Solved = {result[i].Y}; Diff = {Math.Abs(analyticalFunc(xs[i]) - result[i].Y)}");
        }

        static void NonAnaliticalSolve()
        {
            const double A = 0.0;
            const double B = 10;
            Func<double, double, double> originalFunc = (x, y) => (0.8 - y * y) * Math.Cos(x) + 0.3 * y;

            var analyticalAccuracy = new NotAnalyticalAccuracyCalculator();
            var calc = new Solver(originalFunc, analyticalAccuracy);

            var result = calc.Calculate((A, 0.0), B, 0.01);
            var xs = result.Select(p => p.X).ToArray();

            for (int i = 0; i < result.Count; ++i)
                Console.WriteLine($"X = {xs[i]}; Y_Solved = {result[i].Y}");
        }
    }
}