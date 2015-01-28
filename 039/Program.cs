using Tools;

namespace _039
{
    static class Program
    {
        static void Main(string[] args)
        {
            Decorators.TimeItAccurate(Solve, 2000, 100);
        }

        private static int Solve(int upperLimit)
        {
            var solutions = new int[upperLimit + 1];

            for (int perimeter = 2; perimeter <= upperLimit; perimeter += 2)
            {
                int solution = CountPureSolutions(perimeter);
                solutions[perimeter] += solution;
                for (int j = 2; j * perimeter < upperLimit; j++)
                {
                    solutions[j * perimeter] += solution;
                }
            }

            int res = 0;
            int max = 0;
            for (int i = 0; i < upperLimit; i++)
            {
                if (solutions[i] > max)
                {
                    res = i;
                    max = solutions[i];
                }
            }

            return res;
        }

        private static int CountPureSolutions(int perimeter)
        {
            int count = 0;
            int halfPerimeter = perimeter >> 1;

            for (int a = 3; a <= halfPerimeter; a += 2)
            {
                for (int b = (halfPerimeter - a) & (int.MaxValue - 3); b <= halfPerimeter; b += 4)
                {
                    int c = perimeter - (a + b);
                    if (a * a + b * b == c * c)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
