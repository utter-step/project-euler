using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _14
{
    class Program
    {
        private static Dictionary<long, int> _lengthCache;

        private static int CollatzLength(int num)
        {
            long curNum = num;
            int res = 1;

            while (curNum > 1)
            {
                if ((curNum & 1) == 0)
                {
                    curNum >>= 1;
                }
                else
                {
                    curNum += (curNum << 1) + 1;
                }

                res++;

                if (_lengthCache.ContainsKey(curNum))
                {
                    res += _lengthCache[curNum];
                    break;
                }
            }

            _lengthCache.Add(num, res);
            
            return res;
        }

        private static int LongestCollatz(int upperLimit)
        {
            _lengthCache = new Dictionary<long, int>();

            int max = 0;
            int maxNum = 0;

            for (int i = 2; i < upperLimit; i++)
            {
                int length = CollatzLength(i);

                if (length > max)
                {
                    max = length;
                    maxNum = i;
                }
            }

            return maxNum;
        }

        static void Main(string[] args)
        {
            Decorators.TimeIt(LongestCollatz, 1000000);

            Decorators.TimeItAccurate(LongestCollatz, 1000000, 1);
        }
    }
}
