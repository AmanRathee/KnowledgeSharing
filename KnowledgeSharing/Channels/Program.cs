using System.Threading.Channels;
class Program
{
    static async Task Main(string[] args)
    {
        var channel = Channel.CreateUnbounded<string>();
        var producerConsumer = new MessageProcessor(channel);
        // Start the consumer tasks. 
        var consumerTask1 = Task.Run(() => producerConsumer.ConsumeAsync());
        var consumerTask2 = Task.Run(() => producerConsumer.ConsumeAsync());

        // Read messages from the console and produce them to the channel
        //In real, an API endpoint will produce the message.
        Console.WriteLine("Enter new message to process:");
        while (true)
        {
            var message = Console.ReadLine();
            if (string.IsNullOrEmpty(message) || message == "exit") break;
            await producerConsumer.ProduceAsync(message);
        }
        // Complete the channel when done producing messages
        channel.Writer.Complete();
        // Wait for the consumer to finish consuming all messages
        await consumerTask1; await consumerTask2;
    }
}

class MessageProcessor
{
    private readonly Channel<string> _channel;
    public MessageProcessor(Channel<string> channel)
    {
        _channel = channel;
    }
    public async Task ProduceAsync(string message)
    {
        await _channel.Writer.WriteAsync(message);
    }
    public async Task ConsumeAsync()
    {
        await foreach (var message in _channel.Reader.ReadAllAsync())
        {
            //Act on the consumed message
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Consumed message: {message}");
            Console.ResetColor();
        }
    }
}

