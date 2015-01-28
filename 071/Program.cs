using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace _071
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

        public static LinkedList<Fraction> GenerateFractions(int maxDenom)
        {
            var result = new LinkedList<Fraction>();

            result.AddFirst(new Fraction(0, 1));
            result.AddLast(new Fraction(1, 1));

            bool sequenceChanged;

            do
            {
                sequenceChanged = false;

                var current = result.First;
                while (true)
                {
                    var next = current.Next;

                    if (next == null)
                    {
                        break;
                    }

                    int newNumerator = current.Value.Numerator + next.Value.Numerator;
                    int newDenominator = current.Value.Denominator + next.Value.Denominator;
                    if (newDenominator <= maxDenom)
                    {
                        sequenceChanged = true;
                        result.AddAfter(current, new Fraction(newNumerator, newDenominator));
                    }
                    current = next;
                }
            } while (sequenceChanged);

            return result;
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

        public static Fraction Solve(int maxDenom)
        {
            int curTen = 10;

            var curGuess = FindFractions(curTen, 3, 7);

            while (curTen < maxDenom)
            {
                curTen *= 10;
                curGuess = FindFractions(curTen, 3, 7, curGuess);
            }

            return curGuess.Item2;
        }


        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 1000000);
        }
    }
}
