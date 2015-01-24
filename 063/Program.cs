using System;

using Tools;

namespace _063
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.TimeItAccurate(Solve, 100000);
        }

        private static int Solve()
        {
            int res = 0;

            int n = 1;
            int countForN;

            while ((countForN = CountForN(n)) > 0)
            {
                res += countForN;
                n++;
            }

            return res;
        }

        private static int CountForN(int n)
        {
            double pow = (n - 1) / (double) n;

            return 10 - (int)Math.Ceiling(Math.Pow(10, pow));
        }
    }
}
