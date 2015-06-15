using System;
using System.Collections.Generic;
using System.Linq;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using Tools;

namespace _033
{
    static class Program
    {
        private static IEnumerable<int> Except(this IEnumerable<int> first, IEnumerable<int> other)
        {
            var firstList = first.ToList();

            foreach (int i in other)
            {
                if (firstList.Contains(i))
                {
                    firstList.Remove(i);
                }
            }

            return firstList;
        }

        private static IEnumerable<int> DigitsFromNumber(int num)
        {
            var digits = new List<int>();

            while (num > 0)
            {
                digits.Add(num % 10);
                num /= 10;
            }

            return digits;
        }

        private static int NumberFromDigits(IEnumerable<int> digits)
        {
            int power = 1;

            int result = 0;

            foreach (var digit in digits)
            {
                result += digit * power;
                power *= 10;
            }

            return result;
        }

        private static bool TryIncorrectCancel(int numerator, int denominator, out Fraction fraction)
        {
            var digitsOfNumerator   = DigitsFromNumber(numerator);
            var digitsOfDenominator = DigitsFromNumber(denominator);

            int newNumerator = NumberFromDigits(digitsOfNumerator.Except(digitsOfDenominator));
            int newDenominator = NumberFromDigits(digitsOfDenominator.Except(digitsOfNumerator));

            if (newDenominator == 0)
            {
                fraction = new Fraction(0, 1);
                return false;
            }

            fraction = new Fraction(newNumerator, newDenominator);

            return newNumerator != numerator && newDenominator != denominator;
        }

        private static IEnumerable<Fraction> ComputeGoodFrations()
        {
            var fractions = new List<Fraction>();

            for (int denominator = 11; denominator < 100; denominator++)
            {
                for (int numerator = 10; numerator < denominator; numerator++)
                {
                    if (numerator % 10 != 0 || denominator % 10 != 0)
                    {
                        Fraction fraction,
                                 correct;

                        var incorrect = TryIncorrectCancel(numerator, denominator, out fraction);
                        correct = new Fraction(numerator, denominator);

                        if (incorrect && fraction == correct)
                        {
                            fractions.Add(correct);
                        }
                    }
                }
            }

            return fractions;
        }

        private static int ComputeSolution()
        {
            var fracs = ComputeGoodFrations();

            var res = new Fraction(1, 1);

            res = fracs.Aggregate(res, (current, fraction) => current * fraction);

            return res.Denominator;
        }

        static void Main(string[] args)
        {
            Decorators.TimeItAccurate(ComputeSolution, 1);
        }
    }
}
