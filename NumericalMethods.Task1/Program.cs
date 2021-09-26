using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethods.Task1
{
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
        private readonly int _rowsCount;

        private readonly int _columnsCount;

        private readonly double[] _a;

        private readonly double[] _b;

        private readonly double[] _c;

        private readonly double[] _d;

        private readonly double[] _e;

        private readonly double[] _f;

        public FirstTaskMatrix(double[,] matrix)
        {
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

            FirstPhase();
            // SecondPhase();
            // ThirdPhase();
            // FourthPhase();
        }
        private void MultiplyLine(int rowIndex, double element)
        {
            if (IsBelongsToA(rowIndex))
                _a[rowIndex - 1] *= element;

            _b[rowIndex] *= element;
            _d[rowIndex] *= element;
            _e[rowIndex] *= element;
            _f[rowIndex] *= element;

            if (IsBelongsToC(rowIndex))
                _c[rowIndex] *= element;
        }

        private void DevideLine(int rowIndex, double element)
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

        private void SubCurrentFromNext(int rowIndex)
        {
            if (rowIndex < _rowsCount - 1)
            {
                _a[rowIndex] -= _b[rowIndex];
                _b[rowIndex + 1] -= _c[rowIndex];
                _d[rowIndex + 1] -= _d[rowIndex];
                _e[rowIndex + 1] -= _e[rowIndex];
                _f[rowIndex + 1] -= _f[rowIndex];
            }
            else
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "");
        }

        private void SubPrevFromCurrent(int rowIndex)
        {
            if (rowIndex > 0)
            {
                _c[rowIndex - 1] -= _b[rowIndex];
                _b[rowIndex - 1] -= _a[rowIndex - 1];
                _d[rowIndex] -= _d[rowIndex - 1];
                _e[rowIndex] -= _e[rowIndex - 1];
                _f[rowIndex] -= _f[rowIndex - 1];
            }
            else
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "");
        }

        private void FirstPhase()
        {
            for (int i = 0; i <= 5; ++i)
            {
                if (_b[i] == 0)
                    continue;

                DevideLine(i, _b[i]);
                ThrowIfNotEqual();

                if (_a[i] == 0)
                    continue;

                DevideLine(i + 1, _a[i]);
                ThrowIfNotEqual();

                SubCurrentFromNext(i);
                ThrowIfNotEqual();
            }
        }

        private void SecondPhase()
        {
            for (int i = _rowsCount - 1; i > 6; --i)
            {
                if (_b[i] == 0)
                    continue;

                DevideLine(i, _b[i]);
                ThrowIfNotEqual();

                if (_c[i - 1] == 0)
                    continue;

                DevideLine(i - 1, _c[i - 1]);
                ThrowIfNotEqual();

                SubPrevFromCurrent(i);
                ThrowIfNotEqual();
            }
        }

        private void ThirdPhase()
        {
            if (_e[6] != 0)
            {
                DevideLine(6, _e[6]);
                ThrowIfNotEqual();
            }
        }

        private void FourthPhase()
        {
            for (int i = 0; i < _rowsCount; ++i)
            {
                if (i != 5 && _d[i] != 0)
                {
                    DevideLine(i, _d[i]);
                    ThrowIfNotEqual();

                    _d[i] -= _d[5];
                    _e[i] -= _e[5];
                }
            }
            //_a[5] = _d[6];
            //_a[6] = _e[7];

            //_b[6] = _e[6];

            //_c[4] = _d[4];
            //_c[4] = _e[5];
        }

        private bool IsBelongsToC(int rowIndex)
            => rowIndex < _rowsCount - 1;

        private static bool IsBelongsToA(int rowIndex)
            => rowIndex > 0;

        private void ThrowIfNotEqual()
        {
            if (_c[4] != _d[4]
                || _c[5] != _e[5]
                || _b[5] != _d[5]
                || _b[6] != _e[6]
                || _a[5] != _d[6]
                || _a[6] != _e[7])
                throw new InvalidOperationException();
        }

        private string ToString(int digitsAfterComma, char separator = '\t')
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
    }

    class Program
    {
        static double[,] GenerateMatrix()
        {
            var random = new Random();
            var matrix = new double[12, 12];
            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                matrix[i, i] = random.Next(2, 10);

                if (i + 1 < matrix.GetLength(1))
                    matrix[i, i + 1] = random.Next(2, 10);

                if (i + 1 < matrix.GetLength(0))
                    matrix[i + 1, i] = random.Next(2, 10);

                matrix[i, 5] = random.Next(2, 10);

                matrix[i, 6] = random.Next(2, 10);

            }
            return matrix;
        }

        static void Main(string[] args)
        {
            var rawMatrix = new double[,]
            {
                {-9, -6, 0, 0, 0, -4, -7, 0, 0, 0, -37},
                {-5, 2, 10, 0, 0, 1, -2, 0, 0, 0, 5},
                {0, -7, -7, -2, 0, -7, -6, 0, 0, 0, -42},
                {0, 0, 6, -1, -1, -1, -2, 0, 0, 0, -2},
                {0, 0, 0, -4, 1, 1, 0, 0, 0, 0, -1},
                {0, 0, 0, 0, 9, -7, 8, 0, 0, 0, 11},
                {0, 0, 0, 0, 0, 5, 0, -8, 0, 0, -6},
                {0, 0, 0, 0, 0, 10, 2, 3, 8, 0, 46},
                {0, 0, 0, 0, 0, -2, 6, 8, 6, 8, 52},
                {0, 0, 0, 0, 0, 6, -1, 0, -6, -8, -18}
            };
            // var rawMatrix = GenerateMatrix();
            var matrix = new FirstTaskMatrix(rawMatrix);

            Console.WriteLine(matrix);

            Console.ReadKey(true);
        }
    }
}
