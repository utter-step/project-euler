using System;
using System.Collections.Generic;

using Tools;

namespace _092
{
    class Program
    {
        private static readonly int[] _squares = {0, 1, 4, 9, 16, 25, 36, 49, 64, 81};

        private static Dictionary<int, int> _sums;

        private static int SquareDigitSum(int num)
        {
            int res = 0;
            int nNext = (int)((0x1999999AL * num) >> 32);

            while (num > 0)
            {
                res += _squares[num - nNext * 10];

                long invDivisor = 0x1999999A;
                num = nNext;
                nNext = (int)((invDivisor * nNext) >> 32);
            }

            return res;
        }

        private static int FinishCycle(int num)
        {
            int nextStep = num;

            while (nextStep != 1 && nextStep != 89)
            {
                nextStep = SquareDigitSum(nextStep);
            }

            return nextStep;
        }

        private static int Count89(int nOfDigits)
        {
            int largest = 9 * 9 * nOfDigits;

            _sums = new Dictionary<int, int>(largest);

            int upperLimit = 1;

            for (int i = 0; i < nOfDigits; i++)
            {
                upperLimit *= 10;
            }

            var countOfSums = new int[largest + 1];

            for (int i = 1; i < upperLimit; i++)
            {
                int t = SquareDigitSum(i);

                countOfSums[t]++;
            }

            int count = 0;

            for (int i = 1; i <= largest; i++)
            {
                if (FinishCycle(i) == 89)
                {
                    count += countOfSums[i];
                }
            }

            return count;
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(Count89, 7);
        }
    }
}
