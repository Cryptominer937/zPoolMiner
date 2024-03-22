namespace zPoolMiner.Miners
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Threading.Tasks;
    using zPoolMiner.Enums;
    using zPoolMiner.Miners.Grouping;
    using zPoolMiner.Miners.Parsing;

    /// <summary>
    /// Defines the <see cref="Ccminer" />
    /// </summary>
    public class Ccminer : Miner
    {

        private const double DevFee = 5.0;
        /// <summary>
        /// Initializes a new instance of the <see cref="Ccminer"/> class.
        /// </summary>
        public Ccminer() : base("ccminer_NVIDIA")
        {
        }

        // cryptonight benchmark exception
        // cryptonight benchmark exception        /// <summary>
        /// Defines the _cryptonightTotalCount
        /// </summary>
        private int _cryptonightTotalCount = 0;

        /// <summary>
        /// Defines the _cryptonightTotal
        /// </summary>
        private double _cryptonightTotal = 0;

        /// <summary>
        /// Defines the _cryptonightTotalDelim
        /// </summary>
        private const int _cryptonightTotalDelim = 2;

        /// <summary>
        /// Gets a value indicating whether BenchmarkException
        /// </summary>
        private bool BenchmarkException
        {
            get
            {
                return MiningSetup.MinerPath == MinerPaths.Data.NONE;
            }
        }

        /// <summary>
        /// The GET_MAX_CooldownTimeInMilliseconds
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        protected override int GetMaxCooldownTimeInMilliseconds()
        {
            return 60 * 1000 * 3; // 1 minute max, whole waiting time 75seconds
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
                    worker = "c=BTC,ID=Donation";
                }
                if (url.Contains("ahashpool.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=BTC,ID=Donation";

                }
                if (url.Contains("hashrefinery.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=BTC,ID=Donation";

                }
                if (url.Contains("nicehash.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=BTC,ID=Donation";

                }
                if (url.Contains("zergpool.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=BTC,ID=Donation";

                }
                if (url.Contains("blockmasters.co"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=BTC,ID=Donation";

                }
                if (url.Contains("blazepool.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=BTC,ID=Donation";
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
                    worker = zPoolMiner.Globals.GetzergWorker() + "";

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
            string username = GetUsername(btcAddress, worker);

            IsApiReadException = MiningSetup.MinerPath == MinerPaths.Data.NONE;

            string algo = "";
            string apiBind = "";
            if (!IsApiReadException)
            {
                algo = "--algo=" + MiningSetup.MinerName;
                apiBind = " --api-bind=" + ApiPort.ToString();
            }

            LastCommandLine = algo +
                                  " --url=" + url + " --userpass=" + username + ":" + worker + " " + apiBind + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA) + " --devices ";

            LastCommandLine += GetDevicesCommandString();

            ProcessHandle = _Start();
        }

        /// <summary>
        /// The _Stop
        /// </summary>
        /// <param name="willswitch">The <see cref="MinerStopType"/></param>
        protected override void _Stop(MinerStopType willswitch)
        {
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
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
            string timeLimit = (BenchmarkException) ? "" : " --time-limit " + time.ToString();
            string CommandLine = " --algo=" + algorithm.MinerName +
                              " --benchmark" +
                              timeLimit + " " +
                              ExtraLaunchParametersParser.ParseForMiningSetup(
                                                                MiningSetup,
                                                                DeviceType.NVIDIA) +
                              " --devices ";

            CommandLine += GetDevicesCommandString();

            // cryptonight exception helper variables
            _cryptonightTotalCount = BenchmarkTimeInSeconds / _cryptonightTotalDelim;
            _cryptonightTotal = 0.0d;

            return CommandLine;
        }

        /// <summary>
        /// The BenchmarkParseLine
        /// </summary>
        /// <param name="outdata">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        protected override bool BenchmarkParseLine(string outdata)
        {
            // cryptonight exception
            if (BenchmarkException)
            {
                int speedLength = (BenchmarkAlgorithm.CryptoMiner937ID == AlgorithmType.cryptonight) ? 6 : 8;
                if (outdata.Contains("Total: "))
                {
                    int st = outdata.IndexOf("Total:") + 7;
                    int len = outdata.Length - speedLength - st;

                    string parse = outdata.Substring(st, len).Trim();
                    Double.TryParse(parse, NumberStyles.Any, CultureInfo.InvariantCulture, out double tmp);

                    // save speed
                    int i = outdata.IndexOf("Benchmark:");
                    int k = outdata.IndexOf("/s");
                    string hashspeed = outdata.Substring(i + 11, k - i - 9);
                    int b = hashspeed.IndexOf(" ");
                    if (hashspeed.Contains("kH/s"))
                        tmp *= 1000;
                    else if (hashspeed.Contains("MH/s"))
                        tmp *= 1000000;
                    else if (hashspeed.Contains("GH/s"))
                        tmp *= 1000000000;

                    _cryptonightTotal += tmp;
                    _cryptonightTotalCount--;
                }
                if (_cryptonightTotalCount <= 0)
                {
                    double spd = _cryptonightTotal / (BenchmarkTimeInSeconds / _cryptonightTotalDelim);
                    BenchmarkAlgorithm.BenchmarkSpeed = (spd) * (1.0 - DevFee * 0.01);
                    BenchmarkSignalFinnished = true;
                }
            }

            double lastSpeed = BenchmarkParseLine_cpu_ccminer_extra(outdata);
            if (lastSpeed > 0.0d)
            {
                BenchmarkAlgorithm.BenchmarkSpeed = (lastSpeed) * (1.0 - DevFee * 0.01);
                return true;
            }

            if (double.TryParse(outdata, out lastSpeed))
            {
                BenchmarkAlgorithm.BenchmarkSpeed = (lastSpeed) * (1.0 - DevFee * 0.01);
                return true;
            }
            return false;
        }

        /// <summary>
        /// The BenchmarkOutputErrorDataReceivedImpl
        /// </summary>
        /// <param name="outdata">The <see cref="string"/></param>
        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            CheckOutdata(outdata);
        }

        /// <summary>
        /// The GetSummaryAsync
        /// </summary>
        /// <returns>The <see cref="Task{APIData}"/></returns>
        public override async Task<APIData> GetSummaryAsync()
        {
            // cryptonight does not have api bind port
            if (IsApiReadException)
            {
                // check if running
                if (ProcessHandle == null)
                {
                    CurrentMinerReadStatus = MinerApiReadStatus.RESTART;
                    Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Could not read data from cryptonight Proccess is null");
                    return null;
                }
                try
                {
                    var runningProcess = Process.GetProcessById(ProcessHandle.Id);
                }
                catch (ArgumentException ex)
                {
                    CurrentMinerReadStatus = MinerApiReadStatus.RESTART;
                    Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Could not read data from cryptonight reason: " + ex.Message);
                    return null; // will restart outside
                }
                catch (InvalidOperationException ex)
                {
                    CurrentMinerReadStatus = MinerApiReadStatus.RESTART;
                    Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Could not read data from cryptonight reason: " + ex.Message);
                    return null; // will restart outside
                }

                var totalSpeed = 0.0d;
                foreach (var miningPair in MiningSetup.MiningPairs)
                {
                    var algo = miningPair.Device.GetAlgorithm(MinerBaseType.ccminer, AlgorithmType.cryptonight, AlgorithmType.NONE);
                    if (algo != null)
                    {
                        totalSpeed += algo.BenchmarkSpeed;
                    }
                }

                APIData cryptonightData = new APIData(MiningSetup.CurrentAlgorithmType)
                {
                    Speed = totalSpeed
                };
                CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
                // check if speed zero
                if (cryptonightData.Speed == 0) CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;
                return cryptonightData;
            }
            return await GetSummaryCPU_CCMINERAsync();
        }
    }
}
