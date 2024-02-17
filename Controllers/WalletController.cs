using Microsoft.AspNetCore.Mvc;
using MyWalletAPI.Services;
using MyWalletAPI.Models;

namespace MyWalletAPI.Controllers;

public class WalletController<T>(IWalletService<T> service) : ControllerBase where T : Currency
{
    private readonly IWalletService<T> _walletService = service;

    [HttpGet]
    public IActionResult GetBalance()
    {
        return Ok(new BalanceBody(_walletService.GetBalance()));
    }

    [HttpPost]
    public IActionResult AddFunds([FromBody] TransactionBody transactionBody)
    {
        if (transactionBody.Amount <= 0) return BadRequest("Amount must be greater than 0");

        _walletService.AddTransaction(transactionBody.Amount);
        return CreatedAtAction(nameof(AddFunds), "", new BalanceBody(_walletService.GetBalance()));
    }
}

[ApiController]
[Route("[controller]/[Action]")]
public class EuroWalletController(IWalletService<Euro> service) : WalletController<Euro>(service)
{
}

[ApiController]
[Route("[controller]/[Action]")]
public class DollarWalletController(IWalletService<Dollar> service) : WalletController<Dollar>(service)
{
}

public class TransactionBody
{
    public decimal Amount { get; set; }
}

public class BalanceBody(decimal Balance)
{
    public decimal Balance { get; set; } = Balance;
}