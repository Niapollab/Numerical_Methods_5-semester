namespace NumericalMethods.Core.Interfaces
{
    /// <summary>
    /// Представляет дырявую матрицу.
    /// </summary>
    /// <typeparam name="T">Тип элемента матрицы.</typeparam>
    public interface ILeakyMatrix<T>
    {
        /// <summary>
        /// Число строк.
        /// </summary>
        int RowsCount { get; }

        /// <summary>
        /// Число столбцов.
        /// </summary>
        int ColumnsCount { get; }

        /// <summary>
        /// Возвращает true, если элемент с данными индексами содержится в данной матрице.
        /// </summary>
        /// <param name="rowIndex">Индекс строки.</param>
        /// <param name="columnIndex">Индекс столбца.</param>
        bool IsBelongs(int rowIndex, int columnIndex);

        /// <summary>
        /// Возвращает экземпляр T или default, если элемент находится в дырке.
        /// Устанавливает экземпляр T или выбрасывает исключение, если совершена попытка установить элемент в дырку.
        /// </summary>
        T this[int rowIndex, int columnIndex] { get; set; }
    }
}
