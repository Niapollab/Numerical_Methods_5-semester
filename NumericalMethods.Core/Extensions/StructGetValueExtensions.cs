using System;
using System.Collections.Generic;

namespace NumericalMethods.Core.RandomAccess.Extensions
{
    public static class StructGetValueExtensions
    {
        public static Nullable<T> GetValueOrDefault<T>(this IReadOnlyList<Nullable<T>> list, int index, Nullable<T> defaultValue = null)
            where T : struct
        {
            _ = list ?? throw new ArgumentNullException(nameof(list));
            
            if (index < 0 || index >= list.Count)
                return defaultValue;

            return list[index];
        }

        public static Nullable<T> GetValueOrDefault<T>(this IReadOnlyList<T> list, int index, Nullable<T> defaultValue = null)
            where T : struct
        {
            _ = list ?? throw new ArgumentNullException(nameof(list));
            
            if (index < 0 || index >= list.Count)
                return defaultValue;

            return list[index];
        }
        
        public static Nullable<T> GetValueOrDefault<T>(this Nullable<T>[,] matrix, int rowIndex, int columnIndex, Nullable<T> defaultValue = null)
            where T : struct
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));

            if (rowIndex < 0 || rowIndex >= matrix.GetLength(0)
                || columnIndex < 0 || columnIndex >= matrix.GetLength(1))
                return defaultValue;

            return matrix[rowIndex, columnIndex];
        }

        public static Nullable<T> GetValueOrDefault<T>(this T[,] matrix, int rowIndex, int columnIndex, Nullable<T> defaultValue = null)
            where T : struct
        {
            _ = matrix ?? throw new ArgumentNullException(nameof(matrix));
            
            if (rowIndex < 0 || rowIndex >= matrix.GetLength(0)
                || columnIndex < 0 || columnIndex >= matrix.GetLength(1))
                return defaultValue;

            return matrix[rowIndex, columnIndex];
        }
    }
}