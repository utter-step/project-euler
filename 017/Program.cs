using System;

namespace _017
{
    class Program
    {
        #region Length constants
        private static readonly int[] first20 = new int[] {
            "".Length,
            "one".Length,
            "two".Length,
            "three".Length,
            "four".Length,
            "five".Length,
            "six".Length,
            "seven".Length,
            "eight".Length,
            "nine".Length,
            "ten".Length,
            "eleven".Length,
            "twelve".Length,
            "thirteen".Length,
            "fourteen".Length,
            "fifteen".Length,
            "sixteen".Length,
            "seventeen".Length,
            "eighteen".Length,
            "nineteen".Length
        };

        private static readonly int[] dozens = new int[] {
            "".Length,
            "".Length,
            "twenty".Length,
            "thirty".Length,
            "forty".Length,
            "fifty".Length,
            "sixty".Length,
            "seventy".Length,
            "eighty".Length,
            "ninety".Length
        };

        private const int HUNDRED = 7;
        private const int AND = 3;
        private const int ONE_THOUSAND = 11; 
        #endregion

        static void Main(string[] args)
        {
            Tools.Decorators.TimeIt(NumberLetterCountBefore1000);

            Tools.Decorators.TimeItAccurate(NumberLetterCountBefore1000, 1000);
        }

        private static int NumberLetterCountBefore1000()
        {
            int res = 0;

            for (int i = 0; i < 1000; i++)
            {
                res += GetLetterCount(i);
            }

            res += ONE_THOUSAND;

            return res;
        }

        private static int GetLetterCount(int num)
        {
            int res = 0;

            if (num >= 100)
            {
                res += first20[num / 100] + HUNDRED;
                num %= 100;

                if (num > 0) { res += AND; }
                else { return res; }
            }

            if (num >= 20) { res += dozens[num / 10] + first20[num % 10]; }
            else { res += first20[num]; }

            return res;
        }
    }
}
