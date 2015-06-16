using System;
using System.Collections.Generic;

using Tools;

namespace _003
{
    class Program
    {
        private const long NUM_TO_SOLVE = 600851475143L;

        static void Main(string[] args)
        {
            Decorators.Benchmark(MaxPrimeFactor, NUM_TO_SOLVE);
        }

        private static long MaxPrimeFactor(long num)
        {
            int factorLimit = NumUtils.SqrtUpper(num);

            var primes = NumUtils.EratospheneSeive(factorLimit, 3);

            int max = 0;

            foreach (var prime in primes)
            {
                if (num % prime == 0)
                {
                    do {
                        num /= prime;
                    } while (num % prime == 0);
                    max = prime;
                }

                if (num == 1)
                {
                    break;
                }
            }

            return max > num ? max : num;
        }
    }
}
