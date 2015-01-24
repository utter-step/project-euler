using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _034
{
    class Program
    {
        #region Factorials array (0 to 9)
        private static readonly int[] _factorials =
            {
                1,
                1,
                2,
                6,
                24,
                120,
                720,
                720 * 7,
                720 * 7 * 8,
                720 * 7 * 8 * 9
            };
        #endregion

        private static int FactoriadicDigitSum(int n)
        {
            int res = 0;

            while (n > 0)
            {
                res += _factorials[n % 10];
                n /= 10;
            }

            return res;
        }

        static void Main(string[] args)
        {
            int res = 0;

            for (int i = 10; i < 10000000; i++)
            {
                if (FactoriadicDigitSum(i) == i)
                {
                    res += i;
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine(res);
        }
    }
}
