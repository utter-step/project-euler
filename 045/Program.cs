using System;

using Tools;

namespace _045
{
    static class Program
    {
        static void Main()
        {
            Decorators.TimeItAccurate(Solve, 10);
        }

        private static int Solve()
        {
            for (int n = 144; n < int.MaxValue; n++)
            {
                if (IsPentagonal(GetHexagonal(n)))
                {
                    return GetHexagonal(n);
                }
            }
            throw new Exception("Something is wrong in my solution...");
        }

        private static bool IsPentagonal(int x)
        {
            double testN = (Math.Sqrt(24L * x + 1) + 1) / 6;
            if (testN == (int)testN)
            {
                return true;
            }
            return false;
        }

        private static int GetHexagonal(int n)
        {
            return n * (2 * n - 1);
        }
    }
}
