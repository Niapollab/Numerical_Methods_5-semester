namespace NumericalMethods.Core.Utils.Interfaces
{
    public interface IRandomProvider<T>
    {
        T Next();

        T Next(T minValue, T maxValue);
    }
}