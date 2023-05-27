class Program
{
    static readonly CancellationTokenSource cts = new CancellationTokenSource();
    static async Task Main(string[] args)
    {
        Console.WriteLine("Execution started.");
        try
        {
            cts.CancelAfter(1000);
            await OperationAsync(cts.Token);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("\nTasks cancelled: timed out.\n");
        }
        finally
        {
            cts.Dispose();
        }
        Console.WriteLine("Execution ending.");
    }
    private static async Task OperationAsync(CancellationToken token)
    {
        await Task.Delay(4000, token);
    }
}