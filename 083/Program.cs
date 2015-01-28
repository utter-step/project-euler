using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace _083
{
    class Program
    {
        private static int Solve(int[,] matrix)
        {
            return 0;
        }

        private static int[][] MatrixFromFile(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                return sr.ReadToEnd()
                    .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x
                        .Split(',')
                        .Select(int.Parse)
                        .ToArray())
                    .ToArray();
            }
        }

        static T[,] CreateRectangularArray<T>(IList<T[]> arrays)
        {
            int minorLength = arrays[0].Length;
            var ret = new T[arrays.Count, minorLength];
            for (int i = 0; i < arrays.Count; i++)
            {
                var array = arrays[i];
                if (array.Length != minorLength)
                {
                    throw new ArgumentException
                        ("All arrays must be the same length");
                }
                for (int j = 0; j < minorLength; j++)
                {
                    ret[i, j] = array[j];
                }
            }
            return ret;
        }

        static void Main(string[] args)
        {
            var matrix = CreateRectangularArray(MatrixFromFile("../../matrix.txt"));

            Decorators.Benchmark(Solve, matrix);
        }
    }
}
