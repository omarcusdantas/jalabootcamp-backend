using StackExchange.Redis;

namespace MyWalletAPI.Database;

public class DatabaseConfig
{
    public static ConnectionMultiplexer? RedisConnection { get; private set; }

    public static void Initialize()
    {
        try
        {
            RedisConnection = ConnectionMultiplexer.Connect("localhost");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing Redis connection: {ex.Message}");
            throw;
        }
    }
}

