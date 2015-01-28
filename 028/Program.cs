using System;
using Tools;

namespace _028
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();

            Decorators.TimeItAccurate(ComputeDiagonalSums, 1001, 1000);
        }

        private static int ComputeDiagonalSums(int matrixSide)
        {
            if (matrixSide < 1)
            {
                throw new ArgumentOutOfRangeException("matrixSide");
            }

            if ((matrixSide & 1) == 0)
            {
                throw new ArgumentException("Spiral matrix side length cannot be even number", "matrixSide");
            }

            int maxDiagonal = (matrixSide + 1) / 2;

            int sum = 1;
            int start = 1;

            for (int i = 1; i < maxDiagonal; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    start += i * 2;
                    sum += start;
                }
            }

            return sum;
        }
    }
}
