using System.Collections.Generic;

namespace Tools
{
    public class NumberAsFactorization
    {
        private readonly Dictionary<int, int> _factorization;
        private readonly Dictionary<int, int> _initialFactorization;

        public NumberAsFactorization(int number)
        {
            _factorization = NumUtils.ComputePrimeFactorization(number);
            _initialFactorization = new Dictionary<int, int>(_factorization);
        }

        private NumberAsFactorization(Dictionary<int, int> factorization, Dictionary<int, int> initialFactorization)
        {
            _factorization = factorization;
            _initialFactorization = initialFactorization;
        }

        public NumberAsFactorization PowerUp()
        {
            var newFactorization = new Dictionary<int, int>(_factorization);
            foreach (var factor in _factorization.Keys)
            {
                newFactorization[factor] += _initialFactorization[factor];
            }
            return new NumberAsFactorization(newFactorization, _initialFactorization);
        }

        public IReadOnlyDictionary<int, int> Factorization
        {
            get { return _factorization; }
        }

        public override bool Equals(object obj)
        {
            var other = obj as NumberAsFactorization;
            foreach (var factorPower in _factorization)
            {
                int otherValue;
                if (other._factorization.TryGetValue(factorPower.Key, out otherValue))
                {
                    if (otherValue != factorPower.Value)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int result = 0xBADF00D;

            foreach (var i in _factorization)
                result = result ^ ((i.Key << 16) | i.Value);

            return result;
        }
    }
}
