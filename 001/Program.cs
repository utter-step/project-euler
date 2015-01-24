using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1
{
    class Program
    {
        private const int LIMIT = 1000;

        static void Main(string[] args)
        {
            int res = SumOfDivisibleBy3And5(LIMIT);

            Tools.Decorators.TimeItAccurate(SumOfDivisibleBy3And5, LIMIT, 100);
        }

        private static int SumOfDivisibleBy3And5(int limit)
        {
            int countOf3s  = (limit - 1) / 3;
            int countOf5s  = (limit - 1) / 5;
            int countOf15s = (limit - 1) / 15;

            int res = ((countOf3s * (countOf3s + 1)) / 2) * 3 + 
                ((countOf5s * (countOf5s + 1)) / 2) * 5 - 
                ((countOf15s * (countOf15s + 1)) / 2) * 15;
            
            return res;
        }
    }
}
