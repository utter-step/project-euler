using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _070
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.TimeIt(Solve, 10000000);
        }

        private static int Solve(int upperLimit)
        {
            primes = NumUtils.EratospheneSeive((int) Math.Sqrt(upperLimit) + 1);

            int minN = 0;
            double minRatio = 2;

            for (int n = upperLimit / 10; n < upperLimit; n++)
            {
                int totients = TotientFunc(n);

                if (totients == n - 1)
                {
                    continue;
                }

                double ratio = (double) n / totients;

                if (ratio < minRatio)
                {
                    var pc = new NumUtils.PandigitalChecker(n);

                    if (pc.IsPandigital(totients))
                    {
                        minRatio = ratio;
                        minN = n;
                    }
                }
            }

            return minN;
        }

        private static IEnumerable<int> primes;

        private static int[] smallPrimes =
        {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29
        };

        private static List<int> GetPrimeFactors(int num)
        {
            var res = new List<int>();

            foreach (var smallPrime in smallPrimes)
            {
                if (num % smallPrime == 0)
                {
                    res.Add(num);
                    return res;
                }
            }

            foreach (var prime in primes)
            {
                if (num % prime == 0)
                {
                    res.Add(prime);        
                    do { num /= prime; 
                    } while (num % prime == 0);
                }
            }

            if (num > 1)
            {
                res.Add(num);
            }

            return res;
        }

        private static int TotientFunc(int num)
        {
            var primeFactors = GetPrimeFactors(num);

            long res = num;

            foreach (var primeFactor in primeFactors)
            {
                res *= primeFactor - 1;
                res /= primeFactor;
            }

            return (int)res;
        }
    }
}
