using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _150
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = new[] {
                new[] {-1, 2, -3, 5, -4, -8, 3, -3},
                new[] {2, -4, -6, -8, 2, -5, 4, 1},
                new[] {3, -2, 9, -9, 3, 6, -5, 2},
                new[] {1, -3, 5, -7, 8, -2, 2, -6}
            };

            Console.WriteLine(MinSubmatrix(matrix));
        }

        public static int MinSubarray(int[] array)
        {
            int res = -array[0],
                sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum -= array[i];
                res = Math.Max(sum, res);
                sum = Math.Max(0, sum);
            }

            return -res;
        }

        public static int MinSubmatrix(int[][] matrix)
        {
            for (int i = 1; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    matrix[i][j] += matrix[i - 1][j];
                }
            }

            return 0;
        }

        public static int[,] GenerateTriangle(int rows)
        {
            var res = new int[rows, rows];
            var gen = new Lcg(615949, 797807).GetEnumerator();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    gen.MoveNext();
                    res[i, j] = gen.Current;
                }
            }

            return res;
        }

        private class Lcg : IEnumerable<int>
        {
            private readonly int a;
            private readonly int b;

            public Lcg(int a, int b)
            {
                this.a = a;
                this.b = b;
            }

            public IEnumerator<int> GetEnumerator()
            {
                int t = 0;
                const int mask = (1 << 20) - 1;
                const int shift = 1 << 19;

                while (true)
                {
                    t = (a * t + b) & mask;
                    yield return t - shift;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
