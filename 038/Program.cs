using System;

using Tools;

namespace _038
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
            Decorators.Benchmark(Solve);
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
