using System;
using System.Collections.Generic;
using System.Linq;

using Tools;

namespace _024
{
    class Program
    {
        #region Подсчёт уникальных
        class ArrayComparer : EqualityComparer<int[]>
        {
            public override bool Equals(int[] a, int[] b)
            {
                if (a.Length != b.Length)
                {
                    return false;
                }

                for (int i = 0; i < a.Length; i++)
                {
                    if (!a[i].Equals(b[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private static int CountUnique(int[][] array)
        {
            var arrayComparer = new ArrayComparer();

            var testSet = new HashSet<int[]>(array, arrayComparer);

            return testSet.Count;
        }
        #endregion

        private static int PermutationsLexicographicalCompare(int[] permutationA, int[] permutationB)
        {
            for (int i = 0; i < permutationA.Length; i++)
            {
                if (permutationA[i] < permutationB[i])
                {
                    return -1;
                }
                if (permutationA[i] > permutationB[i])
                {
                    return 1;
                }
            }

            return 0;
        }

        private static long Solve(int num)
        {
            var permutations = Permutations.ProducePermutations(10).ToArray();
            //Console.WriteLine(CountUnique(permutations));
            Array.Sort(permutations, PermutationsLexicographicalCompare);

            var resultingPermutation = permutations[num - 1];
            long tenPow = (long)Math.Pow(10, resultingPermutation.Length - 1);

            long res = 0;

            foreach (var digit in resultingPermutation)
            {
                res += digit * tenPow;
                tenPow /= 10;
            }

            return res;
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 1000000);
        }
    }
}
