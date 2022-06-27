using System;

namespace NumericalMethods.Task10.Exceptions
{
    public class RungeSolverException : Exception
    {
        public int IterationNumber { get; }

        public RungeSolverException(int iterationNumber)
            => IterationNumber = iterationNumber;

        public RungeSolverException(int iterationNumber, string message) : base(message)
            => IterationNumber = iterationNumber;
    }
}