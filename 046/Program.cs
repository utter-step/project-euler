using System;
using System.Collections.Generic;

using Tools;

namespace _046
{
    class Program
    {
        static void Main()
        {
            var solver = new Solver();

            Decorators.TimeItAccurate(solver.Solve, 1000);
        }

        class Solver
        {
            private int _currentPrimeLimit = 1;

            private HashSet<int> _primes;

            public int Solve()
            {
                for (int i = 3; i < int.MaxValue; i += 2)
                {
                    if (_currentPrimeLimit < i)
                    {
                        _currentPrimeLimit *= 10;
                        _primes = NumUtils.EratospheneSeive(_currentPrimeLimit);
                    }
                    if (!_primes.Contains(i) && !IsGoldbachTrue(i))
                    {
                        return i;
                    }
                }

                throw new Exception();
            }

            private bool IsGoldbachTrue(int number)
            {
                int twiceSquare = 0;
                for (int i = 0; twiceSquare < number; i++)
                {
                    twiceSquare = 2 * i * i;
                    if (_primes.Contains(number - twiceSquare) || number - twiceSquare == 1)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
