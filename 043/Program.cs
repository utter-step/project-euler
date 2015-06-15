using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _043
{
    class Program
    {
        #region Простые делители
        private static readonly int[] _divisors =
        {
            2,
            3,
            5,
            7,
            11,
            13,
            17
        };
        #endregion

        private static bool CheckProperty(int[] perm)
        {
            for (int i = 1; i < 8; i++)
            {
                int toTest = ArrayToNum(perm, i, 3);
                if (toTest % _divisors[i - 1] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        static int ArrayToNum(int[] num, int start, int count)
        {
            int pow = 1;
            int res = 0;

            for (int i = start + count - 1; i >= start; i--)
            {
                res += num[i] * pow;
                pow *= 10;
            }

            return res;
        }

        static long ArrayToNum(int[] num)
        {
            long pow = 1;
            long res = 0;

            for (int i = num.Length - 1; i >= 0; i--)
            {
                res += num[i] * pow;
                pow *= 10;
            }

            return res;
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve);
        }

        static long Solve()
        {
            var pandigitals = Permutations.ProducePermutations(10);

            long result = 0;

            foreach (var pandigital in pandigitals)
            {
                if (CheckProperty(pandigital))
                {
                    result += ArrayToNum(pandigital);
                }
            }

            return result;
        }
    }
}
