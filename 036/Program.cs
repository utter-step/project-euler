using System.Text;

using Tools;

namespace _36
{
    class Program
    {
        private const int LIMIT = 1000000;

        private static bool IsPalindromic(string s)
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

        private static string ToBinary(int num)
        {
            var sb = new StringBuilder();

            while (num > 0)
            {
                sb.Append(num & 1);
                num >>= 1;
            }

            return sb.ToString();
        }

        private static int SumOfBipalindromic(int upperLimit)
        {
            int res = 0;

            for (int i = 1; i <= upperLimit; i += 2)
            {
                if (IsPalindromic(i.ToString()) && IsPalindromic(ToBinary(i)))
                {
                    res += i;
                }
            }

            return res;
        }

        static void Main(string[] args)
        {
            Decorators.TimeItAccurate(SumOfBipalindromic, LIMIT, 10);
        }
    }
}
