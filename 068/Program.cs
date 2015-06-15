using System;
using System.Linq;
using System.Text;

using Tools;

namespace _068
{
    class Program
    {
        public const int ORDER = 5;
        public const int MIN = 1;
        public const int MAX = 10;
        public const int MIN_OUTER = 4;

        private static readonly string[] _cache = Enumerable.Range(0, 11).Select(x => x.ToString()).ToArray();

        public static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 5000);
        }

        public static string Solve()
        {
            var ten = Enumerable.Range(1, MAX - 1).ToArray();
            var indicies = new[] { 1, 2, 3, 4, 5 };

            var maxStr = "";

            while (indicies != null)
            {
                var innerInit = indicies.Select(i => ten[i - 1]).ToArray();

                foreach (var inner in innerInit.GetPermutations())
                {
                    for (int sum = 14; sum <= 16; sum += 2)
                    {
                        var outer = GenerateMagicRing(inner, sum);

                        if (outer != null)
                        {
                            var ringStr = RingToString(inner, outer);

                            if (String.CompareOrdinal(ringStr, maxStr) > 0)
                            {
                                maxStr = ringStr;
                            }
                        }
                    }
                }

                indicies = MoveNext(indicies);
            }

            return maxStr;
        }

        public static int[] GenerateMagicRing(int[] inner, int sum)
        {
            int[] outer = new int[ORDER];

            for (int i = 0; i < ORDER; i++)
            {
                int candidate = sum - (inner[i] + inner[(i + 1) % ORDER]);
                if (candidate > MAX || candidate < 1)
                {
                    return null;
                }
                outer[i] = candidate;
            }

            var nums = new int[MAX];

            foreach (var num in inner)
            {
                nums[num - 1]++;
            }

            foreach (var num in outer)
            {
                nums[num - 1]++;
            }

            foreach (var num in nums)
            {
                if (num != 1)
                {
                    return null;
                }
            }

            return outer;
        }

        public static string RingToString(int[] inner, int[] outer)
        {
            int minOuterIndex = 0;
            int minOuter = outer[minOuterIndex];

            for (int i = 0; i < outer.Length; i++)
            {
                if (outer[i] < minOuter)
                {
                    minOuter = outer[i];
                    minOuterIndex = i;
                }
            }

            var sb = new StringBuilder(16, 16);

            for (int i = 0; i < outer.Length; i++)
            {
                int index = (minOuterIndex + i) % outer.Length;
                sb.Append(_cache[outer[index]]);
                sb.Append(_cache[inner[index]]);
                sb.Append(_cache[inner[(index + 1) % inner.Length]]);
            }

            return sb.ToString();
        }

        public static int[] MoveNext(int[] state)
        {
            int index = ORDER - 1;

            if (state[index] < MAX - 1)
            {
                state[index]++;
                return state;
            } else
            {
                while (index > 0 &&
                    state[index - 1] >= MAX - (ORDER - index) - 1)
                {
                    index--;
                }
                if (index > 0)
                {
                    state[index - 1]++;
                    state[index] = state[index - 1] + 1;
                    return state;
                }
            }

            return null;
        }
    }
}
