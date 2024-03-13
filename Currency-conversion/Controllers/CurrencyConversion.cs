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
        private readonly ICurrencyConversion _currencyConversion;

        public CurrencyConversion(ICurrencyConversion currencyConversion)
        {
            _currencyConversion = currencyConversion;
        }

        [HttpGet]
        [Route("convert")]
        public IActionResult Currency_Conversion(string sourceCurrency, string targetCurrency, decimal amount)
        {
            decimal finalAmount = 0;
            try
            {
                string json = "";
                //Reading the Json file
                using (StreamReader r = new StreamReader("exchangeRates.json"))
                {
                    json = r.ReadToEnd();
                }
                if(json ==null)
                {
                    LogMessage("No content in the exchangeRates json");
                    throw  new NullReferenceException("No rates in json");
                }    
                
                var res = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(json);

                //Checking for null value for input parameter
                if (sourceCurrency != null && targetCurrency != null && amount != 0)
                {
                    //Creating new string to match the currency conversion pattern
                    string currencyPattern = sourceCurrency + "_" + "TO" + "_" + targetCurrency;

                    if (res != null)
                    {
                        var exchRate = res.Where(a => a.Key.Contains(currencyPattern)).Select(a => a.Value);
                        if(!exchRate.Any())
                        {
                            throw new ArgumentException("Exchange rates not available");
                        }
                        LogMessage($"Exchange rate: {exchRate.First()}");

                        //Method for setting env value
                        //Environment.SetEnvironmentVariable("env", "USD_TO_INR : 81.00");

                        //Method to get Enviroment value
                        var env = Environment.GetEnvironmentVariable("env");
                        if (env != null)
                        {
                            LogMessage("New Exchange rates found in environment");
                            var overRideRate = env.Contains(currencyPattern);
                            if (overRideRate)
                            {
                                //Regex to get the decimal value for env string
                                var envrate = Regex.Split(env, @"[^0-9\.]+").Where(c => c != "." && c.Trim() != "").FirstOrDefault();
                                if(envrate != null)
                                { 
                                    //calculating final amount
                                    finalAmount = amount * decimal.Parse(envrate, CultureInfo.InvariantCulture);
                                    LogMessage($"OverRideRate: {finalAmount}");
                                    return Ok(finalAmount);
                                }
                                else
                                {
                                    throw new NullReferenceException("Enviroment Value cannot be null");
                                }
                            }
                        }
                        else
                        {
                            finalAmount = amount * exchRate.First();
                            LogMessage($"Final Amount: {finalAmount}");
                            return Ok(finalAmount);
                        }
                    }
                    else
                    {
                        throw new NullReferenceException();
                    }
                }
                else
                {
                    throw new NullReferenceException("sourceCurrency or targetCurrency or amount cannnot be null");
                }
            }
            catch(NullReferenceException ex)
            {
                LogMessage(ex.Message);
                return this.BadRequest(ex.Message);
            }
            catch(ArgumentException ex)
            {
                LogMessage(ex.Message);
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
                return this.BadRequest(ex.Message);
            }
            return StatusCode(404, "Not a valid request.");
        }

        private static void LogMessage(string message)
        {
            try 
            {
                Directory.CreateDirectory("logs");
                System.IO.File.AppendAllText("logs\\" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt", DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss") + " : " + message + Environment.NewLine);
            } 
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }
    }
}
