using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;
using Tools;

namespace _18
{
    class Program
    {
        private static int ComputeGoldenPath(int[][] mountain)
        {
            int[][] pathSum = new int[mountain.Length][];
            pathSum[pathSum.Length - 1] = mountain[mountain.Length - 1];

            for (int i = pathSum.Length - 2; i >= 0; i--)
            {
                pathSum[i] = new int[i + 1];
                for (int j = 0; j <= i; j++)
                {
                    int bestWay = Math.Max(pathSum[i + 1][j], pathSum[i + 1][j + 1]);
                    pathSum[i][j] = mountain[i][j] + bestWay;
                }
            }

            return pathSum[0][0];
        }

        private static int[][] MountainFromFile(string fileName)
        {
            string[] lines = null;
            try
            {
                var reader = new StreamReader(fileName);
                lines = reader.ReadToEnd().Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
                reader.Close();

            }
            catch (IOException e)
            {
                Console.WriteLine("Файл {0} невозможно открыть!", fileName);
                Console.WriteLine(e.Message);
            }

            int[][] mountain = new int[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                mountain[i] = lines[i].Split(' ').Select(int.Parse).ToArray();
            }

            return mountain;
        }

        static void Main(string[] args)
        {
            var mountain18 = MountainFromFile("mountain18.txt");
            var mountain67 = MountainFromFile("mountain67.txt");

            Decorators.Benchmark(ComputeGoldenPath, mountain18);
            Decorators.Benchmark(ComputeGoldenPath, mountain67);
        }
    }
}
