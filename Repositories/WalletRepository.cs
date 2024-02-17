using MyWalletAPI.Database;
using MyWalletAPI.Models;
using StackExchange.Redis;

namespace MyWalletAPI.Repositories;

public interface IWalletRepository<T> where T : Currency
{
    void Create(string value);
    RedisValue[] ReadAll();
}

public class WalletRepository<T>(): IWalletRepository<T> where T : Currency
{
    private readonly IDatabase _redisDatabase = DatabaseConfig.RedisConnection.GetDatabase();
    private readonly string key = $"{typeof(T).Name}WalletTransactions";

    public void Create(string value)
    {
        _redisDatabase.ListRightPush(key, value);
    }

    public RedisValue[] ReadAll()
    {
        return _redisDatabase.ListRange(key);
    }
}
