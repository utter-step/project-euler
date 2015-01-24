using System;
using System.Collections.Generic;
using System.Linq;

using Tools;

namespace _061
{
    class Program
    {
        public const int MIN = 1000;
        public const int MAX = 10000;
        public const int ORDER = 8;

        public static void Main(string[] args)
        {
            Decorators.Benchmark(Solve);
        }

        private static int Solve()
        {
            var cycles = new List<NgonalCycle>();
            var orders = new List<int>[ORDER + 1];
            var cycleStarts = new HashSet<int>();

            for (int order = 3; order <= ORDER; order++)
            {
                orders[order] = GenerateNgonalNumbers(order, MIN, MAX);

                foreach (var item in orders[ORDER])
                {
                    if (cycleStarts.Add(item))
                    {
                        cycles.Add(new NgonalCycle(item, order));
                    }
                }
            }

            for (int i = 3; i < ORDER; i++)
            {
                var newCycles = new List<NgonalCycle>();

                foreach (var cycle in cycles)
                {
                    for (int order = 3; order < ORDER; order++)
                    {
                        foreach (var num in orders[order])
                        {
                            if (cycle.IsCompatible(num, order))
                            {
                                newCycles.Add(new NgonalCycle(cycle, num, order));
                            }
                        }
                    }
                }

                cycles = newCycles;
            }

            var closedCycles = cycles
                .Where(cycle => cycle.IsNext(cycle.CycleBody[0]))
                .ToList();

            if (closedCycles.Count == 1)
            {
                return closedCycles[0].CycleBody.Sum();
            }
            return 0;
        }

        public struct NgonalCycle
        {
            public bool[] Slots { get; private set; }
            public int[] CycleBody { get; private set; }

            private int _lastTwo;

            public NgonalCycle(int start, int slot)
                : this()
            {
                Slots = new bool[ORDER + 1];
                Slots[slot] = true;

                CycleBody = new[] { start };

                _lastTwo = start % 100;
            }

            public NgonalCycle(NgonalCycle cycle, int next, int slot)
                : this()
            {
                CycleBody = new int[cycle.CycleBody.Length + 1];
                cycle.CycleBody.CopyTo(CycleBody, 0);
                CycleBody[CycleBody.Length - 1] = next;

                Slots = (bool[])cycle.Slots.Clone();
                Slots[slot] = true;

                _lastTwo = next % 100;
            }

            public bool IsCompatible(int num, int slot)
            {
                return !Slots[slot] && IsNext(num);
            }

            public bool IsNext(int num)
            {
                return _lastTwo == num / 100;
            }
        }

        private static List<int> GenerateNgonalNumbers(int n, int min, int max)
        {
            var res = new List<int>();

            for (int i = 1; i < int.MaxValue; i++)
            {
                int figurate = GenerateFigurateNumber(n, i);

                if (figurate >= max)
                {
                    break;
                }
                if (figurate >= min)
                {
                    res.Add(figurate);
                }
            }

            return res;
        }

        private static int GenerateFigurateNumber(int order, int n)
        {
            return ((order - 2) * n * (n - 1)) / 2 + n;
        }
    }
}
