using System.Diagnostics;

namespace InterlockedDemo
{
    internal static class DemoUsingLock
    {
        static readonly object lockObject = new object();
        static int IncrementValue = 0;
        public static int IncrementUsingLock(int numberOfIteration)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Parallel.For(0, numberOfIteration, number =>
            {
                lock (lockObject)
                {
                    IncrementValue++;
                }
            });
            stopWatch.Stop();
            Console.WriteLine($"Time taken using lock is {stopWatch.ElapsedMilliseconds} Milliseconds");
            return IncrementValue;
        }
    }
}
