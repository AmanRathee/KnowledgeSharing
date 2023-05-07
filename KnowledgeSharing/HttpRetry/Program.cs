class Program
{
    static async Task Main(string[] args)
    {
        await RetryHelper.RetryOnExceptionAsync(3, TimeSpan.FromSeconds(1), async () =>
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync("https://example11111.com/api/endpoint");
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }
        });
    }
}