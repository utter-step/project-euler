using System;

namespace _30
{
    class Program
    {
        #region Powers of five array (0 to 9)
        private static readonly int[] _NToThePowerOfFive =
            {
                0,
                1,
                32,
                243,
                1024,
                25 * 125,
                36 * 36 * 6,
                49 * 49 * 7,
                64 * 64 * 8,
                81 * 81 * 9
            };
        #endregion

        private static int PowerOfFiveDigitSum(int n)
        {
            int res = 0;

            while (n > 0)
            {
                res += _NToThePowerOfFive[n % 10];
                n /= 10;
            }

            return res;
        }

        static void Main(string[] args)
        {
            int sum = 0;

            for (int i = 10; i < 1000000; i++)
            {
                if (PowerOfFiveDigitSum(i) == i)
                {
                    sum += i;
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine(sum);
        }
    }
}
