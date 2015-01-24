using System;
using System.Runtime.CompilerServices;

using Tools;

namespace _4
{
    class Program
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsPalindrome(int num)
        {
            var s = num.ToString();
            int length = s.Length;

            for (int i = 0; i < length / 2; i++)
            {
                if (s[i] != s[length - i - 1])
                {
                    return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(FindLargestPalindromic);
        }

        private static int FindLargestPalindromic()
        {
            int max = 0;
            for (int i = 100; i < 1000; i++)
            {
                for (int j = 999; j > i; j--)
                {
                    if (i * j < max)
                    {
                        break;
                    }

                    if (IsPalindrome(i * j))
                    {
                        max = max < i * j ? i * j : max;
                    }
                }
            }
            return max;
        }
    }
}
