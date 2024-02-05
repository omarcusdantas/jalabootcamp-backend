namespace MyWalletAPI.Models;

public class Converter
{
    public decimal ConvertCurrency(decimal amount, string sourceCurrency, string targetCurrency)
    {
        if (sourceCurrency == nameof(Euro) && targetCurrency == nameof(Dollar))
        {
            return Math.Round(amount * 1.1m, 2);
        }

        if (sourceCurrency == nameof(Dollar) && targetCurrency == nameof(Euro))
        {
            return Math.Round(amount / 1.1m, 2);
        }

        return amount;
    }
}
