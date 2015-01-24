using System;
using System.Collections.Generic;
using Tools;

namespace _37
{
    class Program
    {
        private static HashSet<int> _primes;

        public static bool IsTruncableRtl(int prime)
        {
            prime /= 10;

            if (prime == 0)
            {
                return false;
            }

            while (prime > 0)
            {
                if (!_primes.Contains(prime))
                {
                    return false;
                }
                prime /= 10;
            }

            return true;
        }

        public static bool IsTruncableLtr(int prime)
        {
            int tenPow = 1;
            int length = -1;

            while (tenPow < prime)
            {
                tenPow *= 10;
                length++;
            }

            if (tenPow == 10)
            {
                return false;
            }

            tenPow /= 10;
            prime %= tenPow;

            for (int i = 0; i < length; i++)
            {
                if (!_primes.Contains(prime))
                {
                    return false;
                }

                tenPow /= 10;
                prime %= tenPow;
            }

            return true;
        }

        public static int SumBitruncablePrimes(int upperLimit)
        {
            _primes = NumUtils.EratospheneSeive(upperLimit);

            int sum = 0;

            foreach (int prime in _primes)
            {
                if (IsTruncableRtl(prime) && IsTruncableLtr(prime))
                {
                    sum += prime;
                }
            }

            return sum;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(SumBitruncablePrimes(1000000));
        }
    }
}
