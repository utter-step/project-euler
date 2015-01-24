using System;

namespace _085
{
    class Program
    {
        private static int CountRectangles(int width, int height)
        {
            int res = 0;

            for (int x = 1; x <= width; x++)
            {
                for (int y = 1; y <= height; y++)
                {
                    res += x * y;
                }
            }

            return res;
        }

        static void Main(string[] args)
        {
            for (int x = 1; x < 100; x++)
            {
                for (int y = x; y < 100; y++)
                {
                    int t = CountRectangles(x, y);
                    if (t > 1990000 && t < 2010000)
                    {
                        Console.WriteLine("{0}x{1} == {2}: {3} rectangles", x, y, x * y, t);
                    }
                }
            }
        }
    }
}
