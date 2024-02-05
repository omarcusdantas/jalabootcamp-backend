using MyWalletAPI.Models;
using MyWalletAPI.Repositories;

namespace MyWalletAPI.Services;

public interface IExchangeService<T, TTarget> where T : Currency where TTarget : Currency
{
    void ExchangeFunds(decimal amount);
}

public class Exchange<T,TTarget>(IWalletRepository<T> repository, IWalletRepository<TTarget> repositoryTarget) 
    : IExchangeService<T, TTarget> where T : Currency where TTarget : Currency
{
    private readonly IWalletRepository<T> _repository = repository;
    private readonly IWalletRepository<TTarget> _repositoryTarget = repositoryTarget;

    public void ExchangeFunds(decimal amount)
    {
        if (amount <= GetBalanceOriginWallet()) return;
        
        _repository.Create(Math.Round(amount,2).ToString());

        Converter converter = new();

        decimal value = converter.ConvertCurrency(Math.Round(amount, 2), typeof(T).Name, typeof(TTarget).Name);

        _repositoryTarget.Create(value.ToString());
    }

    public decimal GetBalanceOriginWallet()
    {
        return _repository
            .ReadAll()
            .Select(transaction => Convert.ToDecimal(transaction))
            .Sum();
    }
}
