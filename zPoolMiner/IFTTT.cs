using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using zPoolMiner.Configs;

namespace zPoolMiner
{
    internal class IFTTT
    {
        private const string apiUrl = "https://maker.ifttt.com/trigger/";

        public static void PostToIFTTT(string action, string msg)
        {
            try
            {
                string key = ConfigManager.GeneralConfig.IFTTTKey;
                string worker = ConfigManager.GeneralConfig.WorkerName;
                string minProfit = ConfigManager.GeneralConfig.MinimumProfit.ToString("F2").Replace(',', '.');

                using (WebClient client = new WebClient())
                {
                    var postData = new NameValueCollection();
                    postData["value1"] = worker;
                    postData["value2"] = msg;
                    postData["value3"] = minProfit;

                    var response = client.UploadValues(apiUrl + action + "/with/key/" + key, postData);

                    var responseString = Encoding.Default.GetString(response);
                }
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint("NICEHASH", ex.Message);
            }
        }
    }
}