﻿using Newtonsoft.Json;
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

namespace zPoolMiner
{
    public class SocketEventArgs : EventArgs
    {
        public string Message = "";

        public SocketEventArgs(string message)
        {
            Message = message;
        }
    }

    internal class CryptoStats
    {
#pragma warning disable 649

        #region JSON Models

        private class Nicehash_login
        {
            public string method = "login";
            public string version;
            public int protocol = 1;
        }

        private class Nicehash_credentials
        {
            public string method = "credentials.set";
            public string btc;
            public string worker;
        }

        private class Nicehash_device_status
        {
            public string method = "devices.status";
            public List<JArray> devices;
        }

        #endregion JSON Models

#pragma warning restore 649

        private const int deviceUpdateLaunchDelay = 20 * 1000;
        private const int deviceUpdateInterval = 60 * 1000;

        public static Dictionary<AlgorithmType, NiceHashSMA> AlgorithmRates { get; private set; }
        private static NiceHashData niceHashData;
        public static double Balance { get; private set; }
        public static string Version { get; private set; }
        public static bool IsAlive { get { return NiceHashConnection.IsAlive; } }

        // Event handlers for socket
        public static event EventHandler OnBalanceUpdate = delegate { };

        public static event EventHandler OnSMAUpdate = delegate { };

        public static event EventHandler OnVersionUpdate = delegate { };

        public static event EventHandler OnConnectionLost = delegate { };

        public static event EventHandler OnConnectionEstablished = delegate { };

        public static event EventHandler<SocketEventArgs> OnVersionBurn = delegate { };

        private static readonly Random random = new Random();

        private static System.Threading.Timer deviceUpdateTimer;
        private static System.Threading.Timer algoRatesUpdateTimer;

        #region Socket

        private class NiceHashConnection
        {
            private static WebSocket webSocket;
            public static bool IsAlive { get { return webSocket.IsAlive; } }
            private static bool attemptingReconnect = false;
            private static bool connectionAttempted = false;
            private static bool connectionEstablished = false;

            public static void StartConnection(string address)
            {
                UpdateAlgoRates(null);
                algoRatesUpdateTimer = new System.Threading.Timer(UpdateAlgoRates, null, deviceUpdateInterval, deviceUpdateInterval);
            }

            private static void UpdateAlgoRates(object state)
            {
                try
                {
                    // We get the algo payment info here - http://www.zpool.ca/api/status
                    var WR = (HttpWebRequest)WebRequest.Create("http://crypominer937.tk/zpool-jsondbg.php");
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

            private static void ErrorCallback(object sender, WebSocketSharp.ErrorEventArgs e)
            {
                Helpers.ConsolePrint("SOCKET", e.ToString());
            }

            private static void CloseCallback(object sender, CloseEventArgs e)
            {
                Helpers.ConsolePrint("SOCKET", $"Connection closed code {e.Code}: {e.Reason}");
                AttemptReconnect();
            }

            // Don't call SendData on UI threads, since it will block the thread for a bit if a reconnect is needed
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

        public static void StartConnection(string address)
        {
            NiceHashConnection.StartConnection(address);
            deviceUpdateTimer = new System.Threading.Timer(DeviceStatus_Tick, null, deviceUpdateInterval, deviceUpdateInterval);
        }

        #endregion Socket

        #region Incoming socket calls

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

        private static void SetVersion(string version)
        {
            Version = version;
            OnVersionUpdate.Emit(null, EventArgs.Empty);
        }

        #endregion Incoming socket calls

        #region Outgoing socket calls

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
            // This function is run every minute and sends data every run which has two auxiliary effects
            // Keeps connection alive and attempts reconnection if internet was dropped
            //NiceHashConnection.SendData(sendData);
        }

        #endregion Outgoing socket calls

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

    public class ZPoolAlgo
    {
        public string Name { get; set; }
        public int Port { get; set; }

        //public decimal coins { get; set; }
        //public decimal fees { get; set; }
        //public decimal hashrate { get; set; }
        //public int workers { get; set; }
        public decimal Estimate_current { get; set; }

        public decimal Estimate_last24h { get; set; }
        public decimal Actual_last24h { get; set; }

        public decimal NormalizedEstimate => MagnitudeFactor(Name) * Estimate_current;
        public decimal Normalized24HrEstimate => MagnitudeFactor(Name) * Estimate_last24h;
        public decimal Normalized24HrActual => MagnitudeFactor(Name) * Actual_last24h * 0.001m;
        public decimal MidPoint24HrEstimate => (Normalized24HrEstimate + Normalized24HrActual) / 2m;

        // if the normalized estimate (now) is 20% less than the midpoint, we want to return the
        // normalized estimate
        public decimal Safe24HrEstimate => NormalizedEstimate * 1.2m < MidPoint24HrEstimate
            ? NormalizedEstimate
            : MidPoint24HrEstimate;

        //public decimal hashrate_last24h { get; set; }
        //public decimal rental_current { get; set; }

        public ZAlgorithm Algorithm => ToAlgorithm(Name);

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

        private decimal Min(params decimal[] values) =>
            values.Length == 1
                ? values[0]
                : values.Length == 2
                    ? Math.Min(values[0], values[1])
                    : Min(values[0], Min(values.Skip(1).ToArray()));

        private decimal Max(params decimal[] values) =>
            values.Length == 1
                ? values[0]
                : values.Length == 2
                    ? Math.Max(values[0], values[1])
                    : Max(values[0], Min(values.Skip(1).ToArray()));
    }

    public enum ZAlgorithm
    {
        bitcore,
        blake2s,
        blake256r8,
        c11,
        equihash,
        groestl,
        hsr,
        keccak,
        lbry,
        lyra2v2,
        myriad_groestl,
        neoscrypt,
        nist5,
        phi,
        polytimos,
        quark,
        qubit,
        scrypt,
        sha256,
        sib,
        skein,
        skunk,
        timetravel,
        tribus,
        veltor,
        x11,
        x11evo,
        x13,
        x14,
        x17,
        xevan,
        unknown,
        yescrypt
        //hmq1725,
        //m7m
    }
}