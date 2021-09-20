namespace NumericalMethods.Core
{
    public class UnderTheMainDiagonal<T> : BaseLeakyMatrixVector<T>
    {
        private readonly T[] _elements;

        public UnderTheMainDiagonal(T[,] matrix) : base(matrix)
        {
            _elements = new T[_rowsCount - 1];
            
            for (var i = 0; i < _elements.Length; ++i)
                _elements[i] = matrix[i + 1, i];
        }

        protected override T SafeGetValue(int rowIndex, int columnIndex)
            => _elements[columnIndex];

        protected override bool SafeIsBelongs(int rowIndex, int columnIndex)
            => rowIndex - columnIndex == 1 
            && rowIndex >= 0
            && rowIndex < _elements.Length + 1
            && columnIndex >= 0
            && columnIndex < _elements.Length;

        protected override void SafeSetValue(int rowIndex, int columnIndex, T value)
            => _elements[columnIndex] = value;
    }
}
