using System;
using System.Linq;

using System.IO;
using Tools;

namespace _089
{
    class Program
    {
        private const string FilePath = "../../roman.txt";

        static void Main(string[] args)
        {
            var romans = RomansFromFile(FilePath);

            Decorators.Benchmark(Solve, romans);
        }

        private static string[] RomansFromFile(string filePath)
        {
            using (var sr = new StreamReader(filePath))
            {
                return sr.ReadToEnd().Split('\n');
            }
        }

        private static int Solve(string[] romans)
        {
            return romans.Sum(roman => roman.Length - IntToRoman(RomanToInt(roman)).Length);
        }

        private static string IntToRoman(int digit, int rank=0)
        {
            if (digit >= 10)
            {
                return IntToRoman(digit / 10, rank + 1) + IntToRoman(digit % 10, rank);
            }

            if (rank < 3)
            {
                if (digit == 9)
                {
                    return new String(new[] {_lettersForRank[rank][0], _lettersForRank[rank + 1][0]});
                }
                if (digit == 4)
                {
                    return new String(new[] {_lettersForRank[rank][0], _lettersForRank[rank][1]});
                }
                if (digit < 4)
                {
                    return new String(_lettersForRank[rank][0], digit);
                }
                if (digit < 9)
                {
                    return new String(_lettersForRank[rank][1], 1) + IntToRoman(digit - 5, rank);
                }
            }
            return new String('M', digit);
        }

        private static readonly char[][] _lettersForRank =
        {
            new[] {'I', 'V'},
            new[] {'X', 'L'},
            new[] {'C', 'D'},
            new[] {'M'}
        };

        private static int RomanToInt(string roman)
        {
            var romanDigits = roman.ToCharArray();

            int previous = int.MaxValue;
            int res = 0;

            for (int i = 0; i < romanDigits.Length; i++)
            {
                int current = LetterToInt(romanDigits[i]);
                if (current > previous)
                {
                    res += current - 2 * previous;
                }
                else
                {
                    res += current;
                }
                previous = current;
            }

            return res;
        }

        private static int LetterToInt(char roman)
        {
            switch (roman)
            {
                case 'M':
                    return 1000;
                case 'D':
                    return 500;
                case 'C':
                    return 100;
                case 'L':
                    return 50;
                case 'X':
                    return 10;
                case 'V':
                    return 5;
                case 'I':
                    return 1;
                default:
                    throw new ArgumentException("Unknown roman digit " + roman, "roman");
            }
        }
    }
}
