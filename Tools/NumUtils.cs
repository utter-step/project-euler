using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Tools
{
    public class NumUtils
    {
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
                PrecomputePrimes((int)Math.Sqrt(num) + 1);
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
            int sqrt = (int)Math.Sqrt(num) + 1;
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

            upperLimit++;

            var primeList = new bool[upperLimit];
            primeList[2] = true;

            for (int i = 3; i < upperLimit; i += 2)
            {
                primeList[i] |= true;
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
                        primeList[j] &= false;
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
                return CheckMask(num) && CheckSum(num);
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

                while (num > 0)
                {
                    mask |= 1 << (num % 10);
                    num /= 10;
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
    }
}
