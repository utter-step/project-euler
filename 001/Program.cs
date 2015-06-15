using Tools;

namespace _001
{
    class Program
    {
        private const int LIMIT = 1000;

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve);
        }

        private static int Solve() {
            return SumOfDivisibleBy3And5(LIMIT);
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
