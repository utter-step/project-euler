using System;

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
            Tools.Decorators.TimeIt(SumSquareDifference, 100);

            Tools.Decorators.TimeItAccurate(SumSquareDifference, 100, 10000000);
        }
    }
}
