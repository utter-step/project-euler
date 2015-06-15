using Tools;

namespace _004
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(FindLargestPalindromic, 1000);
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

                    if (NumUtils.IsPalindromic((i * j).ToString()))
                    {
                        max = max < i * j ? i * j : max;
                    }
                }
            }
            return max;
        }
    }
}
