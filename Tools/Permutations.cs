using System;
using System.Collections.Generic;

namespace Tools
{
    public class Permutations
    {
        public static int ArrayToNum(int[] num)
        {
            int pow = 1;
            int res = 0;

            for (int i = num.Length - 1; i >= 0; i--)
            {
                res += num[i] * pow;
                pow *= 10;
            }

            return res;
        }

        private const int INT_SIZE = 4;

        private static int[] GenerateInitialPermutation(int n)
        {
            var res = new int[n];

            for (int i = 0; i < n; i++)
            {
                res[i] = i;
            }

            return res;
        }

        private static int[] GenerateInitialDirection(int n)
        {
            var res = new int[n];

            res[0] = 0;
            for (int i = 1; i < n; i++)
            {
                res[i] = -1;
            }

            return res;
        }

        public static IEnumerable<int[]> ProducePermutations(int length)
        {
            var initialPermutation = GenerateInitialPermutation(length);

            return ProducePermutations(initialPermutation);
        }

        public static IEnumerable<int[]> ProducePermutations(int[] array)
        {
            int length = array.Length;
            int permutationsCount = Methods.Factorial(length);

            var permutation = new int[length];

            Buffer.BlockCopy(array, 0, permutation, 0, length * INT_SIZE);
            int[] directions = GenerateInitialDirection(length);

            for (int i = 1; i < permutationsCount; i++)
            {
                var old = new int[length];
                Buffer.BlockCopy(permutation, 0, old, 0, length * INT_SIZE); 
                yield return old;

                MakeStep(ref permutation, ref directions);
            }

            yield return permutation;
        }

        private static void MakeStep(ref int[] permutation, ref int[] directions)
        {
            int maxDirectedElem = -1;
            int maxDirectedIndex = -1;

            for (int i = 0; i < permutation.Length; i++)
            {
                if (maxDirectedElem < permutation[i] && directions[i] != 0)
                {
                    maxDirectedElem = permutation[i];
                    maxDirectedIndex = i;
                }
            }

            if (maxDirectedIndex < 0)
            {
                return;
            }

            if (directions[maxDirectedIndex] == -1)
            {
                directions.SwapLeft(maxDirectedIndex);
                permutation.SwapLeft(maxDirectedIndex);

                maxDirectedIndex--;
            }
            else if (directions[maxDirectedIndex] == 1)
            {
                directions.SwapRight(maxDirectedIndex);
                permutation.SwapRight(maxDirectedIndex);

                maxDirectedIndex++;
            }

            bool reachedEnd = maxDirectedIndex == 0 || maxDirectedIndex == permutation.Length - 1;
            bool nextIsLarger = !reachedEnd &&
                permutation[directions[maxDirectedIndex] > 0 ? maxDirectedIndex + 1 : maxDirectedIndex - 1] > maxDirectedElem;

            if (reachedEnd || nextIsLarger)
            {
                directions[maxDirectedIndex] = 0;
            }

            for (int i = 0; i < maxDirectedIndex; i++)
            {
                if (permutation[i] > maxDirectedElem)
                {
                    directions[i] = 1;
                }
            }

            for (int i = maxDirectedIndex + 1; i < permutation.Length; i++)
            {
                if (permutation[i] > maxDirectedElem)
                {
                    directions[i] = -1;
                }
            }
        }
    }
}
