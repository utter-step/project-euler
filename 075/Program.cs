using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _075
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, 1500000);
            Decorators.Benchmark(Solve_2, 1500000);
        }

        private static int Solve_2(int upperLimit)
        {
            var perimeterCounts = new int[upperLimit];

            var mLimit = (int)Math.Sqrt(upperLimit / 2);

            for (int m = 2; m <= mLimit; m++)
            {
                for (int n = 1; n < m; n++)
                {
                    int perimeter = 2 * (m * m + n * m);

                    if (perimeter > upperLimit)
                    {
                        break;
                    }

                    if (((m + n) & 1) == 1 && NumUtils.Gcd(m, n) == 1)
                    {
                        perimeterCounts[perimeter - 1]++;

                        int kPerimeter = perimeter * 2;
                        while (kPerimeter <= upperLimit)
                        {
                            perimeterCounts[kPerimeter - 1]++;
                            kPerimeter += perimeter;
                        } 
                    }
                }
            }
            return perimeterCounts.Count(x => x == 1);
        }

        private static int Solve(int upperLimit)
        {
            var perimeterCounts = new int[upperLimit];
            
            foreach (Tuple<int, int> coprime in GenerateCoprimes(2, 1, upperLimit))
            {
                int m = coprime.Item1, n = coprime.Item2;

                int perimeter = 2 * (m * m + n * m);
                perimeterCounts[perimeter - 1]++;

                int kPerimeter = perimeter * 2;
                while (kPerimeter <= upperLimit)
                {
                    perimeterCounts[kPerimeter - 1]++;
                    kPerimeter += perimeter;
                }
            }
            return perimeterCounts.Count(x => x == 1);
        }

        private struct Triangle
        {
            private int a;
            private int b;
            private int c;

            public readonly int Perimeter;

            public Triangle(int m, int n)
            {
                a = m * m - n * n;
                b = 2 * m * n;
                c = m * m + n * n;

                Perimeter = a + b + c;
            }
        }

        private static IEnumerable<Tuple<int, int>> GenerateCoprimes(int m, int n, int upperLimit)
        {
            if (2 * (m * m + n * m) <= upperLimit)
            {
                yield return new Tuple<int, int>(m, n);

                foreach (var coprime in GenerateCoprimes(2 * m - n, m, upperLimit))
                {
                    yield return coprime;
                }

                foreach (var coprime in GenerateCoprimes(2 * m + n, m, upperLimit))
                {
                    yield return coprime;
                }

                foreach (var coprime in GenerateCoprimes(2 * n + m, n, upperLimit))
                {
                    yield return coprime;
                }
            }
        }
    }
}
