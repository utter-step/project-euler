using System;

using Tools;

namespace _086
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            const int LIMIT = 1000000;
            Decorators.Benchmark(Solve, LIMIT);
        }

        public static int Solve(int solutions)
        {
            int M = 100;
            int count = 2060;

            for (int m = M + 1; m < int.MaxValue; m++)
            {
                for (int a = 1; a <= m; a++)
                {
                    for (int b = a; b <= m; b++)
                    {
                        double path = Math.Sqrt((a + b) * (a + b) + m * m);
                        if (path == (int)path)
                        {
                            count++;

                            if (count == solutions)
                            {
                                return m;
                            }
                        }
                    }
                }
            }

            return -1;
        }
    }
}
