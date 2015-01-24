using System;
using System.Linq;

using Tools;

namespace _243
{
    class Program
    {
        private static int TotientFunc(int num)
        {
            var primeFactors = NumUtils.ComputePrimeFactorization_Cached(num);

            long res = num;

            foreach (var primeFactor in primeFactors)
            {
                res *= primeFactor.Key - 1;
                res /= primeFactor.Key;
            }

            return (int)res;
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 15499 / 94744.0);
        }

        private static int[] _smallPrimes;

        static int Solve(double desiredRatio)
        {
            NumUtils.ResetPrimesCache();

            _smallPrimes = NumUtils.EratospheneSeive(100).ToArray();

            int approximation = 1;
            for (int i = 0; i < _smallPrimes.Length; i++)
            {
                approximation *= _smallPrimes[i];
                double ratio = TotientFunc(approximation) / (double)(approximation - 1);
                if (Math.Abs(ratio - desiredRatio) < desiredRatio / 10)
                {
                    if (ratio < desiredRatio)
                    {
                        approximation /= _smallPrimes[i];
                    }
                    break;
                }
            }

            for (int i = approximation; i < int.MaxValue; i += approximation)
            {
                double ratio = TotientFunc(i) / (double)(i - 1);
                if (ratio < desiredRatio)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}
