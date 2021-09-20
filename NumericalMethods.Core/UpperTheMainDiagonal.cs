namespace NumericalMethods.Core
{
    public class UpperTheMainDiagonal<T> : BaseLeakyMatrixVector<T>
    {
        private readonly T[] _elements;

        public UpperTheMainDiagonal(T[,] matrix) : base(matrix)
        {
            _elements = new T[_rowsCount - 1];
            
            for (var i = 0; i < _elements.Length; ++i)
                _elements[i] = matrix[i, i + 1];
        }

        protected override T SafeGetValue(int rowIndex, int columnIndex)
            => _elements[rowIndex];

        protected override bool SafeIsBelongs(int rowIndex, int columnIndex)
            => columnIndex - rowIndex == 1 
            && rowIndex >= 0
            && rowIndex < _elements.Length
            && columnIndex >= 0
            && columnIndex < _elements.Length + 1;

        protected override void SafeSetValue(int rowIndex, int columnIndex, T value)
            => _elements[rowIndex] = value;
    }
}
