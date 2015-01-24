using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _042
{
    class Program
    {
        private static bool IsTriangleNumber(int n)
        {
            var temp = Math.Floor(Math.Sqrt(n << 1));

            return temp * (temp + 1) == (n << 1);
        }

        private static bool IsTriangleWord(string word)
        {
            return IsTriangleNumber(WordSum(word));
        }

        private static IEnumerable<string> GetWords(string filename)
        {
            string[] words;

            using (var reader = new StreamReader(filename))
            {
                words = reader.ReadToEnd().Split(',');
            }

            return words.Select(x => x.Trim('"'));
        }

        private static int WordSum(string word)
        {
            int sum = 0;

            foreach (var c in word)
            {
                sum += (c - 'A') + 1;
            }

            return sum;
        }

        static void Main(string[] args)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();

            #region General solution
            stopwatch.Start();
            int count = GetWords("words.txt").Count(IsTriangleWord);
            stopwatch.Stop();

            Console.WriteLine("{0} triange words. {1} ms", count, stopwatch.ElapsedMilliseconds); 
            #endregion

            #region Using LINQ expressions
            stopwatch.Restart();
            int countLinq = (from s in File.OpenText("words.txt").ReadToEnd().Split(',')
                             select s.Trim('"')).Count(IsTriangleWord);
            stopwatch.Stop();

            Console.WriteLine("{0} triange words. {1} ms", countLinq, stopwatch.ElapsedMilliseconds); 
            #endregion
        }
    }
}
