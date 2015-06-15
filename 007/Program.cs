using System;
using System.Linq;

using Tools;

namespace _007
{
    class Program
    {
        private static int Solve(int position)
        {
            int lim = (int)(position
                * Math.Log(position)
                * Math.Log(Math.Log(position)));

            if (position < 6)
            {
                lim = 12;
            }

            return NumUtils.EratospheneSeive(lim).ElementAt(position + 1);
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 10001);
        }
    }
}
