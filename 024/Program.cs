using System;
using System.Collections.Generic;

using Tools;

namespace _24
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

            public override int GetHashCode(int[] obj)
            {
                return base.GetHashCode();
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

        static void Main(string[] args)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();

            stopwatch.Restart();
            int[][] permutations = Permutations.ProducePermutations(10);
            stopwatch.Stop();

            Console.WriteLine("Generated permutations (array) in {0} ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            Array.Sort(permutations, PermutationsLexicographicalCompare);
            stopwatch.Stop();

            Console.WriteLine("Sorted them in {0} ms.", stopwatch.ElapsedMilliseconds);

            //foreach (var item in permutations[1000000 - 1])
            //{
            //    Console.Write(item);
            //}

            Console.WriteLine(permutations[0][0]);
        }
    }
}
