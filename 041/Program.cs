using System;
using System.Linq;

using Tools;

namespace _41
{
    class Program
    {
        private static bool IsPrime(int n)
        {
            if ((n & 1) == 0)
            {
                return false;
            }

            for (int i = 3; i * i < n; i += 2)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static int Solve()
        {
            var startstate = new[] { 1, 2, 3, 4, 5, 6, 7 };

            var permutations = Permutations.ProducePermutations(startstate)
                .Select(Permutations.ArrayToNum);

            var primePermutations = permutations.Where(IsPrime);

            var answer = primePermutations.Any() ? primePermutations.Max() : 0;

            return answer;
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve);
        }
    }
}
