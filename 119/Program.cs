using System;
using System.Collections.Generic;

using Tools;

namespace _119
{
    class Program
    {
        public const int N = 30;
        public const int MAX_POW = 10;

        public static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 5000);
        }

        public static long Solve()
        {
            var seq = new List<long>(N * 3);

            for (long i = 2; i < N * 3; i++)
            {
                for (int j = 2; j < MAX_POW; j++)
                {
                    var p = NumUtils.BinaryPower(i, j);

                    if (p.SumOfDigits() == i)
                    {
                        seq.Add(p);
                    }
                }
            }

            seq.Sort();

            return seq[N - 1];
        }
    }
}
