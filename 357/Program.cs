using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Tools;

namespace _357
{
    class Program
    {
        public const int UPPER_LIMIT = 1000000000;
        public static HashSet<int> Primes;

        public static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, argument: UPPER_LIMIT);
        }

        public static long Solve(int limit)
        {
            NumUtils.ResetPrimesCache();

            Primes = NumUtils.EratospheneSeive(limit + 1);

            long s = 0;

            #if DEBUG
            foreach (var prime in Primes)
            {
                if (IsPrimeGenerating(prime - 1))
                {
                    Console.WriteLine(prime - 1);
                    s += prime - 1;
                }
            }
            #else
            Parallel.ForEach(Primes, prime => {
                if ((prime - 1) % 4 == 2 && IsPrimeGenerating(prime - 1))
                {
                    Interlocked.Add(ref s, prime - 1);
                }
            });
            #endif

            return s + 1;
        }

        public static bool IsPrimeGenerating(int num)
        {
            var upper = NumUtils.SqrtUpper(num);
            for (int i = 2; i <= upper; i++)
            {
                if (num % i == 0 && !Primes.Contains(i + num / i))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
