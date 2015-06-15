using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Tools;

namespace _094
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 1000000000);
        }

        private static long Solve(int upperLimit)
        {
            long res = 0;

            Parallel.ForEach(Enumerable.Range(5, upperLimit / 3 - 5).Where(i => i % 2 == 1).AsParallel(),
                i => {
                    if (IsIntegerArea(i, i + 1))
                    {
                        Interlocked.Add(ref res, i * 3 + 1);
                    } else if (IsIntegerArea(i, i - 1))
                    {
                        Interlocked.Add(ref res, i * 3 - 1);
                    }
                }
            );
//            for (int i = 5; i <= upperLimit / 3; i += 2)
//            {
//                if (IsIntegerArea(i, i + 1))
//                {
//                    res += i * 3 + 1;
//                } else if (IsIntegerArea(i, i - 1))
//                {
//                    res += i * 3 - 1;
//                }
//            }

            return res;
        }

        private static bool IsIntegerArea(long ab, long c)
        {
            return IsPerfectSquare(4 * ab * ab - c * c);
        }

        private static bool IsPerfectSquare(long n)
        {
            long sqrt = (long)Math.Sqrt(n);
            return sqrt * sqrt == n;
        }
    }
}
