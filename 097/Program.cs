using System;

using Tools;

namespace _97
{
    class Program
    {
        private const int POWER = 7830457;
        private const uint MULT = 28433;

        private static int BinaryLength(ulong num)
        {
            int res = 0;

            while (num > 0)
            {
                res++;

                num >>= 1;
            }

            return res;
        }

        public static ulong ModularPowerOfTwo(int exp, ulong modulus)
        {
            int step = 64 - BinaryLength(modulus);

            ulong res = 1;

            while (exp > step)
            {
                res <<= step;
                res %= modulus;

                exp -= step;
            }

            res <<= exp;
            res %= modulus;

            return res;
        }

        public static ulong ComputeLastDigits(int nOfDigits)
        {
            ulong tenPow = 1;

            for (int i = 0; i < nOfDigits; i++)
            {
                tenPow *= 10;
            }

            var lastTenOfPower = ModularPowerOfTwo(POWER, tenPow);

            return ((lastTenOfPower * MULT) + 1) % tenPow;
        }

        static void Main(string[] args)
        {
            Decorators.TimeItAccurate(ComputeLastDigits, 10, 10);
        }
    }
}
