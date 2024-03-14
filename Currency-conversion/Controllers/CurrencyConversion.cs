using Currency_conversion.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Currency_conversion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyConversion : ControllerBase
    {
        // private readonly ICurrencyConversion _currencyConversion;

        //public CurrencyConversion(ICurrencyConversion currencyConversion)
        //{
        //    _currencyConversion = currencyConversion;
        //}

        private readonly ICurrencyRepository currencyConversion;

        public CurrencyConversion(ICurrencyRepository curr)
        {
           currencyConversion = curr;
        }

        [HttpGet]
        [Route("convert")]
        public IActionResult Currency_Conversion(string sourceCurrency, string targetCurrency, decimal amount)
        {
            return currencyConversion.CurrencyCon(sourceCurrency, targetCurrency, amount);
        }

        //private static void LogMessage(string message)
        //{
        //    try 
        //    {
        //        Directory.CreateDirectory("logs");
        //        System.IO.File.AppendAllText("logs\\" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt", DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss") + " : " + message + Environment.NewLine);
        //    } 
        //    catch (Exception ex)
        //    {
        //        _ = ex.Message;
        //    }
        //}
    }
}
