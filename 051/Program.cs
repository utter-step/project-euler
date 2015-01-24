using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _051
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve);
        }

        private static int Solve()
        {
            var primes = NumUtils.EratospheneSeive(1000000, 10000);

            foreach (var prime in primes)
            {
                if (MaxSimilarDigits(prime / 10) == 3)
                {
                    int digit = MostFrequentDigit(prime / 10);
                    if (digit <= 2 &&
                        GenerateChain(prime, digit)
                        .Where(x => primes.Contains(x)).Count() == 8)
                    {
                        return prime;
                    }
                }
            }

            return 0;
        }

        private static IEnumerable<int> GenerateChain(int prime, int idigit)
        {
            var digit = (char)('0' + idigit);
            var primeStr = prime.ToString();

            var chain = new int[10];

            for (char c = digit; c <= '9'; c++)
            {
                chain[c - '0'] = int.Parse(primeStr.Replace(digit, c));
            }

            return chain;
        }

        private static int MaxSimilarDigits(int n)
        {
            return GetDigits(n).Max();
        }

        private static int MostFrequentDigit(int n)
        {
            var digits = GetDigits(n);

            int max = digits[0];
            int maxIndex = 0;
            for (int i = 1; i < 10; i++)
            {
                if (digits[i] > max)
                {
                    max = digits[i];
                    maxIndex = i;
                }
            }

            return maxIndex;
        }

        private static int[] GetDigits(int n)
        {
            var digits = new int[10];

            while (n > 0)
            {
                digits[n % 10]++;
                n /= 10;
            }

            return digits;
        }
    }
}
