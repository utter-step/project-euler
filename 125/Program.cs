using System;
using System.Collections.Generic;
using System.Linq;

using Tools;

namespace _125
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 100000000);
        }

        public static long Solve(int limit)
        {
            var palindromicSums = new HashSet<long>();

            int squareLimit = (int)Math.Sqrt(limit) + 1;

            var partSums = SquarePartialSums(squareLimit);

            for (int window = 2; window < partSums.Length; window++)
            {
                for (int i = 0; i < partSums.Length - window; i++)
                {
                    long sum = partSums[i + window] - partSums[i];
                    if (sum < limit && NumUtils.IsPalindromic(sum.ToString()))
                    {
                        palindromicSums.Add(sum);
                    }
                }
            }

            return palindromicSums.Sum();
        }

        public static long[] SquarePartialSums(int limit)
        {
            var res = new long[limit + 1];
            res[0] = 0;

            for (int i = 1; i <= limit; i++)
            {
                res[i] = res[i - 1] + i * i;
            }

            return res;
        }
    }
}
