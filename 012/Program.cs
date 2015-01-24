using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;
using System.Diagnostics;
using System.Threading;

namespace _012
{
    class Program
    {
        #region My solution
        private static Dictionary<int, int> divisors;

        private static int GetDivisorsCount(int num)
        {
            int res;

            if (divisors.ContainsKey(num))
            {
                return divisors[num];
            }

            res = 1;

            var factorization = NumUtils.ComputePrimeFactorization_Cached(num);

            foreach (var item in factorization)
            {
                res *= item.Value + 1;
            }

            divisors.Add(num, res);

            return res;
        }

        static void Main(string[] args)
        {
            Decorators.TimeIt(Solve, 500);

            Decorators.TimeItAccurate(Solve, 500, 100);
        }

        private static int Solve(int divisorsMinimum)
        {
            Func<int, int> nthTriangle = x => (x * (x + 1)) / 2;

            divisors = new Dictionary<int, int>();

            int evenDivs = 2,
                oddDivs = 1;

            divisors.Add(1, 1);
            divisors.Add(2, 1);

            NumUtils.PrecomputePrimes(11000);

            for (int i = 3; i < int.MaxValue; )
            {
                int divisorsCount;
                if ((i & 1) == 0)
                {
                    oddDivs = GetDivisorsCount(++i);
                    divisorsCount = evenDivs * oddDivs;
                }
                else
                {
                    evenDivs = GetDivisorsCount(++i >> 1);
                    divisorsCount = oddDivs * evenDivs;
                }

                if (divisorsCount > divisorsMinimum)
                {
                    return nthTriangle(--i);
                }
            }

            return 0;
        }
        #endregion

        //#region Other solutions
        //static void Main(string[] args)
        //{
        //    Stopwatch s = new Stopwatch();
        //    s.Start();
        //    var t = Run(1000);
        //    s.Stop();
        //    Console.WriteLine("{0}, {1} ms", t, s.ElapsedMilliseconds);
        //}

        //private static Dictionary<float, int> factors;

        //public static long Run(int minFactors)
        //{
        //    factors = new Dictionary<float, int>();
        //    long pyramid = 0;
        //    int n = 0;
        //    int numFactors = 0;

        //    while (numFactors <= minFactors)
        //    {
        //        n++;
        //        pyramid = n * ((n + 1) / 2);

        //        if ((n + 1) % 2 == 0)
        //        {
        //            numFactors = count(n) * count((n + 1) / 2);
        //            factors[pyramid] = numFactors;
        //        }
        //        else
        //        {
        //            numFactors = count(n) + 1;
        //            factors[pyramid] = numFactors;
        //        }
        //    }
        //    return pyramid;
        //}

        //public static int count(int n)
        //{
        //    int retval = 0;
        //    int i = 1;
        //    int lastFactor = 0;
        //    if (factors.ContainsKey(n))
        //        return factors[n];
        //    for (; i <= Math.Sqrt(n); i++)
        //    {
        //        if (n % i == 0)
        //        {
        //            retval += 2;
        //            lastFactor = i;
        //        }
        //    }
        //    if (lastFactor * lastFactor == n)
        //        retval--;
        //    factors[n] = retval;
        //    return retval;
        //} 
        //#endregion
    }
}
