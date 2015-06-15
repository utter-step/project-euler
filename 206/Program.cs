using System;

using Tools;


namespace _206
{
    class Program
    {
        public static void Main(string[] args)
        {
            Decorators.Benchmark(Solve);
        }

        public static int Solve()
        {
            long minSquare = 1020304050607080900;
            long maxSquare = 1929394959697989900;

            long min = (long)Math.Sqrt(minSquare);
            long max = (long)Math.Sqrt(maxSquare) + 1;

            while (min % 100 != 30 && min % 100 != 70)
            {
                min += 10;
            }

            for (long i = min; i <= max; )
            {
                if (CheckNumeric(i * i))
                {
                    return (int)i;
                }
                long mod = i % 100;
                if (mod == 30)
                {
                    i += 40;
                }
                else if (mod == 70)
                {
                    i += 60;
                }
            }

            return 0;
        }

        private static bool CheckNumeric(long num)
        {
            num /= 100;
            for (long i = 9; i >= 1; i--)
            {
                if (num % 10 != i)
                {
                    return false;
                }
                num /= 100;
            }
            return true;
        }
    }
}
