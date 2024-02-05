using Microsoft.AspNetCore.Mvc;
using MyWalletAPI.Services;
using MyWalletAPI.Models;

namespace MyWalletAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class ExchangeController(
        IExchangeService<Dollar, Euro> dollarToEuroExchangeService,
        IExchangeService<Euro, Dollar> euroToDollarExchangeService) : ControllerBase
    {
        private readonly IExchangeService<Dollar, Euro> _dollarToEuroExchangeService = dollarToEuroExchangeService;
        private readonly IExchangeService<Euro, Dollar> _euroToDollarExchangeService = euroToDollarExchangeService;

        [HttpPost]
        [Route("{originCurrency}/{targetCurrency}")]
        public IActionResult Exchange(
            [FromRoute] string originCurrency,
            [FromRoute] string targetCurrency,
            [FromBody] TransactionBody transactionBody)
        {
            try
            {
                if (originCurrency == nameof(Dollar) && targetCurrency == nameof(Euro))
                {
                    _dollarToEuroExchangeService.ExchangeFunds(transactionBody.Amount);
                }
                else if (originCurrency == nameof(Euro) && targetCurrency == nameof(Dollar))
                {
                    _euroToDollarExchangeService.ExchangeFunds(transactionBody.Amount);
                }
                else
                {
                    return BadRequest("Unsupported currency exchange");
                }

                return CreatedAtAction(nameof(Exchange), "", "Transaction successful");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}