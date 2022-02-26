namespace NumericalMethods.Core.Utils.Interfaces
{
    public interface IRangedRandomProvider<T>
    {
        T Next(T minValue, T maxValue);
    }
}