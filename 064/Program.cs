using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _064
{
    class Program
    {
        private const int LIMIT = 10000;

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, LIMIT);
        }

        private static int Solve(int limit)
        {
            int res = 0;

            for (int i = 2; i <= limit; i++)
            {
                int _sqrt = (int)Math.Sqrt(i);

                if (_sqrt * _sqrt == i)
                {
                    continue;
                }

                var rootChain = new RootCycle(i);

                res += rootChain.CycleDetails.CycleLength & 1;
            }

            return res;
        }

        private class ChainParams
        {
            public int Multiplier;
            public int Subtracted;
            public int IntegerPart;

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
