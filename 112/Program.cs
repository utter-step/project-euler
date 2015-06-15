using System;
using Tools;

namespace _112
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 0.99);
        }

        private static int Solve(double ratio)
        {
            double bouncy = 0;

            for (int i = 1; i < int.MaxValue; i++)
            {
                if (IsBouncy(i))
                {
                    bouncy++;
                }

                if (bouncy / i == ratio)
                {
                    return i;
                }
            }

            return 0;
        }

        private static bool IsBouncy(int n)
        {
            int prevDigit = n % 10;
            n /= 10;
            int currentDigit = n % 10;

            while (prevDigit - currentDigit == 0 && n > 0)
            {
                prevDigit = currentDigit;
                n /= 10;
                currentDigit = n % 10;
            }

            int sign = Math.Sign(prevDigit - currentDigit);
            int currentSign = sign;

            while (n > 9)
            {
                prevDigit = currentDigit;
                n /= 10;
                currentDigit = n % 10;

                currentSign = Math.Sign(prevDigit - currentDigit);

                if (currentSign != 0 && currentSign != sign)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
