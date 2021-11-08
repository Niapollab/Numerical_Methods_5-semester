using NUnit.Framework;

namespace NumericalMethods.Task1.Tests
{
    [TestFixture]
    public class FirstTaskMatrixTests
    {
        [Test]
        public void CalculateMoodleTest10x10()
        {
            double[,] rawMatrix = GetMoodleMatrix10x10();
            var matrix = new TestFirstTaskMatrix(rawMatrix);

            Assert.DoesNotThrow(() => matrix.Solve());
        }

        private static double[,] GetMoodleMatrix10x10()
        {
            return new double[,]
            {
                {-9, -6, 0, 0, 0, -4, -7, 0, 0, 0, -37},
                {-5, 2, 10, 0, 0, 1, -2, 0, 0, 0, 5},
                {0, -7, -7, -2, 0, -7, -6, 0, 0, 0, -42},
                {0, 0, 6, -1, -1, -1, -2, 0, 0, 0, -2},
                {0, 0, 0, -4, 1, 1, 0, 0, 0, 0, -1},
                {0, 0, 0, 0, 9, -7, 8, 0, 0, 0, 11},
                {0, 0, 0, 0, 0, 5, 0, -8, 0, 0, -6},
                {0, 0, 0, 0, 0, 10, 2, 3, 8, 0, 46},
                {0, 0, 0, 0, 0, -2, 6, 8, 6, 8, 52},
                {0, 0, 0, 0, 0, 6, -1, 0, -6, -8, -18}
            };
        }
    }
}