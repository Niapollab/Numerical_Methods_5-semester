using System;
using System.Collections.Generic;
using NumericalMethods.Core.Interfaces;

namespace NumericalMethods.Core
{
    public abstract class BaseLeakyMatrixVector<T> : ILeakyMatrixVector<T>
    {
        protected readonly int _rowsCount;

        protected readonly int _columnsCount;

        public BaseLeakyMatrixVector(T[,] matrix)
        {
            _rowsCount = matrix.GetLength(0);
            _columnsCount = matrix.GetLength(1);
        }

        public T this[int rowIndex, int columnIndex] 
        { 
            get
            {
                if (IsBelongs(rowIndex, columnIndex))
                    return SafeGetValue(rowIndex, columnIndex);

                return default;
            }
            set
            {
                if (IsBelongs(rowIndex, columnIndex))
                    SafeSetValue(rowIndex, columnIndex, value);
                else if (!EqualityComparer<T>.Default.Equals(value, default))
                    throw new ArgumentOutOfRangeException("Невозможно установить не нулевой элемент в [{rowIndex},{columnIndex}], так как там находится дырка.");
            }
        }

        public bool IsBelongs(int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >=_rowsCount || columnIndex < 0 || columnIndex >=_columnsCount)
                return false;

            return SafeIsBelongs(rowIndex, columnIndex);
        }

        protected abstract bool SafeIsBelongs(int rowIndex, int columnIndex);

        protected abstract T SafeGetValue(int rowIndex, int columnIndex);

        protected abstract void SafeSetValue(int rowIndex, int columnIndex, T value);
    }
}
