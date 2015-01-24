using Tools;

namespace _55
{
    class Program
    {
        private const int LYCHREL_CONSTANT = 50;

        public static long ReverseNumber(long num)
        {
            long res = 0;

            while (num > 0)
            {
                res = res * 10 + num % 10;

                num /= 10;
            }

            return res;
        }

        public static bool IsLychrelNumber(int num)
        {
            long next = num + ReverseNumber(num);

            for (int i = 0; i < LYCHREL_CONSTANT; i++)
            {
                if (NumUtils.IsPalindromic(next.ToString()))
                {
                    return false;
                }

                next = next + ReverseNumber(next);
            }

            return true;
        }

        public static int CountLychrels(int limit)
        {
            int res = 0;

            for (int i = 0; i < limit; i++)
            {
                if (IsLychrelNumber(i))
                {
                    res++;
                }
            }
            return res;
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(CountLychrels, 10000);
        }
    }
}
