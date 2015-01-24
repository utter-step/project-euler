using System;
using System.Collections.Concurrent;
using System.Linq;

using Cudafy;
using Cudafy.Host;
using Cudafy.Translator;

namespace _417
{
    class Program
    {
        private static readonly ConcurrentDictionary<int, int> _cache = new ConcurrentDictionary<int, int>(); 

        private static int PeriodLength_CurrentRealisation(int denom)
        {
            while (denom % 5 == 0)
            {
                denom /= 5;
            }

            while ((denom & 1) == 0)
            {
                denom >>= 1;
            }

            int res;

            if (_cache.TryGetValue(denom, out res))
            {
                return res;
            }

            int start = 10 % denom;

            for (int i = 1; i < int.MaxValue; i++)
            {
                if (start == 1)
                {
                    _cache.TryAdd(denom, i);

                    return i;
                }

                start = (start * 10) % denom;

                if (start == 0)
                {
                    _cache.TryAdd(denom, 0);

                    return 0;
                }
            }

            throw new SystemException();
        }

        [Cudafy]
        private static void PeriodLength_Сuda(int denom, int[] result)
        {
            while (denom % 5 == 0)
            {
                denom /= 5;
            }

            while ((denom & 1) == 0)
            {
                denom >>= 1;
            }

            int start = 10 % denom;
            for (int i = 1; i < int.MaxValue; i++)
            {
                if (start == 1)
                {
                    result[0] = i;
                }

                start = (start * 10) % denom;

                if (start == 0)
                {
                    result[0] = 0;
                }
            }
        }

        [Cudafy]
        private static void Add(int a, int b, int[] result)
        {
            result[0] = a + b;
        }

        static void Main(string[] args)
        {
            //Console.Write("Enter limit: ");
            //int limit = int.Parse(Console.ReadLine());

            //var nums = Enumerable.Range(3, limit - 3).AsParallel();

            //var stopwatch = new System.Diagnostics.Stopwatch();

            //stopwatch.Start();
            //var lengths = (from num in nums
            //               select PeriodLength_CurrentRealisation(num));

            //long sum = Enumerable.Aggregate<int, long>(lengths, 0, (current, length) => current + length);

            //stopwatch.Stop();

            //Console.WriteLine("{0}. {1} ms", sum, stopwatch.ElapsedMilliseconds);

            var cm = CudafyTranslator.Cudafy();
            var gpu = CudafyHost.GetDevice(CudafyModes.Target, CudafyModes.DeviceId);
            gpu.LoadModule(cm);
            
            int res;
            var resArr = gpu.Allocate<int>();

            gpu.Launch().PeriodLength_Cuda(13, resArr);
            gpu.CopyFromDevice(resArr, out res);

            Console.WriteLine(PeriodLength_CurrentRealisation(13));
            Console.WriteLine(res);
        }
    }
}
