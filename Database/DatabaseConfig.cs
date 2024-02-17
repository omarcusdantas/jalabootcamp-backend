using StackExchange.Redis;

namespace MyWalletAPI.Database;

public class DatabaseConfig
{
    private static IConfiguration Configuration { get; set; }

    public static ConnectionMultiplexer? RedisConnection { get; private set; }

    public static void Initialize(IConfiguration configuration)
    {
        Configuration = configuration;

        try
        {
            string redisConnectionString = Configuration["ConnectionString"];
            RedisConnection = ConnectionMultiplexer.Connect(redisConnectionString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing Redis connection: {ex.Message}");
            throw;
        }
    }
}