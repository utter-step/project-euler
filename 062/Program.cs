using System;
using System.Collections.Generic;

using Tools;

namespace _062
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 3);
        }

        private static long Solve(int chainLength)
        {
            var counts = new Dictionary<string, int>();
            var smallestCubesForPermutation = new Dictionary<string, long>();

            for (long i = 1; i < int.MaxValue; i++)
            {
                var cube = i * i * i;

                var sPerm = SmallestPermutation(cube);

                if (counts.ContainsKey(sPerm))
                {
                    counts[sPerm]++;

                    if (counts[sPerm] == chainLength)
                    {
                        return smallestCubesForPermutation[sPerm];
                    }
                }
                else
                {
                    counts.Add(sPerm, 1);
                    smallestCubesForPermutation.Add(sPerm, cube);
                }
            }

            return -1;
        }

        private static string SmallestPermutation(long num)
        {
            var chars = num.ToString().ToCharArray();

            Array.Sort(chars);

            return new String(chars);
        }
    }
}
