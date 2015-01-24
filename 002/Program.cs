using System;
using System.Collections.Generic;

using Tools;

namespace _2
{
    class Program
    {
        private const int LIMIT = 4000000;

        private static IEnumerable<int> Fibs(int max)
        {
            for (int prevFib = 1, curFib = 1; prevFib < max;)
            {
                yield return prevFib;
                int newFib = prevFib + curFib;
                prevFib = curFib;
                curFib = newFib;
            }
        }

        private static IEnumerable<int> EvenFilter(IEnumerable<int> input)
        {
            foreach (int num in input)
            {
                if ((num & 1) == 0)
                {
                    yield return num;
                }
            }
        }
        
        static void Main(string[] args)
        {
            Decorators.Benchmark(SumOfEvenFibs, LIMIT);
        }

        private static long SumOfEvenFibs(int limit)
        {
            long res = 0;

            foreach (var item in EvenFilter(Fibs(limit)))
            {
                res += item;
            }
            return res;
        }
    }
}
