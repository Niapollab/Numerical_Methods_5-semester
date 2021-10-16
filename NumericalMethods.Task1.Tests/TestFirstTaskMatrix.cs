using System;
using System.Collections.Generic;

namespace NumericalMethods.Task1.Tests
{
    public class TestFirstTaskMatrix : FirstTaskMatrix
    {
        private readonly double[,] _realMatrix;

        public TestFirstTaskMatrix(double[,] matrix) : base(matrix)
        {
            _realMatrix = matrix;
        }

        protected override void DevideLine(int rowIndex, double element)
        {
            base.DevideLine(rowIndex, element);

            for (var columnIndex = 0; columnIndex < _columnsCount; ++columnIndex)
                _realMatrix[rowIndex, columnIndex] /= element;

            ThrowIfCorrupted();
        }

        protected override void FirstPhase()
        {
            base.FirstPhase();
            ThrowIfCorrupted();
        }

        protected override void SecondPhase()
        {
            base.SecondPhase();
            ThrowIfCorrupted();
        }

        protected override void ThirdPhase()
        {
            base.ThirdPhase();

            for (var rowIndex = 0; rowIndex < _rowsCount; ++rowIndex)
            {
                if (rowIndex != 5 && _realMatrix[rowIndex, 5] != 0)
                {
                    _realMatrix[rowIndex, 5] -= _realMatrix[5, 5];
                    _realMatrix[rowIndex, 6] -= _realMatrix[5, 6];
                    _realMatrix[rowIndex, _columnsCount - 1] -= _realMatrix[5, _columnsCount - 1];
                }
            }

            ThrowIfCorrupted();
        }

        protected override void FourthPhase()
        {
            base.FourthPhase();

            for (var rowIndex = 0; rowIndex < _rowsCount; ++rowIndex)
            {
                if (rowIndex != 6 && _realMatrix[rowIndex, 6] != 0)
                {
                    _realMatrix[rowIndex, 5] -= _realMatrix[6, 5];
                    _realMatrix[rowIndex, 6] -= _realMatrix[6, 6];
                    _realMatrix[rowIndex, _columnsCount - 1] -= _realMatrix[6, _columnsCount - 1];
                }
            }

            ThrowIfCorrupted();
        }

        protected override void FifthPhase()
        {
            base.FifthPhase();
            ThrowIfCorrupted();
        }

        protected override void SubCurrentFromNext(int rowIndex)
        {
            base.SubCurrentFromNext(rowIndex);

            if (rowIndex < _rowsCount - 1)
                for (var columnIndex = 0; columnIndex < _columnsCount; ++columnIndex)
                    _realMatrix[rowIndex + 1, columnIndex] -= _realMatrix[rowIndex, columnIndex];

            ThrowIfCorrupted();
        }

        protected override void SubPrevFromCurrent(int rowIndex)
        {
            base.SubPrevFromCurrent(rowIndex);

            if (rowIndex > 0)
                for (var columnIndex = 0; columnIndex < _columnsCount; ++columnIndex)
                    _realMatrix[rowIndex - 1, columnIndex] -= _realMatrix[rowIndex, columnIndex];

            ThrowIfCorrupted();
        }

        protected override void CalculatePhase()
        {
            base.CalculatePhase();
            ThrowIfCorrupted();
        }

        private void ThrowIfCorrupted()
        {
            ThrowIfNotEqualIntersectedElements();
            ThrowIfACorrupted();
            ThrowIfBCorrupted();
            ThrowIfCCorrupted();
            ThrowIfColumnCorrupted(_d, 5);
            ThrowIfColumnCorrupted(_e, 6);
            ThrowIfColumnCorrupted(_f, _columnsCount - 1);
        }

        private void ThrowIfNotEqualIntersectedElements()
        {
            var preds = new Dictionary<string, bool>()
            {
                {"_c[4] != _d[4]", _c[4] != _d[4]},
                {"_c[5] != _e[5]", _c[5] != _e[5]},
                {"_b[5] != _d[5]", _b[5] != _d[5]},
                {"_b[6] != _e[6]", _b[6] != _e[6]},
                {"_a[5] != _d[6]", _a[5] != _d[6]},
                {"_a[6] != _e[7]", _a[6] != _e[7]},
            };

            foreach (KeyValuePair<string, bool> pred in preds)
                if (pred.Value)
                    throw new InvalidOperationException(pred.Key);
        }

        private void ThrowIfACorrupted()
        {
            for (var i = 0; i < _a.Length; ++i)
                if (_a[i] != _realMatrix[i + 1, i])
                    throw new InvalidOperationException($"_a[{i}] != _realMatrix[{i + 1}, {i}]");
        }

        private void ThrowIfBCorrupted()
        {
            for (var i = 0; i < _b.Length; ++i)
                if (_b[i] != _realMatrix[i, i])
                    throw new InvalidOperationException($"_b[{i}] != _realMatrix[{i}, {i}]");
        }

        private void ThrowIfCCorrupted()
        {
            for (var i = 0; i < _c.Length; ++i)
                if (_c[i] != _realMatrix[i, i + 1])
                    throw new InvalidOperationException($"_c[{i}] != _realMatrix[{i}, {i + 1}]");
        }

        private void ThrowIfColumnCorrupted(double[] vector, int columnIndex)
        {
            for (var i = 0; i < vector.Length; ++i)
                if (vector[i] != _realMatrix[i, columnIndex])
                    throw new InvalidOperationException($"vector[{i}] != _realMatrix[{i}, {columnIndex}]");
        }
    }
}