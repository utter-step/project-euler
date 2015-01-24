using System;
using System.Collections.Generic;
using System.Linq;

using Tools;

namespace _49
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve);
        }

        static string Solve()
        {
            var results = new List<string>(5);

            var primes = NumUtils.EratospheneSeive(10000, 1000);

            var permutations = Permutations.ProducePermutations(4);

            foreach (var prime in primes)
            {
                var primePerms = NumberPermutations(prime, permutations);

                foreach (var primePerm in primePerms)
                {
                    if (primePerm > prime && primes.Contains(primePerm))
                    {
                        int diff = primePerm - prime;

                        int possiblePrime = primePerm + diff;

                        if (primePerms.Contains(possiblePrime) && primes.Contains(possiblePrime))
                        {
                            results.Add(String.Join("", prime, primePerm, primePerm + diff));
                        }
                    }
                }
            }

            return String.Join("\n", results);
        }

        static HashSet<int> NumberPermutations(int number, int[][] permutations)
        {
            var res = new HashSet<int>();
            var numA = NumToArray(number);

            foreach (var permutation in permutations)
            {
                res.Add(ArrayToNum(ArrayReplace(numA, permutation)));
            }

            res.Remove(number);

            return res;
        }

        static int[] NumToArray(int num)
        {
            return num
                .ToString()
                .Select(x => x - '0')
                .ToArray();
        }

        static int ArrayToNum(int[] num)
        {
            int pow = 1;
            int res = 0;

            for (int i = num.Length - 1; i >= 0; i--)
            {
                res += num[i] * pow;
                pow *= 10;
            }

            return res;
        }

        static int[] ArrayReplace(int[] num, int[] indicies)
        {
            var res = new int[num.Length];

            for (int i = 0; i < num.Length; i++)
            {
                res[i] = num[indicies[i]];
            }

            return res;
        }
    }
}
