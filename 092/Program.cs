using System;
using Tools;

namespace _092
{
    class Program
    {
        private static readonly int[] _squares = {0, 1, 4, 9, 16, 25, 36, 49, 64, 81};

        private static int SquareDigitSum(int num)
        {
            int res = 0;

            while (num > 0)
            {
                res += _squares[num % 10];
                num /= 10;
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
            int upperLimit = 1;

            for (int i = 0; i < nOfDigits; i++)
            {
                upperLimit *= 10;
            }

            int largest = 9 * 9 * nOfDigits;

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
            Decorators.TimeIt(Count89, 7);
        }
    }
}
