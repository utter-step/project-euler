using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

using Tools;

namespace _074
{
    public class FactorialDigitsCycle : Cycle<int>
    {
        public FactorialDigitsCycle(int initialState)
            : base(initialState) { }

        protected override int MakeStep(int state)
        {
            int res = 0;

            while (state > 0)
            {
                res += _factorials[state % 10];
                state /= 10;
            }

            return res;
        }

        #region Factorials array (0 to 9)
        private static readonly int[] _factorials =
            {
                1,
                1,
                2,
                6,
                24,
                120,
                720,
                720 * 7,
                720 * 7 * 8,
                720 * 7 * 8 * 9
            }; 
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();

            int count = 0;

            stopwatch.Start();
            Parallel.For(1, 2000000, i =>
            {
                var t = new FactorialDigitsCycle(i);
                if (t.CycleDetails.CycleBody.Count() + t.CycleDetails.CycleHead.Count() == 60)
                {
                    Interlocked.Increment(ref count);
                }
            });
            stopwatch.Stop();

            Console.WriteLine("{0}. {1} ms", count, stopwatch.ElapsedMilliseconds);
        }
    }
}
