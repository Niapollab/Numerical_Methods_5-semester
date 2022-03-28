using System;
using NumericalMethods.Task5.Models;

namespace NumericalMethods.Task5.Readers
{
    public class MockInputParamsReader : IInputParamsReader
    {
        private readonly InputParams _inputParams;

        public MockInputParamsReader(InputParams inputParams)
            => _inputParams = inputParams ?? throw new ArgumentNullException(nameof(inputParams));

        public InputParams Read()
            => _inputParams;
    }
}