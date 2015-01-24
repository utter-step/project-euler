using System;
using System.Collections.Generic;

using Tools;

namespace _35
{
    class Program
    {
        private const int LIMIT = 1000000;

        private static IEnumerable<int> CycleNumber(int num)
        {
            int length = 1;
            int tenPow = 1;

            while (tenPow * 10 <= num)
            {
                tenPow *= 10;
                length++;
            }

            int curNum = num;

            for (int i = 0; i < length; i++)
            {
                yield return curNum;

                curNum = (curNum % tenPow) * 10 + curNum / tenPow;
            }
        }

        private static int ComputeResult(int limit)
        {
            var primes = NumUtils.EratospheneSeive(limit);

            int result = 0;

            var tested = new HashSet<int>();

            foreach (int prime in primes)
            {
                bool isCyclic = true;
                int count = 0;

                foreach (int nextCyclic in CycleNumber(prime))
                {
                    if (tested.Contains(nextCyclic) || !primes.Contains(nextCyclic))
                    {
                        isCyclic = prime == 11;
                        break;
                    }

                    tested.Add(nextCyclic);
                    count++;
                }

                if (isCyclic)
                {
                    result += count;
                }
            }

            return result;
        }

        static void Main(string[] args)
        {
            Decorators.TimeItAccurate(ComputeResult, LIMIT, 10);
        }
    }
}
