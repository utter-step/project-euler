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

            if (solutions < count)
            {
                return -1;
            }

            for (int m = M + 1; m < int.MaxValue / 2; m++)
            {
                for (int w = 2; w <= m * 2; w++)
                {
                    double path = Math.Sqrt(w * w + m * m);
                    if (path == (int)path)
                    {
                        count += w / 2 - Math.Max(0, w - m - 1);

                        if (count >= solutions)
                        {
                            return m;
                        }
                    }
                }
            }

            return -1;
        }
    }
}
