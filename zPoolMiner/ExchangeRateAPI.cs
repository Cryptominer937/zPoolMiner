namespace zPoolMiner
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using zPoolMiner.Configs;

    /// <summary>
    /// Defines the <see cref="ExchangeRateAPI" />
    /// </summary>
    internal class ExchangeRateAPI
    {
        /// <summary>
        /// Defines the <see cref="Result" />
        /// </summary>
        public class Result
        {
            /// <summary>
            /// Gets or sets the algorithms
            /// </summary>
            public Object algorithms { get; set; }

            /// <summary>
            /// Gets or sets the servers
            /// </summary>
            public Object servers { get; set; }

            /// <summary>
            /// Gets or sets the idealratios
            /// </summary>
            public Object idealratios { get; set; }

            /// <summary>
            /// Gets or sets the exchanges
            /// </summary>
            public List<Dictionary<string, string>> exchanges { get; set; }

            /// <summary>
            /// Gets or sets the exchanges_fiat
            /// </summary>
            public Dictionary<string, double> exchanges_fiat { get; set; }
        }

        /// <summary>
        /// Defines the <see cref="ExchangeRateJSON" />
        /// </summary>
        public class ExchangeRateJSON
        {
            /// <summary>
            /// Gets or sets the result
            /// </summary>
            public Result result { get; set; }

            /// <summary>
            /// Gets or sets the method
            /// </summary>
            public string method { get; set; }
        }

        /// <summary>
        /// Defines the apiUrl
        /// </summary>
        internal const string apiUrl = "https://api2.nicehash.com/main/api/v2/exchangeRate/list/";

        /// <summary>
        /// Defines the exchanges_fiat
        /// </summary>
        private static Dictionary<string, double> exchanges_fiat = null;

        /// <summary>
        /// Defines the USD_BTC_rate
        /// </summary>
        private static double USD_BTC_rate = -1;

        /// <summary>
        /// Defines the ActiveDisplayCurrency
        /// </summary>
        public static string ActiveDisplayCurrency = "USD";

        /// <summary>
        /// Gets a value indicating whether ConverterActive
        /// </summary>
        private static bool ConverterActive
        {
            get { return ConfigManager.GeneralConfig.DisplayCurrency != "USD"; }
        }

        /// <summary>
        /// The ConvertToActiveCurrency
        /// </summary>
        /// <param name="amount">The <see cref="double"/></param>
        /// <returns>The <see cref="double"/></returns>
        public static double ConvertToActiveCurrency(double amount)
        {
            if (!ConverterActive)
            {
                return amount;
            }

            // if we are still null after an update something went wrong. just use USD hopefully itll update next tick
            if (exchanges_fiat == null || ActiveDisplayCurrency == "USD")
            {
                // Moved logging to update for berevity 
                return amount;
            }

            //Helpers.ConsolePrint("CurrencyConverter", "Current Currency: " + ConfigManager.Instance.GeneralConfig.DisplayCurrency);
            double usdExchangeRate = 1.0;
            if (exchanges_fiat.TryGetValue(ActiveDisplayCurrency, out usdExchangeRate))
                return amount * usdExchangeRate;
            else
            {
                Helpers.ConsolePrint("CurrencyConverter", "Unknown Currency Tag: " + ActiveDisplayCurrency + " falling back to USD rates");
                ActiveDisplayCurrency = "USD";
                return amount;
            }
        }

        /// <summary>
        /// The GetUSDExchangeRate
        /// </summary>
        /// <returns>The <see cref="double"/></returns>
        public static double GetUSDExchangeRate()
        {
            if (USD_BTC_rate > 0)
            {
                return USD_BTC_rate;
            }
            return 0.0;
        }

        /// <summary>
        /// The UpdateAPI
        /// </summary>
        /// <param name="worker">The <see cref="string"/></param>
        public static void UpdateAPI(string worker)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12
                       | SecurityProtocolType.Ssl3;
                var WR = (HttpWebRequest)WebRequest.Create("https://blockchain.info/ticker");
                var Response = WR.GetResponse();
                var SS = Response.GetResponseStream();
                SS.ReadTimeout = 20 * 1000;
                var Reader = new StreamReader(SS);
                var ResponseFromServer = Reader.ReadToEnd();
                if (ResponseFromServer.Length == 0 || ResponseFromServer[0] != '{')
                    throw new Exception("Not JSON!");
                Reader.Close();
                Response.Close();

                dynamic fiat_rates = JObject.Parse(ResponseFromServer);
                try
                {
                    //USD_BTC_rate = Helpers.ParseDouble((string)fiat_rates[ConfigManager.GeneralConfig.DisplayCurrency]["last"]);
                    USD_BTC_rate = Helpers.ParseDouble((string)fiat_rates["USD"]["last"]);

                    exchanges_fiat = new Dictionary<string, double>();
                    foreach (var c in _supportedCurrencies)
                        exchanges_fiat.Add(c, Helpers.ParseDouble((string)fiat_rates[c]["last"]) / USD_BTC_rate);
                }
                catch (Exception)
                {
                    Helpers.ConsolePrint("CurrencyAPI", "Currency update failed will retry on next cycle");
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = reader.ReadToEnd();
                            //TODO: use JSON.net to parse this string and look at the error message
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Defines the _supportedCurrencies
        /// </summary>
        private static readonly string[] _supportedCurrencies = {
            "AUD",
            "BRL",
            "CAD",
            "CHF",
            "CLP",
            "CNY",
            "DKK",
            "EUR",
            "GBP",
            "HKD",
            "INR",
            "ISK",
            "JPY",
            "KRW",
            "NZD",
            "PLN",
            "RUB",
            "SEK",
            "SGD",
            "THB",
            "TWD",
            "USD"
        };
    }
}
