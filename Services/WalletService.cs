using MyWalletAPI.Models;
using MyWalletAPI.Repositories;

namespace MyWalletAPI.Services;

public interface IWalletService<T> where T : Currency
{
    void AddTransaction(decimal amount);
    decimal GetBalance();
}

public class WalletService<T>(IWalletRepository<T> repository) : IWalletService<T> where T : Currency
{
    private readonly IWalletRepository<T> _repository = repository;

    public void AddTransaction(decimal amount)
    {
        string value = Math.Round(amount, 2).ToString();
        _repository.Create(value);
    }

    public decimal GetBalance()
    {
        return _repository
            .ReadAll()
            .Select(transaction => Convert.ToDecimal(transaction))
            .Sum();
    }
}