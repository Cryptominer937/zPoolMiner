namespace zPoolMiner.Miners
{
    using Newtonsoft.Json;
    using SQLite.Net;
    using SQLite.Net.Attributes;
    using SQLite.Net.Platform.Win32;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using zPoolMiner.Configs;
    using zPoolMiner.Devices;
    using zPoolMiner.Enums;

    /// <summary>
    /// Defines the <see cref="ProspectorPlatforms" />
    /// </summary>
    public static class ProspectorPlatforms
    {
        /// <summary>
        /// Gets a value indicating whether IsInit
        /// </summary>
        public static bool IsInit
        {
            get
            {
                return NVPlatform >= 0 || AMDPlatform >= 0;
            }
        }

        /// <summary>
        /// Defines the NVPlatform
        /// </summary>
        public static int NVPlatform = -1;

        /// <summary>
        /// Defines the AMDPlatform
        /// </summary>
        public static int AMDPlatform = -1;

        /// <summary>
        /// The PlatformForDeviceType
        /// </summary>
        /// <param name="type">The <see cref="DeviceType"/></param>
        /// <returns>The <see cref="int"/></returns>
        public static int PlatformForDeviceType(DeviceType type)
        {
            if (IsInit)
            {
                if (type == DeviceType.NVIDIA) return NVPlatform;
                if (type == DeviceType.AMD) return AMDPlatform;
            }
            return -1;
        }
    }

    /// <summary>
    /// Defines the <see cref="Prospector" />
    /// </summary>
    public class Prospector : Miner
    {
        /// <summary>
        /// Defines the <see cref="hashrates" />
        /// </summary>
        private class hashrates
        {
            /// <summary>
            /// Gets or sets the id
            /// </summary>
            [PrimaryKey, AutoIncrement]
            public int id { get; set; }

            /// <summary>
            /// Gets or sets the session_id
            /// </summary>
            public int session_id { get; set; }

            /// <summary>
            /// Gets or sets the coin
            /// </summary>
            public string coin { get; set; }

            /// <summary>
            /// Gets or sets the device
            /// </summary>
            public string device { get; set; }

            /// <summary>
            /// Gets or sets the time
            /// </summary>
            public int time { get; set; }

            /// <summary>
            /// Gets or sets the rate
            /// </summary>
            public double rate { get; set; }
        }

        /// <summary>
        /// Defines the <see cref="sessions" />
        /// </summary>
        private class sessions
        {
            /// <summary>
            /// Gets or sets the id
            /// </summary>
            [PrimaryKey, AutoIncrement]
            public int id { get; set; }

            /// <summary>
            /// Gets or sets the start
            /// </summary>
            public string start { get; set; }
        }

        /// <summary>
        /// Defines the <see cref="ProspectorDatabase" />
        /// </summary>
        private class ProspectorDatabase : SQLiteConnection
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ProspectorDatabase"/> class.
            /// </summary>
            /// <param name="path">The <see cref="string"/></param>
            public ProspectorDatabase(string path)
                : base(new SQLitePlatformWin32(), path)
            {
            }

            /// <summary>
            /// The QueryLastSpeed
            /// </summary>
            /// <param name="device">The <see cref="string"/></param>
            /// <returns>The <see cref="double"/></returns>
            public double QueryLastSpeed(string device)
            {
                try
                {
                    return Table<hashrates>().Where(x => x.device == device).OrderByDescending(x => x.time).Take(1).FirstOrDefault().rate;
                }
                catch (Exception e)
                {
                    Helpers.ConsolePrint("PROSPECTORSQL", e.ToString());
                    return 0;
                }
            }

            /// <summary>
            /// The QuerySpeedsForSession
            /// </summary>
            /// <param name="id">The <see cref="int"/></param>
            /// <returns>The <see cref="IEnumerable{hashrates}"/></returns>
            public IEnumerable<hashrates> QuerySpeedsForSession(int id)
            {
                try
                {
                    return Table<hashrates>().Where(x => x.session_id == id);
                }
                catch (Exception e)
                {
                    Helpers.ConsolePrint("PROSPECTORSQL", e.ToString());
                    return new List<hashrates>();
                }
            }

            /// <summary>
            /// The LastSession
            /// </summary>
            /// <returns>The <see cref="sessions"/></returns>
            public sessions LastSession()
            {
                try
                {
                    return Table<sessions>().LastOrDefault();
                }
                catch (Exception e)
                {
                    Helpers.ConsolePrint("PROSPECTORSQL", e.ToString());
                    return new sessions();
                }
            }
        }

        /// <summary>
        /// Defines the database
        /// </summary>
        private ProspectorDatabase database;

        /// <summary>
        /// Defines the benchmarkTimeWait
        /// </summary>
        private int benchmarkTimeWait;

        /// <summary>
        /// Defines the DevFee
        /// </summary>
        private const double DevFee = 0.07;// 1% devfee

        /// <summary>
        /// Defines the platformStart
        /// </summary>
        private const string platformStart = "platform ";

        /// <summary>
        /// Defines the platformEnd
        /// </summary>
        private const string platformEnd = " - ";

        /// <summary>
        /// Defines the apiPort
        /// </summary>
        private const int apiPort = 42000;

        /// <summary>
        /// Initializes a new instance of the <see cref="Prospector"/> class.
        /// </summary>
        public Prospector()
            : base("Prospector")
        {
            ConectionType = NHMConectionType.STRATUM_TCP;
            IsNeverHideMiningWindow = true;
        }

        /// <summary>
        /// The GET_MAX_CooldownTimeInMilliseconds
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        protected override int GetMaxCooldownTimeInMilliseconds()
        {
            return 3600000; // 1hour
        }

        /// <summary>
        /// The deviceIDString
        /// </summary>
        /// <param name="id">The <see cref="int"/></param>
        /// <param name="type">The <see cref="DeviceType"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string deviceIDString(int id, DeviceType type)
        {
            var platform = 0;
            if (InitPlatforms())
            {
                platform = ProspectorPlatforms.PlatformForDeviceType(type);
            }
            else
            {  // fallback
                Helpers.ConsolePrint(MinerTag(), "Failed to get platforms, falling back");
                if (ComputeDeviceManager.Available.HasNVIDIA && type != DeviceType.NVIDIA)
                    platform = 1;
            }
            return String.Format("{0}-{1}", platform, id);
        }

        /// <summary>
        /// The GetConfigFileName
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        private string GetConfigFileName()
        {
            return String.Format("config_{0}.toml", MiningSetup.MiningPairs[0].Device.ID);
        }

        /// <summary>
        /// The prepareConfigFile
        /// </summary>
        /// <param name="pool">The <see cref="string"/></param>
        /// <param name="wallet">The <see cref="string"/></param>
        /// <param name="worker">The <see cref="string"/></param>
        private void prepareConfigFile(string pool, string wallet, string worker)
        {
            if (MiningSetup.MiningPairs.Count > 0)
            {
                try
                {
                    if (MiningSession.DONATION_SESSION)
                    {
                        if (pool.Contains("zpool.ca"))
                        {
                            wallet = Globals.DemoUser;
                            worker = "c=DOGE,ID=Donation";
                        }
                        if (pool.Contains("ahashpool.com"))
                        {
                            wallet = Globals.DemoUser;
                            worker = "c=DOGE,ID=Donation";
                        }
                        if (pool.Contains("hashrefinery.com"))
                        {
                            wallet = Globals.DemoUser;
                            worker = "c=DOGE,ID=Donation";
                        }
                        if (pool.Contains("nicehash.com"))
                        {
                            wallet = Globals.DemoUser;
                            worker = "c=DOGE,ID=Donation";
                        }
                        if (pool.Contains("zergpool.com"))
                        {
                            wallet = Globals.DemoUser;
                            worker = "c=DOGE,ID=Donation";
                        }
                        if (pool.Contains("blockmasters.co"))
                        {
                            wallet = Globals.DemoUser;
                            worker = "c=DOGE,ID=Donation";
                        }
                        if (pool.Contains("blazepool.com"))
                        {
                            wallet = Globals.DemoUser;
                            worker = "c=DOGE,ID=Donation";
                        }
                        if (pool.Contains("miningpoolhub.com"))
                        {
                            wallet = "cryptominer.Devfee";
                            worker = "x";
                        }
                        else
                        {
                            wallet = Globals.DemoUser;
                        }
                    }
                    else
                    {
                        if (pool.Contains("zpool.ca"))
                        {
                            wallet = zPoolMiner.Globals.GetzpoolUser();
                            worker = zPoolMiner.Globals.GetzpoolWorker();
                        }
                        if (pool.Contains("ahashpool.com"))
                        {
                            wallet = zPoolMiner.Globals.GetahashUser();
                            worker = zPoolMiner.Globals.GetahashWorker();
                        }
                        if (pool.Contains("hashrefinery.com"))
                        {
                            wallet = zPoolMiner.Globals.GethashrefineryUser();
                            worker = zPoolMiner.Globals.GethashrefineryWorker();
                        }
                        if (pool.Contains("nicehash.com"))
                        {
                            wallet = zPoolMiner.Globals.GetnicehashUser();
                            worker = zPoolMiner.Globals.GetnicehashWorker();
                        }
                        if (pool.Contains("zergpool.com"))
                        {
                            wallet = zPoolMiner.Globals.GetzergUser();
                            worker = zPoolMiner.Globals.GetzergWorker();
                        }
                        if (pool.Contains("minemoney.co"))
                        {
                            wallet = zPoolMiner.Globals.GetminemoneyUser();
                            worker = zPoolMiner.Globals.GetminemoneyWorker();
                        }
                        if (pool.Contains("blazepool.com"))
                        {
                            wallet = zPoolMiner.Globals.GetblazepoolUser();
                            worker = zPoolMiner.Globals.GetblazepoolWorker();
                        }
                        if (pool.Contains("blockmasters.co"))
                        {
                            wallet = zPoolMiner.Globals.GetblockmunchUser();
                            worker = zPoolMiner.Globals.GetblockmunchWorker();
                        }
                        if (pool.Contains("miningpoolhub.com"))
                        {
                            wallet = zPoolMiner.Globals.GetMPHUser();
                            worker = zPoolMiner.Globals.GetMPHWorker();
                        }
                    }
                    var sb = new StringBuilder();

                    sb.AppendLine("[general]");
                    sb.AppendLine(String.Format("gpu-coin = \"{0}\"", MiningSetup.MinerName));
                    sb.AppendLine(String.Format("default-username = \"{0}\"", wallet));
                    sb.AppendLine(String.Format("default-password = \"{0}\"", worker + ""));

                    sb.AppendLine(String.Format("[pools.{0}]", MiningSetup.MinerName));
                    sb.AppendLine(String.Format("url = \"{0}\"", pool));

                    foreach (var dev in MiningSetup.MiningPairs)
                    {
                        sb.AppendLine(String.Format("[gpus.{0}]", deviceIDString(dev.Device.ID, dev.Device.DeviceType)));
                        sb.AppendLine("enabled = true");
                        sb.AppendLine(String.Format("label = \"{0}\"", dev.Device.Name));
                    }

                    sb.AppendLine("[cpu]");
                    sb.AppendLine("enabled = false");

                    System.IO.File.WriteAllText(WorkingDirectory + GetConfigFileName(), sb.ToString());
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// The InitPlatforms
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        private bool InitPlatforms()
        {
            if (ProspectorPlatforms.IsInit) return true;

            CleanAllOldLogs();
            var handle = BenchmarkStartProcess(" list-devices");
            handle.Start();

            handle.WaitForExit(20 * 1000);  // 20 seconds
            handle = null;

            try
            {
                string latestLogFile = "";
                var dirInfo = new DirectoryInfo(WorkingDirectory + "logs\\");
                foreach (var file in dirInfo.GetFiles())
                {
                    latestLogFile = file.Name;
                    break;
                }
                if (File.Exists(dirInfo + latestLogFile))
                {
                    var lines = File.ReadAllLines(dirInfo + latestLogFile);
                    foreach (var line in lines)
                    {
                        if (line != null)
                        {
                            string lineLowered = line.ToLower();
                            if (lineLowered.Contains(platformStart))
                            {
                                int platStart = lineLowered.IndexOf(platformStart);
                                string plat = lineLowered.Substring(platStart, line.Length - platStart);
                                plat = plat.Replace(platformStart, "");
                                plat = plat.Substring(0, plat.IndexOf(platformEnd));

                                int platIndex = -1;
                                if (int.TryParse(plat, out platIndex))
                                {
                                    if (lineLowered.Contains("nvidia"))
                                    {
                                        Helpers.ConsolePrint(MinerTag(), "Setting nvidia platform: " + platIndex);
                                        ProspectorPlatforms.NVPlatform = platIndex;
                                        if (ProspectorPlatforms.AMDPlatform >= 0) break;
                                    }
                                    else if (lineLowered.Contains("amd"))
                                    {
                                        Helpers.ConsolePrint(MinerTag(), "Setting amd platform: " + platIndex);
                                        ProspectorPlatforms.AMDPlatform = platIndex;
                                        if (ProspectorPlatforms.NVPlatform >= 0) break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e) { Helpers.ConsolePrint(MinerTag(), e.ToString()); }

            return ProspectorPlatforms.IsInit;
        }

        /// <summary>
        /// The CleanAllOldLogs
        /// </summary>
        protected void CleanAllOldLogs()
        {
            // clean old logs
            try
            {
                var dirInfo = new DirectoryInfo(WorkingDirectory + "logs\\");
                if (dirInfo != null && dirInfo.Exists)
                {
                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// The _Stop
        /// </summary>
        /// <param name="willswitch">The <see cref="MinerStopType"/></param>
        protected override void _Stop(MinerStopType willswitch)
        {
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
        }

        /// <summary>
        /// Defines the <see cref="HashrateApiResponse" />
        /// </summary>
        private class HashrateApiResponse
        {
            /// <summary>
            /// Gets or sets the coin
            /// </summary>
            public string coin { get; set; }

            /// <summary>
            /// Gets or sets the device
            /// </summary>
            public string device { get; set; }

            /// <summary>
            /// Gets or sets the rate
            /// </summary>
            public double rate { get; set; }

            /// <summary>
            /// Gets or sets the time
            /// </summary>
            public string time { get; set; }
        }

        /// <summary>
        /// The GetSummaryAsync
        /// </summary>
        /// <returns>The <see cref="Task{APIData}"/></returns>
        public override async Task<ApiData> GetSummaryAsync()
        {
            CurrentMinerReadStatus = MinerApiReadStatus.NONE;
            ApiData ad = new ApiData(MiningSetup.CurrentAlgorithmType, MiningSetup.CurrentSecondaryAlgorithmType);

            WebClient client = new WebClient();
            HashrateApiResponse[] resp = null;
            try
            {
                string url = String.Format("http://localhost:{0}/api/v0/hashrates", apiPort);
                Stream data = client.OpenRead(url);
                StreamReader reader = new StreamReader(data);
                string s = await reader.ReadToEndAsync();
                data.Close();
                reader.Close();

                resp = JsonConvert.DeserializeObject<HashrateApiResponse[]>(s, Globals.JsonSettings);
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint(MinerTag(), "GetSummary exception: " + ex.Message);
            }

            if (resp != null)
            {
                ad.Speed = 0;
                foreach (var response in resp)
                {
                    if (response.coin == MiningSetup.MinerName)
                    {
                        ad.Speed += response.rate;
                        CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
                    }
                }
                if (ad.Speed == 0)
                {
                    CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;
                }
            }

            return ad;
        }

        /// <summary>
        /// The Start
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="btcAddress">The <see cref="string"/></param>
        /// <param name="worker">The <see cref="string"/></param>
        public override void Start(string url, string btcAddress, string worker)
        {
            if (!IsInit)
            {
                Helpers.ConsolePrint(MinerTag(), "MiningSetup is not initialized exiting Start()");
                return;
            }
            LastCommandLine = GetStartupCommand(url, btcAddress, worker);

            ProcessHandle = _Start();
        }

        /// <summary>
        /// The GetStartupCommand
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="btcAddress">The <see cref="string"/></param>
        /// <param name="worker">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string GetStartupCommand(string url, string btcAddress, string worker)
        {
            string username = GetUsername(btcAddress, worker);
            prepareConfigFile(url, username, worker);
            return "--config " + GetConfigFileName();
        }

        /// <summary>
        /// The BenchmarkCreateCommandLine
        /// </summary>
        /// <param name="algorithm">The <see cref="Algorithm"/></param>
        /// <param name="time">The <see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            // Prospector can take a very long time to start up
            benchmarkTimeWait = time + 60;
            // network stub
            string url = Globals.GetLocationURL(algorithm.CryptoMiner937ID, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], ConectionType);
            return GetStartupCommand(url, Globals.GetBitcoinUser(), ConfigManager.GeneralConfig.WorkerName.Trim());
        }

        /// <summary>
        /// The IsActiveProcess
        /// </summary>
        /// <param name="pid">The <see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool IsActiveProcess(int pid)
        {
            try
            {
                return Process.GetProcessById(pid) != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// The BenchmarkThreadRoutine
        /// </summary>
        /// <param name="CommandLine">The <see cref="object"/></param>
        protected override void BenchmarkThreadRoutine(object CommandLine)
        {
            Thread.Sleep(ConfigManager.GeneralConfig.MinerRestartDelayMS);

            BenchmarkSignalQuit = false;
            BenchmarkSignalHanged = false;
            BenchmarkSignalFinnished = false;
            BenchmarkException = null;

            var startTime = DateTime.Now;

            try
            {
                Helpers.ConsolePrint("BENCHMARK", "Benchmark starts");
                Helpers.ConsolePrint(MinerTag(), "Benchmark should end in : " + benchmarkTimeWait + " seconds");
                BenchmarkHandle = BenchmarkStartProcess((string)CommandLine);
                Stopwatch _benchmarkTimer = new Stopwatch();
                _benchmarkTimer.Reset();
                _benchmarkTimer.Start();
                //BenchmarkThreadRoutineStartSettup();
                // wait a little longer then the benchmark routine if exit false throw
                //var timeoutTime = BenchmarkTimeoutInSeconds(BenchmarkTimeInSeconds);
                //var exitSucces = BenchmarkHandle.WaitForExit(timeoutTime * 1000);
                // don't use wait for it breaks everything
                BenchmarkProcessStatus = BenchmarkProcessStatus.Running;
                bool keepRunning = true;
                while (keepRunning && IsActiveProcess(BenchmarkHandle.Id))
                {
                    //string outdata = BenchmarkHandle.StandardOutput.ReadLine();
                    //BenchmarkOutputErrorDataReceivedImpl(outdata);
                    // terminate process situations
                    if (_benchmarkTimer.Elapsed.TotalSeconds >= (benchmarkTimeWait + 2)
                        || BenchmarkSignalQuit
                        || BenchmarkSignalFinnished
                        || BenchmarkSignalHanged
                        || BenchmarkSignalTimedout
                        || BenchmarkException != null)
                    {
                        string imageName = MinerExeName.Replace(".exe", "");
                        // maybe will have to KILL process
                        KillProspectorClaymoreMinerBase(imageName);
                        if (BenchmarkSignalTimedout)
                        {
                            throw new Exception("Benchmark timedout");
                        }
                        if (BenchmarkException != null)
                        {
                            throw BenchmarkException;
                        }
                        if (BenchmarkSignalQuit)
                        {
                            throw new Exception("Termined by user request");
                        }
                        if (BenchmarkSignalFinnished)
                        {
                            break;
                        }
                        keepRunning = false;
                        break;
                    }
                    else
                    {
                        // wait a second reduce CPU load
                        Thread.Sleep(1000);
                    }
                }
                BenchmarkHandle.WaitForExit(20 * 1000);  // Wait up to 20s for exit
            }
            catch (Exception ex)
            {
                BenchmarkThreadRoutineCatch(ex);
            }
            finally
            {
                BenchmarkAlgorithm.BenchmarkSpeed = 0;

                if (database == null)
                {
                    try
                    {
                        database = new ProspectorDatabase(WorkingDirectory + "info.db");
                    }
                    catch (Exception e) { Helpers.ConsolePrint(MinerTag(), e.ToString()); }
                }

                var session = database.LastSession();
                var sessionStart = Convert.ToDateTime(session.start);
                if (sessionStart < startTime)
                {
                    throw new Exception("Session not recorded!");
                }

                var hashrates = database.QuerySpeedsForSession(session.id);

                double speed = 0;
                int speedRead = 0;
                foreach (var hashrate in hashrates)
                {
                    if (hashrate.coin == MiningSetup.MinerName && hashrate.rate > 0)
                    {
                        speed += hashrate.rate;
                        speedRead++;
                    }
                }

                BenchmarkAlgorithm.BenchmarkSpeed = (speed / speedRead) * (1 - DevFee);

                BenchmarkThreadRoutineFinish();
            }
        }

        // stub benchmarks read from file
        /// <summary>
        /// The BenchmarkOutputErrorDataReceivedImpl
        /// </summary>
        /// <param name="outdata">The <see cref="string"/></param>
        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            CheckOutdata(outdata);
        }

        /// <summary>
        /// The BenchmarkParseLine
        /// </summary>
        /// <param name="outdata">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        protected override bool BenchmarkParseLine(string outdata)
        {
            Helpers.ConsolePrint("BENCHMARK", outdata);
            return false;
        }
    }
}