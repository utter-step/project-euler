using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _069
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 1000000);
        }

        private static int Solve(int upperLimit)
        {
            NumUtils.ResetPrimesCache();

            _smallPrimes = NumUtils.EratospheneSeive(100).ToArray();

            int approximate = 1;

            for (int i = 0; i < _smallPrimes.Length; i++)
            {
                approximate *= _smallPrimes[i];
                if (approximate >= upperLimit)
                {
                    approximate /= _smallPrimes[i];
                    break;
                }
            }

            double maxRatio = 0;

            int maxN = 0;

            for (int n = approximate; n < upperLimit; n += approximate)
            {
                int totients = TotientFunc(n);

                if (totients == n - 1)
                {
                    continue;
                }

                double ratio = (double)n / totients;

                if (ratio > maxRatio)
                {
                    maxRatio = ratio;
                    maxN = n;
                }
            }

            return maxN;
        }

        private static int[] _smallPrimes;

        private static int TotientFunc(int num)
        {
            int k = 1;

            if ((num & 1) == 0)
            {
                num >>= 1;
                while ((num & 1) == 0)
                {
                    k <<= 1;
                    num >>= 1;
                }
            }

            var primeFactors = NumUtils.ComputePrimeFactorization_Cached(num);

            long res = num;

            foreach (var primeFactor in primeFactors)
            {
                res *= primeFactor.Key - 1;
                res /= primeFactor.Key;
            }

            return (int)res * k;
        }
    }
}
