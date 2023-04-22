using InterlockedDemo;

class Program
{
    const int numberOfIteration = 50000000;
    static async Task Main(string[] args)
    {
        DemoUsingLock.IncrementUsingLock(numberOfIteration);
        DemoUsingInterlocked.IncrementUsingInterlocked(numberOfIteration);
    }
}