namespace zPoolMiner.Miners
{
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using zPoolMiner.Configs;
    using zPoolMiner.Enums;
    using zPoolMiner.Miners.Grouping;
    using zPoolMiner.Miners.Parsing;

    /// <summary>
    /// Defines the <see cref="DSTM" />
    /// </summary>
    public class DSTM : Miner
    {
        /// <summary>
        /// Defines the <see cref="Result" />
        /// </summary>
        private class Result
        {
            /// <summary>
            /// Gets or sets the Gpu_id
            /// </summary>
            public int Gpu_id { get; set; }

            /// <summary>
            /// Gets or sets the Temperature
            /// </summary>
            public int Temperature { get; set; }

            /// <summary>
            /// Gets or sets the Sol_ps
            /// </summary>
            public double Sol_ps { get; set; }

            /// <summary>
            /// Gets or sets the Avg_sol_ps
            /// </summary>
            public double Avg_sol_ps { get; set; }

            /// <summary>
            /// Gets or sets the Sol_pw
            /// </summary>
            public double Sol_pw { get; set; }

            /// <summary>
            /// Gets or sets the Avg_sol_pw
            /// </summary>
            public double Avg_sol_pw { get; set; }

            /// <summary>
            /// Gets or sets the Power_usage
            /// </summary>
            public double Power_usage { get; set; }

            /// <summary>
            /// Gets or sets the Avg_power_usage
            /// </summary>
            public double Avg_power_usage { get; set; }

            /// <summary>
            /// Gets or sets the Accepted_shares
            /// </summary>
            public int Accepted_shares { get; set; }

            /// <summary>
            /// Gets or sets the Rejected_shares
            /// </summary>
            public int Rejected_shares { get; set; }

            /// <summary>
            /// Gets or sets the Latency
            /// </summary>
            public int Latency { get; set; }
        }

        /// <summary>
        /// Defines the <see cref="JsonApiResponse" />
        /// </summary>
        private class JsonApiResponse
        {
            /// <summary>
            /// Gets or sets the Id
            /// </summary>
            public uint Id { get; set; }

            /// <summary>
            /// Gets or sets the Result
            /// </summary>
            public Result[] Result { get; set; }

            /// <summary>
            /// Gets or sets the Uptime
            /// </summary>
            public uint Uptime { get; set; }

            /// <summary>
            /// Gets or sets the Contime
            /// </summary>
            public uint Contime { get; set; }

            /// <summary>
            /// Gets or sets the Server
            /// </summary>
            public string Server { get; set; }

            /// <summary>
            /// Gets or sets the Port
            /// </summary>
            public uint Port { get; set; }

            /// <summary>
            /// Gets or sets the User
            /// </summary>
            public string User { get; set; }

            /// <summary>
            /// Gets or sets the Version
            /// </summary>
            public string Version { get; set; }

            /// <summary>
            /// Gets or sets the Error
            /// </summary>
            public object Error { get; set; }
        }

        /// <summary>
        /// Defines the benchmarkTimeWait
        /// </summary>
        private int benchmarkTimeWait = 2 * 45;

        /// <summary>
        /// Defines the LOOK_FOR_START
        /// </summary>
        private const string LOOK_FOR_START = "avg:";

        /// <summary>
        /// Defines the benchmark_read_count
        /// </summary>
        private int benchmark_read_count;

        /// <summary>
        /// Defines the benchmark_sum
        /// </summary>
        private double benchmark_sum;

        /// <summary>
        /// Defines the LOOK_FOR_END
        /// </summary>
        private const string LOOK_FOR_END = "i/s:";

        /// <summary>
        /// Defines the DevFee
        /// </summary>
        private const double DevFee = 8.0;

        /// <summary>
        /// Initializes a new instance of the <see cref="DSTM"/> class.
        /// </summary>
        public DSTM() : base("dstm")
        {
            ConectionType = NHMConectionType.NONE;
            IsNeverHideMiningWindow = true;
        }

        /// <summary>
        /// The Start
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="btcAddress">The <see cref="string"/></param>
        /// <param name="worker">The <see cref="string"/></param>
        public override void Start(string url, string btcAddress, string worker)
        {
            LastCommandLine = GetStartCommand(url, btcAddress, worker);
            var vcp = "msvcp120.dll";
            var vcpPath = WorkingDirectory + vcp;
            ProcessHandle = _Start();
        }

        /// <summary>
        /// The GetStartCommand
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="btcAddress">The <see cref="string"/></param>
        /// <param name="worker">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string GetStartCommand(string url, string btcAddress, string worker)
        {
            if (MiningSession.DONATION_SESSION)
            {
                if (url.Contains("zpool.ca"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }

                if (url.Contains("ahashpool.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }

                if (url.Contains("hashrefinery.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }

                if (url.Contains("nicehash.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }

                if (url.Contains("zergpool.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }

                if (url.Contains("blockmasters.co"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }

                if (url.Contains("blazepool.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }

                if (url.Contains("miningpoolhub.com"))
                {
                    btcAddress = "cryptominer.Devfee";
                    worker = "x";
                }
                else
                {
                    btcAddress = Globals.DemoUser;
                }
            }
            else
            {
                if (url.Contains("zpool.ca"))
                {
                    btcAddress = zPoolMiner.Globals.GetzpoolUser();
                    worker = zPoolMiner.Globals.GetzpoolWorker();
                }

                if (url.Contains("ahashpool.com"))
                {
                    btcAddress = zPoolMiner.Globals.GetahashUser();
                    worker = zPoolMiner.Globals.GetahashWorker();
                }

                if (url.Contains("hashrefinery.com"))
                {
                    btcAddress = zPoolMiner.Globals.GethashrefineryUser();
                    worker = zPoolMiner.Globals.GethashrefineryWorker();
                }

                if (url.Contains("nicehash.com"))
                {
                    btcAddress = zPoolMiner.Globals.GetnicehashUser();
                    worker = zPoolMiner.Globals.GetnicehashWorker();
                }

                if (url.Contains("zergpool.com"))
                {
                    btcAddress = zPoolMiner.Globals.GetzergUser();
                    worker = zPoolMiner.Globals.GetzergWorker();
                }

                if (url.Contains("minemoney.co"))
                {
                    btcAddress = zPoolMiner.Globals.GetminemoneyUser();
                    worker = zPoolMiner.Globals.GetminemoneyWorker();
                }

                if (url.Contains("blazepool.com"))
                {
                    btcAddress = zPoolMiner.Globals.GetblazepoolUser();
                    worker = zPoolMiner.Globals.GetblazepoolWorker();
                }

                if (url.Contains("blockmasters.co"))
                {
                    btcAddress = zPoolMiner.Globals.GetblockmunchUser();
                    worker = zPoolMiner.Globals.GetblockmunchWorker();
                }

                if (url.Contains("miningpoolhub.com"))
                {
                    btcAddress = zPoolMiner.Globals.GetMPHUser();
                    worker = zPoolMiner.Globals.GetMPHWorker();
                }
            }

            var ret = GetDevicesCommandString()
                + " --server " + url.Split(':')[0]
                + " --user " + btcAddress + " --pass " + worker + "" + " --port "
                + url.Split(':')[1] + " --telemetry=127.0.0.1:" + ApiPort;

            return ret;
        }

        /// <summary>
        /// The GetDevicesCommandString
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        protected override string GetDevicesCommandString()
        {
            var deviceStringCommand = " --dev ";

            foreach (var nvidia_pair in MiningSetup.MiningPairs)
                deviceStringCommand += nvidia_pair.Device.ID + " ";

            deviceStringCommand += " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            return deviceStringCommand;
        }

        // benchmark stuff
        /// <summary>
        /// The KillMinerBase
        /// </summary>
        /// <param name="exeName">The <see cref="string"/></param>
        protected void KillMinerBase(string exeName)
        {
            foreach (Process process in Process.GetProcessesByName(exeName))
            {
                try { process.Kill(); } catch (Exception e) { Helpers.ConsolePrint(MinerDeviceName, e.ToString()); }
            }
        }

        /// <summary>
        /// The BenchmarkCreateCommandLine
        /// </summary>
        /// <param name="algorithm">The <see cref="Algorithm"/></param>
        /// <param name="time">The <see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            CleanAllOldLogs();

            var server = Globals.GetLocationURL(algorithm.CryptoMiner937ID, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], ConectionType);
            var ret = " --logfile=benchmark_log.txt" + GetStartCommand(server, Globals.DemoUser, ConfigManager.GeneralConfig.WorkerName.Trim());
            benchmarkTimeWait = Math.Max(time * 3, 120);  // dstm takes a long time to get started
            return ret;
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

            try
            {
                Helpers.ConsolePrint("BENCHMARK", "Benchmark starts");
                Helpers.ConsolePrint(MinerTag(), "Benchmark should end in : " + benchmarkTimeWait + " seconds");
                BenchmarkHandle = BenchmarkStartProcess((string)CommandLine);
                BenchmarkHandle.WaitForExit(benchmarkTimeWait + 2);
                var _benchmarkTimer = new Stopwatch();
                _benchmarkTimer.Reset();
                _benchmarkTimer.Start();
                BenchmarkProcessStatus = BenchmarkProcessStatus.Running;
                var keepRunning = true;

                while (keepRunning && IsActiveProcess(BenchmarkHandle.Id))
                {
                    if (_benchmarkTimer.Elapsed.TotalSeconds >= (benchmarkTimeWait + 2)
                        || BenchmarkSignalQuit
                        || BenchmarkSignalFinnished
                        || BenchmarkSignalHanged
                        || BenchmarkSignalTimedout
                        || BenchmarkException != null)
                    {
                        var imageName = MinerExeName.Replace(".exe", "");
                        // maybe will have to KILL process
                        KillMinerBase(imageName);

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

                        if (BenchmarkSignalFinnished) break;
                        keepRunning = false;
                        break;
                    }
                    else
                    {
                        // wait a second reduce CPU load
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                BenchmarkThreadRoutineCatch(ex);
            }
            finally
            {
                BenchmarkAlgorithm.BenchmarkSpeed = 0;
                // find latest log file
                Thread.Sleep(1000);
                var latestLogFile = "";
                var dirInfo = new DirectoryInfo(WorkingDirectory);

                foreach (var file in dirInfo.GetFiles("*_log.txt"))
                {
                    latestLogFile = file.Name;
                    break;
                }

                // read file log
                if (File.Exists(WorkingDirectory + latestLogFile))
                {
                    var lines = new string[0];
                    var read = false;
                    var iteration = 0;

                    while (!read)
                    {
                        if (iteration < 10)
                        {
                            try
                            {
                                lines = File.ReadAllLines(WorkingDirectory + latestLogFile);
                                read = true;
                                Helpers.ConsolePrint(MinerTag(), "Successfully read log after " + iteration.ToString() + " iterations");
                            }
                            catch (Exception ex)
                            {
                                Helpers.ConsolePrint(MinerTag(), ex.Message);
                                Thread.Sleep(1000);
                            }

                            iteration++;
                        }
                        else
                        {
                            read = true;  // Give up after 10s
                            Helpers.ConsolePrint(MinerTag(), "Gave up on iteration " + iteration.ToString());
                        }
                    }

                    var addBenchLines = BenchLines.Count == 0;

                    foreach (var line in lines)
                    {
                        if (line != null)
                        {
                            BenchLines.Add(line);
                            var lineLowered = line.ToLower();

                            if (lineLowered.Contains(LOOK_FOR_START))
                            {
                                benchmark_sum += GetNumber(lineLowered);
                                ++benchmark_read_count;
                            }
                        }
                    }

                    if (benchmark_read_count > 0)
                    {
                        BenchmarkAlgorithm.BenchmarkSpeed = benchmark_sum / benchmark_read_count;
                    }
                }

                BenchmarkThreadRoutineFinish();
            }
        }

        /// <summary>
        /// The CleanAllOldLogs
        /// </summary>
        protected void CleanAllOldLogs()
        {
            // clean old logs
            try
            {
                var dirInfo = new DirectoryInfo(WorkingDirectory);
                var deleteContains = "_log.txt";

                if (dirInfo != null && dirInfo.Exists)
                {
                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        if (file.Name.Contains(deleteContains))
                        {
                            file.Delete();
                        }
                    }
                }
            }
            catch { }
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

        /// <summary>
        /// The GetNumber
        /// </summary>
        /// <param name="outdata">The <see cref="string"/></param>
        /// <returns>The <see cref="double"/></returns>
        protected double GetNumber(string outdata) => GetNumber(outdata, LOOK_FOR_START, LOOK_FOR_END);

        /// <summary>
        /// The GetNumber
        /// </summary>
        /// <param name="outdata">The <see cref="string"/></param>
        /// <param name="LOOK_FOR_START">The <see cref="string"/></param>
        /// <param name="LOOK_FOR_END">The <see cref="string"/></param>
        /// <returns>The <see cref="double"/></returns>
        protected double GetNumber(string outdata, string LOOK_FOR_START, string LOOK_FOR_END)
        {
            try
            {
                double mult = 1;
                var speedStart = outdata.IndexOf(LOOK_FOR_START);
                var speed = outdata.Substring(speedStart, outdata.Length - speedStart);
                speed = speed.Replace(LOOK_FOR_START, "");
                Helpers.ConsolePrint(MinerTag(), speed);
                speed = speed.Substring(0, speed.IndexOf(LOOK_FOR_END));
                Helpers.ConsolePrint(MinerTag(), speed);

                if (speed.Contains("k"))
                {
                    mult = 1000;
                    speed = speed.Replace("k", "");
                }
                else if (speed.Contains("m"))
                {
                    mult = 1000000;
                    speed = speed.Replace("m", "");
                }

                speed = speed.Trim();
                return (double.Parse(speed, CultureInfo.InvariantCulture) * mult) * (1.0 - DevFee * 0.01);
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint("getNumber", ex.Message + " | args => " + outdata + " | " + LOOK_FOR_END + " | " + LOOK_FOR_START);
            }

            return 0;
        }

        /// <summary>
        /// The GetSummaryAsync
        /// </summary>
        /// <returns>The <see cref="Task{APIData}"/></returns>
        public override async Task<ApiData> GetSummaryAsync()
        {
            CurrentMinerReadStatus = MinerApiReadStatus.NONE;
            var ad = new ApiData(MiningSetup.CurrentAlgorithmType);
            TcpClient client = null;
            dynamic resp = null;

            try
            {
                var bytesToSend = ASCIIEncoding.ASCII.GetBytes("{\"id\":0,\"jsonrpc\":\"2.0\",\"method\":\"sol_ps\"}n");
                client = new TcpClient("127.0.0.1", ApiPort);
                var nwStream = client.GetStream();
                await nwStream.WriteAsync(bytesToSend, 0, bytesToSend.Length);
                var bytesToRead = new byte[client.ReceiveBufferSize];
                var bytesRead = await nwStream.ReadAsync(bytesToRead, 0, client.ReceiveBufferSize);
                var respStr = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                resp = JsonConvert.DeserializeObject(respStr);
                Helpers.ConsolePrint(MinerTag(), "MINER RESPONCE:" + respStr);
                client.Close();
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint(MinerTag(), ex.Message);
            }

            // double speeds = 0;
            uint tmpSpeed = 0;

            if (resp != null && resp.error == null)
            {
                foreach (var result in resp.result)
                {
                    tmpSpeed = result.sol_ps;
                    ad.Speed += tmpSpeed;
                }

                //  ad.Speed = speeds;
                CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;

                if (ad.Speed == 0)
                {
                    CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;
                }
            }

            return ad;
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
        /// The GET_MAX_CooldownTimeInMilliseconds
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        protected override int GetMaxCooldownTimeInMilliseconds()
        {
            return 60 * 1000 * 5; // 5 minute max, whole waiting time 75seconds
        }
    }
}