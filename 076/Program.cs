using System;
using System.Linq;

using Tools;

namespace _76
{
    class Program
    {
        private static int ComputeSummationsOptimal(int sum)
        {
            int[] nominals = Enumerable.Range(1, 99).ToArray();

            int[] ways = new int[sum + 1];
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

        private static int Solve()
        {
            return ComputeSummationsOptimal(100);
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve);
        }
    }
}
