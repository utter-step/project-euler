using System.Collections.Generic;

using Tools;

namespace _029
{
    class Program
    {
        static void Main()
        {
            Decorators.TimeItAccurate(Solve, 120, 100);
        }

        private static int Solve(int lim)
        {
            return CountDistinctPower(lim, lim);
        }

        private static int CountDistinctPower(int a, int b)
        {
            var distinctPowers = new HashSet<NumberAsFactorization>();

            for (int intA = 2; intA <= a; intA++)
            {
                var num = new NumberAsFactorization(intA);
                for (int intB = 2; intB <= b; intB++)
                {
                    num = num.PowerUp();
                    distinctPowers.Add(num);
                }
            }

            return distinctPowers.Count;
        }
    }
}
