using System;

using Tools;

namespace _047
{
    class Program
    {
        private const int LengthOfSerie = 4;

        private static int CountDistinctPrimeFactors(int num)
        {
            return NumUtils.ComputePrimeFactorization_Cached(num).Count;
        }

        private static int Solve(int lengthOfSerie)
        {
            for (int i = 2; i < int.MaxValue; i++)
            {
                int count = 0;
                while (CountDistinctPrimeFactors(i) == LengthOfSerie && count < LengthOfSerie)
                {
                    count++;
                    i++;
                }
                if (count == LengthOfSerie)
                {
                    return i;
                }
            }
            throw new Exception();
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, LengthOfSerie);
        }
    }
}
