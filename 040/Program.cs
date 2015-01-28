using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace _040
{
    class Program
    {
        private static string ChampernownesConstant(int minLength)
        {
            var sb = new StringBuilder(minLength);

            for (int i = 1; sb.Length < minLength; i++)
            {
                sb.Append(i);
            }

            return sb.ToString();
        }

        private static int Solve(int upperLimit)
        {
            string fractionDigits = ChampernownesConstant(upperLimit);

            int res = 1;

            for (int i = 1; i <= upperLimit; i *= 10)
            {
                res *= fractionDigits[i - 1] - '0';
            }
            return res;
        }

        private static int SoD_1(int n)
        {
            int res = 0;

            while (n > 9)
            {
                res += n % 10;
                n /= 10;
            }

            return res + n;
        }

        private static int SoD_2(int n)
        {
            if (((n >> 31) & 1) == 1)
            {
                n *= -1;
            }

            int res = 0;
            int tenPow = 1;

            while (tenPow < n)
            {
                res += (n % (tenPow * 10)) / tenPow;
                tenPow *= 10;
            }

            if (tenPow == n)
            {
                res++;
            }

            return res;
        }

        static int Count2(int x)
        {
            if (((x >> 31) & 1) == 1) 
                x *= -1; 
            
            int res = 0; 
            while (x != 0) 
            { 
                if (x < 10) 
                { 
                    res += x; 
                    return res; 
                } 
                res += x - (int)(x * 0.1) * 10; 
                x = (int)(x * 0.1); 
            } 
            return res;
        }

        private static bool Negative(int x)
        {
            return ((x >> 31) & 1) == 1;
        }

        private static BigInteger SoD_3(BigInteger n)
        {
            BigInteger res = 0;
            var s = n.ToString();

            int len = s.Length;

            for (int i = 0; i < len; i++)
            {
                res += s[i] - '0';
            }

            return res;
        }

        static void Main(string[] args)
        {
            var testVar = int.MaxValue / 10;
            int testCount = 10000000;

            Tools.Decorators.TimeItAccurate(SoD_1, testVar, testCount);

            Console.WriteLine();
            Console.WriteLine();

            Tools.Decorators.TimeItAccurate(SoD_2, testVar, testCount);

            Console.WriteLine();
            Console.WriteLine();

            Tools.Decorators.TimeItAccurate(Count2, testVar, testCount);

            Console.WriteLine(Negative(6));
        }
    }
}
