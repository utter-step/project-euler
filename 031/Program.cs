using System;
using Tools;

namespace _31
{
    class Program
    {
        private const int SUM = 2000;

        #region Primitive
        private static long ComputeChange(int penceSum)
        {
            long variants = 0;

            for (int i = penceSum; i >= 0; i -= 200)
            {
                for (int j = i; j >= 0; j -= 100)
                {
                    for (int k = j; k >= 0; k -= 50)
                    {
                        for (int l = k; l >= 0; l -= 20)
                        {
                            for (int m = l; m >= 0; m -= 10)
                            {
                                for (int n = m; n >= 0; n -= 5)
                                {
                                    for (int o = n; o >= 0; o -= 2)
                                    {
                                        variants++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return variants;
        } 
        #endregion

        #region Recursive
        private static readonly int[] _nominals =
            {
                200, 100, 50, 20, 10, 5, 2, 1
            };

        private static long[,] memo = new long[SUM + 1, _nominals.Length];

        private static long ComputeChangeRecursive(int sum, int step = 0)
        {
            long res = 0;

            if (step == 7)
            {
                return 1;
            }

            int targetSum = sum;
            if (memo[targetSum, step] > 0)
            {
                return memo[targetSum, step];
            }

            while (sum >= 0)
            {
                res += ComputeChangeRecursive(sum, step + 1);
                sum -= _nominals[step];
            }

            memo[targetSum, step] = res;
            return res;
        } 
        #endregion

        #region Optimal
        private static long ComputeChangeOptimal(int sum)
        {
            int[] nominals = { 1, 2, 5, 10, 20, 50, 100, 200 };

            long[] ways = new long[sum + 1];
            ways[0] = 1;

            for (int i = 0; i < nominals.Length; i++)
            {
                for (int j = nominals[i]; j <= sum; j++)
                {
                    ways[j] += ways[j - nominals[i]];
                }
            }

            return ways[sum];
        }
        #endregion

        private static long RecursiveWrapper(int sum)
        {
            return ComputeChangeRecursive(sum);
        }

        static void Main(string[] args)
        {
            Decorators.TimeItAccurate(ComputeChange, SUM, 2);
            Decorators.TimeItAccurate(RecursiveWrapper, SUM, 10);
            Decorators.TimeItAccurate(ComputeChangeOptimal, SUM, 10);
        }
    }
}
