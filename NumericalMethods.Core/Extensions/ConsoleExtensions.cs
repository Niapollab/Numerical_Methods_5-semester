using System;
using System.Collections.Generic;

namespace NumericalMethods.Core.Extensions
{
    public static class ConsoleExtensions
    {
        public static IEnumerable<string> EnumerateLines()
        {
            var line = Console.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                yield return line;
                line = Console.ReadLine();
            }
        }
    }
}
