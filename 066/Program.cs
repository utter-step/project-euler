using System;
using System.Collections.Generic;
using System.Linq;

using Tools;

namespace _066
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 1000);
        }

        private static int Solve(int limitD)
        {
            LongInteger maxX = 0;
            int maxD = 0;

            for (int d = 2; d <= limitD; d++)
            {
                int sqrt = (int)Math.Sqrt(d);

                if (sqrt * sqrt == d)
                {
                    continue;
                }

                LongInteger x = GetDiophantineX(d);

                if (x > maxX)
                {
                    maxX = x;
                    maxD = d;
                }
            }

            return maxD;
        }

        private static LongInteger GetDiophantineX(int D)
        {
            var cycle = new RootCycle(D);
            var continuedFraction = new ContinuedFraction(cycle);

            int k = 1;
            int cycleLength = cycle.CycleDetails.CycleLength;

            foreach (var fraction in continuedFraction)
            {
                if ((k & 1) == 1 && k % cycleLength == cycleLength - 1)
                {
                    return fraction.Numerator;
                }
                k++;
            }

            return 0;
        }

        private struct Fraction
        {
            public LongInteger Numerator { get; private set; }
            public LongInteger Denominator { get; private set; }

            public Fraction(LongInteger numerator, LongInteger denominator)
                : this()
            {
                Numerator = numerator;
                Denominator = denominator;

                if (Numerator < 0 || Denominator < 0)
                {
                    Console.WriteLine(this);
                }
            }

            public override string ToString()
            {
                return String.Format("{0}/{1}", Numerator, Denominator);
            }
        }

        private struct ContinuedFraction : IEnumerable<Fraction>
        {
            private int[] _start;
            private int[] _cycle;

            private LongInteger _prevP;
            private LongInteger _prevQ;

            private LongInteger _P;
            private LongInteger _Q;

            public int N { get; private set; }

            public ContinuedFraction(RootCycle cycle)
                : this()
            {
                _start = cycle.CycleDetails.CycleHead.Select(x => x.IntegerPart).ToArray();
                _cycle = cycle.CycleDetails.CycleBody.Select(x => x.IntegerPart).ToArray();

                _prevP = 1;
                _prevQ = 0;

                _P = _start[0];
                _Q = 1;
            }

            public IEnumerator<Fraction> GetEnumerator()
            {
                int n = 1;

                while (n < _start.Length)
                {
                    LongInteger tempP = _P;
                    LongInteger tempQ = _Q;

                    _P = _start[n] * _P + _prevP;
                    _Q = _start[n] * _Q + _prevQ;

                    _prevP = tempP;
                    _prevQ = tempQ;

                    n++;
                    N++;

                    yield return new Fraction(_P, _Q);
                }

                n = 0;

                while (true)
                {
                    if (n == _cycle.Length)
                    {
                        n = 0;
                    }

                    LongInteger tempP = _P;
                    LongInteger tempQ = _Q;

                    _P = _cycle[n] * _P + _prevP;
                    _Q = _cycle[n] * _Q + _prevQ;

                    _prevP = tempP;
                    _prevQ = tempQ;

                    n++;
                    N++;

                    yield return new Fraction(_P, _Q);
                }
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        private class ChainParams
        {
            public readonly int Multiplier;
            public readonly int Subtracted;
            public readonly int IntegerPart;

            public ChainParams(int integerPart, int multiplier, int subtracted)
            {
                IntegerPart = integerPart;
                Multiplier = multiplier;
                Subtracted = subtracted;
            }

            public override int GetHashCode()
            {
                return Multiplier.GetHashCode() ^ Subtracted.GetHashCode() ^ IntegerPart.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                var other = obj as ChainParams;

                if (other == null)
                {
                    return false;
                }

                return other.IntegerPart == IntegerPart &&
                    other.Multiplier == Multiplier &&
                    other.Subtracted == Subtracted;
            }

            public override string ToString()
            {
                return IntegerPart.ToString();
            }
        }

        private class RootCycle : Cycle<ChainParams>
        {
            private double _sqrt;
            private int _n;

            public RootCycle(int n)
                : base(
                    new ChainParams(
                        (int)Math.Sqrt(n),
                        1,
                        (int)Math.Sqrt(n)),
                    false)
            {
                _n = n;
                _sqrt = Math.Sqrt(n);

                ComputeCycle();
            }

            protected override ChainParams MakeStep(ChainParams state)
            {
                int divisor = _n - state.Subtracted * state.Subtracted;

                int integerPart = (int)(state.Multiplier * (_sqrt + state.Subtracted) / divisor);
                int multiplier = divisor / state.Multiplier;
                int subtracted = -(state.Subtracted - multiplier * integerPart);

                return new ChainParams(integerPart, multiplier, subtracted);
            }
        }
    }
}
