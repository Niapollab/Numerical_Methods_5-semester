using System;

namespace NumericalMethods.Task3.Models
{
    class InputParams
    {
        public InputParams(double[,] matrix, double eigenvalueAccuracy, double eigenvectorAccuracy, int maxIterationsNumber)
        {
            Matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));
            EigenvalueAccuracy = eigenvalueAccuracy <= 0 ? throw new ArgumentException("Eigenvalue accuracy must be greater than zero.", nameof(eigenvalueAccuracy)) : eigenvalueAccuracy;
            EigenvectorAccuracy = eigenvectorAccuracy <= 0 ? throw new ArgumentException("Eigenvector accuracy must be greater than zero.", nameof(eigenvectorAccuracy)) : eigenvectorAccuracy;
            MaxIterationsNumber = maxIterationsNumber <= 0 ? throw new ArgumentException("Max iterations number must be greater than zero.", nameof(maxIterationsNumber)) : maxIterationsNumber;
        }

        public double[,] Matrix { get; }

        public double EigenvalueAccuracy { get; }

        public double EigenvectorAccuracy { get; }

        public int MaxIterationsNumber { get; }
    }
}