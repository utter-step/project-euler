using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _065
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 100);
        }

        private static int Solve(int index)
        {
            var previousNumerator = new LongInteger(3);
            var previousDenominator = LongInteger.One;

            var numerator = new LongInteger(8);
            var denominator = new LongInteger(3);

            for (int i = 4; i <= index; i++)
            {
                var tNum = numerator;
                var tDen = denominator;

                if (i % 3 == 0)
                {
                    int k = (i / 3) * 2;

                    numerator = numerator * k + previousNumerator;
                    denominator = denominator * k + previousDenominator;
                }

                else
                {
                    numerator += previousNumerator;
                    denominator += previousDenominator;
                }

                previousNumerator = tNum;
                previousDenominator = tDen;
            }

            return numerator.ToString().Select(x => x - '0').Sum();
        }
    }
}
