using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _21
{
    class Program
    {
        private const int LIMIT = 10000;

        private static int SumOfDivisors(int num)
        {
            var factorization = NumUtils.ComputePrimeFactorization_Cached(num);

            int res = 1;
            foreach (var factor in factorization)
            {
                if (factor.Value == 1)
                {
                    res *= factor.Key + 1;
                }
                else
                {
                    res *= (NumUtils.BinaryPower(factor.Key, factor.Value + 1) - 1) / (factor.Key - 1);
                }
            }

            return res - num;
        }

        private static int[] DivSums(int limit)
        {
            var divisorSums = new int[limit];

            for (int i = 0; i < limit; i++)
            {
                divisorSums[i] = 1;
            }

            for (int i = 2; i <= limit / 2; i++)
            {
                for (int j = i * 2; j < limit; j += i)
                {
                    divisorSums[j] += i;
                }
            }

            return divisorSums;
        }

        private static int SumOfAmicable(int upperLimit)
        {
            int maxPrimeFactor = (int)Math.Sqrt(upperLimit) + 1;

            NumUtils.PrecomputePrimes(maxPrimeFactor);

            int[] sums = new int[upperLimit];

            for (int i = 1; i < upperLimit; i++)
            {
                sums[i] = SumOfDivisors(i);
            }

            int res = 0;

            for (int i = 1; i < upperLimit; i++)
            {
                bool isDifferent = sums[i] != i;
                if (sums[i] > 0 && sums[i] < LIMIT && sums[sums[i]] == i && isDifferent)
                {
                    res += i;
                }
            }
            return res;
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(SumOfAmicable, LIMIT);
        }
    }
}
