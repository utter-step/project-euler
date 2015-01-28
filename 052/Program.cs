using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _052
{
    class Program
    {
        private static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 6);
        }

        private static int Solve(int repeatsCount)
        {
            int start = 1;

            for (int i = 0; i < repeatsCount - 1; i++)
            {
                start *= 10;
            }

            for (int end = start + (start / 3) * 2; end < int.MaxValue; end = start + (start / 3) * 2)
            {
                for (int i = start; i <= end; i++)
                {
                    var pc = new NumUtils.PandigitalChecker(i);

                    bool found = true;

                    for (int n = 2; n <= repeatsCount && found; n++)
                    {
                        found = pc.IsPandigital(i * n);
                    }

                    if (found)
                    {
                        return i;
                    }
                }

                start *= 10;
            }

            throw new ArgumentException();
        }
    }
}
