using System;
using System.Collections.Generic;
using System.Linq;

namespace _026
{
    public class Fraction
    {
        private int[] _decimalDigits;
        private int _cycleStart;

        private static int GCD(int a, int b)
        {
            if (a % b == 0)
            {
                return b;
            }

            bool aIsEven = (a & 1) == 0;
            bool bIsEven = (b & 1) == 0;

            if (aIsEven && bIsEven)
            {
                return 2 * GCD(a >> 1, b >> 1);
            } else if (aIsEven) {
                return GCD(a >> 1, b);
            } else if (bIsEven) {
                return GCD(a, b >> 1);
            } else if (a > b) {
                return GCD((a - b) >> 1, b);
            } else {
                return GCD((b - a) >> 1, a);
            }
        }

        public int IntegerPart { get; private set; }
        public int Numerator { get; private set; }
        public int Denominator { get; private set; }
        public int PeriodLength
        {
            get
            {
                return _decimalDigits.Length - _cycleStart;
            }
        }

        public Fraction(int numerator, int denominator)
        {
            IntegerPart = numerator / denominator;
            numerator -= denominator * IntegerPart;

            int gcd = numerator < 2 ? 1 : GCD(numerator, denominator);

            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
            ComputeDecimalForm();
        }

        private void ComputeDecimalForm()
        {
            HashSet<int> numerators = new HashSet<int>();
            List<int> decimalDigits = new List<int>();
            int numerator = Numerator;
            int denominator = Denominator;

            while (numerator % denominator != 0)
            {
                if (numerator < denominator)
                {
                    numerator *= 10;
                    while (numerator < denominator)
                    {
                        numerators.Add(numerator);
                        decimalDigits.Add(0);
                        numerator *= 10;
                    }
                }

                if (!numerators.Add(numerator))
                {
                    int cycleStart = 0;

                    foreach (var item in numerators)
                    {
                        if (numerator == item)
                        {
                            break;
                        }
                        cycleStart++;
                    }

                    _cycleStart = cycleStart;
                    _decimalDigits = decimalDigits.ToArray();
                    return;
                }

                decimalDigits.Add(numerator / denominator);
                numerator = numerator % denominator;
            }
            if (numerator > 0) { decimalDigits.Add(numerator / denominator); }

            _decimalDigits = decimalDigits.ToArray();
            _cycleStart = _decimalDigits.Length;
        }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder(_decimalDigits.Length + 4);
            sb.Append(IntegerPart);

            if (_decimalDigits.Length > 0)
            {
                sb.Append('.');
                for (int i = 0; i < _cycleStart; i++)
                {
                    sb.Append(_decimalDigits[i]);
                }

                if (_cycleStart < _decimalDigits.Length)
                {
                    sb.Append('(');
                    for (int i = _cycleStart; i < _decimalDigits.Length; i++)
                    {
                        sb.Append(_decimalDigits[i]);
                    }
                    sb.Append(')');
                }
            }

            return String.Format("{0}/{1} == {2}", Numerator + IntegerPart * Denominator, Denominator, sb);
        }
    }

    class Program
    {
        private const int LIMIT = 1000;

        static void Main(string[] args)
        {
            //var s = new System.Diagnostics.Stopwatch();

            //s.Start();
            //Fraction[] fractions = new Fraction[LIMIT - 1];

            //for (int i = 0; i < LIMIT - 1; )
            //{
            //    fractions[i] = new Fraction(1, ++i);
            //}

            //Array.Sort(fractions, (a, b) => b.PeriodLength - a.PeriodLength);
            //s.Stop();

            //Console.WriteLine(fractions[0]);
            //Console.WriteLine("\n{0} ms", s.ElapsedMilliseconds);
            do
            {
                Console.Write("Enter numerator: ");
                int num = int.Parse(Console.ReadLine());

                Console.Write("Enter denominator: ");
                int denom = int.Parse(Console.ReadLine());

                Console.WriteLine();
                var frac = new Fraction(num, denom);
                Console.WriteLine("{0}\nLength of period: {1}", frac, frac.PeriodLength);
                Console.WriteLine("ESC to exit...");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
