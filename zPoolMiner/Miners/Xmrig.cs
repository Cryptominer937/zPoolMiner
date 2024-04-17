namespace zPoolMiner.Miners
{
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using zPoolMiner.Configs;
    using zPoolMiner.Enums;
    using zPoolMiner.Miners.Parsing;

    /// <summary>
    /// Defines the <see cref="Xmrig" />
    /// </summary>
    public class Xmrig : Miner
    {
        /// <summary>
        /// Defines the _benchmarkTimeWait
        /// </summary>
        private int _benchmarkTimeWait = 120;

        /// <summary>
        /// Defines the _lookForStart
        /// </summary>
        private const string _lookForStart = "speed 2.5s/60s/15m";

        /// <summary>
        /// Defines the _lookForEnd
        /// </summary>
        private const string _lookForEnd = "h/s max";

        /// <summary>
        /// Initializes a new instance of the <see cref="Xmrig"/> class.
        /// </summary>
        public Xmrig() : base("Xmrig")
        {
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
            LastCommandLine = GetStartCommand(url, btcAddress, worker);
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
            var extras = ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.CPU);
            return $" -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --api-port {ApiPort}";
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
            return 60 * 1000 * 5;  // 5 min
        }

        /// <summary>
        /// The GetSummaryAsync
        /// </summary>
        /// <returns>The <see cref="Task{APIData}"/></returns>
        public override async Task<ApiData> GetSummaryAsync()
        {
            return await GetSummaryCPUAsync();
        }

        /// <summary>
        /// The IsApiEof
        /// </summary>
        /// <param name="third">The <see cref="byte"/></param>
        /// <param name="second">The <see cref="byte"/></param>
        /// <param name="last">The <see cref="byte"/></param>
        /// <returns>The <see cref="bool"/></returns>
        protected override bool IsApiEof(byte third, byte second, byte last)
        {
            return third == 0x7d && second == 0xa && last == 0x7d;
        }

        /// <summary>
        /// The BenchmarkCreateCommandLine
        /// </summary>
        /// <param name="algorithm">The <see cref="Algorithm"/></param>
        /// <param name="time">The <see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            var server = Globals.GetLocationURL(algorithm.CryptoMiner937ID,
                Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation],
                ConectionType);
            _benchmarkTimeWait = time;
            string btcAddress = "";
            string worker = "";
            if (server.Contains("zpool.ca"))
            {
                btcAddress = Globals.DemoUser;
                worker = "c=DOGE,ID=Donation";
            }
            if (server.Contains("ahashpool.com"))
            {
                btcAddress = Globals.DemoUser;
                worker = "c=DOGE,ID=Donation";
            }
            if (server.Contains("hashrefinery.com"))
            {
                btcAddress = Globals.DemoUser;
                worker = "c=DOGE,ID=Donation";
            }
            if (server.Contains("nicehash.com"))
            {
                btcAddress = Globals.DemoUser;
                worker = "c=DOGE,ID=Donation";
            }
            if (server.Contains("zergpool.com"))
            {
                btcAddress = Globals.DemoUser;
                worker = "c=DOGE,ID=Donation";
            }
            if (server.Contains("blockmasters.co"))
            {
                btcAddress = Globals.DemoUser;
                worker = "c=DOGE,ID=Donation";
            }
            if (server.Contains("blazepool.com"))
            {
                btcAddress = Globals.DemoUser;
                worker = "c=DOGE,ID=Donation";
            }
            if (server.Contains("miningpoolhub.com"))
            {
                btcAddress = "cryptominer.Devfee";
                worker = "x";
            }
            return GetStartCommand(server, btcAddress, worker)
                + " -l benchmark_log.txt --print-time=2";
        }

        /// <summary>
        /// The BenchmarkThreadRoutine
        /// </summary>
        /// <param name="CommandLine">The <see cref="object"/></param>
        protected override void BenchmarkThreadRoutine(object CommandLine)
        {
            BenchmarkThreadRoutineAlternate(CommandLine, _benchmarkTimeWait);
        }

        /// <summary>
        /// The ProcessBenchLinesAlternate
        /// </summary>
        /// <param name="lines">The <see cref="string[]"/></param>
        protected override void ProcessBenchLinesAlternate(string[] lines)
        {
            // Xmrig reports 2.5s and 60s averages, so prefer to use 60s values for benchmark
            // but fall back on 2.5s values if 60s time isn't hit
            var twoSecTotal = 0d;
            var sixtySecTotal = 0d;
            var twoSecCount = 0;
            var sixtySecCount = 0;

            foreach (var line in lines)
            {
                BenchLines.Add(line);
                var lineLowered = line.ToLower();
                if (lineLowered.Contains(_lookForStart))
                {
                    var speeds = Regex.Match(lineLowered, $"{_lookForStart} (.+?) {_lookForEnd}").Groups[1].Value.Split();
                    if (double.TryParse(speeds[1], out var sixtySecSpeed))
                    {
                        sixtySecTotal += sixtySecSpeed;
                        ++sixtySecCount;
                    }
                    else if (double.TryParse(speeds[0], out var twoSecSpeed))
                    {
                        // Store 2.5s data in case 60s is never reached
                        twoSecTotal += twoSecSpeed;
                        ++twoSecCount;
                    }
                }
            }

            if (sixtySecCount > 0 && sixtySecTotal > 0)
            {
                // Run iff 60s averages are reported
                BenchmarkAlgorithm.BenchmarkSpeed = sixtySecTotal / sixtySecCount;
            }
            else if (twoSecCount > 0)
            {
                // Run iff no 60s averages are reported but 2.5s are
                BenchmarkAlgorithm.BenchmarkSpeed = twoSecTotal / twoSecCount;
            }
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
        /// The BenchmarkParseLine
        /// </summary>
        /// <param name="outdata">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        protected override bool BenchmarkParseLine(string outdata)
        {
            Helpers.ConsolePrint(MinerTag(), outdata);
            return false;
        }
    }
}