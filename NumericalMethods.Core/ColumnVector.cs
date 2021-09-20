using System;

namespace NumericalMethods.Core
{
    public class ColumnVector<T> : BaseLeakyMatrixVector<T>
    {
        private readonly int _filterIndex;

        private readonly T[] _elements;

        public ColumnVector(T[,] matrix, int columnIndex) : base(matrix)
        {
            if (columnIndex < 0 || columnIndex >= _columnsCount)
                throw new ArgumentException();
            
            _filterIndex = columnIndex;
            _elements = new T[_columnsCount];
            
            for (var i = 0; i < _elements.Length; ++i)
                _elements[i] = matrix[i, columnIndex];
        }

        protected override T SafeGetValue(int rowIndex, int columnIndex)
            => _elements[rowIndex];

        protected override bool SafeIsBelongs(int rowIndex, int columnIndex)
            => columnIndex == _filterIndex;

        protected override void SafeSetValue(int rowIndex, int columnIndex, T value)
            => _elements[rowIndex] = value;
    }
}
