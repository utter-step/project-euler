using System;
using System.Linq;

using Tools;

namespace _076
{
    class Program
    {
        private static int[] Nominals;
        private const int PRIME_LIM = 100;

        private static int ComputeSummationsOptimal(int sum)
        {
            int[] ways = new int[sum + 1];
            ways[0] = 1;

            for (int i = 0; i < Nominals.Length; i++)
            {
                for (int j = Nominals[i]; j <= sum; j++)
                {
                    ways[j] += ways[j - Nominals[i]];
                }
            }

            return ways[sum];
        }

        private static int Solve()
        {
            Nominals = NumUtils.EratospheneSeive(PRIME_LIM).ToArray();
            for (int i = 10; i < PRIME_LIM; i++)
            {
                if (ComputeSummationsOptimal(i) > 5000)
                {
                    return i;
                }
            }

            return 0;
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve);
        }
    }
}
