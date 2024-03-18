
using Currency_conversion.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestCurrencyConversion
{
    public class CurrencyTest
    {

        [Fact]
        public void SuccessFullConversion()
        {
            //var mockRepository = new Mock<ICurrencyRepository>();
            //var currencyConversion = new CurrencyRepository(mockRepository.Object);
            var currencyConversion = new CurrencyRepository();
            var data = currencyConversion.CurrencyCon("USD", "INR", 10);
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void ErrorConversionForZeroAmount()
        {
            var currencyConversion = new CurrencyRepository();
            var data = currencyConversion.CurrencyCon("USD", "INR", 0);
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public void ErrorConversionForOtherThenUSDINREUR()
        {
            var currencyConversion = new CurrencyRepository();
            var data = currencyConversion.CurrencyCon("AUS", "INR", 10);
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public void ErrorConversionForNullValue()
        {
            var currencyConversion = new CurrencyRepository();
            var data = currencyConversion.CurrencyCon("","",0);
            Assert.IsType<BadRequestObjectResult>(data);
        }
    }
}