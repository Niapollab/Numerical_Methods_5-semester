using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethods.Task1
{
    /// <summary>
    /// Представляет матрицу решения первой задачи.
    /// </summary>
    /// <example>
    /// *	*				*	*					
    /// *	*	*			*	*					
    /// 	*	*	*		*	*					
    /// 		*	*	*	*	*					
    /// 			*	*	*	*					
    /// 				*	*	*					
    /// 					*	*	*				
    /// 					*	*	*	*			
    /// 					*	*	*	*	*		
    /// 					*	*		*	*	*	
    /// 					*	*			*	*	*
    /// 					*	*				*	*
    ///
    ///                     d   e                 a     b     c
    /// f - Right side
    /// </example>
    public class FirstTaskMatrix
    {
        protected readonly int _rowsCount;

        protected readonly int _columnsCount;

        protected readonly double[] _a;

        protected readonly double[] _b;

        protected readonly double[] _c;

        protected readonly double[] _d;

        protected readonly double[] _e;

        protected readonly double[] _f;

        protected readonly double[] _result;

        private bool _solved;

        public IReadOnlyList<double> Result => _result;

        public double Accuracy { get; private set; }

        public FirstTaskMatrix(double[,] matrix)
        {
            _solved = false;

            _rowsCount = matrix.GetLength(0);
            _columnsCount = matrix.GetLength(1);

            _a = new double[_rowsCount - 1];
            for (int i = 0; i + 1 < _rowsCount && i < _columnsCount; ++i)
                _a[i] = matrix[i + 1, i];

            _b = new double[_rowsCount];
            for (int i = 0; i < _rowsCount && i < _columnsCount; ++i)
                _b[i] = matrix[i, i];

            _c = new double[_rowsCount - 1];
            for (int i = 0; i < _rowsCount - 1 && i + 1 < _columnsCount; ++i)
                _c[i] = matrix[i, i + 1];
              
            _d = new double[_rowsCount];
            for (int i = 0; i < _rowsCount && 5 < _columnsCount; ++i)
                _d[i] = matrix[i, 5];

            _e = new double[_rowsCount];
            for (int i = 0; i < _rowsCount && 6 < _columnsCount; ++i)
                _e[i] = matrix[i, 6];

            _f = new double[_rowsCount];
            for (int i = 0; i < _rowsCount; ++i)
                _f[i] = matrix[i, _columnsCount - 1];

            _result = new double[_rowsCount];
        }

        protected virtual void DevideLine(int rowIndex, double element)
        {
            if (IsBelongsToA(rowIndex))
                _a[rowIndex - 1] /= element;

            _b[rowIndex] /= element;
            _d[rowIndex] /= element;
            _e[rowIndex] /= element;
            _f[rowIndex] /= element;

            if (IsBelongsToC(rowIndex))
                _c[rowIndex] /= element;
        }

        protected virtual void SubCurrentFromNext(int rowIndex)
        {
            if (rowIndex < _rowsCount - 1)
            {
                int nextRow = rowIndex + 1;
                _a[rowIndex] -= _b[rowIndex];
                _b[nextRow] -= _c[rowIndex];
                _d[nextRow] -= _d[rowIndex];
                _e[nextRow] -= _e[rowIndex];
                _f[nextRow] -= _f[rowIndex];

                if (nextRow == 4)
                    _c[4] = _d[4];
                else if (nextRow == 5)
                    _c[5] = _e[5];
            }
            else
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "");
        }

        protected virtual void SubPrevFromCurrent(int rowIndex)
        {
            if (rowIndex > 0)
            {
                int prevRow = rowIndex - 1;
                _c[rowIndex - 1] -= _b[rowIndex];
                _b[prevRow] -= _a[prevRow];
                _d[prevRow] -= _d[rowIndex];
                _e[prevRow] -= _e[rowIndex];
                _f[prevRow] -= _f[rowIndex];

                if (prevRow == 7)
                    _a[6] = _e[7];
                else if (prevRow == 6)
                    _a[5] = _d[6];
            }
            else
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "");
        }

        protected virtual void FirstPhase()
        {
            for (var rowIndex = 0; rowIndex <= 5; ++rowIndex)
            {
                if (_b[rowIndex] == 0)
                    continue;

                DevideLine(rowIndex, _b[rowIndex]);

                if (_a[rowIndex] == 0)
                    continue;

                DevideLine(rowIndex + 1, _a[rowIndex]);

                SubCurrentFromNext(rowIndex);
            }
        }

        protected virtual void SecondPhase()
        {
            for (var rowIndex = _rowsCount - 1; rowIndex > 6; --rowIndex)
            {
                if (_b[rowIndex] == 0)
                    continue;

                DevideLine(rowIndex, _b[rowIndex]);

                if (_c[rowIndex - 1] == 0)
                    continue;

                DevideLine(rowIndex - 1, _c[rowIndex - 1]);

                SubPrevFromCurrent(rowIndex);
            }

            if (_e[6] != 0)
                DevideLine(6, _e[6]);
        }

        protected virtual void ThirdPhase()
        {
            for (var rowIndex = 0; rowIndex < _rowsCount; ++rowIndex)
                if (rowIndex != 5 && _d[rowIndex] != 0)
                    DevideLine(rowIndex, _d[rowIndex]);

            for (var rowIndex = 0; rowIndex < _rowsCount; ++rowIndex)
            {
                if (rowIndex != 5 && _d[rowIndex] != 0)
                {
                    _d[rowIndex] -= _d[5];
                    _e[rowIndex] -= _e[5];
                    _f[rowIndex] -= _f[5];
                }
            }

            _a[5] = _d[6];
            _a[6] = _e[7];
            _b[5] = _d[5];
            _b[6] = _e[6];
            _c[4] = _d[4];
            _c[5] = _e[5];

        }

        protected virtual void FourthPhase()
        {
            if (_e[6] != 0)
                DevideLine(6, _e[6]);

            for (var rowIndex = 0; rowIndex < _rowsCount; ++rowIndex)
            {
                if (rowIndex != 6 && _e[rowIndex] != 0)
                    DevideLine(rowIndex, _e[rowIndex]); 
            }

            for (var rowIndex = 0; rowIndex < _rowsCount; ++rowIndex)
            {
                if (rowIndex != 6 && _e[rowIndex] != 0)
                {
                    _d[rowIndex] -= _d[6];
                    _e[rowIndex] -= _e[6];
                    _f[rowIndex] -= _f[6];
                }
            }

            _a[5] = _d[6];
            _a[6] = _e[7];
            _b[5] = _d[5];
            _b[6] = _e[6];
            _c[4] = _d[4];
            _c[5] = _e[5];
        }

        protected virtual void FifthPhase()
        {
            for (var rowIndex = 0; rowIndex < _rowsCount; ++rowIndex)
                if (_b[rowIndex] != 0)
                    DevideLine(rowIndex, _b[rowIndex]);
        }

        protected virtual void CalculatePhase()
        {
            for (var rowIndex = 4; rowIndex <= 7; ++rowIndex)
                _result[rowIndex] = _f[rowIndex];

            for (var rowIndex = 3; rowIndex >= 0; --rowIndex)
                _result[rowIndex] = _f[rowIndex] - _c[rowIndex] * _result[rowIndex + 1];

            for (var rowIndex = 8; rowIndex < _rowsCount; ++rowIndex)
                _result[rowIndex] = _f[rowIndex] - _a[rowIndex - 1] * _result[rowIndex - 1];
        }

        protected virtual bool IsBelongsToC(int rowIndex)
            => rowIndex < _rowsCount - 1;

        protected virtual bool IsBelongsToA(int rowIndex)
            => rowIndex > 0;

        public double[] Solve()
        {
            if (!_solved)
            {
                _solved = true;
                FirstPhase();
                SecondPhase();
                ThirdPhase();
                FourthPhase();
                FifthPhase();
                CalculatePhase();
                Accuracy = CalculateAccuracy();
            }

            return _result;
        }

        public string ToString(int digitsAfterComma, char separator = '\t')
        {
            var builder = new StringBuilder();
            var leakyMatrix = new Dictionary<(int, int), double>();

            for (int i = 0; i + 1 < _rowsCount && i < _columnsCount; ++i)
                if (!leakyMatrix.ContainsKey((i + 1, i)))
                    leakyMatrix.Add((i + 1, i), _a[i]);

            for (int i = 0; i < _rowsCount && i < _columnsCount; ++i)
                if (!leakyMatrix.ContainsKey((i, i)))
                    leakyMatrix.Add((i, i), _b[i]);

            for (int i = 0; i < _rowsCount - 1 && i + 1 < _columnsCount; ++i)
                if (!leakyMatrix.ContainsKey((i, i + 1)))
                    leakyMatrix.Add((i, i + 1), _c[i]);

            for (int i = 0; i < _rowsCount && 5 < _columnsCount; ++i)
                if (!leakyMatrix.ContainsKey((i, 5)))
                    leakyMatrix.Add((i, 5), _d[i]);

            for (int i = 0; i < _rowsCount && 6 < _columnsCount; ++i)
                if (!leakyMatrix.ContainsKey((i, 6)))
                    leakyMatrix.Add((i, 6), _e[i]);

            for (int i = 0; i < _rowsCount; ++i)
                if (!leakyMatrix.ContainsKey((i, _columnsCount - 1)))
                    leakyMatrix.Add((i, _columnsCount - 1), _f[i]);

            for (int i = 0; i < _rowsCount; ++i)
            {
                for (int j = 0; j < _columnsCount; ++j)
                {
                    if (leakyMatrix.ContainsKey((i, j)))
                        builder.Append(Math.Round(leakyMatrix[(i, j)], digitsAfterComma));
                    else
                        builder.Append('0');
                    builder.Append(separator);
                }
                builder.Remove(builder.Length - 1, 1);
                builder.Append(Environment.NewLine);
            }
            builder.Remove(builder.Length - Environment.NewLine.Length, Environment.NewLine.Length);

            return builder.ToString();
        }

        public override string ToString()
            => ToString(2);

        private double CalculateAccuracy()
        {
            double accuracy = 0;

            for (int i = 0; i < _rowsCount; ++i)
                accuracy = Math.Max(accuracy, Math.Abs(_result[i] - 1));

            return accuracy;
        }
    }

    class Program
    {
        static double[,] GenerateMatrix(int rowsCount, int columnsCount, int minValue, int maxValue)
        {
            var random = new Random();
            var matrix = new double[rowsCount, columnsCount];
            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                matrix[i, i] = random.NextNotZero(minValue, maxValue);

                if (i + 1 < matrix.GetLength(1))
                    matrix[i, i + 1] = random.NextNotZero(minValue, maxValue + 1);

                if (i + 1 < matrix.GetLength(0))
                    matrix[i + 1, i] = random.NextNotZero(minValue, maxValue + 1);

                matrix[i, 5] = random.NextNotZero(minValue, maxValue + 1);

                matrix[i, 6] = random.NextNotZero(minValue, maxValue + 1);
            }
            return matrix;
        }

        static double[,] AppendRightSide(double[,] matrix, double[] rightSide)
        {
            var result = new double[matrix.GetLength(0), matrix.GetLength(1) + 1];

            for (int i = 0; i < matrix.GetLength(0); ++i)
                for (int j = 0; j < matrix.GetLength(1); ++j)
                    result[i, j] = matrix[i, j];

            for (int i = 0; i < matrix.GetLength(0); ++i)
                result[i, matrix.GetLength(1)] = rightSide[i];

            return result;
        }

        static double RelativeAccuracy(double[] exactSolution, double[] solution)
        {
            const double Q = 1e-5;
            double relativeAccuracy = 0;

            for (int i = 0; i < exactSolution.Length; ++i)
            {
                if (Math.Abs(exactSolution[i]) > Q)
                {
                    relativeAccuracy = Math.Max(relativeAccuracy, Math.Abs((solution[i] - exactSolution[i]) / exactSolution[i]));
                }
                else
                {
                    relativeAccuracy = Math.Max(relativeAccuracy, Math.Abs(solution[i] - exactSolution[i]));
                }
            }

            return relativeAccuracy;
        }

        static (double Accuracy, double RelativeAccuracy) FindAccuracies(int N, int minValue, int maxValue)
        {
            var rand = new Random();

            var matrixWithoutRightSide = GenerateMatrix(N, N, minValue, maxValue);

            var exactSolution = Enumerable.Range(1, N).Select(x => rand.NextNotZero(minValue, maxValue)).ToArray();
            var exactSingleSolution = Enumerable.Repeat(1, N).Select(x => (double)x).ToArray();

            var rightSideBuilder = new RightSideBuilder(matrixWithoutRightSide);
            var rightSide = rightSideBuilder.CalculateRightSide(exactSolution);
            var singleRightSide = rightSideBuilder.CalculateRightSide(exactSingleSolution);

            var matrix = new FirstTaskMatrix(AppendRightSide(matrixWithoutRightSide, rightSide));
            var solution = matrix.Solve();

            var matrixWithSingleRightSide = new FirstTaskMatrix(AppendRightSide(matrixWithoutRightSide, singleRightSide));
            matrixWithSingleRightSide.Solve();

            return (matrixWithSingleRightSide.Accuracy, RelativeAccuracy(exactSolution, solution));
        }

        static void Main()
        {
            const int TestCount = 3;
            var testCases = new (int n, int minValue, int maxValue)[]
            {
                (10, -10, 10),
                (10, -100, 100),
                (10, -1000, 1000),
                (100, -10, 10),
                (100, -100, 100),
                (100, -1000, 1000),
                (1000, -10, 10),
                (1000, -100, 100),
                (1000, -1000, 1000),
            };

            foreach (var (n, minValue, maxValue) in testCases)
            {
                var dirs = new (double Accuracy, double RelativeAccuracy)[TestCount];
                for (int i = 0; i < TestCount; ++i)
                    dirs[i] = FindAccuracies(n, minValue, maxValue);

                Console.WriteLine($"N = {n}; Min = {minValue}; Max = {maxValue};");
                Console.WriteLine($"Средняя относительная погрешность системы: {dirs.Select(x => x.Accuracy).Average()}");
                Console.WriteLine($"Среднее значение оценки точности: {dirs.Select(x => x.RelativeAccuracy).Average()}");
            }

            Console.ReadKey(true);
        }
    }
}
