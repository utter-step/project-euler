using System;
using Tools;

namespace _020
{
    static class Program
    {
        static void Main()
        {
            Decorators.Benchmark(Solve, 100);
        }

        public static int Solve(int n)
        {
            var li = LongInteger.One;

            for (int i = 2; i <= n; i++)
            {
                int tI = i;
                while (tI % 10 == 0)
                {
                    tI /= 10;
                }

                li *= tI;
            }

            int res = 0;

            foreach (var c in li.ToString())
            {
                res += c - '0';
            }

            return res;
        }
    }
}
