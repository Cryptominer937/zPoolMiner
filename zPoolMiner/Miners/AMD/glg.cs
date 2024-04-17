namespace zPoolMiner.Miners
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using zPoolMiner.Configs;
    using zPoolMiner.Devices;
    using zPoolMiner.Enums;
    using zPoolMiner.Miners.Grouping;
    using zPoolMiner.Miners.Parsing;

    /// <summary>
    /// Defines the <see cref="Glg" />
    /// </summary>
    internal class Glg : Miner
    {
        /// <summary>
        /// Defines the GPUPlatformNumber
        /// </summary>
        private readonly int GPUPlatformNumber;

        /// <summary>
        /// Defines the _benchmarkTimer
        /// </summary>
        private Stopwatch _benchmarkTimer = new Stopwatch();

        private const double DevFee = 6.0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Glg"/> class.
        /// </summary>
        public Glg()
            : base("glg_AMD")
        {
            GPUPlatformNumber = ComputeDeviceManager.Available.AmdOpenCLPlatformNum;
            IsKillAllUsedMinerProcs = true;
        }

        // use ONLY for exiting a benchmark
        /// <summary>
        /// The Killglg
        /// </summary>
        public void Killglg()
        {
            foreach (Process process in Process.GetProcessesByName("gatelessgate"))
            {
                try { process.Kill(); } catch (Exception e) { Helpers.ConsolePrint(MinerDeviceName, e.ToString()); }
            }
        }

        /// <summary>
        /// The EndBenchmarkProcces
        /// </summary>
        public override void EndBenchmarkProcces()
        {
            if (BenchmarkProcessStatus != BenchmarkProcessStatus.Killing && BenchmarkProcessStatus != BenchmarkProcessStatus.DoneKilling)
            {
                BenchmarkProcessStatus = BenchmarkProcessStatus.Killing;

                try
                {
                    Helpers.ConsolePrint("BENCHMARK", string.Format("Trying to kill benchmark process {0} algorithm {1}", BenchmarkProcessPath, BenchmarkAlgorithm.AlgorithmName));
                    Killglg();
                }
                catch { }
                finally
                {
                    BenchmarkProcessStatus = BenchmarkProcessStatus.DoneKilling;
                    Helpers.ConsolePrint("BENCHMARK", string.Format("Benchmark process {0} algorithm {1} KILLED", BenchmarkProcessPath, BenchmarkAlgorithm.AlgorithmName));
                    // BenchmarkHandle = null;
                }
            }
        }

        /// <summary>
        /// The GET_MAX_CooldownTimeInMilliseconds
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        protected override int GetMaxCooldownTimeInMilliseconds()
        {
            return 90 * 1000 * 2; // 3 minute max, whole waiting time 75seconds
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
        /// The Start
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="btcAddress">The <see cref="string"/></param>
        /// <param name="worker">The <see cref="string"/></param>
        public override void Start(string url, string btcAddress, string worker)
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

            if (!IsInit)
            {
                Helpers.ConsolePrint(MinerTag(), "MiningSetup is not initialized exiting Start()");
                return;
            }

            var username = GetUsername(btcAddress, worker);

            LastCommandLine = " --gpu-platform " + GPUPlatformNumber +
                              " -k " + MiningSetup.MinerName +
                              " --url=" + url +
                              " --userpass=" + username + ":" + worker + " " +
                              " -p " + worker +
                              " --api-listen" +
                              " --api-port=" + ApiPort.ToString() +
                              " " +
                              ExtraLaunchParametersParser.ParseForMiningSetup(
                                                                MiningSetup,
                                                                DeviceType.AMD) +
                              " --device ";

            LastCommandLine += GetDevicesCommandString();

            ProcessHandle = _Start();
        }

        // new decoupled benchmarking routines
        /// <summary>
        /// The BenchmarkCreateCommandLine
        /// </summary>
        /// <param name="algorithm">The <see cref="Algorithm"/></param>
        /// <param name="time">The <see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            string CommandLine;

            var url = Globals.GetLocationURL(algorithm.CryptoMiner937ID, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], ConectionType);

            // demo for benchmark
            var username = Globals.DemoUser;

            if (ConfigManager.GeneralConfig.WorkerName.Length > 0)
                username += "." + ConfigManager.GeneralConfig.WorkerName.Trim();

            // cd to the cgminer for the process bins
            CommandLine = " /C \"cd /d " + WorkingDirectory + " && gatelessgate.exe " +
                          " --gpu-platform " + GPUPlatformNumber +
                          " -k " + algorithm.MinerName +
                          " --url=" + url + "/#xnsub" +
                          " --userpass=" + Globals.DemoUser +
                          " -p c=DOGE,Benchmark" +
                          " --sched-stop " + DateTime.Now.AddSeconds(time).ToString("HH:mm") +
                          " -T --log 10 --log-file dump.txt" +
                          " --api-listen" +
                          " --api-port=" + ApiPort.ToString() +
                          " " +
                          ExtraLaunchParametersParser.ParseForMiningSetup(
                                                                MiningSetup,
                                                                DeviceType.AMD) +
                          " --device ";

            CommandLine += GetDevicesCommandString();

            CommandLine += " && del dump.txt\"";

            return CommandLine;
        }

        /// <summary>
        /// The BenchmarkParseLine
        /// </summary>
        /// <param name="outdata">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        protected override bool BenchmarkParseLine(string outdata)
        {
            //  Helpers.ConsolePrint("BENCHMARK", out);
            var hashSpeed = "";
            var kspeed = 1;

            if (outdata.Contains("Terminating execution as planned"))
            {
                return true;
            }

            if (outdata.Contains("(avg)") && outdata.Contains("h/s") && BenchmarkAlgorithm.CryptoMiner937ID != AlgorithmType.DaggerHashimoto)
            {
                var i = outdata.IndexOf("(avg):");
                var k = outdata.IndexOf("h/s");

                if (outdata.Contains("h/s"))
                {
                    hashSpeed = outdata.Substring(i + 6, k - i - 6);
                    kspeed = 1;
                }

                if (outdata.Contains("Kh/s"))
                {
                    hashSpeed = outdata.Substring(i + 6, k - i - 7);
                    kspeed = 1000;
                }

                if (outdata.Contains("Mh/s"))
                {
                    hashSpeed = outdata.Substring(i + 6, k - i - 7);
                    kspeed = 1000000;
                }

                var speed = double.Parse(hashSpeed, CultureInfo.InvariantCulture);
                Helpers.ConsolePrint("BENCHMARK", "Final Speed: " + hashSpeed);
                BenchmarkAlgorithm.BenchmarkSpeed = (speed * kspeed) * (1.0 - DevFee * 0.01);
                return false;
            }

            return false;
        }

        /// <summary>
        /// The BenchmarkThreadRoutineStartSettup
        /// </summary>
        protected override void BenchmarkThreadRoutineStartSettup()
        {
            // sgminer extra settings
            var NHDataIndex = BenchmarkAlgorithm.CryptoMiner937ID;

            if (Globals.CryptoMiner937Data == null)
            {
                Helpers.ConsolePrint("BENCHMARK", "Skipping gatelessgate benchmark because there is no internet " +
                    "connection. Gatelessgate needs internet connection to do benchmarking.");

                throw new Exception("No internet connection");
            }

            if (Globals.CryptoMiner937Data[NHDataIndex].paying == 0)
            {
                Helpers.ConsolePrint("BENCHMARK", "Skipping gatelessgate benchmark because there is no work " +
                    "[algo: " + BenchmarkAlgorithm.AlgorithmName + "(" + NHDataIndex + ")]");

                throw new Exception("No work can be used for benchmarking");
            }

            _benchmarkTimer.Reset();
            _benchmarkTimer.Start();
        }

        /// <summary>
        /// The BenchmarkOutputErrorDataReceivedImpl
        /// </summary>
        /// <param name="outdata">The <see cref="string"/></param>
        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            if (_benchmarkTimer.Elapsed.TotalSeconds >= BenchmarkTimeInSeconds)
            {
                var resp = GetAPIDataAsync(ApiPort, "quit").Result.TrimEnd(new char[] { (char)0 });
                Helpers.ConsolePrint("BENCHMARK", "gatelessgate Response: " + resp);
            }

            if (_benchmarkTimer.Elapsed.TotalSeconds >= BenchmarkTimeInSeconds + 2)
            {
                _benchmarkTimer.Stop();
                // this is safe in a benchmark
                Killglg();
                BenchmarkSignalHanged = true;
            }

            if (!BenchmarkSignalFinnished && outdata != null)
            {
                CheckOutdata(outdata);
            }
        }

        /// <summary>
        /// The GetFinalBenchmarkString
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        protected override string GetFinalBenchmarkString()
        {
            if (BenchmarkAlgorithm.BenchmarkSpeed <= 0)
            {
                Helpers.ConsolePrint("gatelessgate_GetFinalBenchmarkString", International.GetText("gatelessgate_precise_try"));
                return International.GetText("gatelessgate_precise_try");
            }

            return base.GetFinalBenchmarkString();
        }

        /// <summary>
        /// The BenchmarkThreadRoutine
        /// </summary>
        /// <param name="CommandLine">The <see cref="object"/></param>
        protected override void BenchmarkThreadRoutine(object CommandLine)
        {
            Thread.Sleep(ConfigManager.GeneralConfig.MinerRestartDelayMS * 3); // increase wait for sgminer

            BenchmarkSignalQuit = false;
            BenchmarkSignalHanged = false;
            BenchmarkSignalFinnished = false;
            BenchmarkException = null;

            try
            {
                Helpers.ConsolePrint("BENCHMARK", "Benchmark starts");
                BenchmarkHandle = BenchmarkStartProcess((string)CommandLine);
                BenchmarkThreadRoutineStartSettup();
                // wait a little longer then the benchmark routine if exit false throw
                // var timeoutTime = BenchmarkTimeoutInSeconds(BenchmarkTimeInSeconds);
                // var exitSucces = BenchmarkHandle.WaitForExit(timeoutTime * 1000);
                // don't use wait for it breaks everything
                BenchmarkProcessStatus = BenchmarkProcessStatus.Running;

                while (true)
                {
                    var outdata = BenchmarkHandle.StandardOutput.ReadLine();
                    BenchmarkOutputErrorDataReceivedImpl(outdata);
                    // terminate process situations
                    if (BenchmarkSignalQuit
                        || BenchmarkSignalFinnished
                        || BenchmarkSignalHanged
                        || BenchmarkSignalTimedout
                        || BenchmarkException != null)
                    {
                        // EndBenchmarkProcces();
                        // this is safe in a benchmark
                        Killglg();

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

                        if (BenchmarkSignalHanged)
                        {
                            throw new Exception("gatelessgate is not responding");
                        }

                        if (BenchmarkSignalFinnished) break;
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
                BenchmarkThreadRoutineFinish();
            }
        }

        // TODO _currentMinerReadStatus
        /// <summary>
        /// The GetSummaryAsync
        /// </summary>
        /// <returns>The <see cref="Task{APIData}"/></returns>
        public override async Task<ApiData> GetSummaryAsync()
        {
            string resp;
            var ad = new ApiData(MiningSetup.CurrentAlgorithmType);

            resp = await GetAPIDataAsync(ApiPort, "summary");

            if (resp == null)
            {
                CurrentMinerReadStatus = MinerApiReadStatus.NONE;
                return null;
            }
            // // sgminer debug log
            // Helpers.ConsolePrint("sgminer-DEBUG_resp", resp);

            try
            {
                // Checks if all the GPUs are Alive first
                var resp2 = await GetAPIDataAsync(ApiPort, "devs");

                if (resp2 == null)
                {
                    CurrentMinerReadStatus = MinerApiReadStatus.NONE;
                    return null;
                }
                // // sgminer debug log
                // Helpers.ConsolePrint("sgminer-DEBUG_resp2", resp2);

                var checkGPUStatus = resp2.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 1; i < checkGPUStatus.Length - 1; i++)
                {
                    if (checkGPUStatus[i].Contains("Enabled=Y") && !checkGPUStatus[i].Contains("Status=Alive"))
                    {
                        Helpers.ConsolePrint(MinerTag(), ProcessTag() + " GPU " + i + ": Sick/Dead/NoStart/Initialising/Disabled/Rejecting/Unknown");
                        CurrentMinerReadStatus = MinerApiReadStatus.WAIT;
                        return null;
                    }
                }

                var resps = resp.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                if (resps[1].Contains("SUMMARY"))
                {
                    var data = resps[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    // Get miner's current total speed
                    var speed = data[4].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    // Get miner's current total MH
                    var total_mh = double.Parse(data[18].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[1], new CultureInfo("en-US"));

                    ad.Speed = double.Parse(speed[1]) * 1000;

                    if (total_mh <= PreviousTotalMH)
                    {
                        Helpers.ConsolePrint(MinerTag(), ProcessTag() + " gatelessgate might be stuck as no new hashes are being produced");
                        Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Prev Total MH: " + PreviousTotalMH + " .. Current Total MH: " + total_mh);
                        CurrentMinerReadStatus = MinerApiReadStatus.NONE;
                        return null;
                    }

                    PreviousTotalMH = total_mh;
                }
                else
                {
                    ad.Speed = 0;
                }
            }
            catch
            {
                CurrentMinerReadStatus = MinerApiReadStatus.NONE;
                return null;
            }

            CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
            // check if speed zero
            if (ad.Speed == 0) CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;

            return ad;
        }
    }
}