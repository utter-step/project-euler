using System;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public class LongInteger : IComparable<LongInteger>
    {
        public static readonly LongInteger Zero = new LongInteger(0);
        public static readonly LongInteger One = new LongInteger(1);

        private const int Positive = 1;
        private const int Negative = -1;

        private const int BASE = 10000;
        private const int DIGITS_IN_PART = 4;

        private List<int> _parts;

        private int _sign;
        private int _maxRadix;

        public int LastDigit
        {
            get
            {
                return _parts[0] % 10;
            }
        }

        public LongInteger(int num, int capacity = 3)
        {
            _sign = num < 0 ? Negative : Positive;
            num *= _sign;

            _parts = new List<int>(new int[capacity + 3]);

            int i = 0;
            while (num > 0)
            {
                _parts[i++] = num % BASE;
                num /= BASE;
            }

            TrailZeroes();
        }

        public LongInteger(LongInteger longInteger)
        {
            _sign = longInteger._sign;

            _parts = new List<int>();
            _parts.AddRange(longInteger._parts);

            _maxRadix = longInteger._maxRadix;
        }

        public static LongInteger Pow(LongInteger n, int exp)
        {
            LongInteger res = 1;
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

        public static LongInteger operator -(LongInteger a)
        {
            var res = new LongInteger(a) {_sign = Negative * a._sign};

            return res;
        }

        public static LongInteger operator +(LongInteger a, LongInteger b)
        {
            if (a._sign == b._sign)
            {
                var res = Add(a, b);
                res._sign = a._sign;

                return res;
            }
            else if (a._sign == Negative)
            {
                return b - (-a);
            }
            else
            {
                return a - (-b);
            }
        }

        public static LongInteger operator -(LongInteger a, LongInteger b)
        {
            if (a._sign != b._sign)
            {
                var res = Add(a, b);
                res._sign = a._sign;

                return res;
            }
            else if (a._sign == Negative)
            {
                return (-b) - (-a);
            }
            else if (CompareParts(a, b) >= 0)
            {
                return Subtract(a, b);
            }
            else
            {
                var res = Subtract(b, a);
                res._sign = Negative;

                return res;
            }
        }

        private static int CompareParts(LongInteger a, LongInteger b)
        {
            if (a._parts.Count.CompareTo(b._parts.Count) != 0)
            {
                return a._parts.Count.CompareTo(b._parts.Count);
            }
            for (int i = a._parts.Count - 1; i >= 0; i--)
            {
                if (a._parts[i].CompareTo(b._parts[i]) != 0)
                {
                    return a._parts[i].CompareTo(b._parts[i]);
                }
            }
            return 0;
        }

        public static LongInteger operator /(LongInteger a, LongInteger b)
        {
            throw new NotImplementedException();
        }

        public static LongInteger operator *(LongInteger a, LongInteger b)
        {
            if (a._sign == b._sign)
            {
                return Multiply(a, b);
            }
            var res = Multiply(a, b);
            res._sign = -1;

            return res;
        }

        public static LongInteger operator *(LongInteger a, int b)
        {
            if (b >= BASE)
            {
                return a * new LongInteger(b);
            }

            var res = MultiplySmall(a, b);

            if (b < 0)
            {
                res._sign = -1;
            }

            if (res._sign == a._sign)
            {
                res._sign = 1;
            }

            return res;
        }

        private static LongInteger Add(LongInteger a, LongInteger b)
        {
            int radix = Math.Max(a._maxRadix, b._maxRadix);

            var res = new LongInteger(0) { _parts = new List<int>() };

            int i, carry = 0;
            for (i = 0; i <= radix || carry > 0; i++)
            {
                int current = carry + 
                    (i < a._parts.Count ? a._parts[i] : 0) + 
                    (i < b._parts.Count ? b._parts[i] : 0);

                carry = current / BASE;
                res._parts.Add(current - carry * BASE);
            }

            res.TrailZeroes();

            return res;
        }

        private static LongInteger Subtract(LongInteger a, LongInteger b)
        {
            int radix = Math.Max(a._maxRadix, b._maxRadix);

            var res = new LongInteger(0, 1);

            int i, carry = 0;
            for (i = 0; i <= radix || carry != 0; i++)
            {
                int current = - carry +
                    (i < a._parts.Count ? a._parts[i] : 0) -
                    (i < b._parts.Count ? b._parts[i] : 0);

                carry = current < 0 ? 1 : 0;
                res._parts.Add(current + carry * BASE);
            }

            res.TrailZeroes();
            
            return res;
        }

        private static LongInteger Multiply(LongInteger a, LongInteger b)
        {
            var res = new LongInteger(0);
            res._parts = new List<int>(new int[a._maxRadix + b._maxRadix + 2]);

            for (int i = 0; i <= a._maxRadix; i++)
            {
                for (int j = 0, carry = 0; j <= b._maxRadix || carry > 0; j++)
                {
                    int current = res._parts[i + j] +
                        a._parts[i] * (j < b._parts.Count ? b._parts[j] : 0) + carry;

                    carry = current / BASE;
                    res._parts[i + j] = current - carry * BASE;
                }
            }

            res.TrailZeroes();

            return res;
        }

        private static LongInteger MultiplySmall(LongInteger a, int b)
        {
            var res = new LongInteger(0) { _parts = new List<int>(new int[a._maxRadix + 2]) };

            int i, carry = 0;
            for (i = 0; i <= a._maxRadix || carry > 0; i++)
            {
                int current = carry + (i < a._parts.Count ? a._parts[i] * b : 0);

                carry = current / BASE;
                res._parts[i] = current - carry * BASE;
            }

            res.TrailZeroes();

            return res;
        }

        public static implicit operator LongInteger(int i)
        {
            return new LongInteger(i);
        }

        private static int CompareTo(LongInteger a, LongInteger b)
        {
            if (a._sign == Negative && b._sign == Positive)
                return -1;
            else if (a._sign == Positive && b._sign == Negative)
                return 1;
            else if (a._sign == Positive)
                return CompareParts(a, b);
            else
                return Invert(CompareParts(a, b));
        }

        private static int Invert(int x)
        {
            if (x == 0)
                return 0;
            else if (x < 0)
                return 1;
            else
                return -1;
        }

        public int CompareTo(LongInteger other)
        {
            return CompareTo(this, other);
        }

        public bool Equals(LongInteger x) { return CompareTo(this, x) == 0; }
        public static bool operator <(LongInteger x, LongInteger y) { return CompareTo(x, y) < 0; }
        public static bool operator >(LongInteger x, LongInteger y) { return CompareTo(x, y) > 0; }
        public static bool operator <=(LongInteger x, LongInteger y) { return CompareTo(x, y) <= 0; }
        public static bool operator >=(LongInteger x, LongInteger y) { return CompareTo(x, y) >= 0; }
        public static bool operator ==(LongInteger x, LongInteger y) { return CompareTo(x, y) == 0; }
        public static bool operator !=(LongInteger x, LongInteger y) { return CompareTo(x, y) != 0; }
        public override bool Equals(object obj)
        {
            return (obj is LongInteger) && (CompareTo(this, (LongInteger)obj) == 0);
        }

        public override string ToString()
        {
            string s = (_sign < 0 ? "-" : "") + _parts[_maxRadix];

            for (int i = 1; i <= _maxRadix; i++)
            {
                s += _parts[_maxRadix - i].ToString().PadLeft(DIGITS_IN_PART, '0');
            }

            return s;
        }

        private void TrailZeroes()
        {
            for (int i = _parts.Count - 1; i >= 0; i--)
            {
                if (_parts[i] == 0 && i != 0)
                {
                    _parts.RemoveAt(i);
                }
                else
                {
                    _maxRadix = i;
                    return;
                }
            }
        }

        public override int GetHashCode()
        {
            return _parts.Aggregate(0, (current, part) => current ^ (part.GetHashCode() ^ 0xBADF00D));
        }
    }
}
