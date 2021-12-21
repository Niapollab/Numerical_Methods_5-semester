using System;
using NumericalMethods.Task3.Interfaces;
using NumericalMethods.Task3.Models;

namespace NumericalMethods.Task3.Readers
{
    class MockInputParamsReader : IInputParamsReader
    {
        private readonly InputParams _inputParams;

        public MockInputParamsReader(InputParams inputParams)
        {
            _inputParams = inputParams ?? throw new ArgumentNullException(nameof(inputParams));
        }

        public InputParams Read()
        {
            return _inputParams;
        }
    }
}