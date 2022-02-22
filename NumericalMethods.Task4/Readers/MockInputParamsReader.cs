using System;
using NumericalMethods.Task4.Interfaces;
using NumericalMethods.Task4.Models;

namespace NumericalMethods.Task4.Readers
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