using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using System.Runtime.Serialization;

namespace Tools
{
    [Serializable]
    public class Fraction : IEquatable<Fraction>, ISerializable
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GCD(int a, int b)
        {
            if (a % b == 0)
            {
                return b;
            }

            bool aIsEven = (a & 1) == 0;
            bool bIsEven = (b & 1) == 0;

            if (aIsEven && bIsEven)
            {
                return 2 * GCD(a >> 1, b >> 1);
            }
            else if (aIsEven)
            {
                return GCD(a >> 1, b);
            }
            else if (bIsEven)
            {
                return GCD(a, b >> 1);
            }
            else if (a > b)
            {
                return GCD((a - b) >> 1, b);
            }
            else
            {
                return GCD((b - a) >> 1, a);
            }
        }

        private int[] _decimalDigits;
        private int _cycleStart;

        private readonly int _originalNumerator;
        private readonly int _originalDenominator;

        public int IntegerPart { get; private set; }
        public int Numerator { get; private set; }
        public int Denominator { get; private set; }
        public int PeriodLength
        {
            get
            {
                return _decimalDigits.Length - _cycleStart;
            }
        }

        public Fraction(int numerator, int denominator)
        {
            _originalDenominator = denominator;
            _originalNumerator = numerator;

            SetProps(numerator, denominator);
        }

        private void SetProps(int numerator, int denominator)
        {
            IntegerPart = numerator / denominator;
            numerator -= denominator * IntegerPart;

            int gcd = numerator < 2 ? 1 : GCD(numerator, denominator);

            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
            ComputeDecimalForm();
        }

        private Fraction(SerializationInfo info, StreamingContext context)
        {
            _originalDenominator = info.GetInt32("denominator");
            _originalNumerator = info.GetInt32("numerator");

            SetProps(_originalNumerator, _originalDenominator);
        }

        private void ComputeDecimalForm()
        {
            var numerators = new HashSet<int>();
            var decimalDigits = new List<int>();
            int numerator = Numerator;
            int denominator = Denominator;
            
            while (numerator % denominator != 0)
            {
                if (numerator < denominator)
                {
                    numerator *= 10;
                    while (numerator < denominator)
                    {
                        numerators.Add(numerator);
                        decimalDigits.Add(0);
                        numerator *= 10;
                    }
                }

                if (!numerators.Add(numerator))
                {
                    int cycleStart = 0;

                    foreach (var item in numerators)
                    {
                        if (numerator == item)
                        {
                            break;
                        }
                        cycleStart++;
                    }

                    _cycleStart = cycleStart;
                    _decimalDigits = decimalDigits.ToArray();
                    return;
                }

                decimalDigits.Add(numerator / denominator);
                numerator = numerator % denominator;
            }
            if (numerator > 0) { decimalDigits.Add(numerator / denominator); }

            _decimalDigits = decimalDigits.ToArray();
            _cycleStart = _decimalDigits.Length;
        }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder(_decimalDigits.Length + 4);
            sb.Append(IntegerPart);

            if (_decimalDigits.Length > 0)
            {
                sb.Append('.');
                for (int i = 0; i < _cycleStart; i++)
                {
                    sb.Append(_decimalDigits[i]);
                }

                if (_cycleStart < _decimalDigits.Length)
                {
                    sb.Append('(');
                    for (int i = _cycleStart; i < _decimalDigits.Length; i++)
                    {
                        sb.Append(_decimalDigits[i]);
                    }
                    sb.Append(')');
                } 
            }

            return String.Format("{0}/{1} == {2}", Numerator + IntegerPart * Denominator, Denominator, sb);
        }

        public bool Equals(Fraction other)
        {
            return IntegerPart == other.IntegerPart &&
                   Numerator   == other.Numerator &&
                   Denominator == other.Denominator;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals(obj as Fraction);
        }

        public override int GetHashCode()
        {
            return _originalDenominator.GetHashCode() ^ _originalNumerator.GetHashCode();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("numerator", _originalNumerator);
            info.AddValue("denominator", _originalDenominator);
        }

        public static bool operator ==(Fraction f1, Fraction f2)
        {
            return f1.Equals(f2);
        }

        public static bool operator !=(Fraction f1, Fraction f2)
        {
            return !f1.Equals(f2);
        }

        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            int lcm = (f1.Denominator * f2.Denominator) / GCD(f1.Denominator, f2.Denominator);

            int numerator = f1.Numerator * (lcm / f1.Denominator) +
                            f2.Numerator * (lcm / f2.Denominator) + 
                            f1.IntegerPart * lcm +
                            f2.IntegerPart * lcm;

            return new Fraction(numerator, lcm);
        }

        public static Fraction operator *(Fraction f1, Fraction f2)
        {
            return new Fraction(f1._originalNumerator * f2._originalNumerator,
                                f1._originalDenominator * f2._originalDenominator);
        }
    }
}
