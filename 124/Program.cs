using System;
using System.Linq;

using Tools;

namespace _124
{
	class Program
	{
		public const int N = 100000;
		public const int K = 10000;

		public static void Main(string[] args)
		{
            Decorators.Benchmark(Solve);
		}

		private static int Solve()
		{
			return GetE(N, K);
		}

		private static int GetE(int n, int k)
		{
            var numrads = Enumerable
				.Range(1, n)
				.Select(num => new NumRad(num))
                .ToArray();

            Array.Sort(numrads);
            NumUtils.ResetPrimesCache();

            return numrads[k - 1].Num;
		}

		public struct NumRad : IComparable<NumRad>
		{
			public int Num { get; private set; }
			public int Rad { get; private set; }

			public NumRad(int num) : this()
			{
                var factorization = NumUtils
                    .ComputePrimeFactorization_Cached(num);

                int rad = 1;

                foreach (var factor in factorization)
                {
                    rad *= factor.Key;
                }

                Num = num;
				Rad = rad;
			}

			public int CompareTo(NumRad other)
			{
				if (Rad == other.Rad)
				{
					return Num.CompareTo(other.Num);
				}
				return Rad.CompareTo(other.Rad);
			}

            public override string ToString()
            {
                return string.Format("[NumRad: Num={0}, Rad={1}]", Num, Rad);
            }
		}
	}
}
