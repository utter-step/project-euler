using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Tools;

namespace _098
{
    class Program
    {
        private static HashSet<char> _set;

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
            _set = new HashSet<char>();

            var anagrams = words.GroupBy(WordToKey).Where(g => g.Count() > 1);
            int maxLength = anagrams.Max(x => x.Key.Length);

            var anagramsByLength = GroupAnagramsByLength(anagrams, maxLength);

            var uniqueCount = new Dictionary<string, int>();

            foreach (var group in anagrams)
            {
                uniqueCount[group.Key] = CountUnique(group.Key);
            }

            int maxFound = 0;
            int maxFoundLength = 0;

            for (int i = (int)Math.Sqrt(Math.Pow(10, maxLength)) - 1; i >= 96; i--)
            {
                int candidate = i * i;
                var candidateString = candidate.ToString();

                if (candidateString.Length < maxFoundLength)
                {
                    return maxFound;
                }

                if (anagramsByLength[candidateString.Length] == null)
                {
                    continue;
                }

                var candidateUnique = CountUnique(candidateString);
                var currentAnagrams = anagramsByLength[candidateString.Length];

                foreach (var pair in currentAnagrams)
                {
                    if (candidateUnique != uniqueCount[pair.Key])
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

            return maxFound;
        }

        public static int CountUnique(string word)
        {
            int count = 0;
            foreach (var c in word)
            {
                if (_set.Add(c))
                {
                    count += 1;
                }
            }
            _set.Clear();
            return count;
        }

        public static string WordToKey(string word)
        {
            var chars = word.ToCharArray();
            Array.Sort(chars);
            return new String(chars);
        }

        public static Dictionary<string, string[]>[] GroupAnagramsByLength(
            IEnumerable<IGrouping<string, string>> anagrams, int maxLength)
        {
            var anagramsByLength = new Dictionary<string, string[]>[maxLength + 1];

            foreach (var group in anagrams)
            {
                if (anagramsByLength[group.Key.Length] != null)
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

            return anagramsByLength;
        }

        public static char[] NumToTr(string num, string word)
        {
            var res = new char[127];

            for (int i = 0; i < word.Length; i++)
            {
                res[word[i]] = num[i];
            }

            return res;
        }

        public static string ApplyTr(char[] mask, string word)
        {
            return new String(word.Select(c => mask[c]).ToArray());
        }
    }
}
