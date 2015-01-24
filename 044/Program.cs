using System;
using System.Collections.Generic;
using System.Linq;

using Tools;

namespace _044
{
    static class Program
    {
        static void Main()
        {
            Decorators.Benchmark(Solve, 10000);
        }

        private static long Solve(int count)
        {
            var pentagonals = GeneratePentagonal(count);
            var pentagonalsArray = pentagonals.ToArray();

            long maxComputed = pentagonalsArray[count - 1];

            for (int start = 0; start < pentagonalsArray.Length; start++)
            {
                for (int end = start + 1; end < pentagonalsArray.Length; end++)
                {
                    long diff = pentagonalsArray[end] - pentagonalsArray[start];
                    if (pentagonals.Contains(diff))
                    {
                        long sum = pentagonalsArray[end] + pentagonalsArray[start];
                        if (sum < maxComputed)
                        {
                            if (pentagonals.Contains(sum))
                            {
                                return diff;
                            }
                        }
                        else
                        {
                            if (IsPentagonal(sum))
                            {
                                return diff;
                            }
                        }
                    }
                } 
            }

            return -1;
        }

        private static HashSet<long> GeneratePentagonal(int count)
        {
            var result = new HashSet<long>();
            for (int i = 1; i <= count; i++)
            {
                result.Add((i * (3L * i - 1)) / 2);
            }

            return result;
        } 

        private static bool IsPentagonal(long x)
        {
            double testN = (Math.Sqrt(24 * x + 1) + 1) / 6;
            if (testN == (int)testN)
            {
                return true;
            }
            return false;
        }
    }
}
