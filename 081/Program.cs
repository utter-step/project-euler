using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace _081
{
    class Program
    {
        private static int Solve(int[,] matrix)
        {
            var pathSum = new int[matrix.GetLength(0) + 1, matrix.GetLength(1) + 1];

            for (int i = 2; i <= matrix.GetLength(0); i++)
            {
                pathSum[i, 0] = 0xFFFFFFF;
            }
            for (int i = 2; i <= matrix.GetLength(1); i++)
            {
                pathSum[0, i] = 0xFFFFFFF;
            }

            for (int i = 1; i < pathSum.GetLength(0); i++)
            {
                for (int j = 1; j < pathSum.GetLength(1); j++)
                {
                    pathSum[i, j] = matrix[i - 1, j - 1] + Math.Min(pathSum[i, j - 1], pathSum[i - 1, j]);
                }
            }
            return pathSum[pathSum.GetLength(0) - 1, pathSum.GetLength(1) - 1];
        }

        private static int[][] MatrixFromFile(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                return sr.ReadToEnd()
                    .Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries)
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
