using System;
using System.Numerics;

using Tools;

namespace _080
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 100);
        }

        private static int Solve(int limit)
        {
            int res = 0;

            for (int i = 2; i <= limit; i++)
            {
                int sqrt = (int)Math.Sqrt(i);

                if (sqrt * sqrt == i)
                {
                    continue;
                }

                res += SumOfSqrtDigits(i, 100);
            }

            return res;
        }

        private static int SumOfSqrtDigits(int n, int nDigits)
        {
            int res = 0;

            int hundredsPow = 0;
            int hundreds = 1;

            while (hundreds * 100 < n)
            {
                hundredsPow++;
                hundreds *= 100;
            }

            BigInteger r = 0;
            int x;
            BigInteger p = 0;
            BigInteger c, y;

            for (int i = 0; i < nDigits; i++)
            {
                r *= 100;
                c = r;

                if (hundreds > 0)
                {
                    c += n / hundreds;
                    n -= n / hundreds;

                    hundreds /= 100;
                }

                x = 0;

                while (x * (20 * p + x) <= c)
                {
                    x++;
                }
                x = x - 1;
                y = x * (20 * p + x);

                res += x;
                p = p * 10 + x;

                r = c - y;
            }

            return res;
        }
    }
}
