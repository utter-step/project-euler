using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _38
{
    class Program
    {
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

        static void Main(string[] args)
        {
            var pc = new NumUtils.PandigitalChecker(123456788);

            Decorators.Benchmark(Solve);

            //Console.WriteLine("Truly pandigital");
            //Decorators.TimeItAccurate(pc.IsPandigital, 887654321, 10000000);

            //Console.WriteLine("Mask ok, sum failed");
            //Decorators.TimeItAccurate(pc.IsPandigital, 123456766, 10000000);

            //Console.WriteLine("Sum and mask failed");
            //Decorators.TimeItAccurate(pc.IsPandigital, 123456778, 10000000);

            //Decorators.TimeItAccurate(Solve, 1000000);
        }

        static int Test()
        {
            var pc = new NumUtils.PandigitalChecker(11111054);

            Console.WriteLine(pc.IsPandigital(1000044));

            int pans = 0;
            for (int i = 10000000; i <= 99999999; i++)
            {
                if (pc.IsPandigital(i))
                {
                    pans++;
                }
            }

            return pans;
        }

        static int Solve()
        {
            var pc = new NumUtils.PandigitalChecker(123456789);

            int max = 0;

            for (int num = 1; num < 10000; num++)
            {
                int curNum = num;

                int nextNum = num + num;
                while (NumLength(curNum) < 9)
                {
                    int nextLen = NumLength(nextNum);
                    for (int i = 0; i < nextLen; i++)
                    {
                        curNum *= 10;
                    }
                    curNum += nextNum;
                    nextNum += num;
                }

                if (pc.IsPandigital(curNum))
                {
                    if (curNum > max)
                    {
                        max = curNum;
                    }
                }
            }

            return max;
        }

        static int NumLength(int num)
        {
            return num.ToString().Length;
        }
    }
}
