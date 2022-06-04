using System;

namespace NumericalMethods.Task8.Exceptions
{
    class RungeSolverException : Exception
    {
        public int IterationNumber { get; }

        public RungeSolverException(int iterationNumber)
            => IterationNumber = iterationNumber;

        public RungeSolverException(int iterationNumber, string message) : base(message)
            => IterationNumber = iterationNumber;
    }
}