using Microsoft.AspNetCore.Mvc;

namespace Currency_conversion.Interface
{
    public interface ICurrencyConversion
    {
        IActionResult Currency_Conversion(string sourceCurrency, string targetCurrency, decimal amount);
    }
}
