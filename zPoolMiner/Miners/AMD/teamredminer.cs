using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using zPoolMiner.Configs;
using zPoolMiner.Devices;
using zPoolMiner.Enums;
using zPoolMiner.Miners.Grouping;
using zPoolMiner.Miners.Parsing;

namespace zPoolMiner.Miners
{
    internal class teamredminer : Miner
    {
        private readonly int GPUPlatformNumber;
        private Stopwatch _benchmarkTimer = new Stopwatch();
        private int count;

        public teamredminer()
            : base("teamredminer_AMD")
        {
            GPUPlatformNumber = ComputeDeviceManager.Available.AmdOpenCLPlatformNum;
            IsKillAllUsedMinerProcs = true;
            IsNeverHideMiningWindow = true;
        }

        protected override int GetMaxCooldownTimeInMilliseconds() => 240;

        protected override void _Stop(MinerStopType willswitch)
        {
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
            //    Killteamredminer();
        }

        public override void Start(string url, string btcAdress, string worker)
        {
            if (!IsInit)
            {
                Helpers.ConsolePrint(MinerTag(), "MiningSetup is not initialized exiting Start()");
                return;
            }

            var username = GetUsername(btcAdress, worker);
            IsApiReadException = true; //** in miner
            // add failover
            var alg = url.Substring(url.IndexOf("://") + 3, url.IndexOf(".") - url.IndexOf("://") - 3);
            var port = url.Substring(url.IndexOf(".com:") + 5, url.Length - url.IndexOf(".com:") - 5);

            LastCommandLine = " -a " + MiningSetup.MinerName + " -o " + url +
                              " -u " + username +
                              " -p x " +
                              " " +
                              ExtraLaunchParametersParser.ParseForMiningSetup(
                                                                MiningSetup,
                                                                DeviceType.AMD) +
                              " -d ";

            LastCommandLine += GetDevicesCommandString();
            ProcessHandle = _Start();
        }

        // new decoupled benchmarking routines

        #region Decoupled benchmarking routines

        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            string CommandLine;

            var url = Globals.GetLocationURL(algorithm.CryptoMiner937ID, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], ConectionType);

            // demo for benchmark
            var username = Globals.DemoUser;

            if (ConfigManager.GeneralConfig.WorkerName.Length > 0)
                username += "." + ConfigManager.GeneralConfig.WorkerName.Trim();

            CommandLine = " -a " + MiningSetup.MinerName + "" +
                          " --url " + url +
                          " --user " + username +
                          " -p x " +
                          ExtraLaunchParametersParser.ParseForMiningSetup(
                                                                MiningSetup,
                                                                DeviceType.AMD) +
                          " -d ";

            CommandLine += GetDevicesCommandString();

            return CommandLine;
        }

        protected override bool BenchmarkParseLine(string outdata)
        {
            var hashSpeed = "";
            var kspeed = 1;
            // Pool lyra2z.eu.nicehash.com share accepted.
            // Stats GPU 0 - lyra2z: 3.279Mh/s (3.279Mh/s)
            if (outdata.Contains("- lyra2z: "))
            {
                var i = outdata.IndexOf("- lyra2z: ");
                var k = outdata.IndexOf("Mh/s (");
                hashSpeed = outdata.Substring(i + 10, k - i - 10).Trim();
                Helpers.ConsolePrint(hashSpeed, "");
                if (outdata.ToUpper().Contains("H/S")) kspeed = 1;

                if (outdata.ToUpper().Contains("KH/S"))
                {
                    kspeed = 1000;
                }

                if (outdata.ToUpper().Contains("MH/S"))
                {
                    kspeed = 1000000;
                }

                var speed = double.Parse(hashSpeed, CultureInfo.InvariantCulture);
                BenchmarkAlgorithm.BenchmarkSpeed = Math.Max(BenchmarkAlgorithm.BenchmarkSpeed, speed * kspeed);
                // Killteamredminer();
                BenchmarkSignalFinnished = true;
                // BenchmarkSignalHanged = true;
                return true;
            }

            return false;
        }

        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            CheckOutdata(outdata);
        }

        #endregion Decoupled benchmarking routines

        // TODO _currentMinerReadStatus
        public override async Task<ApiData> GetSummaryAsync()
        {
            // if (!IsApiReadException) return await GetSummaryCpuCcminerAsync();
            // check if running
            if (ProcessHandle == null)
            {
                CurrentMinerReadStatus = MinerApiReadStatus.RESTART;
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Could not read data from teamredminer Proccess is null");
                return null;
            }

            try
            {
                Process.GetProcessById(ProcessHandle.Id);
            }
            catch (ArgumentException ex)
            {
                CurrentMinerReadStatus = MinerApiReadStatus.RESTART;
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Could not read data from teamredminer reason: " + ex.Message);
                return null; // will restart outside
            }
            catch (InvalidOperationException ex)
            {
                CurrentMinerReadStatus = MinerApiReadStatus.RESTART;
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Could not read data from teamredminer reason: " + ex.Message);
                return null; // will restart outside
            }

            var totalSpeed = 0.0d;

            foreach (var miningPair in MiningSetup.MiningPairs)
            {
                var algo = miningPair.Device.GetAlgorithm(MinerBaseType.teamredminer, AlgorithmType.ethash, AlgorithmType.NONE);

                if (algo != null)
                {
                    totalSpeed += algo.BenchmarkSpeed;
                }
            }

            var teamredminerData = new ApiData(MiningSetup.CurrentAlgorithmType)
            {
                Speed = totalSpeed
            };

            CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
            // check if speed zero
            if (teamredminerData.Speed == 0) CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;
            return teamredminerData;
        }
    }
}