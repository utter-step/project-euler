using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _3
{
    class Program
    {
        private const long NUM_TO_SOLVE = 600851475143L;

        static void Main(string[] args)
        {
            Decorators.TimeIt(MaxPrimeFactor, NUM_TO_SOLVE);

            Decorators.TimeItAccurate(MaxPrimeFactor, NUM_TO_SOLVE, 10);
        }

        private static long MaxPrimeFactor(long num)
        {
            int factorLimit = (int)Math.Sqrt(num) + 1;

            var primes = NumUtils.EratospheneSeive(factorLimit, 3);

            int max = 0;

            foreach (var prime in primes)
            {
                if (num % prime == 0)
                {
                    do {
                        num /= prime;
                    } while (num % prime == 0);
                    max = prime;
                }

                if (num == 1) 
                {
                    break;
                }
            }

            return max > num ? max : num;
        }
    }
}
