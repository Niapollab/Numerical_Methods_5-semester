using System;
using NumericalMethods.Task5.Models;

namespace NumericalMethods.Task5.Readers
{
    class ConsoleInputReader : IInputParamsReader
    {
        public InputParams Read()
        {
            double segmentStart = ReadDouble("Введите начало отрезка: ");
            double segmentEnd = ReadDouble("Введите конец отрезка: ");
            int segmentCount = ReadInt("Введите число разбиений: ");

            return new InputParams(segmentStart, segmentEnd, segmentCount);
        }

        private double ReadDouble(string message)
        {
            do
            {
                Console.Write(message);
                
                if (double.TryParse(Console.ReadLine(), out var number))
                    return number;
                else
                    Console.WriteLine("Не удалось распознать введенное значение. Повторите попытку ввода.");
            } while (true);
        }

        private int ReadInt(string message)
        {
            do
            {
                Console.Write(message);
                
                if (int.TryParse(Console.ReadLine(), out var number))
                    return number;
                else
                    Console.WriteLine("Не удалось распознать введенное значение. Повторите попытку ввода.");
            } while (true);
        }
    }
}