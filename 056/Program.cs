using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _056
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve);
        }

        private static int Solve()
        {
            int max = 0;

            for (int a = 2; a < 100; a++)
            {
                var aBase = new LongInteger(a);

                for (int b = 0; b < 100; b++)
                {
                    int sod = SumOfDigits(aBase);

                    if (sod > max)
                    {
                        max = sod;
                    }

                    aBase *= a;
                }
            }

            return max;
        }

        private static int SumOfDigits(LongInteger num)
        {
            var s = num.ToString();

            int res = 0;

            foreach (var c in s)
            {
                res += c - '0';
            }

            return res;
        }
    }
}
