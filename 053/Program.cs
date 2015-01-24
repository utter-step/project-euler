using System;
using Tools;

namespace _053
{
    class Program
    {
        private static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 100000000);
        }

        private const int nLim = 1000000;

        private static int Solve(int upperLimit)
        {
            int res = 0;

            logSums = new double[nLim + 1];

            for (int i = 1; i <= nLim; i++)
            {
                logSums[i] = Math.Log(i) + logSums[i - 1];
            }

            double limitLog = Math.Log(upperLimit);

            for (int n = 1; n <= nLim; n++)
            {
                for (int r = 1; r <= n; r++)
                {
                    if (BinomialLog(n, r) > limitLog)
                    {
                        res += n + 1 - 2 * r;
                        break;
                    }
                }
            }

            return res;
        }

        private static double[] logSums;

        private static double BinomialLog(int n, int k)
        {
            return logSums[n] - (logSums[k] + logSums[n - k]);
        }
    }
}
