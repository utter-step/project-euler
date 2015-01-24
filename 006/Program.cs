using System;

using Tools;

namespace _6
{
    class Program
    {
        private static long SumOfSquares(int start, int end)
        {
            Func<int, long> sumOfSquares = x => (((long)x + 1) * (2 * x + 1) * x) / 6; 

            long res = sumOfSquares(end) - sumOfSquares(start - 1);

            return res;
        }

        private static long SquareOfSum(int start, int end)
        {
            int sum = (end * (end + 1)) / 2 - ((start - 1) * start) / 2;

            return (long)sum * sum;
        }

        private static long SumSquareDifference(int end)
        {
            return SquareOfSum(1, end) - SumOfSquares(1, end);
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(SumSquareDifference, 100);
        }
    }
}
