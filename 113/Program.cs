using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using Tools;

namespace _113
{
    class Program
    {
        static void Main(string[] args)
        {
            //var googol = (BigInteger)NumUtils.BinaryPower(new BigInteger(10), 100);
            //Decorators.TimeIt(Solve, googol);

            FillArrays(6);
            Console.WriteLine(up.Sum() + down.Sum() - 10);
        }

        private static int Solve(BigInteger limit)
        {
            int notBouncy = 100;

            for (BigInteger i = 100; i < limit; i += 1)
            {
                if (!IsBouncy(i))
                {
                    notBouncy++;
                }
            }

            return notBouncy;
        }

        private static int[][] ups;
        private static int[][] stills;
        private static int[][] downs;

        private static void FillArrays(int steps) 
        {

        }

        private static bool IsBouncy(BigInteger n)
        {
            BigInteger ten = 10;

            int prevDigit = (int)(n % ten);
            n /= ten;
            int currentDigit = (int)(n % ten);

            while (prevDigit - currentDigit == 0 && n > 0)
            {
                prevDigit = (int)(n % ten);
                n /= ten;
                currentDigit = (int)(n % ten);
            }

            int sign = Math.Sign(prevDigit - currentDigit);
            int currentSign = sign;

            while (n > 9)
            {
                prevDigit = currentDigit;
                n /= ten;
                currentDigit = (int)(n % ten);

                currentSign = Math.Sign(prevDigit - currentDigit);

                if (currentSign != 0 && currentSign != sign)
                {
                    return true;
                }
            }
            return false;
        }

        private const byte TEN = (byte)10;

        private static int LastDigit(BigInteger n)
        {
            var a = n.ToByteArray();
            return a[a.Length - 1] % TEN;
        }
    }
}
