using System;
using Tools;

namespace _016
{
    class Program
    {
        public static int SumOfDigits(LongInteger lInt)
        {
            int res = 0;

            foreach (var c in lInt.ToString())
            {
                res += c - '0';
            }

            return res;
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(SumOfDigitsInPowerOfTwo, 1000);
        }

        private static int SumOfDigitsInPowerOfTwo(int power)
        {
            var two = new LongInteger(2);

            var powered = LongInteger.Pow(two, power);

            return SumOfDigits(powered);
        }
    }
}
