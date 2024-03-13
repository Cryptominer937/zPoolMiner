using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using zPoolMiner.Configs;
using zPoolMiner.Enums;
using zPoolMiner.Miners.Grouping;
using zPoolMiner.Miners.Parsing;

namespace zPoolMiner.Miners
{
    public class Hsrneoscrypt_hsr : Miner
    {
        private int benchmarkTimeWait = 11 * 60;
        
        private const double DevFee = 1.0;
        
        public Hsrneoscrypt_hsr() : base("hsrneoscrypt_NVIDIA")
        {
        }

        private bool BenchmarkException
        {
            get
            {
                return MiningSetup.MinerPath == MinerPaths.Data.hsrneoscrypt_hsr;
            }
        }

        protected override int GET_MAX_CooldownTimeInMilliseconds()
        {
            if (this.MiningSetup.MinerPath == MinerPaths.Data.hsrneoscrypt_hsr)
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

            IsAPIReadException = MiningSetup.MinerPath == MinerPaths.Data.hsrneoscrypt_hsr;

            LastCommandLine = " --url=" + url +
                                  " --user=" + btcAdress +
                          " -p " + worker +
                                  ExtraLaunchParametersParser.ParseForMiningSetup(
                                                                MiningSetup,
                                                                DeviceType.NVIDIA) +
                                  " --devices ";
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

            //if (ConfigManager.GeneralConfig.WorkerName.Length > 0)
            //username += "." + ConfigManager.GeneralConfig.WorkerName.Trim();

            string CommandLine = " --url=" + url +
                                  " --user=" + Globals.DemoUser +
                          " -p c=BTC,ID=Benchmark " +
                                  ExtraLaunchParametersParser.ParseForMiningSetup(
                                                                MiningSetup,
                                                                DeviceType.NVIDIA) +
                                  " --devices ";
            CommandLine += GetDevicesCommandString();

            Helpers.ConsolePrint(MinerTAG(), CommandLine);

            return CommandLine;
        }

        protected override bool BenchmarkParseLine(string outdata)
        {
            string hashSpeed = "";
            int kspeed = 1;
            Helpers.ConsolePrint(MinerTAG(), outdata);
            if (BenchmarkException)
            {
                if (outdata.Contains("speed is "))
                {
                    int st = outdata.IndexOf("speed is ");
                    int k = outdata.IndexOf("H/s");
                    if (outdata.Contains("kH/s"))
                    {
                        hashSpeed = outdata.Substring(st + 9, k - st - 10);
                        kspeed = 1000;
                    }
                    if (outdata.Contains("MH/s"))
                    {
                        hashSpeed = outdata.Substring(st + 9, k - st - 10);
                        kspeed = 1000000;
                    }

                    double speed = Double.Parse(hashSpeed, CultureInfo.InvariantCulture);
                    BenchmarkAlgorithm.BenchmarkSpeed = (speed * kspeed) * (1.0 - DevFee * 0.01);
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

        public override async Task<APIData> GetSummaryAsync()
        {
            // CryptoNight does not have api bind port
            APIData hsrData = new APIData(MiningSetup.CurrentAlgorithmType)
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
                    var algo = miningPair.Device.GetAlgorithm(MinerBaseType.hsrneoscrypt_hsr, AlgorithmType.Hsr, AlgorithmType.NONE);
                    if (algo != null)
                    {
                        totalSpeed += algo.BenchmarkSpeed;
                        Helpers.ConsolePrint(MinerTAG(), ProcessTag() + " Could not read data from hsrminer. Used benchmark hashrate");
                    }
                }

                hsrData.Speed = totalSpeed;
                return hsrData;
            }

            //  return await GetSummaryCPU_hsrneoscryptAsync();
            return hsrData;
        }
    }
}
