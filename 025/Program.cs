using System;

using Tools;
using System.Numerics;

namespace _025
{
    class Program
    {
        private const int LIMIT = 1000;

        private static int BinarySearch(int limit, int lo, int hi)
        {
            if (hi == lo) {
                return lo;
            }

            var  fib = new DynamicMatrix(new BigInteger[,] 
            { 
                { 0, 1 },
                { 1, 1 } 
            });

            int med = lo + (hi - lo) / 2;
            if (NumUtils.BinaryPower(fib, med)[0, 1].ToString().Length < limit)
            {
                return BinarySearch(limit, med + 1, hi);
            }
            else
            {
                return BinarySearch(limit, lo, med);
            }
        }

        private static int Solve(int countOfDigits)
        {
            return BinarySearch(countOfDigits, 1, 10000);
        }

        static void Main(string[] args)
        {
            Decorators.TimeIt(Solve, 1000);
        }
    }
}
