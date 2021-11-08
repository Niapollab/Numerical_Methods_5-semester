namespace NumericalMethods.Task1
{
    public class RightSideBuilder
    {
        private readonly double[,] _matrix;

        public RightSideBuilder(double[,] matrix)
        {
            _matrix = (double[,])matrix.Clone();
        }

        public double[] CalculateRightSide(double[] expectedAnswer)
        {
            var rightSide = new double[_matrix.GetLength(0)];

            for (var i = 0; i < _matrix.GetLength(0); ++i)
                for (var j = 0; j < _matrix.GetLength(1); ++j)
                    rightSide[i] += _matrix[i, j] * expectedAnswer[j];

            return rightSide;
        }
    }
}
