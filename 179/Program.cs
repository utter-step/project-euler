using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace _179
{
    class Program
    {
        private static int CountDivisors(int num)
        {
            var factorization = NumUtils.ComputePrimeFactorization_Cached(num);

            int res = 1;

            foreach (var factor in factorization)
            {
                res *= factor.Value + 1;
            }

            return res;
        }

        private static IEnumerable<int> ComputeDivisorsFunc(int upperLimit)
        {
            var countOfDivisors = new int[upperLimit + 1];

            #region New
            //var primes = NumUtils.EratospheneSeive(upperLimit / 2);

            //countOfDivisors[1] = 1;

            //for (int i = 2; i <= upperLimit; i++)
            //{
            //    if (countOfDivisors[i] == 0)
            //    {
            //        int count = CountDivisors(i);

            //        countOfDivisors[i] = count;

            //        foreach (var prime in primes)
            //        {
            //            int curCount = count;

            //            int curnum = i * prime;
            //            int filler;

            //            if (curnum > upperLimit || curnum < 0)
            //            {
            //                break;
            //            }

            //            if (i % prime == 0)
            //            {
            //                curCount /= 2;

            //                filler = curCount * 3;
            //            }
            //            else
            //            {
            //                filler = curCount * 2;
            //            }

            //            while (curnum <= upperLimit && curnum > 0)
            //            {
            //                countOfDivisors[curnum] = filler;

            //                filler += curCount;
            //                curnum *= prime;
            //            } 
            //        }
            //    }
            //} 
            #endregion

            #region Old
            int twoPowCount = 2;

            for (int i = 2; i <= upperLimit; i <<= 1)
            {
                countOfDivisors[i] = twoPowCount++;
            }

            for (int i = 3; i <= upperLimit; i += 2)
            {
                int count = CountDivisors(i);
                countOfDivisors[i] = count;

                int filler = count * 2;

                for (int j = 2 * i; j <= upperLimit; j <<= 1)
                {
                    countOfDivisors[j] = filler;

                    filler += count;
                }
            } 
            #endregion

            return countOfDivisors.Skip(1);
        }

        private static IEnumerable<int> ComputeDivisorsCount(int upperLimit)
        {
            var countOfDivisors = new int[upperLimit + 1];

            for (int i = 2; i <= upperLimit / 2; i++)
            {
                for (int j = i; j <= upperLimit; j += i)
                {
                    countOfDivisors[j]++;
                }
            }

            return countOfDivisors.Skip(1);
        }

        private static int Solve(int upperLimit)
        {
            var divCounts = ComputeDivisorsCount(upperLimit).ToArray();

            int res = 0;

            for (int i = 0; i < upperLimit - 1; )
            {
                if (divCounts[i++] == divCounts[i])
                {
                    res++;
                }
            }

            return res;
        }

        private static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 1000000);
        }
    }
}
