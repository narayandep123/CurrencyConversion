using Currency_conversion.Controllers;
using Currency_conversion.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestCurrencyConversion
{
    public class CurrencyTest
    {

        [Fact]
        public void SuccessFullConversion()
        {
            var mockRepository = new Mock<ICurrencyConversion>();
            var currencyConversion = new CurrencyConversion(mockRepository.Object);
            var data = currencyConversion.Currency_Conversion("USD", "INR", 10);
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void ErrorConversionForZeroAmount()
        {
            var mockRepository = new Mock<ICurrencyConversion>();
            var currencyConversion = new CurrencyConversion(mockRepository.Object);
            var data = currencyConversion.Currency_Conversion("USD", "INR",0);
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public void ErrorConversionForOtherThenUSDINREUR()
        {
            var mockRepository = new Mock<ICurrencyConversion>();
            var currencyConversion = new CurrencyConversion(mockRepository.Object);
            var data = currencyConversion.Currency_Conversion("AUS", "INR", 10);
            Assert.IsType<BadRequestObjectResult>(data);
        }
    }
}