using System;
using System.Collections.Generic;

namespace NumericalMethods.Core.Extensions
{
    public static class ClassGetValueExtensions
    {
        public static T GetValueOrDefault<T>(this IReadOnlyList<T> list, int index, T defaultValue = null)
            where T : class
        {
            _ = list ?? throw new ArgumentNullException(nameof(list));
            
            if (index < 0 || index >= list.Count)
                return defaultValue;

            return list[index];
        }
        
        public static T GetValueOrDefault<T>(this T[,] matrix, int rowIndex, int columnIndex, T defaultValue = null)
            where T : class
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            
            if (rowIndex < 0 || rowIndex >= matrix.GetLength(0)
                || columnIndex < 0 || columnIndex >= matrix.GetLength(1))
                return defaultValue;

            return matrix[rowIndex, columnIndex];
        }
    }
}