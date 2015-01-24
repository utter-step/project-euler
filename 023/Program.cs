using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _023
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve);
            Decorators.Benchmark(Solve_2);
        }

        private static int Solve_2()
        {
            const int maxImpossible = 20161;

            var amicables = ComputeAmicables(maxImpossible).ToArray();

            var possible = new bool[maxImpossible + 1];

            for (int i = 0; i < amicables.Length; i++)
            {
                for (int j = i; j < amicables.Length; j++)
                {
                    if (amicables[i] + amicables[j] > maxImpossible)
                    {
                        break;
                    }
                    possible[amicables[i] + amicables[j]] = true;
                }
            }

            int res = 0;

            for (int i = 0; i < possible.Length; i++)
            {
                if (!possible[i])
                {
                    res += i;
                }
            }

            return res;
        }

        private static int Solve()
        {
            const int maxImpossible = 20161;

            var amicables = ComputeAmicables(maxImpossible);

            int res = 0;

            for (int n = 1; n <= maxImpossible; n++)
            {
                bool found = false;
                int max = n / 2 + 1;
                foreach (var amicable in amicables)
                {
                    if (amicable > max)
                    {
                        break;
                    }
                    if (amicables.Contains(n - amicable))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    res += n;
                }
            }

            return res;
        }

        private static HashSet<int> ComputeAmicables(int upperLimit)
        {
            NumUtils.ResetPrimesCache();

            var res = new List<int>(upperLimit / 100);

            for (int n = 12; n <= upperLimit; n++)
            {
                if (SumOfDivisors(n) > n)
                {
                    res.Add(n);
                }
            }

            return new HashSet<int>(res);
        }

        private static int SumOfDivisors(int num)
        {
            var factorization = NumUtils.ComputePrimeFactorization_Cached(num);

            int res = 1;
            foreach (var factor in factorization)
            {
                if (factor.Value == 1)
                {
                    res *= factor.Key + 1;
                }
                else
                {
                    res *= (NumUtils.BinaryPower(factor.Key, factor.Value + 1) - 1) / (factor.Key - 1);
                }
            }

            return res - num;
        }

    }
}
