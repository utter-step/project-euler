using System;
using System.Diagnostics;

namespace Tools
{
    public static class Decorators
    {
        public static void Benchmark<TIn, TResult>(Func<TIn, TResult> func, TIn argument)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            #region Pre-heat
            for (int i = 0; i < 3; i++)
            {
                func.Invoke(argument);
                if (stopwatch.ElapsedMilliseconds > 10000)
                {
                    break;
                } 
            }
            #endregion
            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds / 3 > 1000)
            {
                var result = func.Invoke(argument);
                Console.WriteLine("Result:\n{2}({3}) == {0}\n\nComputed in: {1} ms.", result,
                    stopwatch.ElapsedMilliseconds / 3,
                    func.Method.Name,
                    argument);
                return;
            }
            TimeItAccurate(func, argument, 1000 / (((int)stopwatch.ElapsedMilliseconds / 3) > 0 ?
                (int)stopwatch.ElapsedMilliseconds / 3 : 1));
        }

        public static void Benchmark<TIn>(Func<TIn> func)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            #region Pre-heat
            for (int i = 0; i < 3; i++)
            {
                func.Invoke();
            }
            #endregion
            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds / 3 > 1000)
            {
                var result = func.Invoke();
                Console.WriteLine("Result:\n{2}() == {0}\n\nComputed in: {1} ms.", result,
                    stopwatch.ElapsedMilliseconds / 3,
                    func.Method.Name);
                return;
            }
            TimeItAccurate(func, 1000 / (((int)stopwatch.ElapsedMilliseconds / 3) > 0 ?
                (int)stopwatch.ElapsedMilliseconds / 3 : 1));
        }

        public static void TimeIt<TIn, TResult>(Func<TIn, TResult> func, TIn argument)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var result = func.Invoke(argument);
            stopwatch.Stop();

            Console.WriteLine("Result:\n{2}({3}) == {0}\n\nComputed in: {1} ms.", result, 
                stopwatch.ElapsedMilliseconds,
                func.Method.Name,
                argument);
        }

        public static void TimeIt<T>(Func<T> func)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var result = func.Invoke();
            stopwatch.Stop();

            Console.WriteLine("Result:\n{2}() == {0}\n\nComputed in: {1} ms.", 
                result, 
                stopwatch.ElapsedMilliseconds, 
                func.Method.Name);
        }

        public static void TimeItAccurate<T>(Func<T> func, int iterationsCount)
        {
            #region Pre-heat
            for (int i = 0; i < 10; i++)
            {
                func.Invoke();
            }
            #endregion

            var stopwatch = new Stopwatch();
            var result = default(T);

            stopwatch.Start();
            for (int i = 0; i < iterationsCount; i++)
            {
                result = func.Invoke();
            }
            stopwatch.Stop();

            Console.WriteLine("Result:\n{4}() == {0}\n\n{1} iterations done in {2} ms.\nAverage time: {3:f5} ms.",
                result,
                iterationsCount,
                stopwatch.ElapsedMilliseconds,
                stopwatch.ElapsedMilliseconds / (double)iterationsCount,
                func.Method.Name);
        }

        public static void TimeItAccurate<TIn, TResult>(Func<TIn, TResult> func, TIn argument, int iterationsCount)
        {
            #region Pre-heat
            for (int i = 0; i < 10; i++)
            {
                func.Invoke(argument);
            }
            #endregion

            var stopwatch = new Stopwatch();
            var result = default(TResult);

            stopwatch.Start();
            for (int i = 0; i < iterationsCount; i++)
            {
                result = func.Invoke(argument);
            }
            stopwatch.Stop();

            Console.WriteLine("Result:\n{4}({5}) == {0}\n\n{1} iterations done in {2} ms.\nAverage time: {3:f5} ms.",
                result,
                iterationsCount,
                stopwatch.ElapsedMilliseconds,
                stopwatch.ElapsedMilliseconds / (double)iterationsCount,
                func.Method.Name,
                argument);
        }
    }
}
