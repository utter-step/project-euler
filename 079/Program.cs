using System;
using System.IO;
using System.Linq;
using Tools;

namespace _079
{
    class Program
    {
        private const string FilePath = "../../keylog.txt";

        static void Main(string[] args)
        {
            var attempts = AttemptsFromFile(FilePath);

            Decorators.Benchmark(Solve, attempts);
        }

        private static string[] AttemptsFromFile(string filePath)
        {
            using (var sr = new StreamReader(filePath))
            {
                return sr.ReadToEnd().Split('\n');
            }
        }

        private static string Solve(string[] attempts)
        {
            var digits = Enumerable.Range(0, 10).Select(x => new PasscodeDigit(x)).ToArray();

            foreach (var attempt in attempts)
            {
                for (int i = 0; i < attempt.Length; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        digits[attempt[i] - '0'].SetAfter(attempt[j] - '0');
                    }
                    for (int j = i + 1; j < attempt.Length; j++)
                    {
                        digits[attempt[i] - '0'].SetBefore(attempt[j] - '0');
                    }
                }
            }

            var usedDigits = digits.Where(x => x.Used).ToArray();

            Array.Sort(usedDigits);

            return String.Join("", usedDigits);
        }

        struct PasscodeDigit : IComparable<PasscodeDigit>
        {
            public bool Used { get; private set; }
            private readonly int _digit;

            private readonly int[] _compareTo;

            public PasscodeDigit(int digit)
                : this()
            {
                _digit = digit;
                _compareTo = new int[10];
            }

            public void SetBefore(int digit)
            {
                Used = true;
                _compareTo[digit] = -1;
            }

            public void SetAfter(int digit)
            {
                Used = true;
                _compareTo[digit] = 1;
            }

            public int CompareTo(PasscodeDigit other)
            {
                return _compareTo[other._digit];
            }

            public override string ToString()
            {
                return _digit.ToString();
            }
        }
    }
}
