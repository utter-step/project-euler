using Tools;

namespace _015
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve);
        }

        public static long Solve()
        {
            return ComputeLaticePaths(20, 20);
        }

        public static long ComputeLaticePaths(int width, int height)
        {
            var latice = new long[width + 2, height + 2];

            for (int x = width; x >= 0; x--)
            {
                latice[x, height] = 1;
                for (int y = height - 1; y >= 0; y--)
                {
                    latice[x, y] = latice[x + 1, y] + latice[x, y + 1];
                }
            }

            return latice[0, 0];
        }
    }
}
