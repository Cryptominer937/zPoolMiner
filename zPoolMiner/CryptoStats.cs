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
        public static Dictionary<AlgorithmType, NiceHashSMA> AlgorithmRates { get; private set; }

        /// <summary>
        /// Defines the niceHashData
        /// </summary>
        private static NiceHashData niceHashData;

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
        public static event EventHandler OnVersionUpdate = delegate { };

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
                    // We get the algo payment info here - http://www.zpool.ca/api/status
                    var WR = (HttpWebRequest)WebRequest.Create("http://www.zpool.ca/api/status");
                    var Response = WR.GetResponse();
                    var SS = Response.GetResponseStream();
                    SS.ReadTimeout = 20 * 1000;
                    var Reader = new StreamReader(SS);
                    var ResponseFromServer = Reader.ReadToEnd().Trim();
                    if (ResponseFromServer.Length == 0 || ResponseFromServer[0] != '{')
                        throw new Exception("Not JSON!");
                    Reader.Close();
                    Response.Close();

                    var zData = JsonConvert.DeserializeObject<Dictionary<string, ZPoolAlgo>>(ResponseFromServer);
                    ZSetAlgorithmRates(zData.Values.ToArray());
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
                    if (AlgorithmRates == null || niceHashData == null)
                    {
                        niceHashData = new NiceHashData();
                        AlgorithmRates = niceHashData.NormalizedSMA();
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
                    niceHashData.AppendPayingForAlgo(algoKey, algo[1].Value<double>());
                }
                AlgorithmRates = niceHashData.NormalizedSMA();
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
                if (niceHashData == null) niceHashData = new NiceHashData(data);
                foreach (var algo in data)
                {
                    niceHashData.AppendPayingForAlgo((AlgorithmType)algo.NiceHashAlgoId(), (double)algo.MidPoint24HrEstimate);
                }
                AlgorithmRates = niceHashData.NormalizedSMA();
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
        private static void SetVersion(string version)
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
                WR.UserAgent = "zPoolMiner/" + Application.ProductVersion;
                if (worker.Length > 64) worker = worker.Substring(0, 64);
                WR.Headers.Add("NiceHash-Worker-ID", worker);
                WR.Headers.Add("NHM-Active-Miners-Group", ActiveMinersGroup);
                WR.Timeout = 30 * 1000;
                WebResponse Response = WR.GetResponse();
                Stream SS = Response.GetResponseStream();
                SS.ReadTimeout = 20 * 1000;
                StreamReader Reader = new StreamReader(SS);
                ResponseFromServer = Reader.ReadToEnd();
                if (ResponseFromServer.Length == 0 || ResponseFromServer[0] != '{')
                    throw new Exception("Not JSON!");
                Reader.Close();
                Response.Close();
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint("NICEHASH", ex.Message);
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
        public decimal Normalized24HrEstimate => MagnitudeFactor(Name) * Estimate_last24h;

        /// <summary>
        /// Gets the Normalized24HrActual
        /// </summary>
        public decimal Normalized24HrActual => MagnitudeFactor(Name) * Actual_last24h * 0.001m;

        /// <summary>
        /// Gets the MidPoint24HrEstimate
        /// </summary>
        public decimal MidPoint24HrEstimate => (Normalized24HrEstimate + Normalized24HrActual) / 2m;

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
                case "equihash": return ZAlgorithm.equihash;
                case "groestl": return ZAlgorithm.groestl;
                case "hsr": return ZAlgorithm.hsr;
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
                case "tribus": return ZAlgorithm.tribus;
                case "veltor": return ZAlgorithm.veltor;
                case "x11": return ZAlgorithm.x11;
                case "x11evo": return ZAlgorithm.x11evo;
                case "x13": return ZAlgorithm.x13;
                case "x14": return ZAlgorithm.x14;
                case "x17": return ZAlgorithm.x17;
                case "xevan": return ZAlgorithm.xevan;
                case "yescrypt": return ZAlgorithm.yescrypt;
                    //case "hmq1725": return zAlgorithm.hmq1725;
                    //case "m7m": return zAlgorithm.m7m;
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
                case "equihash": return 4;
                case "groestl": return 5;
                case "hsr": return 6;
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
                case "tribus": return 23;
                case "veltor": return 24;
                case "x11": return 25;
                case "x11evo": return 26;
                case "x13": return 27;
                case "x14": return 28;
                case "x17": return 29;
                case "xevan": return 30;
                case "yescrypt": return 31;

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
                case "decred":
                case "blakecoin":
                case "blake2s":
                case "keccak":
                case "scrypt":
                case "x11":
                case "quark":
                case "qubit":
                    return 1;

                case "equihash": return 1e6m;
                case "sha256":
                    return 1e-3m;

                default: return 1e3m;
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
        hsr,

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
        tribus,

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
        x17,

        /// <summary>
        /// Defines the xevan
        /// </summary>
        xevan,

        /// <summary>
        /// Defines the unknown
        /// </summary>
        unknown,

        /// <summary>
        /// Defines the yescrypt
        /// </summary>
        yescrypt        //hmq1725,
        //m7m
    }
}
