using System;
using System.Collections.Generic;
using NumericalMethods.Task4.Interfaces;
using NumericalMethods.Task4.Models;

namespace NumericalMethods.Task4.Readers
{
    class ConsoleInputReader : IInputParamsReader
    {
        public InputParams Read()
        {
            double a = ReadDouble("Введите начало отрезка: ");
            double b = ReadDouble("Введите конец отрезка: ");
            int n = ReadInt("Введите число разбиений: ");
            IReadOnlyList<double> coefficients = ReadCoefficients();

            return new InputParams(a, b, n, coefficients);
        }

        private IReadOnlyList<double> ReadCoefficients()
        {
            var currentCoefficientIndex = 1;
            var coefficients = new List<double>();
            do
            {
                Console.Write($"Введите коэффициент c{currentCoefficientIndex}: ");
                
                string line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                    return coefficients;

                if (double.TryParse(line, out var number))
                {
                    coefficients.Add(number);
                    ++currentCoefficientIndex;
                }
                else
                    Console.WriteLine("Не удалось распознать введенное значение. Повторите попытку ввода.");

            } while (true);
        }

        private double ReadDouble(string message)
        {
            do
            {
                Console.Write(message);
                
                if (double.TryParse(Console.ReadLine(), out var number))
                    return number;
            } while (true);
        }

        private int ReadInt(string message)
        {
            do
            {
                Console.Write(message);
                
                if (int.TryParse(Console.ReadLine(), out var number))
                    return number;
            } while (true);
        }
    }
}