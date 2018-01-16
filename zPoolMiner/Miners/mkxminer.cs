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
    internal class Mkxminer : Miner
    {
        private int benchmarkTimeWait = 11 * 60;
        private readonly int GPUPlatformNumber;

        public Mkxminer() : base("mkxminer_AMD")

        {
            GPUPlatformNumber = ComputeDeviceManager.Avaliable.AMDOpenCLPlatformNum;
            IsKillAllUsedMinerProcs = true;
        }

        private bool BenchmarkException
        {
            get
            {
                return MiningSetup.MinerPath == MinerPaths.Data.hsrneoscrypt;
            }
        }

        protected override int GET_MAX_CooldownTimeInMilliseconds()
        {
            if (this.MiningSetup.MinerPath == MinerPaths.Data.hsrneoscrypt)
            {
                return 60 * 1000 * 11; // wait wait for hashrate string
            }
            return 660 * 1000; // 11 minute max
        }

        public override void Start(string url, string btcAdress, string worker)
        {
            if (!IsInit)
            {
                Helpers.ConsolePrint(MinerTAG(), "MiningSetup is not initialized exiting Start()");
                return;
            }
            string username = GetUsername(btcAdress, worker);

            LastCommandLine = " --url=" + url +
                                 " --user=" + btcAdress +
                         " -p " + worker + "-I 23 " +
                                 ExtraLaunchParametersParser.ParseForMiningSetup(
                                                               MiningSetup,
                                                               DeviceType.AMD) +
                                 " --devices ";
            LastCommandLine += GetDevicesCommandString();

            LastCommandLine += GetDevicesCommandString();

            ProcessHandle = _Start();
        }

        protected override void _Stop(MinerStopType willswitch)
        {
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
        }

        // new decoupled benchmarking routines

        #region Decoupled benchmarking routines

        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            string url = Globals.GetLocationURL(algorithm.NiceHashID, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], this.ConectionType);

            string username = Globals.DemoUser;

            if (ConfigManager.GeneralConfig.WorkerName.Length > 0)
                username += "." + ConfigManager.GeneralConfig.WorkerName.Trim();

            string CommandLine = " --url=" + url +
                                  " --user=" + Globals.DemoUser +
                          " -p Benchmark -I 23" +
                                  ExtraLaunchParametersParser.ParseForMiningSetup(
                                                                MiningSetup,
                                                                DeviceType.AMD) +
                                  " --devices ";
            CommandLine += GetDevicesCommandString();

            Helpers.ConsolePrint(MinerTAG(), CommandLine);

            return CommandLine;
        }

        protected override bool BenchmarkParseLine(string outdata)
        {
            Helpers.ConsolePrint(MinerTAG(), outdata);
            if (BenchmarkException)
            {
                if (outdata.Contains("> "))
                {
                    int st = outdata.IndexOf("> ");
                    int end = outdata.IndexOf("MH/s");
                    //      int len = outdata.Length - speedLength - st;

                    //          string parse = outdata.Substring(st, len-1).Trim();
                    //          double tmp = 0;
                    //          Double.TryParse(parse, NumberStyles.Any, CultureInfo.InvariantCulture, out tmp);

                    // save speed
                    //       int i = outdata.IndexOf("Benchmark:");
                    //       int k = outdata.IndexOf("/s");
                    string hashspeed = outdata.Substring(st + 9, end - st - 9);
                    /*
                    int b = hashspeed.IndexOf(" ");
                       if (hashspeed.Contains("k"))
                           tmp *= 1000;
                       else if (hashspeed.Contains("m"))
                           tmp *= 1000000;
                       else if (hashspeed.Contains("g"))
                           tmp *= 1000000000;
                   }
                   */

                    double speed = Double.Parse(hashspeed, CultureInfo.InvariantCulture);
                    BenchmarkAlgorithm.BenchmarkSpeed = speed * 1000;
                    BenchmarkSignalFinnished = true;
                }
            }
            return false;
        }

        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            CheckOutdata(outdata);
        }

        #endregion Decoupled benchmarking routines

        // TODO _currentMinerReadStatus
        public override async Task<APIData> GetSummaryAsync()
        {
            // CryptoNight does not have api bind port
            APIData mkxminerData = new APIData(MiningSetup.CurrentAlgorithmType)
            {
                Speed = 0
            };
            if (IsAPIReadException)
            {
                // check if running
                if (ProcessHandle == null)
                {
                    //_currentMinerReadStatus = MinerAPIReadStatus.RESTART;
                    Helpers.ConsolePrint(MinerTAG(), ProcessTag() + " Could not read data from hsrminer Proccess is null");
                    return null;
                }
                try
                {
                    var runningProcess = Process.GetProcessById(ProcessHandle.Id);
                }
                catch (ArgumentException ex)
                {
                    //_currentMinerReadStatus = MinerAPIReadStatus.RESTART;
                    Helpers.ConsolePrint(MinerTAG(), ProcessTag() + " Could not read data from hsrminer reason: " + ex.Message);
                    return null; // will restart outside
                }
                catch (InvalidOperationException ex)
                {
                    //_currentMinerReadStatus = MinerAPIReadStatus.RESTART;
                    Helpers.ConsolePrint(MinerTAG(), ProcessTag() + " Could not read data from hsrminer reason: " + ex.Message);
                    return null; // will restart outside
                }

                var totalSpeed = 0.0d;
                foreach (var miningPair in MiningSetup.MiningPairs)
                {
                    var algo = miningPair.Device.GetAlgorithm(MinerBaseType.hsrneoscrypt, AlgorithmType.NeoScrypt, AlgorithmType.NONE);
                    if (algo != null)
                    {
                        totalSpeed += algo.BenchmarkSpeed;
                        Helpers.ConsolePrint(MinerTAG(), ProcessTag() + " Could not read data from hsrminer. Used benchmark hashrate");
                    }
                }

                mkxminerData.Speed = totalSpeed;
                return mkxminerData;
            }

            //  return await GetSummaryCPU_hsrneoscryptAsync();
            return mkxminerData;
        }
    }
}