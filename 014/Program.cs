using Tools;

namespace _014
{
    class Program
    {
        private static int[] _lengthCache;

        private static int CollatzLength(int num)
        {
            if (_lengthCache[num] > 0)
            {
                return _lengthCache[num];
            }

            if ((num & 1) == 0)
            {
                // this lines is never exuted, but removing them SLOWS DOWN Mono
                // INVESTIGATE!!!
                _lengthCache[num] = _lengthCache[num / 2] + 1;
                return _lengthCache[num];
            }

            long curNum = num;
            int res = 1;

            while (curNum > 1)
            {
                if ((curNum & 1) == 0)
                {
                    curNum >>= 1;
                } else
                {
                    curNum += (curNum << 1) + 1;
                }

                res++;
            }

            int original_res = res;

            while (num <= _lengthCache.Length)
            {
                _lengthCache[num] = res++;
                num <<= 1;
            }

            return original_res;
        }

        private static int LongestCollatz(int upperLimit)
        {
            _lengthCache = new int[upperLimit + 1];

            int t = 1;
            for (int i = 2; i <= upperLimit + 1; i <<= 1)
            {
                _lengthCache[i] = t++;
            }

            int max = 0;
            int maxNum = 0;

            for (int i = 2; i < upperLimit; i++)
            {
                int length = CollatzLength(i);

                if (length > max)
                {
                    max = length;
                    maxNum = i;
                }
            }

            return maxNum;
        }

        static void Main(string[] args)
        {
            Decorators.Benchmark(LongestCollatz, 1000000);
        }
    }
}
