using System;
using System.Linq;
using System.IO;

using Tools;

namespace _022
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names;
            using (var file = new StreamReader("../../names.txt"))
            {
                names = (from name in file.ReadToEnd().Split(',')
                    select name.Trim('"')).ToArray();
            }

            Decorators.Benchmark(ScoreNames, names);
        }

        private static int ScoreNames(string[] names)
        {
            Array.Sort(names);

            int res = 0;
            for (int i = 0; i < names.Length; )
            {
                int score = 0;
                foreach (char c in names[i])
                {
                    score += c - 'A' + 1;
                }
                res += score * (++i);
            }

            return res;
        }
    }
}
