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
    public class hsrneoscrypt : Miner
    {
        private int benchmarkTimeWait = 11 * 60;

        public hsrneoscrypt() : base("hsrneoscrypt_NVIDIA")
        {
        }

        private bool benchmarkException
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

            IsAPIReadException = MiningSetup.MinerPath == MinerPaths.Data.hsrneoscrypt;

            /*
            string algo = "";
            string apiBind = "";
            if (!IsAPIReadException) {
                algo = "--algo=" + MiningSetup.MinerName;
                apiBind = " --api-bind=" + APIPort.ToString();
            }
            */
            /*
            LastCommandLine = algo +
                                  " --url=" + url +
                                  " --userpass=" + username + ":x " +
                                  apiBind + " " +
                                  ExtraLaunchParametersParser.ParseForMiningSetup(
                                                                MiningSetup,
                                                                DeviceType.NVIDIA) +
                                  " --devices ";
*/
            //add failover
            string alg = url.Substring(url.IndexOf("://") + 3, url.IndexOf(".") - url.IndexOf("://") - 3);
            string port = url.Substring(url.IndexOf(".com:") + 5, url.Length - url.IndexOf(".com:") - 5);
            /*
            LastCommandLine = algo +
                              " --url=" + url +
                              " --userpass=" + username + ":x " +
                              " --url stratum+tcp://" + alg + ".hk.nicehash.com:" + port +
                              " --userpass=" + username + ":x " +
                              " --url stratum+tcp://" + alg + ".in.nicehash.com:" + port +
                              " --userpass=" + username + ":x " +
                              " --url stratum+tcp://" + alg + ".jp.nicehash.com:" + port +
                              " --userpass=" + username + ":x " +
                              " --url stratum+tcp://" + alg + ".usa.nicehash.com:" + port +
                              " --userpass=" + username + ":x " +
                              " --url stratum+tcp://" + alg + ".br.nicehash.com:" + port +
                              " --userpass=" + username + ":x " +
                              " --url stratum+tcp://" + alg + ".eu.nicehash.com:" + port +
                              " --userpass=" + username + ":x " +
                              apiBind + " " +
                                  ExtraLaunchParametersParser.ParseForMiningSetup(
                                                                MiningSetup,
                                                                DeviceType.NVIDIA) +
                                  " --devices ";

            LastCommandLine += GetDevicesCommandString();
*/

            LastCommandLine = " --url=" + url +
                                  " --user=" + btcAdress +
                          " -p x " +
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

            if (ConfigManager.GeneralConfig.WorkerName.Length > 0)
                username += "." + ConfigManager.GeneralConfig.WorkerName.Trim();

            string CommandLine = " --url=" + url +
                                  " --user=" + Globals.DemoUser +
                          " -p x " +
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
            Helpers.ConsolePrint(MinerTAG(), outdata);
            if (benchmarkException)
            {
                if (outdata.Contains("speed is "))
                {
                    int st = outdata.IndexOf("speed is ");
                    int end = outdata.IndexOf("kH/s");
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

        public override async Task<APIData> GetSummaryAsync()
        {
            // CryptoNight does not have api bind port
            APIData hsrData = new APIData(MiningSetup.CurrentAlgorithmType);
            hsrData.Speed = 0;
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

                hsrData.Speed = totalSpeed;
                return hsrData;
            }

            //  return await GetSummaryCPU_hsrneoscryptAsync();
            return hsrData;
        }
    }
}