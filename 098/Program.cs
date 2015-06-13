using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Tools;

namespace _098
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = File.OpenText("../../p098_words.txt")
                .ReadToEnd()
                .Split(',')
                .Select(s => s.Trim('"'))
                .ToArray();
            Decorators.Benchmark(Solve, words);
        }

        public static int Solve(IEnumerable<string> words)
        {
            var anagrams = words.GroupBy(WordToKey).Where(g => g.Count() > 1);
            var anagramsByLength = new Dictionary<int, Dictionary<string, string[]>>();

            foreach (var group in anagrams)
            {
                if (anagramsByLength.ContainsKey(group.Key.Length))
                {
                    anagramsByLength[group.Key.Length][group.Key] = group.ToArray();
                }
                else
                {
                    anagramsByLength[group.Key.Length] = new Dictionary<string, string[]>{
                        { group.Key, group.ToArray() },
                    };
                }
            }

            int maxLength = anagramsByLength.Max(pair => pair.Key);
            int maxFound = 0;
            int maxFoundLength = 0;

            for (int i = (int)Math.Sqrt(Math.Pow(10, maxLength)); i >= 96; i--)
            {
                int candidate = i * i;
                var candidateString = candidate.ToString();

                if (candidateString.Length < maxFoundLength)
                {
                    return maxFound;
                }

                if (!anagramsByLength.ContainsKey(candidateString.Length))
                {
                    continue;
                }

                var candidateUnique = candidateString.Distinct().Count();

                var currentAnagrams = anagramsByLength[candidateString.Length];

                foreach (var pair in currentAnagrams)
                {
                    if (candidateUnique != pair.Key.Distinct().Count())
                    {
                        continue;
                    }
                    var trMask = NumToTr(candidateString, pair.Value[0]);

                    foreach (var word in pair.Value.Skip(1))
                    {
                        var num = ApplyTr(trMask, word);
                        if (num[0] != '0' && NumUtils.IsSquare(int.Parse(num)))
                        {
                            maxFound = Math.Max(
                                Math.Max(candidate, maxFound),
                                int.Parse(num)
                            );
                            maxFoundLength = candidateString.Length;
                        }
                    }
                }
            }

            return 0;
        }

        public static string WordToKey(string word)
        {
            var chars = word.ToCharArray();
            Array.Sort(chars);
            return new String(chars);
        }

        public static Dictionary<char, char> NumToTr(string num, string word)
        {
            var res = new Dictionary<char, char>();

            for (int i = 0; i < word.Length; i++)
            {
                res[word[i]] = num[i];
            }

            return res;
        }

        public static string ApplyTr(Dictionary<char, char> mask, string word)
        {
            return new String(word.Select(c => mask[c]).ToArray());
        }
    }
}
