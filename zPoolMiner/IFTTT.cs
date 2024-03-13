namespace zPoolMiner
{
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Text;
    using zPoolMiner.Configs;

    /// <summary>
    /// Defines the <see cref="IFTTT" />
    /// </summary>
    internal class IFTTT
    {
        /// <summary>
        /// Defines the apiUrl
        /// </summary>
        private const string apiUrl = "https://maker.ifttt.com/trigger/";

        /// <summary>
        /// The PostToIFTTT
        /// </summary>
        /// <param name="action">The <see cref="string"/></param>
        /// <param name="msg">The <see cref="string"/></param>
        public static void PostToIFTTT(string action, string msg)
        {
            try
            {
                string key = ConfigManager.GeneralConfig.IFTTTKey;
                string worker = ConfigManager.GeneralConfig.WorkerName;
                string minProfit = ConfigManager.GeneralConfig.MinimumProfit.ToString("F2").Replace(',', '.');

                using (WebClient client = new WebClient())
                {
                    var postData = new NameValueCollection
                    {
                        ["value1"] = worker,
                        ["value2"] = msg,
                        ["value3"] = minProfit
                    };

                    var response = client.UploadValues(apiUrl + action + "/with/key/" + key, postData);

                    var responseString = Encoding.Default.GetString(response);
                }
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint("CryptoMiner937", ex.Message);
            }
        }
    }
}
