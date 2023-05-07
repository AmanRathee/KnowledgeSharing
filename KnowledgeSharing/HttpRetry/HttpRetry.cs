using System;
using System.Threading.Tasks;

public static class RetryHelper
{
    public static async Task RetryOnExceptionAsync(int retryTimes, TimeSpan retryDelay, Func<Task> operation)
    {
        for (int i = 0; i < retryTimes; i++)
        {
            try
            {
                await operation();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Transient failure occurred, retrying in {retryDelay.TotalSeconds} seconds ({i + 1}/{retryTimes}): {ex.Message}");
                await Task.Delay(retryDelay);
            }
        }
        throw new Exception($"Failed after {retryTimes} retries.");
    }
}
