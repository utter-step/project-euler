using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Tools
{
    public static class NumUtils
    {
        #region SqrtUpper

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SqrtUpper(int n)
        {
            return (int)Math.Sqrt(n) + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SqrtUpper(long n)
        {
            return (int)Math.Sqrt(n) + 1;
        }

        #endregion

        private static HashSet<int> _primes;
        private static int _primesLimit = 0;
        private const int CACHE_CONSTANT = 3;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Gcd(int a, int b)
        {
            while (b != 0)
            {
                b = a % (a = b);
            }
            return a;
        }

        public static void ResetPrimesCache()
        {
            _primesLimit = 0;
            _primes = null;
        }

        public static void PrecomputePrimes(int upperLimit)
        {
            _primes = EratospheneSeive(upperLimit * CACHE_CONSTANT, 3);
            _primesLimit = upperLimit * CACHE_CONSTANT - 1;
        }

        #region Prime factorization
        public static Dictionary<int, int> ComputePrimeFactorization_Cached(int num)
        {
            if (_primesLimit * _primesLimit < num)
            {
                PrecomputePrimes(SqrtUpper(num));
            }

            var factors = new Dictionary<int, int>();

            if ((num & 1) == 0)
            {
                factors.Add(2, 1);
                num >>= 1;
            }

            while ((num & 1) == 0)
            {
                num >>= 1;
                factors[2]++;
            }

            foreach (var factor in _primes)
            {
                if (num % factor == 0)
                {
                    factors.Add(factor, 1);
                    num /= factor;
                }
                while (num % factor == 0)
                {
                    num /= factor;
                    factors[factor]++;
                }

                if (factor * factor > num)
                {
                    break;
                }

                if (num == 1)
                {
                    return factors;
                }
            }

            if (num > 1)
            {
                factors.Add(num, 1);
            }

            return factors;
        }

        public static Dictionary<int, int> ComputePrimeFactorization(int num)
        {
            int sqrt = SqrtUpper(num);
            int maxFactor = sqrt;

            var primes = EratospheneSeive(maxFactor, 3);

            var factors = new Dictionary<int, int>();

            if ((num & 1) == 0)
            {
                factors.Add(2, 1);
                num >>= 1;
            }

            while ((num & 1) == 0)
            {
                num >>= 1;
                factors[2]++;
            }

            foreach (var factor in primes)
            {
                if (num % factor == 0)
                {
                    factors.Add(factor, 1);
                    num /= factor;
                }
                while (num % factor == 0)
                {
                    num /= factor;
                    factors[factor]++;
                }

                if (factor > maxFactor)
                {
                    break;
                }

                if (num == 1)
                {
                    return factors;
                }
            }

            if (num > 1)
            {
                factors.Add(num, 1);
            }

            return factors;
        } 
        #endregion

        #region BinaryPower
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static dynamic BinaryPower(dynamic n, int exp)
        {
            dynamic res = 1;
            while (exp > 0)
            {
                if ((exp & 1) == 1)
                {
                    exp--;
                    res *= n;
                }

                n *= n;
                exp >>= 1;
            }

            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DynamicMatrix BinaryPower(DynamicMatrix matrix, int exp)
        {
            DynamicMatrix res = DynamicMatrix.IdentityMatrix(matrix.GetLength(0), matrix.GetInnerType());
            while (exp > 0)
            {
                if ((exp & 1) == 1)
                {
                    exp--;
                    res *= matrix;
                }

                matrix *= matrix;
                exp >>= 1;
            }

            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int BinaryPower(int n, int exp)
        {
            int res = 1;
            while (exp > 0)
            {
                if ((exp & 1) == 1)
                {
                    exp--;
                    res *= n;
                }

                n *= n;
                exp >>= 1;
            }

            return res;
        } 

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long BinaryPower(long n, int exp)
        {
            long res = 1;
            while (exp > 0)
            {
                if ((exp & 1) == 1)
                {
                    exp--;
                    res *= n;
                }

                n *= n;
                exp >>= 1;
            }

            return res;
        }
        #endregion

        #region ModularBinaryPower
        public static int ModularBinaryPower(int n, int exp, int modulus)
        {
            int result = 1;

            while (exp > 0)
            {
                if ((exp & 1) == 1)
                {
                    result = (result * n) % modulus;
                }
                exp >>= 1;
                n = (n * n) % modulus;
            }

            return result;
        } 
        #endregion

        #region Eratosphene seive
        public static int[] EratospheneSeive_MemoryAccurate(int upperLimit, int lowerLimit = 2)
        {
            var bArray = new BitArray(upperLimit + 1, true);

            for (int j = 2 * 2; j > 0 && j <= upperLimit; j += 2)
            {
                bArray[j] = false;
            }

            for (int i = 3; i * i <= upperLimit; i += 2)
            {
                if (bArray[i])
                {
                    for (int j = i * 2; j > 0 && j <= upperLimit; j += i)
                    {
                        bArray[j] = false;
                    }
                }
            }

            var list = new List<int>();
            for (int i = lowerLimit; i <= upperLimit; i++)
            {
                if (bArray[i])
                {
                    list.Add(i);
                }
            }

            return list.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<int> EratospheneSeive(int upperLimit, int lowerLimit = 2)
        {
            if (upperLimit < 2)
            {
                return new HashSet<int>();
            }

            if (upperLimit <= _primesLimit)
            {
                var primes = new HashSet<int>(_primes);
                primes.RemoveWhere(x => x >= upperLimit || x < lowerLimit);
                return primes;
            }

            upperLimit++;

            var primeList = new bool[upperLimit];
            primeList[2] = true;

            for (int i = 3; i < upperLimit; i += 2)
            {
                primeList[i] = true;
            }

            var primeSet = lowerLimit == 2 ? new HashSet<int> { 2 } : new HashSet<int>();

            if ((lowerLimit & 1) == 0)
            {
                lowerLimit++;
            }

            for (int i = 3; i * i < upperLimit; i += 2)
            {
                if (primeList[i])
                {
                    for (int j = i * i; j < upperLimit; j += i)
                    {
                        primeList[j] = false;
                    }
                }
            }

            for (int i = lowerLimit; i < upperLimit; i += 2)
            {
                if (primeList[i])
                {
                    primeSet.Add(i);
                }
            }

            return primeSet;
        } 
        #endregion

        #region Feature tests (is prime, is palindromic, etc.)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPrime(int n)
        {
            if (n < 1)
            {
                return false;
            }

            if ((n & 1) == 0 && n != 2)
            {
                return false;
            }

            for (int i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static int SumOfDigits(long n)
        {
            long s = 0;

            while (n > 0)
            {
                s += n % 10;
                n /= 10;
            }

            return (int)s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SumOfDigits(int x)
        {
            var res = 0;
            var xNext = (int)((0x1999999AL * x) >> 32);

            while (x >= 10)
            {
                res += x - xNext * 10;
                x = xNext;
                long invDivisor = 0x1999999A;
                xNext = (int)((invDivisor * xNext) >> 32);;
            }
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSquare(int n)
        {
            var root = Math.Sqrt(n);
            return (int)root == root;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPalindromic(string s)
        {
            int lastIndex = s.Length - 1;
            int middle = s.Length / 2;

            for (int i = 0; i < middle; i++)
            {
                if (s[i] != s[lastIndex - i])
                {
                    return false;
                }
            }

            return true;
        }

        public class PandigitalChecker
        {
            private readonly int baseNum;
            private readonly int sumBase;
            private readonly int maskBase;

            private char[] digitsSorted;

            public PandigitalChecker(int baseNum)
            {
                this.baseNum = baseNum;
                sumBase = SumOfDigits(baseNum);
                maskBase = CreateMask(baseNum);
            }

            public bool IsPandigital(int num)
            {
                return WeakTest(num) && StrongTest(num);
            }

            private bool WeakTest(int num)
            {
                return CheckSum(num) && CheckMask(num);
            }

            private bool StrongTest(int num)
            {
                if (digitsSorted == null)
                {
                    digitsSorted = baseNum.ToString().ToCharArray();
                    Array.Sort(digitsSorted);
                }
                    
                var cs = num.ToString().ToCharArray();
                if (cs.Length != digitsSorted.Length)
                {
                    return false;
                }

                Array.Sort(cs);

                for (int i = 0; i < cs.Length; i++)
                {
                    if (cs[i] != digitsSorted[i])
                    {
                        return false;
                    }
                }

                return true;
            }

            private bool CheckMask(int num)
            {
                return CreateMask(num) == maskBase;
            }

            private static int CreateMask(int num)
            {
                int mask = 0;
                int xNext = (int)((0x1999999AL * num) >> 32);

                while (num > 0)
                {
                    mask |= 1 << (num - xNext * 10);
                    num = xNext;
                    long invDivisor = 0x1999999A;
                    xNext = (int)((invDivisor * xNext) >> 32);
                }

                return mask;
            }

            private bool CheckSum(int num)
            {
                return SumOfDigits(num) == sumBase;
            }

            public static int SumOfDigits(int num)
            {
                int res = 0;

                while (num > 0)
                {
                    res += num % 10;
                    num /= 10;
                }

                return res;
            }
        }
        #endregion

        #region Binary algorythms

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NumLeadingZeroes(int x)
        {
            int y, m, n;

            y = -(x >> 16);
            m = (y >> 16) & 16;
            n = 16 - m;
            x >>= m;
            y = x - 0x100;
            m = (y >> 16) & 8;
            n += m;
            x <<= m;
            y = x - 0x1000;
            m = (y >> 16) & 4;
            n += m;
            x <<= m;
            y = x - 0x4000;
            m = (y >> 16) & 2;
            n += m;
            x <<= m;
            y = x >> 14;
            m = y & ~(y >> 1);
            return n + 2 - m;
        }

        #endregion

        private static int[] _binaryApproximation = {
            9, 9, 9, 8, 8, 8,
            7, 7, 7, 6, 6, 6, 6, 5, 5, 5, 4, 4, 4, 3, 3, 3, 3,
            2, 2, 2, 1, 1, 1, 0, 0, 0, 0
        };

        private static int[] _fixApproximation = {
            1, 10, 100, 1000, 10000,
            100000, 1000000, 10000000, 100000000, 1000000000
        };

        public static readonly int[] TenPows = {
            0, 1, 10, 100, 1000, 10000, 100000,
            1000000, 10000000, 100000000, 1000000000
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IntLog10(int x)
        {
            int y;

            y = _binaryApproximation[NumLeadingZeroes(x)];
            if (x < _fixApproximation[y])
            {
                y = y - 1;
            }
            return y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int DigitsCount(int x)
        {
            return IntLog10(x) + 1;
        }

        public static int[] Digits(int x)
        {
            var res = new int[DigitsCount(x)];
            int xNext = (int)((0x1999999AL * x) >> 32);
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = x - xNext * 10;
                x = xNext;
                long invDivisor = 0x1999999A;
                xNext = (int)((invDivisor * xNext) >> 32);
            }
            return res;
        }
    }
}
