using Tools;

namespace _072
{
    class Program
    {
        private static long TotientFunc(int num)
        {
            var primeFactors = NumUtils.ComputePrimeFactorization_Cached(num);

            long res = num;

            foreach (var primeFactor in primeFactors)
            {
                res *= primeFactor.Key - 1;
                res /= primeFactor.Key;
            }

            return res;
        }

        public static long CountFractions(int maxDenom)
        {
            long res = 0;

            for (int i = 2; i <= maxDenom; i++)
            {
                res += TotientFunc(i);
            }

            return res;
        }

        public static long Solve(int maxDenom)
        {
            NumUtils.ResetPrimesCache();
            return CountFractions(maxDenom);
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 1000000);
        }
    }
}
