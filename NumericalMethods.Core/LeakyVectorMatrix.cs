using System;
using System.Collections.Generic;
using System.Linq;
using NumericalMethods.Core.Interfaces;

namespace NumericalMethods.Core
{
    /// <summary>
    /// Представляет дырявую матрицу построенную на векторах.
    /// </summary>
    /// <typeparam name="T">Тип элемента матрицы.</typeparam>
    public class LeakyVectorMatrix<T> : ILeakyMatrix<T>
    {
        /// <summary>
        /// Дырявые вектора.
        /// </summary>
        private readonly IEnumerable<ILeakyMatrixVector<T>> _leakyMatrixVectors;

        public int RowsCount { get; }

        public int ColumnsCount { get; }

        /// <summary>
        /// Инициализирует дырявую матрицу построенную на векторах.
        /// </summary>
        /// <param name="rowsCount">Число строк.</param>
        /// <param name="columnsCount">Число столбцов.</param>
        /// <param name="leakyMatrixVectors">Перечисление дырявых векторов.</param>
        public LeakyVectorMatrix(int rowsCount, int columnsCount, IEnumerable<ILeakyMatrixVector<T>> leakyMatrixVectors)
        {
            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            _leakyMatrixVectors = leakyMatrixVectors;

            if (HasDifferentElementsAtIntersectionsOfVectors())
                throw new ArgumentException("Элементы на пересечения векторов не должны различаться.", nameof(leakyMatrixVectors));
        }

        /// <summary>
        /// Возвращает true, если существуют разные по значению элементы на пересечении двух векторов.
        /// </summary>
        private bool HasDifferentElementsAtIntersectionsOfVectors()
        {
            for (var i = 0; i < RowsCount; ++i)
            {
                for (var j = 0; j < ColumnsCount; ++j)
                {
                    bool hasDifferingElements = _leakyMatrixVectors
                        .Where(el => el.IsBelongs(i, j))
                        .Select(el => el[i, j])
                        .Distinct()
                        .Count() > 1;
                    
                    if (hasDifferingElements)
                        return true;
                }
            }
            return false;
        }

        public bool IsBelongs(int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= RowsCount)
                throw new ArgumentOutOfRangeException();

            if (columnIndex < 0 || columnIndex >= ColumnsCount)
                throw new ArgumentOutOfRangeException();
            
            foreach (ILeakyMatrixVector<T> vector in _leakyMatrixVectors)
                if (vector.IsBelongs(rowIndex, columnIndex))
                    return true;
            
            return false;
        }

        public T this[int rowIndex, int columnIndex]
        { 
            get
            {
                if (rowIndex < 0 || rowIndex >= RowsCount)
                    throw new ArgumentOutOfRangeException();

                if (columnIndex < 0 || columnIndex >= ColumnsCount)
                    throw new ArgumentOutOfRangeException();

                foreach (ILeakyMatrixVector<T> vector in _leakyMatrixVectors)
                    if (vector.IsBelongs(rowIndex, columnIndex))
                        return vector[rowIndex, columnIndex];

                return default;
            }
            set
            {
                if (rowIndex < 0 || rowIndex >= RowsCount)
                    throw new ArgumentOutOfRangeException();

                if (columnIndex < 0 || columnIndex >= ColumnsCount)
                    throw new ArgumentOutOfRangeException();

                var hasBelongs = false;

                foreach (ILeakyMatrixVector<T> vector in _leakyMatrixVectors)
                {
                    if (vector.IsBelongs(rowIndex, columnIndex))
                    {
                        vector[rowIndex, columnIndex] = value;
                        hasBelongs = true;
                    }
                }

                if (!EqualityComparer<T>.Default.Equals(value, default) && !hasBelongs)
                    throw new ArgumentOutOfRangeException($"Невозможно установить не нулевой элемент в [{rowIndex},{columnIndex}], так как там находится дырка.");
            }
        }
    }
}
