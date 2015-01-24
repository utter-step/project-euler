using System;
using System.Linq;

using Tools;

namespace _7
{
    class Program
    {
        private static int Solve(int position)
        {
            int multiplier = 10 * (int)Math.Log(Math.Log(position));

            return NumUtils.EratospheneSeive(position * multiplier).ToArray()[position - 1];
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 10001);
        }
    }
}
