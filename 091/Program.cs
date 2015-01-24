using System;

using Tools;

namespace _91
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(CountTriangles, 50);
        }

        private static int CountTriangles(int squareAreaLimit)
        {
            int res = 0;

            for (int x1 = 0; x1 <= squareAreaLimit; x1++)
            {
                for (int y1 = 0; y1 <= squareAreaLimit; y1++)
                {
                    for (int x2 = x1; x2 <= squareAreaLimit; x2++)
                    {
                        int yStart = 0;
                        if (x1 == x2)
                        {
                            yStart = y1 + 1;
                        }
                        for (int y2 = yStart; y2 <= squareAreaLimit; y2++)
                        {
                            if (IsRightTriangle(x1, y1, x2, y2))
                            {
                                res++;
                            }
                        }
                    }
                }
            }

            return res;
        }

        private static bool IsRightTriangle(int x1, int y1, int x2, int y2)
        {
            int sideMin = (x1 * x1) + (y1 * y1);
            int sideMed = (x2 * x2) + (y2 * y2);
            int sideMax = (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);

            if (sideMin < sideMed)
            {
                if (sideMax < sideMin)
                {
                    int temp = sideMin;
                    sideMin = sideMax;
                    sideMax = temp;
                }
            } else {
                if (sideMed < sideMax)
                {
                    int temp = sideMin;
                    sideMin = sideMed;
                    sideMed = temp;
                }
                else
                {
                    int temp = sideMin;
                    sideMin = sideMax;
                    sideMax = temp;
                }
            }
            if (sideMax < sideMed)
            {
                int temp = sideMed;
                sideMed = sideMax;
                sideMax = temp;
            }

            return sideMin > 0 && sideMed + sideMin == sideMax;
        }
    }
}
