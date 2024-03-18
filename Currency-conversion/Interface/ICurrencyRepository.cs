using Microsoft.AspNetCore.Mvc;

namespace Currency_conversion.Interface
{
    public interface ICurrencyRepository
    {
        IActionResult CurrencyCon(string sourceCurrency, string targetCurrency, decimal amount);
    }
}
