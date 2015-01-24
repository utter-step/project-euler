using System;

using Tools;

namespace _058
{
    class Program
    {
        private static int GetSizeForRatio(double minimalDesiredRatio)
        {
            if (minimalDesiredRatio < 0 || minimalDesiredRatio > 1)
            {
                throw new ArgumentOutOfRangeException("minimalDesiredRatio");
            }

            double totalCount = 1;
            int primesCount = 0;

            int currentElem = 1;
            for (int i = 1; i < int.MaxValue; i++)
            {
                totalCount += 4;

                for (int j = 0; j < 4; j++)
                {
                    currentElem += i * 2;
                    if (NumUtils.IsPrime(currentElem))
                    {
                        primesCount++;
                    }
                }

                if (primesCount / totalCount < minimalDesiredRatio)
                {
                    return i * 2 + 1;
                }
            }

            throw new Exception("Something awful happend.");
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(GetSizeForRatio, 0.1);
        }
    }
}
