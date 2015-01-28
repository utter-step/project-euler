using System;
using System.Linq;

using System.IO;
using Tools;

namespace _099
{
    class Program
    {
        private const string FilePath = "../../base_exp.txt";

        static void Main(string[] args)
        {
            var baseExps = LinesFromFile(FilePath).ToArray();

            Decorators.Benchmark(Solve, baseExps);
        }

        private static string[] LinesFromFile(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                return sr.ReadToEnd().Split('\n');
            }
        }

        private static int Solve(string[] baseExpPairs)
        {
            int maxI = -1;
            double max = 0;

            for (int i = 0; i < baseExpPairs.Length; i++)
            {
                var baseExpPair = baseExpPairs[i];
                var pair = baseExpPair.Split(',');
                double n = int.Parse(pair[0]);
                double exp = int.Parse(pair[1]);

                double curExp = exp * Math.Log(n);

                if (curExp > max)
                {
                    max = curExp;
                    maxI = i;
                } 
            }

            return maxI + 1;
        }
    }
}
