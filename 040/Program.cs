using System.Text;

namespace _040
{
    class Program
    {
        private static string ChampernownesConstant(int minLength)
        {
            var sb = new StringBuilder(minLength);

            for (int i = 1; sb.Length < minLength; i++)
            {
                sb.Append(i);
            }

            return sb.ToString();
        }

        private static int Solve(int upperLimit)
        {
            string fractionDigits = ChampernownesConstant(upperLimit);

            int res = 1;

            for (int i = 1; i <= upperLimit; i *= 10)
            {
                res *= fractionDigits[i - 1] - '0';
            }
            return res;
        }

        static void Main(string[] args)
        {
            Tools.Decorators.Benchmark(Solve, 1000000);
        }
    }
}
