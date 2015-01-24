using Tools;

namespace _010
{
    class Program
    {
        static void Main(string[] args)
        {
            Decorators.TimeIt(SumOfPrimes, 2000000);

            Decorators.TimeItAccurate(SumOfPrimes, 2000000, 10);
        }

        private static long SumOfPrimes(int upperLimit)
        {
            var primes = NumUtils.EratospheneSeive(upperLimit);

            long sum = 0;

            foreach (var item in primes)
            {
                sum += item;
            }
            return sum;
        }
    }
}
