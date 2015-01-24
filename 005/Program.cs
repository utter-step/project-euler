using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _5
{
    class Program
    {
        private static long ComputeDivisibleByRange(int lo, int hi)
        {
            var mults = new int[hi + 1];
            for (int i = lo; i <= hi; i++)
            {
                var factors = NumUtils.ComputePrimeFactorization(i);
                foreach (var factor in factors)
                {
                    mults[factor.Key] = mults[factor.Key] < factor.Value ? factor.Value : mults[factor.Key];
                }
            }

            long res = 1;

            for (int i = lo; i <= hi; i++)
            {
                while (mults[i] > 0)
                {
                    res *= i;
                    mults[i]--;
                }
            }

            return res;
        }

        private static long Solve(int max)
        {
            return ComputeDivisibleByRange(1, max);
        }

        static void Main(string[] args)
        {
            Decorators.TimeIt(Solve, 20);

            Console.WriteLine();
            Console.WriteLine();

            Decorators.TimeItAccurate(Solve, 20, 100000);
        }
    }
}
