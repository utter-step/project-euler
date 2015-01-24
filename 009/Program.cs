using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _9
{
    class Program
    {
        private static int FindPythagoreanC(int a, int b, int limit)
        {
            int possibleC = limit - a - b;
            if (possibleC * possibleC == a * a + b * b)
            {
                return possibleC;
            }
            return -1;
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 1000);
        }

        private static long Solve(int sumLimit)
        {
            long res = 1;
            int aLimit = sumLimit / 3;

            for (int a = 1; a < sumLimit / 3; a++)
            {
                int bLimit = sumLimit - a - aLimit;
                for (int b = a; b < bLimit; b++)
                {
                    int c = FindPythagoreanC(a, b, sumLimit);
                    if (c > 0)
                    {
                        res = res * a * b * c;
                    }
                }
            }

            return res;
        }
    }
}
