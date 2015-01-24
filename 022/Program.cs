using System;
using System.Linq;
using System.Text;
using System.IO;

namespace _22
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new System.Diagnostics.Stopwatch();

            s.Start();
            int res = ScoreNames("names.txt");
            s.Stop();

            Console.WriteLine("{0} - {1} ms", res, s.ElapsedMilliseconds);
        }

        private static int ScoreNames(string fileName)
        {
            string[] names;
            using (var file = new StreamReader(fileName))
            {
                names = (from name in file.ReadToEnd().Split(',')
                         select name.Trim('"')).ToArray();
            }

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
