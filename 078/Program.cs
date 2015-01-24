using System;
using System.Collections.Generic;

using Tools;

namespace _78
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.TimeIt(Solve, 1000000);
        }

        private static int Solve(int divisibleBy)
        {
            const int LIMIT = int.MaxValue / 16;
            for (int i = 1; i < LIMIT; i++)
            {
                Console.Write("{0}\r", i);
                if (CountPartitions(i) % divisibleBy == 0)
                {
                    _partitions.Clear();
                    _partitions.Add(new[] {1});

                    _cachedSolutions.Clear();
                    _cachedSolutions.Add(1);

                    _maxComputed = 0;
                    return i;
                }
            }

            return -1;
        }

        #region Brute DP solution
        private static List<int[]> _partitions = new List<int[]>(new[]
        {
            new[] {1},
        });

        private static List<int> _cachedSolutions = new List<int>(new[]
        {
            1
        });
        private static int _maxComputed;

        private static int CountPartitions(int nCoins)
        {
            if (nCoins <= _maxComputed)
            {
                return _cachedSolutions[nCoins];
            }

            for (int i = _maxComputed + 1; i <= nCoins; i++)
            {
                _partitions.Add(new int[i + 1]);
                _partitions[i][1] = 1;
                for (int j = 2; j <= i; j++)
                {
                    // To get actual values - remove modulo and use BigInteger
                    if (j <= i / 2)
                    {
                        _partitions[i][j] = (_partitions[i][j - 1] + _partitions[i - j][j]) % 10000000;
                    }
                    else
                    {
                        _partitions[i][j] = (_partitions[i][j - 1] + _cachedSolutions[i - j]) % 10000000;
                    }
                }
                _cachedSolutions.Add(_partitions[i][i]);
            }

            _maxComputed = nCoins;
            if (_maxComputed - LastCleared > 50)
            {
                ClearPartitions();
            }
            return _cachedSolutions[nCoins];
        }

        private static int LastCleared;

        private static void ClearPartitions()
        {
            for (int i = LastCleared / 2; i < _maxComputed / 2; i++)
            {
                _partitions[i] = null;
            }
            LastCleared = _maxComputed;
        }
        #endregion
    }
}
