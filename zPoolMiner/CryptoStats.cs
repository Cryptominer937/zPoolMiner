namespace zPoolMiner
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using WebSocketSharp;
    using zPoolMiner.Configs;
    using zPoolMiner.Devices;
    using zPoolMiner.Enums;
    using zPoolMiner.Miners;

    /// <summary>
    /// Defines the <see cref="SocketEventArgs" />
    /// </summary>
    public class SocketEventArgs : EventArgs
    {
        /// <summary>
        /// Defines the Message
        /// </summary>
        public string Message = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketEventArgs"/> class.
        /// </summary>
        /// <param name="message">The <see cref="string"/></param>
        public SocketEventArgs(string message)
        {
            Message = message;
        }
    }

    /// <summary>
    /// Defines the <see cref="CryptoStats" />
    /// </summary>
    internal class CryptoStats
    {
#pragma warning disable 649
        /// <summary>
        /// Defines the <see cref="Nicehash_login" />
        /// </summary>
        private class Nicehash_login
        {
            /// <summary>
            /// Defines the method
            /// </summary>
            public string method = "login";

            /// <summary>
            /// Defines the version
            /// </summary>
            public string version;
            
            /// <summary>
            /// Defines the protocol
            /// </summary>
            public int protocol = 1;
        }
        class github_version
        {
            public string tag_name;
            public string target_commitish;
        }
        /// <summary>
        /// Defines the <see cref="Nicehash_credentials" />
        /// </summary>
        private class Nicehash_credentials
        {
            /// <summary>
            /// Defines the method
            /// </summary>
            public string method = "credentials.set";

            /// <summary>
            /// Defines the btc
            /// </summary>
            public string btc;

            /// <summary>
            /// Defines the worker
            /// </summary>
            public string worker;
        }

        /// <summary>
        /// Defines the <see cref="Nicehash_device_status" />
        /// </summary>
        private class Nicehash_device_status
        {
            /// <summary>
            /// Defines the method
            /// </summary>
            public string method = "devices.status";

            /// <summary>
            /// Defines the devices
            /// </summary>
            public List<JArray> devices;
        }

#pragma warning restore 649
#pragma warning restore 649        /// <summary>
        /// Defines the deviceUpdateLaunchDelay
        /// </summary>
        private const int deviceUpdateLaunchDelay = 20 * 1000;

        /// <summary>
        /// Defines the deviceUpdateInterval
        /// </summary>
        private const int deviceUpdateInterval = 60 * 1000;

        /// <summary>
        /// Gets or sets the AlgorithmRates
        /// </summary>
        public static Dictionary<AlgorithmType, CryptoMiner937API> AlgorithmRates { get; private set; }

        /// <summary>
        /// Defines the CryptoMiner937Data
        /// </summary>
        private static CryptoMiner937Data CryptoMiner937Data;

        /// <summary>
        /// Gets or sets the Balance
        /// </summary>
        public static double Balance { get; private set; }

        /// <summary>
        /// Gets or sets the Version
        /// </summary>
        public static string Version { get; private set; }

        /// <summary>
        /// Gets a value indicating whether IsAlive
        /// </summary>
        public static bool IsAlive
        {
            get { return NiceHashConnection.IsAlive; }
        }

        // Event handlers for socket
        /// <summary>
        /// Defines the OnBalanceUpdate
        /// </summary>
        public static event EventHandler OnBalanceUpdate = delegate { };

        /// <summary>
        /// Defines the OnSMAUpdate
        /// </summary>
        public static event EventHandler OnSMAUpdate = delegate { };

        /// <summary>
        /// Defines the OnVersionUpdate
        /// </summary>
        public static event EventHandler OnVersionUpdate;

        /// <summary>
        /// Defines the OnConnectionLost
        /// </summary>
        public static event EventHandler OnConnectionLost = delegate { };

        /// <summary>
        /// Defines the OnConnectionEstablished
        /// </summary>
        public static event EventHandler OnConnectionEstablished = delegate { };

        /// <summary>
        /// Defines the OnVersionBurn
        /// </summary>
        public static event EventHandler<SocketEventArgs> OnVersionBurn = delegate { };

        /// <summary>
        /// Defines the random
        /// </summary>
        private static readonly Random random = new Random();

        /// <summary>
        /// Defines the deviceUpdateTimer
        /// </summary>
        private static System.Threading.Timer deviceUpdateTimer;

        /// <summary>
        /// Defines the algoRatesUpdateTimer
        /// </summary>
        private static System.Threading.Timer algoRatesUpdateTimer;

        /// <summary>
        /// Defines the <see cref="NiceHashConnection" />
        /// </summary>
        private class NiceHashConnection
        {
            /// <summary>
            /// Defines the webSocket
            /// </summary>
            private static WebSocket webSocket;

            /// <summary>
            /// Gets a value indicating whether IsAlive
            /// </summary>
            public static bool IsAlive
            {
                get { return webSocket.IsAlive; }
            }

            /// <summary>
            /// Defines the attemptingReconnect
            /// </summary>
            private static bool attemptingReconnect = false;

            /// <summary>
            /// Defines the connectionAttempted
            /// </summary>
            private static bool connectionAttempted = false;

            /// <summary>
            /// Defines the connectionEstablished
            /// </summary>
            private static bool connectionEstablished = false;

            /// <summary>
            /// The StartConnection
            /// </summary>
            /// <param name="address">The <see cref="string"/></param>
            public static void StartConnection(string address)
            {
                UpdateAlgoRates(null);
                algoRatesUpdateTimer = new System.Threading.Timer(UpdateAlgoRates, null, deviceUpdateInterval, deviceUpdateInterval);
            }

            /// <summary>
            /// The UpdateAlgoRates
            /// </summary>
            /// <param name="state">The <see cref="object"/></param>
            private static void UpdateAlgoRates(object state)
            {

                try
                {
                    var zpool = "";
                    if (ConfigManager.GeneralConfig.zpoolenabled == true) { zpool = "1"; }
                    var ahash = "";
                    if (ConfigManager.GeneralConfig.ahashenabled == true) { ahash = ",2"; }
                    var hashrefinery = "";
                    if (ConfigManager.GeneralConfig.hashrefineryenabled == true) { hashrefinery = ",3"; }
                    var nicehash = "";
                    if (ConfigManager.GeneralConfig.nicehashenabled == true) { nicehash = ",4"; }
                    var zerg = "";
                    if (ConfigManager.GeneralConfig.zergenabled == true) { zerg = ",5"; }
                    var minemoney = "";
                    if (ConfigManager.GeneralConfig.minemoneyenabled == true) { minemoney = ",6"; }
                    var starpool = "";
                    if (ConfigManager.GeneralConfig.starpoolenabled == true) { starpool = ",7"; }
                    var blockmunch = "";
                    if (ConfigManager.GeneralConfig.blockmunchenabled == true) { blockmunch = ",8"; }
                    var blazepool = "";
                    if (ConfigManager.GeneralConfig.blazepoolenabled == true) { blazepool = ",9"; }
                    var MPH = "";
                    if (ConfigManager.GeneralConfig.MPHenabled == true) { MPH = ",10"; }

                    var averaging = (ConfigManager.GeneralConfig.Averaging);
                    var devapion = "Live API";
                    var url = String.Format("http://api.zergpool.com:8080/api/status");
                    if (ConfigManager.GeneralConfig.devapi == true)
                    {
                        url = String.Format("http://localhost");
                        devapion = "Dev API";
                    }
                    // We get the algo payment info here - http://www.zpool.ca/api/status - But we use Crypto's API below
                    //Helpers.ConsolePrint("API Data URL", url);
                    //Helpers.ConsolePrint("API URL", devapion);
                    var WR = (HttpWebRequest)WebRequest.Create(url);
                    WR.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                    WR.UserAgent = "MultiPoolMiner V" + Application.ProductVersion;
                    var Response = WR.GetResponse();
                    var SS = Response.GetResponseStream();
                    SS.ReadTimeout = 20 * 1000;
                    var Reader = new StreamReader(SS);
                    var ResponseFromServer = Reader.ReadToEnd().Trim().ToLower();
                    if (ResponseFromServer.Length == 0 || ResponseFromServer[0] != '{')
                        throw new Exception("Not JSON!");
                    Reader.Close();
                    Response.Close();

                    var zData = JsonConvert.DeserializeObject<Dictionary<string, ZPoolAlgo>>(ResponseFromServer);
                    ZSetAlgorithmRates(zData.Values.ToArray());

                    for (int h = 0; h < 24; h += 3)
                    {

                        var timeFrom1 = new TimeSpan(h, 00, 0);
                        var timeTo1 = new TimeSpan(h, 01, 30);
                        var timeNow = DateTime.Now.TimeOfDay;
                        // Helpers.ConsolePrint("SOCKET", "Received10: ");
                        if (timeNow > timeFrom1 && timeNow < timeTo1)
                        {
                            //Helpers.ConsolePrint("GITHUB", "Check new version");
                            try
                            {
                                string ghv = GetVersion("");
                                //Helpers.ConsolePrint("GITHUB", ghv);
                                SetVersion(ghv);
                            }
                            catch (Exception er)
                            {
                                //Helpers.ConsolePrint("GITHUB", er.ToString());
                            }
                        }
                        //Debugging Enable
                        //Helpers.ConsolePrint("API Data", ResponseFromServer);
                    }
                }
                catch (Exception e)
                {
                    int x = 0;
                }
            }

            /// <summary>
            /// The ConnectCallback
            /// </summary>
            /// <param name="sender">The <see cref="object"/></param>
            /// <param name="e">The <see cref="EventArgs"/></param>
            private static void ConnectCallback(object sender, EventArgs e)
            {
                try
                {
                    if (AlgorithmRates == null || CryptoMiner937Data == null)
                    {
                        CryptoMiner937Data = new CryptoMiner937Data();
                        AlgorithmRates = CryptoMiner937Data.NormalizedSMA();
                    }
                    //send login
                    var version = "NHML/" + Application.ProductVersion;
                    var login = new Nicehash_login
                    {
                        version = version
                    };
                    var loginJson = JsonConvert.SerializeObject(login);
                    SendData(loginJson);

                    DeviceStatus_Tick(null);  // Send device to populate rig stats

                    OnConnectionEstablished.Emit(null, EventArgs.Empty);
                }
                catch (Exception er)
                {
                    Helpers.ConsolePrint("SOCKET", er.ToString());
                }
            }

            /// <summary>
            /// The ReceiveCallback
            /// </summary>
            /// <param name="sender">The <see cref="object"/></param>
            /// <param name="e">The <see cref="MessageEventArgs"/></param>
            private static void ReceiveCallback(object sender, MessageEventArgs e)
            {
                try
                {
                    if (e.IsText)
                    {
                        Helpers.ConsolePrint("SOCKET", "Received: " + e.Data);
                        dynamic message = JsonConvert.DeserializeObject(e.Data);
                        if (message.method == "sma")
                        {
                            SetAlgorithmRates(message.data);
                        }
                        else if (message.method == "balance")
                        {
                            SetBalance(message.value.Value);
                        }
                        else if (message.method == "versions")
                        {
                            SetVersion(message.legacy.Value);
                        }
                        else if (message.method == "burn")
                        {
                            OnVersionBurn.Emit(null, new SocketEventArgs(message.message.Value));
                        }
                    }
                }
                catch (Exception er)
                {
                    Helpers.ConsolePrint("SOCKET", er.ToString());
                }
            }

            /// <summary>
            /// The ErrorCallback
            /// </summary>
            /// <param name="sender">The <see cref="object"/></param>
            /// <param name="e">The <see cref="WebSocketSharp.ErrorEventArgs"/></param>
            private static void ErrorCallback(object sender, WebSocketSharp.ErrorEventArgs e)
            {
                Helpers.ConsolePrint("SOCKET", e.ToString());
            }

            /// <summary>
            /// The CloseCallback
            /// </summary>
            /// <param name="sender">The <see cref="object"/></param>
            /// <param name="e">The <see cref="CloseEventArgs"/></param>
            private static void CloseCallback(object sender, CloseEventArgs e)
            {
                Helpers.ConsolePrint("SOCKET", $"Connection closed code {e.Code}: {e.Reason}");
                AttemptReconnect();
            }

            // Don't call SendData on UI threads, since it will block the thread for a bit if a reconnect is needed
            /// <summary>
            /// The SendData
            /// </summary>
            /// <param name="data">The <see cref="string"/></param>
            /// <param name="recurs">The <see cref="bool"/></param>
            /// <returns>The <see cref="bool"/></returns>
            public static bool SendData(string data, bool recurs = false)
            {
                try
                {
                    if (webSocket != null && webSocket.IsAlive)
                    {  // Make sure connection is open
                        // Verify valid JSON and method
                        dynamic dataJson = JsonConvert.DeserializeObject(data);
                        if (dataJson.method == "credentials.set" || dataJson.method == "devices.status" || dataJson.method == "login")
                        {
                            Helpers.ConsolePrint("SOCKET", "Sending data: " + data);
                            webSocket.Send(data);
                            return true;
                        }
                    }
                    else if (webSocket != null)
                    {
                        if (AttemptReconnect() && !recurs)
                        {  // Reconnect was successful, send data again (safety to prevent recursion overload)
                            SendData(data, true);
                        }
                        else
                        {
                            Helpers.ConsolePrint("SOCKET", "Socket connection unsuccessfull, will try again on next device update (1min)");
                        }
                    }
                    else
                    {
                        if (!connectionAttempted)
                        {
                            Helpers.ConsolePrint("SOCKET", "Data sending attempted before socket initialization");
                        }
                        else
                        {
                            Helpers.ConsolePrint("SOCKET", "webSocket not created, retrying");
                            StartConnection(Links.NHM_Socket_Address);
                        }
                    }
                }
                catch (Exception e)
                {
                    Helpers.ConsolePrint("SOCKET", e.ToString());
                }
                return false;
            }

            /// <summary>
            /// The AttemptReconnect
            /// </summary>
            /// <returns>The <see cref="bool"/></returns>
            private static bool AttemptReconnect()
            {
                if (attemptingReconnect)
                {
                    return false;
                }
                if (webSocket.IsAlive)
                {  // no reconnect needed
                    return true;
                }
                attemptingReconnect = true;
                var sleep = connectionEstablished ? 10 + random.Next(0, 20) : 0;
                Helpers.ConsolePrint("SOCKET", "Attempting reconnect in " + sleep + " seconds");
                // More retries on first attempt
                var retries = connectionEstablished ? 5 : 25;
                if (connectionEstablished)
                {  // Don't wait if no connection yet
                    Thread.Sleep(sleep * 1000);
                }
                else
                {
                    // Don't not wait again
                    connectionEstablished = true;
                }
                for (int i = 0; i < retries; i++)
                {
                    webSocket.Connect();
                    Thread.Sleep(100);
                    if (webSocket.IsAlive)
                    {
                        attemptingReconnect = false;
                        return true;
                    }
                    Thread.Sleep(1000);
                }
                attemptingReconnect = false;
                OnConnectionLost.Emit(null, EventArgs.Empty);
                return false;
            }
        }

        /// <summary>
        /// The StartConnection
        /// </summary>
        /// <param name="address">The <see cref="string"/></param>
        public static void StartConnection(string address)
        {
            NiceHashConnection.StartConnection(address);
            deviceUpdateTimer = new System.Threading.Timer(DeviceStatus_Tick, null, deviceUpdateInterval, deviceUpdateInterval);
        }

        /// <summary>
        /// The SetAlgorithmRates
        /// </summary>
        /// <param name="data">The <see cref="JArray"/></param>
        private static void SetAlgorithmRates(JArray data)
        {
            try
            {
                foreach (var algo in data)
                {
                    var algoKey = (AlgorithmType)algo[0].Value<int>();
                    CryptoMiner937Data.AppendPayingForAlgo(algoKey, algo[1].Value<double>());
                }
                AlgorithmRates = CryptoMiner937Data.NormalizedSMA();
                OnSMAUpdate.Emit(null, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Helpers.ConsolePrint("SOCKET", e.ToString());
            }
        }

        /// <summary>
        /// The ZSetAlgorithmRates
        /// </summary>
        /// <param name="data">The <see cref="ZPoolAlgo[]"/></param>
        private static void ZSetAlgorithmRates(ZPoolAlgo[] data)
        {
            try
            {
                if (CryptoMiner937Data == null) CryptoMiner937Data = new CryptoMiner937Data(data);
                foreach (var algo in data)
                {
                    CryptoMiner937Data.AppendPayingForAlgo((AlgorithmType)algo.NiceHashAlgoId(), (double)algo.MidPoint24HrEstimate);
                }
                AlgorithmRates = CryptoMiner937Data.NormalizedSMA();
                OnSMAUpdate.Emit(null, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Helpers.ConsolePrint("SOCKET", e.ToString());
            }
        }

        /// <summary>
        /// The SetBalance
        /// </summary>
        /// <param name="balance">The <see cref="string"/></param>
        private static void SetBalance(string balance)
        {
            try
            {
                double.TryParse(balance, NumberStyles.Number, CultureInfo.InvariantCulture, out double bal);
                Balance = bal;
                OnBalanceUpdate.Emit(null, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Helpers.ConsolePrint("SOCKET", e.ToString());
            }
        }

        /// <summary>
        /// The SetVersion
        /// </summary>
        /// <param name="version">The <see cref="string"/></param>
        internal static void SetVersion(string version)
        {
            Version = version;
            OnVersionUpdate.Emit(null, EventArgs.Empty);
        }

        /// <summary>
        /// The SetCredentials
        /// </summary>
        /// <param name="btc">The <see cref="string"/></param>
        /// <param name="worker">The <see cref="string"/></param>
        public static void SetCredentials(string btc, string worker)
        {
            var data = new Nicehash_credentials
            {
                btc = btc,
                worker = worker
            };
            if (BitcoinAddress.ValidateBitcoinAddress(data.btc) && BitcoinAddress.ValidateWorkerName(worker))
            {
                var sendData = JsonConvert.SerializeObject(data);

                // Send as task since SetCredentials is called from UI threads
                Task.Factory.StartNew(() => NiceHashConnection.SendData(sendData));
            }
        }

        /// <summary>
        /// The DeviceStatus_Tick
        /// </summary>
        /// <param name="state">The <see cref="object"/></param>
        public static void DeviceStatus_Tick(object state)
        {
            var devices = ComputeDeviceManager.Avaliable.AllAvaliableDevices;
            var deviceList = new List<JArray>();
            var activeIDs = MinersManager.GetActiveMinersIndexes();
            foreach (var device in devices)
            {
                try
                {
                    var array = new JArray
                    {
                        device.Index,
                        device.Name
                    };
                    int status = Convert.ToInt32(activeIDs.Contains(device.Index)) + (((int)device.DeviceType + 1) * 2);
                    array.Add(status);
                    array.Add((uint)device.Load);
                    array.Add((uint)device.Temp);
                    array.Add((uint)device.FanSpeed);

                    deviceList.Add(array);
                }
                catch (Exception e) { Helpers.ConsolePrint("SOCKET", e.ToString()); }
            }
            var data = new Nicehash_device_status
            {
                devices = deviceList
            };
            var sendData = JsonConvert.SerializeObject(data);
        }
        public static string GetVersion(string worker)
        {
            string url = "https://api.github.com/repos/Cryptominer937/zPoolMiner/releases";
            string r1 = GetGitHubAPIData(url);
            //Helpers.ConsolePrint("GITHUB!", r1);
            //string r1 = GetNiceHashApiData(url, "");
            if (r1 == null) return null;
            github_version[] nhjson;
            try
            {
                nhjson = JsonConvert.DeserializeObject<github_version[]>(r1, Globals.JsonSettings);
                var latest = Array.Find(nhjson, (n) => n.target_commitish == "master");
                return latest.tag_name;
            }
            catch
            { }
            return "";
        }

        public static string GetGitHubAPIData(string URL)
        {
            string ResponseFromServer;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpWebRequest WR = (HttpWebRequest)WebRequest.Create(URL);
                WR.UserAgent = "NiceHashMinerLegacy/" + Application.ProductVersion;
                WR.Timeout = 10 * 1000;
                WR.Credentials = CredentialCache.DefaultCredentials;
                //idHTTP1.IOHandler:= IdSSLIOHandlerSocket1;
                // ServicePointManager.SecurityProtocol = (SecurityProtocolType)SslProtocols.Tls12;
                Thread.Sleep(200);
                WebResponse Response = WR.GetResponse();
                Stream SS = Response.GetResponseStream();
                SS.ReadTimeout = 5 * 1000;
                StreamReader Reader = new StreamReader(SS);
                ResponseFromServer = Reader.ReadToEnd();
                if (ResponseFromServer.Length == 0 || (ResponseFromServer[0] != '{' && ResponseFromServer[0] != '['))
                    throw new Exception("Not JSON!");
                Reader.Close();
                Response.Close();

            }
            catch (Exception ex)
            {
                //Helpers.ConsolePrint("GITHUB", ex.Message);
                return null;
            }

            return ResponseFromServer;
        }
        /// <summary>
        /// The GetCryptominerAPIData
        /// </summary>
        /// <param name="URL">The <see cref="string"/></param>
        /// <param name="worker">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string GetCryptominerAPIData(string URL, string worker)
        {
            string ResponseFromServer;
            try
            {
                string ActiveMinersGroup = MinersManager.GetActiveMinersGroup();

                HttpWebRequest WR = (HttpWebRequest)WebRequest.Create(URL);
                WR.UserAgent = "MultiPoolMiner V" + Application.ProductVersion;
                if (worker.Length > 64) worker = worker.Substring(0, 64);
                WR.Headers.Add("NiceHash-Worker-ID", worker);
                WR.Headers.Add("NHM-Active-Miners-Group", ActiveMinersGroup);
                WR.Timeout = 10 * 1000;
                WebResponse Response = WR.GetResponse();
                Stream SS = Response.GetResponseStream();
                SS.ReadTimeout = 5 * 1000;
                StreamReader Reader = new StreamReader(SS);
                ResponseFromServer = Reader.ReadToEnd();
                if (ResponseFromServer.Length == 0 || ResponseFromServer[0] != '{')
                    throw new Exception("Not JSON!");
                Reader.Close();
                Response.Close();
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint("CryptoMiner937", ex.Message);
                return null;
            }

            return ResponseFromServer;

        }
    }

    /// <summary>
    /// Defines the <see cref="ZPoolAlgo" />
    /// </summary>
    public class ZPoolAlgo
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Port
        /// </summary>
        public int Port { get; set; }

        public string Url { get; set; }

        public string Pool { get; set; }

        //public decimal coins { get; set; }
        //public decimal fees { get; set; }
        //public decimal hashrate { get; set; }
        //public int workers { get; set; }

        //public decimal coins { get; set; }
        //public decimal fees { get; set; }
        //public decimal hashrate { get; set; }
        //public int workers { get; set; }
        /// <summary>
        /// Gets or sets the Estimate_current
        /// </summary>
        public decimal Estimate_current { get; set; }

        /// <summary>
        /// Gets or sets the Estimate_last24h
        /// </summary>
        public decimal Estimate_last24h { get; set; }

        /// <summary>
        /// Gets or sets the Actual_last24h
        /// </summary>
        public decimal Actual_last24h { get; set; }

        /// <summary>
        /// Gets the NormalizedEstimate
        /// </summary>
        public decimal NormalizedEstimate => MagnitudeFactor(Name) * Estimate_current;

        /// <summary>
        /// Gets the Normalized24HrEstimate
        /// </summary>
        public decimal Normalized24HrEstimate => MagnitudeFactor(Name) * Estimate_current;

        /// <summary>
        /// Gets the Normalized24HrActual
        /// </summary>
        public decimal Normalized24HrActual => MagnitudeFactor(Name) * Estimate_current;

        /// <summary>
        /// Gets the MidPoint24HrEstimate
        /// </summary>
        public decimal MidPoint24HrEstimate => MagnitudeFactor(Name) * Estimate_current;

        // if the normalized estimate (now) is 20% less than the midpoint, we want to return the
        // normalized estimate

        // if the normalized estimate (now) is 20% less than the midpoint, we want to return the
        // normalized estimate
        /// <summary>
        /// Gets the Safe24HrEstimate
        /// </summary>
        public decimal Safe24HrEstimate => NormalizedEstimate * 1.2m < MidPoint24HrEstimate
            ? NormalizedEstimate
            : MidPoint24HrEstimate;

        //public decimal hashrate_last24h { get; set; }
        //public decimal rental_current { get; set; }

        //public decimal hashrate_last24h { get; set; }
        //public decimal rental_current { get; set; }
        /// <summary>
        /// Gets the Algorithm
        /// </summary>
        public ZAlgorithm Algorithm => ToAlgorithm(Name);

        /// <summary>
        /// The ToAlgorithm
        /// </summary>
        /// <param name="s">The <see cref="string"/></param>
        /// <returns>The <see cref="ZAlgorithm"/></returns>
        private ZAlgorithm ToAlgorithm(string s)
        {
            switch (s.ToLower())
            {
                case "bitcore": return ZAlgorithm.bitcore;
                case "blake2s": return ZAlgorithm.blake2s;
                case "blakecoin": return ZAlgorithm.blake256r8;
                case "c11": return ZAlgorithm.c11;
                case "cryptonight": return ZAlgorithm.cryptonight;
                case "equihash": return ZAlgorithm.equihash;
                case "groestl": return ZAlgorithm.groestl;
                //case "hsr": return ZAlgorithm.hsr;
                case "keccak": return ZAlgorithm.keccak;
                case "lbry": return ZAlgorithm.lbry;
                case "lyra2v2": return ZAlgorithm.lyra2v2;
                case "myr-gr": return ZAlgorithm.myriad_groestl;
                case "neoscrypt": return ZAlgorithm.neoscrypt;
                case "nist5": return ZAlgorithm.nist5;
                case "phi": return ZAlgorithm.phi;
                case "polytimos": return ZAlgorithm.polytimos;
                case "quark": return ZAlgorithm.quark;
                case "qubit": return ZAlgorithm.qubit;
                case "scrypt": return ZAlgorithm.scrypt;
                case "sha256": return ZAlgorithm.sha256;
                case "sib": return ZAlgorithm.sib;
                case "skein": return ZAlgorithm.skein;
                case "skunk": return ZAlgorithm.skunk;
                case "timetravel": return ZAlgorithm.timetravel;
                //case "tribus": return ZAlgorithm.tribus;
                case "veltor": return ZAlgorithm.veltor;
                case "x11": return ZAlgorithm.x11;
                case "x11evo": return ZAlgorithm.x11evo;
                case "x13": return ZAlgorithm.x13;
                case "x14": return ZAlgorithm.x14;
                //case "x17": return ZAlgorithm.x17;
                //case "xevan": return ZAlgorithm.xevan;
                case "yescrypt": return ZAlgorithm.yescrypt;
                //case "m7m": return ZAlgorithm.m7m;
                case "daggerhashimoto": return ZAlgorithm.daggerhashimoto;
                case "lyra2z": return ZAlgorithm.lyra2z;
                //case "hmq1725": return ZAlgorithm.hmq1725;
                case "yescryptr16": return ZAlgorithm.yescryptr16;
                case "sia": return ZAlgorithm.sia;
                case "decred": return ZAlgorithm.decred;
                case "pascal": return ZAlgorithm.pascal;
                //case "keccakc": return ZAlgorithm.keccakc;
                case "sha256t": return ZAlgorithm.sha256t;
                case "cryptonightv7": return ZAlgorithm.cryptonightv7;
                case "x16r": return ZAlgorithm.x16r;
                case "randomxmonero": return ZAlgorithm.randomxmonero;
                //case "randomarq": return ZAlgorithm.randomarq;
                case "randomx": return ZAlgorithm.randomx;
                //case "randomsfx": return ZAlgorithm.randomsfx;
                //case "cryptonight_heavy": return ZAlgorithm.cryptonight_heavy ;
                case "cryptonight_heavyx": return ZAlgorithm.cryptonight_heavyx;
                //case "cryptonight_saber": return ZAlgorithm.cryptonight_saber;
                //case "cryptonight_fast": return ZAlgorithm.cryptonight_fast;
                case "cryptonight_haven": return ZAlgorithm.cryptonight_haven;
                case "cryptonight_upx": return ZAlgorithm.cryptonight_upx;
                case "yespower": return ZAlgorithm.yespower;
                case "cpupower": return ZAlgorithm.cpupower;
                case "power2b": return ZAlgorithm.power2b;
                //case "yescryptr8g": return ZAlgorithm.yescryptr8g;
                //case "yespoweriots": return ZAlgorithm.yespoweriots;
                //case "chukwa": return ZAlgorithm.chukwa;
                case "yescryptr32": return ZAlgorithm.yescryptr32;
                //case "x16s": return ZAlgorithm.x16s;
                //case "sonoa": return ZAlgorithm.sonoa;
                case "bcd": return ZAlgorithm.bcd;
                ///case "phi2": return ZAlgorithm.phi2;
                case "hex": return ZAlgorithm.hex;
                case "allium": return ZAlgorithm.allium;
                case "cryptonight_gpu": return ZAlgorithm.cryptonight_gpu;
                //case "cryptonight_xeq": return ZAlgorithm.cryptonight_xeq;
                //case "cryptonight_conceal": return ZAlgorithm.cryptonight_conceal;
                //case "lyra2v3": return ZAlgorithm.lyra2v3;
                //case "equihash96": return ZAlgorithm.equihash96;
                case "equihash125": return ZAlgorithm.equihash125;
                case "equihash144": return ZAlgorithm.equihash144;
                case "equihash192": return ZAlgorithm.equihash192;
                case "scryptn2": return ZAlgorithm.scryptn2;
                case "karlsenhash": return ZAlgorithm.karlsenhash;
            }

            return ZAlgorithm.unknown;
        }

        /// <summary>
        /// The NiceHashAlgoId
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        public int NiceHashAlgoId()
        {
            switch (Name)
            {
                case "bitcore": return 0;
                case "blake2s": return 1;
                case "blakecoin": return 2;
                case "c11": return 3;
                case "cryptonight": return 33;
                case "equihash": return 4;
                case "groestl": return 5;
                //case "hsr": return 6;
                case "keccak": return 7;
                case "lbry": return 8;
                case "lyra2v2": return 9;
                case "myr-gr": return 10;
                case "neoscrypt": return 11;
                case "nist5": return 12;
                case "phi": return 13;
                case "polytimos": return 14;
                case "quark": return 15;
                case "qubit": return 16;
                case "scrypt": return 17;
                case "sha256": return 18;
                case "sib": return 19;
                case "skein": return 20;
                case "skunk": return 21;
                case "timetravel": return 22;
                //case "tribus": return 23;
                case "veltor": return 24;
                case "x11": return 25;
                case "x11evo": return 26;
                case "x13": return 27;
                case "x14": return 28;
                //case "x17": return 29;
                //case "xevan": return 30;
                case "yescrypt": return 31;
                //case "m7m": return 32;
                case "daggerhashimoto": return 35;
                case "lyra2z": return 36;
                //case "hmq1725": return 37;
                case "yescryptr16": return 38;
                case "sia": return 39;
                case "decred": return 40;
                case "pascal": return 41;
                //case "keccakc": return 42;
                case "sha256t": return 43;
                case "cryptonightv7": return 44;
                case "x16r": return 45;
                case "randomxmonero": return 46;
                //case "randomarq": return 47;
                case "randomx": return 48;
                //case "randomsfx": return 49;
                //case "cryptonight_heavy": return 50;
                case "cryptonight_heavyx": return 51;
                //case "cryptonight_saber": return 52;
                //case "cryptonight_fast": return 53;
                case "cryptonight_haven": return 54;
                case "cryptonight_upx": return 55;
                case "yespower": return 56;
                case "cpupower": return 57;
                case "power2b": return 58;
                //case "yescryptr8g": return 59;
                //case "yespoweriots": return 60;
                //case "chukwa": return 61;
                case "yescryptr32": return 62;
                //case "x16s": return 63;
                //case "sonoa": return 64;
                case "bcd": return 65;
                ///case "phi2": return 66;
                case "hex": return 67;
                case "allium": return 68;
                //case "lyra2v3": return 69;
                case "cryptonight_gpu": return 70;
                //case "cryptonight_xeq": return 71;
                //case "cryptonight_conceal": return 72;
                case "equihash144": return 73;
                case "equihash125": return 74;
                case "equihash192": return 75;
                //case "equihash96": return 76;
                case "scryptn2": return 77;
                case "karlsenhash": return 78;

                default: return -1;
            }
        }

        /// <summary>
        /// The MagnitudeFactor
        /// </summary>
        /// <param name="s">The <see cref="string"/></param>
        /// <returns>The <see cref="decimal"/></returns>
        private decimal MagnitudeFactor(string s)
        {
            switch (s)
            {


                //PH Below
                case "sha256":
                    return 0.000001M; //end PH
                //TH Below
                case "lbry":
                case "keccak":
                case "blake2s":
                    return 0.001M; //end TH
                //GH Below
                case "decred":
                case "blakecoin":
                //case "keccakc":
                case "pascal":
                case "scrypt":
                case "x11":
                case "quark":
                case "qubit":
                case "sha256t":
                case "lyra2z":
                case "dedal":
                case "groestl":
                case "hex":
                case "honeycomb":
                case "k12":
                case "lyra2v2":
                //case "lyra2v3":
                case "myr-gr":
                case "nist5":
                case "odocrypt":
                case "phi":
                case "sha3d":
                case "sib":
                case "skein":
                //case "tribus":
                case "veil":
                case "verushash":
                case "x13":
                case "x14":
                case "x15":
                //case "x17":
                case "bitcore":
                case "skunk":
                case "c11":
                case "bcd":
                    return 1; //end GH
                //MH Below
                //case "hmq1725":
                case "jeonghash":
                //case "m7m":
                case "mtp":
                case "neoscrypt":
                case "padihash":
                case "pawelhash":
                ///case "phi2":
                case "progpow":
                //case "sonoa":
                case "x12":
                case "x16r":
                case "x16rt":
                case "x16rv2":
                //case "x16s":
                case "x21s":
                case "x22i":
                case "x25x":
                //case "xevan":
                case "allium":
                //case "equihash96":
                case "cryptonight_upx":
                    return 1000; // end MH
                //KH Below
                case "karlsenhash":
                case "yescrypt":
                case "yescryptr16":
                case "randomx":
                //case "randomarq":
                //case "randomsfx":
                case "cryptonight_haven":
                //case "cryptonight_conceal":
                //case "cryptonight_fast":
                //case "cryptonight_saber":
                case "cryptonight_heavyx":
                //case "cryptonight_heavy":
                case "cryptonight_gpu":
                //case "cryptonight_xeq":
                case "yespower":
                case "cpupower":
                case "power2b":
                case "yespoweriots":
                //case "yescryptr8g":
                case "yescryptr32":
                case "argon2d4096":
                case "equihash":
                case "equihash125":
                case "equihash144":
                case "equihash192":
                case "lyra2z330":
                case "yescryptR8":
                case "yespowerR16":
                case "scryptn2":

                    return 1000000; //end KH

                default: return 1000;
            }
        }

        /// <summary>
        /// The Min
        /// </summary>
        /// <param name="values">The <see cref="decimal[]"/></param>
        /// <returns>The <see cref="decimal"/></returns>
        private decimal Min(params decimal[] values) =>
            values.Length == 1
                ? values[0]
                : values.Length == 2
                    ? Math.Min(values[0], values[1])
                    : Min(values[0], Min(values.Skip(1).ToArray()));

        /// <summary>
        /// The Max
        /// </summary>
        /// <param name="values">The <see cref="decimal[]"/></param>
        /// <returns>The <see cref="decimal"/></returns>
        private decimal Max(params decimal[] values) =>
            values.Length == 1
                ? values[0]
                : values.Length == 2
                    ? Math.Max(values[0], values[1])
                    : Max(values[0], Min(values.Skip(1).ToArray()));
    }

    /// <summary>
    /// Defines the ZAlgorithm
    /// </summary>
    public enum ZAlgorithm
    {
        /// <summary>
        /// Defines the bitcore
        /// </summary>
        bitcore,

        /// <summary>
        /// Defines the blake2s
        /// </summary>
        blake2s,

        /// <summary>
        /// Defines the blake256r8
        /// </summary>
        blake256r8,

        /// <summary>
        /// Defines the c11
        /// </summary>
        c11,
        cryptonight,

        /// <summary>
        /// Defines the equihash
        /// </summary>
        equihash,

        /// <summary>
        /// Defines the groestl
        /// </summary>
        groestl,

        /// <summary>
        /// Defines the hsr
        /// </summary>
        //hsr,

        /// <summary>
        /// Defines the keccak
        /// </summary>
        keccak,

        /// <summary>
        /// Defines the lbry
        /// </summary>
        lbry,

        /// <summary>
        /// Defines the lyra2v2
        /// </summary>
        lyra2v2,

        /// <summary>
        /// Defines the myriad_groestl
        /// </summary>
        myriad_groestl,

        /// <summary>
        /// Defines the neoscrypt
        /// </summary>
        neoscrypt,

        /// <summary>
        /// Defines the nist5
        /// </summary>
        nist5,

        /// <summary>
        /// Defines the phi
        /// </summary>
        phi,

        /// <summary>
        /// Defines the polytimos
        /// </summary>
        polytimos,

        /// <summary>
        /// Defines the quark
        /// </summary>
        quark,

        /// <summary>
        /// Defines the qubit
        /// </summary>
        qubit,

        /// <summary>
        /// Defines the scrypt
        /// </summary>
        scrypt,

        /// <summary>
        /// Defines the sha256
        /// </summary>
        sha256,

        /// <summary>
        /// Defines the sib
        /// </summary>
        sib,

        /// <summary>
        /// Defines the skein
        /// </summary>
        skein,

        /// <summary>
        /// Defines the skunk
        /// </summary>
        skunk,

        /// <summary>
        /// Defines the timetravel
        /// </summary>
        timetravel,

        /// <summary>
        /// Defines the tribus
        /// </summary>
        //tribus,

        /// <summary>
        /// Defines the veltor
        /// </summary>
        veltor,

        /// <summary>
        /// Defines the x11
        /// </summary>
        x11,

        /// <summary>
        /// Defines the x11evo
        /// </summary>
        x11evo,

        /// <summary>
        /// Defines the x13
        /// </summary>
        x13,

        /// <summary>
        /// Defines the x14
        /// </summary>
        x14,

        /// <summary>
        /// Defines the x17
        /// </summary>
        //x17,

        /// <summary>
        /// Defines the xevan
        /// </summary>
        //xevan,

        /// <summary>
        /// Defines the unknown
        /// </summary>
        unknown,

        /// <summary>
        /// Defines the yescrypt
        /// </summary>
        yescrypt,        //hmq1725,
        //m7m,

        daggerhashimoto,
        lyra2z,
        //hmq1725,
        yescryptr16,
        sia,
        decred,
        pascal,
        keccakc,
        sha256t,
        cryptonightv7,
        x16r,
        randomxmonero,
        //randomarq,
        randomx,
        //randomsfx,
        //cryptonight_heavy,
        cryptonight_heavyx,
        //cryptonight_saber,
        //cryptonight_fast,
        cryptonight_haven,
        cryptonight_upx,
        yespower,
        cpupower,
        power2b,
        //yescryptr8g,
        //yespoweriots,
        //chukwa,
        yescryptr32,
        //x16s,
        //sonoa,
        bcd,
        //phi2,
        hex,
        allium,
        cryptonight_gpu,
        //cryptonight_xeq,
        //cryptonight_conceal,
        //lyra2v3,
        //equihash96,
        equihash125,
        equihash144,
        equihash192,
        scryptn2,
        karlsenhash

    }
}
