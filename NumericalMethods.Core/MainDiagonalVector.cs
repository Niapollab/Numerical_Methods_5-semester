namespace NumericalMethods.Core
{
    public class MainDiagonalVector<T> : BaseLeakyMatrixVector<T>
    {
        private readonly T[] _elements;

        public MainDiagonalVector(T[,] matrix) : base(matrix)
        {
            _elements = new T[_rowsCount];
            
            for (var i = 0; i < _elements.Length; ++i)
                _elements[i] = matrix[i, i];
        }

        protected override T SafeGetValue(int rowIndex, int columnIndex)
            => _elements[columnIndex];

        protected override bool SafeIsBelongs(int rowIndex, int columnIndex)
            => rowIndex == columnIndex 
            && rowIndex >= 0 
            && rowIndex < _elements.Length;

        protected override void SafeSetValue(int rowIndex, int columnIndex, T value)
            => _elements[columnIndex] = value;
    }
}
