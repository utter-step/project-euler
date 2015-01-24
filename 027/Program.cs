using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tools;

namespace _027
{
    class Program
    {
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        private static int Next(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            if (n > 0)
            {
                return -n - 2;
            }
            return -n;
        }

        static void Main(string[] args)
        {
            Decorators.TimeItAccurate(Solve, 1000, 1000);
        }

        private static int Solve(int maxAB)
        {
            primes = NumUtils.EratospheneSeive(maxAB * maxAB + maxAB);

            int maxA = 0,
                maxB = 0,
                maxLength = 0;

            foreach (var b in primes)
            {
                if (b > maxAB)
                {
                    return maxA * maxB;
                }
                int a = 0;
                while (a <= maxAB)
                {
                    int len = TestSequence(a, b);
                    if (len > maxLength)
                    {
                        maxLength = len;
                        maxA = a;
                        maxB = b;
                    }
                    a = Next(a);
                }
            }
            return maxA * maxB;
        }

        private static HashSet<int> primes;

        private static int TestSequence(int a, int b)
        {
            for (int n = 0; n < int.MaxValue; n++)
            {
                int possiblePrime = n * n + n * a + b;

                if (!primes.Contains(possiblePrime))
                {
                    return n;
                }
            }
            throw new Exception();
        }
    }
}
