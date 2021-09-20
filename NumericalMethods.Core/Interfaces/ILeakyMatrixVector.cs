namespace NumericalMethods.Core.Interfaces
{
    /// <summary>
    /// Представляет коллекцию элементов дырявой матрицы.
    /// </summary>
    /// <typeparam name="T">Тип элемента матрицы.</typeparam>
    public interface ILeakyMatrixVector<T>
    {
        /// <summary>
        /// Возвращает экземпляр T или default, если элемент находится в дырке.
        /// Устанавливает экземпляр T или выбрасывает исключение, если совершена попытка установить элемент в дырку.
        /// </summary>
        T this[int rowIndex, int columnIndex] { get; set; }

        /// <summary>
        /// Возвращает true, если элемент с данными индексами содержится в данной коллекции.
        /// </summary>
        /// <param name="rowIndex">Индекс строки.</param>
        /// <param name="columnIndex">Индекс столбца.</param>
        bool IsBelongs(int rowIndex, int columnIndex);
    }
}
