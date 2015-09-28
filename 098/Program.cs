using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Tools;

namespace _098
{
    class Program
    {
        private static HashSet<char> _cSet;
        private static HashSet<int> _iSet;

        private static char[] _charMap = {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
        };

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
            _cSet = new HashSet<char>();
            _iSet = new HashSet<int>();

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

                var candidateLength = NumUtils.DigitsCount(candidate);

                if (candidateLength < maxFoundLength)
                {
                    return maxFound;
                }

                if (anagramsByLength[candidateLength] == null)
                {
                    continue;
                }

                var candidateUnique = CountUniqueDigits(candidate);
                var currentAnagrams = anagramsByLength[candidateLength];

                foreach (var pair in currentAnagrams)
                {
                    if (candidateUnique != uniqueCount[pair.Key])
                    {
                        continue;
                    }
                    var trMask = NumToTr(candidate, pair.Value[0]);

                    if (trMask == null)
                    {
                        continue;
                    }

                    foreach (var word in pair.Value.Skip(1))
                    {
                        var num = ApplyTr(trMask, word);
                        if (num[0] != '0' && NumUtils.IsSquare(int.Parse(num)))
                        {
                            maxFound = Math.Max(
                                Math.Max(candidate, maxFound),
                                int.Parse(num)
                            );
                            maxFoundLength = candidateLength;
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
                if (_cSet.Add(c))
                {
                    count += 1;
                }
            }
            _cSet.Clear();
            return count;
        }

        public static int CountUniqueDigits(int num)
        {
            int count = 0;
            int nNum = (int)((0x1999999AL * num) >> 32);
            while (num > 0)
            {
                if (_iSet.Add(num - nNum * 10))
                {
                    count++;
                }
                long invDivisor = 0x1999999A;
                num = nNum;
                nNum = (int)((invDivisor * nNum) >> 32);
            }
            _iSet.Clear();
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

        public static char[] NumToTr(int num, string word)
        {
            var res = new char[127];
            var digits = NumUtils.Digits(num);
            var l = digits.Length;

            for (int i = word.Length - 1; i >= 0; i--)
            {
                if (res[word[i]] != '\0')
                {
                    return null;
                }

                res[word[i]] = _charMap[digits[l - i - 1]];
            }

            return res;
        }

        public static string ApplyTr(char[] mask, string word)
        {
            return new String(word.Select(c => mask[c]).ToArray());
        }
    }
}
