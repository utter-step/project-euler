using System;
using Tools;

namespace _057
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 1000);
        }

        private static int Solve(int nToTest)
        {
            var numerator = new LongInteger(3);
            var denominator = new LongInteger(2);

            int res = 0;

            for (int i = 0; i < nToTest; i++)
            {
                if (numerator.ToString().Length > denominator.ToString().Length)
                {
                    res++;
                }

                var temp = denominator;

                denominator += numerator;
                numerator += 2 * temp;
            }

            return res;
        }
    }
}
