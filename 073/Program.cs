using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace _73
{
    class Program
    {
        public struct Fraction
        {
            public readonly int Numerator;
            public readonly int Denominator;

            public Fraction(int num, int denom)
                : this()
            {
                Numerator = num;
                Denominator = denom;
            }

            public override string ToString()
            {
                return String.Format("{0}/{1}", Numerator, Denominator);
            }
        }

        public static Tuple<Fraction, Fraction> FindFractions(int maxDenom, int rNum, int rDen, Tuple<Fraction, Fraction> start=null)
        {
            int a, b, c, d;
            if (start == null)
            {
                a = 0;
                b = 1;
                c = 1;
                d = maxDenom;
            }
            else
            {
                a = start.Item1.Numerator;
                b = start.Item1.Denominator;
                c = start.Item2.Numerator;
                d = start.Item2.Denominator;
            }

            int prePrevNum = 0,
                prePrevDen = 0;

            while (c != rNum || d != rDen)
            {
                int k = (maxDenom + b) / d;

                int tC = c,
                    tD = d;

                c = k * c - a;
                d = k * d - b;

                prePrevNum = a;
                prePrevDen = b;

                a = tC;
                b = tD;
            }

            return new Tuple<Fraction,Fraction>(new Fraction(prePrevNum, prePrevDen), new Fraction(a, b));
        }

        public static int CountFractions(int maxDenom, Tuple<Fraction, Fraction> start, Fraction end)
        {
            int a, b, c, d;
            a = start.Item1.Numerator;
            b = start.Item1.Denominator;
            c = start.Item2.Numerator;
            d = start.Item2.Denominator;

            int count = -2;

            while (c != end.Numerator || d != end.Denominator)
            {
                int k = (maxDenom + b) / d;

                int tC = c,
                    tD = d;

                c = k * c - a;
                d = k * d - b;

                a = tC;
                b = tD;

                count++;
            }

            return count;
        }

        private static int Solve(int maxDenom)
        {
            int curMax = maxDenom;

            while (curMax % 10 == 0 && curMax >= 100)
            {
                curMax /= 10;
            }

            var curStart = FindFractions(curMax, 1, 3);

            while (curMax < maxDenom)
            {
                curMax *= 10;
                curStart = FindFractions(curMax, 1, 3, curStart);
            }

            return CountFractions(maxDenom, curStart, new Fraction(1, 2));
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 100000);
        }
    }
}
