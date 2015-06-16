using System;
using System.Collections.Generic;
using System.Linq;

using Tools;

namespace _087
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 50000000);
        }

        private static int Solve(int upperLimit)
        {
            var sums = new HashSet<int>();

            int squareRoot = NumUtils.SqrtUpper(upperLimit);
            int cubicRoot = (int)Math.Pow(upperLimit, 1.0 / 3) + 1;
            int fourthRoot = (int)Math.Pow(upperLimit, 1.0 / 4) + 1;

            var primes = NumUtils.EratospheneSeive(squareRoot).ToArray();

            for (int i = 0; i < primes.Length && primes[i] < fourthRoot; i++)
            {
                int a = primes[i];
                int fourth = a * a * a * a;
                for (int j = 0; j < primes.Length && primes[j] < cubicRoot; j++)
                {
                    int b = primes[j];
                    int third = b * b * b;
                    for (int k = 0; k < primes.Length && primes[k] < squareRoot; k++)
                    {
                        int candidate = fourth
                                        + third
                                        + primes[k] * primes[k];

                        if (candidate < upperLimit)
                        {
                            sums.Add(candidate);
                        }
                    }
                }
            }

            return sums.Count;
        }
    }
}
