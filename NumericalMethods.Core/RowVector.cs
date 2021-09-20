using System;

namespace NumericalMethods.Core
{
    public class RowVector<T> : BaseLeakyMatrixVector<T>
    {
        private readonly int _filterIndex;

        private readonly T[] _elements;

        public RowVector(T[,] matrix, int rowIndex) : base(matrix)
        {
            if (rowIndex < 0 || rowIndex >= _rowsCount)
                throw new ArgumentException();
            
            _filterIndex = rowIndex;
            _elements = new T[_rowsCount];
            
            for (var i = 0; i < _elements.Length; ++i)
                _elements[i] = matrix[rowIndex, i];
        }

        protected override T SafeGetValue(int rowIndex, int columnIndex)
            => _elements[columnIndex];

        protected override bool SafeIsBelongs(int rowIndex, int columnIndex)
            => rowIndex == _filterIndex;

        protected override void SafeSetValue(int rowIndex, int columnIndex, T value)
            => _elements[columnIndex] = value;
    }
}
