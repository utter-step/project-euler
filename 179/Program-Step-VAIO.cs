using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var primes = NumUtils.EratospheneSeive(upperLimit / 2);

            countOfDivisors[1] = 1;

            for (int i = 2; i <= upperLimit; i++)
            {
                if (countOfDivisors[i] == 0)
                {
                    int count = CountDivisors(i);

                    countOfDivisors[i] = count;

                    foreach (var prime in primes)
                    {
                        int curnum = i * prime;
                        int filler;

                        if (curnum > upperLimit || curnum < 0)
                        {
                            break;
                        }

                        if (i % prime == 0)
                        {
                            filler = count;
                        }
                        else
                        {
                            filler = count * 2;
                        }

                        while (curnum <= upperLimit && curnum > 0)
                        {
                            countOfDivisors[curnum] = filler;

                            filler += count;
                            curnum *= prime;
                        }
                    }
                }
            }

            #region Old
            //int twoPowCount = 2;

            //for (int i = 2; i <= upperLimit; i <<= 1)
            //{
            //    countOfDivisors[i] = twoPowCount++;
            //}

            //for (int i = 3; i <= upperLimit; i += 2)
            //{
            //    int count = CountDivisors(i);
            //    countOfDivisors[i] = count;

            //    int filler = count * 2;

            //    for (int j = 2 * i; j <= upperLimit; j <<= 1)
            //    {
            //        countOfDivisors[j] = filler;

            //        filler += count;
            //    }
            //} 
            #endregion

            return countOfDivisors.Skip(1);
        }

        private static HashSet<int> ES(int upper)
        {
            return NumUtils.EratospheneSeive(upper);
        } 

        private static void Main(string[] args)
        {
            var divs = ComputeDivisorsFunc(100).ToList();

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("{0}:\t{1}\t{2}", i + 1, divs[i], CountDivisors(i + 1));
            }
        }
    }
}
