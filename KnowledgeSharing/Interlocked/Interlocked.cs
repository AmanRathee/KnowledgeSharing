using System.Diagnostics;
namespace InterlockedDemo
{
    internal static class DemoUsingInterlocked
    {
        static int IncrementValue = 0;
        public static int IncrementUsingInterlocked(int numberOfIteration)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Parallel.For(0, numberOfIteration, number =>
            {
                System.Threading.Interlocked.Increment(ref IncrementValue);
            });
            stopWatch.Stop();
            Console.WriteLine($"Time taken using Interlocked is {stopWatch.ElapsedMilliseconds} Milliseconds");
            return IncrementValue;
        }
    }
}
