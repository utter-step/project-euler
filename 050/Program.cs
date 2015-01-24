using System.Linq;

using Tools;

namespace _050
{
    static class Program
    {
        static void Main()
        {
            Decorators.TimeItAccurate(Solve, 1000000, 10);
        }

        private static int Solve(int limit)
        {
            var primes = NumUtils.EratospheneSeive(limit);

            int count = primes.Count;

            var sumsTo = new int[count + 1];

            int pos = 0;
            foreach (var prime in primes)
            {
                sumsTo[pos + 1] = sumsTo[pos++] + prime;
            }

            int max = 0;
            int maxLength = 0;

            for (int i = 1; i <= count - maxLength; i++)
            {
                int remove = sumsTo[i - 1];
                for (int l = maxLength + 1; l < count - i; l++)
                {
                    int sum = sumsTo[i + l] - remove;
                    if (sum > limit)
                    {
                        break;
                    }
                    if (primes.Contains(sum))
                    {
                        max = sum;
                        maxLength = l;
                    }
                }
            }

            return max;
        }
    }
}
