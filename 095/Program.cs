using System.Linq;

using Tools;

namespace _095
{
    class Program
    {
        const int LIMIT = 1000000;

        private static int[] _divisorSums;

        class AmicableCycle : Cycle<int>
        {
            public AmicableCycle(int initialState) : base(initialState)
            {

            }

            protected override int MakeStep(int state)
            {
                int sod = _divisorSums[state];

                if (sod < LIMIT && sod >= _initialState)
                {
                    return sod;
                }
                return 1;
            }
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, LIMIT);
        }

        private static int Solve(int limit)
        {
            InitializeDivSums(limit);

            int maxLen = 0;
            AmicableCycle maxCycle = null;

            for (int i = 100; i < limit; i++)
            {
                var c = new AmicableCycle(i);
                int len = c.CycleDetails.CycleLength;

                if (len > maxLen)
                {
                    maxLen = len;
                    maxCycle = c;
                }
            }

            return maxCycle.CycleDetails.CycleBody.Min();
        }

        private static void InitializeDivSums(int limit)
        {
            _divisorSums = new int[limit];

            for (int i = 0; i < limit; i++)
            {
                _divisorSums[i] = 1;
            }

            for (int i = 2; i <= limit / 2; i++)
            {
                for (int j = i * 2; j < limit; j += i)
                {
                    _divisorSums[j] += i;
                }
            }
        }
    }
}
