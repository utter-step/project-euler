using System;
using System.Collections.Generic;

namespace Tools
{
    public static class Methods
    {
        public static void ShuffleArray<T>(this T[] arr)
        {
            var rnd = new Random();

            int n = arr.Length;
            int index;
            while (n > 1)
            {
                index = rnd.Next(n--);
                Swap(ref arr[n], ref arr[index]);
            }
        }

        public static void SwapRight<T>(this T[] array, int index)
        {
            Swap(ref array[index], ref array[index + 1]);
        }

        public static void SwapLeft<T>(this T[] array, int index)
        {
            Swap(ref array[index], ref array[index - 1]);
        }

        public static void Swap<T>(this T[] array, int first, int second)
        {
            Swap(ref array[first], ref array[second]);
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static IEnumerable<int[]> GetPermutations(this int[] array)
        {
            return Permutations.ProducePermutations(array);
        }

        public static int SumOfDigits(this int n)
        {
            return NumUtils.SumOfDigits(n);
        }

        public static int SumOfDigits(this long n)
        {
            return NumUtils.SumOfDigits(n);
        }

        public static int Factorial(int n)
        {
            int res = 1;

            while (n > 0)
            {
                res *= n;
                n--;
            }

            return res;
        }
    }

}
