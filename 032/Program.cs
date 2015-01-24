using System;
using System.Collections.Generic;
using System.Linq;

using Tools;

namespace _032
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.TimeIt(Solve);
            Decorators.TimeItAccurate(Solve, 10);
        }

        private static int Solve()
        {
            var arr = new int[9];

            for (int i = 0; i < 9;)
            {
                arr[i] = ++i;
            }

            var permutations = Permutations.ProducePermutations(arr);

            var results = new HashSet<int>();

            foreach (var permutation in permutations)
            {
                for (int i = 1; i < 3; i++)
                {
                    int product = ToNumber(permutation, 5, 9);

                    if (ToNumber(permutation, 0, i) * ToNumber(permutation, i, 5) == product)
                    {
                        results.Add(product);
                    }
                }
            }

            return results.Sum();
        }

        private static int ToNumber(int[] digits, int start, int end)
        {
            int pow = 1;
            int res = 0;

            for (int i = end - 1; i >= start; i--)
            {
                res += digits[i] * pow;
                pow *= 10;
            }

            return res;
        }
    }
}
