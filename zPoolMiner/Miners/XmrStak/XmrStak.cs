namespace zPoolMiner.Miners
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using zPoolMiner.Configs;
    using zPoolMiner.Enums;

    /// <summary>
    /// Defines the <see cref="XmrStak" />
    /// </summary>
    public abstract class XmrStak : Miner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmrStak"/> class.
        /// </summary>
        /// <param name="name">The <see cref="string"/></param>
        protected XmrStak(string name)
            : base(name)
        {
            ConectionType = NHMConectionType.NONE;
            IsNeverHideMiningWindow = true;
        }

        /// <summary>
        /// The PrepareConfigFile
        /// </summary>
        /// <param name="pool">The <see cref="string"/></param>
        /// <param name="wallet">The <see cref="string"/></param>
        protected abstract void PrepareConfigFile(string pool, string wallet);

        /// <summary>
        /// The GetConfigFileName
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        protected string GetConfigFileName()
        {
            var ids = MiningSetup.MiningPairs.Select(pair => pair.Device.ID).ToList();
            return $"config_{string.Join(",", ids)}.txt";
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
        /// The GetSummaryAsync
        /// </summary>
        /// <returns>The <see cref="Task{APIData}"/></returns>
        public override async Task<APIData> GetSummaryAsync()
        {
            return await GetSummaryCPUAsync("api.json", true);
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
            return second == 0x7d && last == 0x7d;
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
                Helpers.ConsolePrint(MinerTAG(), "MiningSetup is not initialized exiting Start()");
                return;
            }
            string username = GetUsername(btcAddress, worker);
            LastCommandLine = GetConfigFileName();

            PrepareConfigFile(url, username);

            ProcessHandle = _Start();
        }

        /// <summary>
        /// The BenchmarkCreateCommandLine
        /// </summary>
        /// <param name="algorithm">The <see cref="Algorithm"/></param>
        /// <param name="time">The <see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            string url = Globals.GetLocationURL(algorithm.CryptoMiner937ID, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], ConectionType);
            PrepareConfigFile(url, Globals.DemoUser);
            return "benchmark_mode " + GetConfigFileName();
        }

        /// <summary>
        /// The BenchmarkParseLine
        /// </summary>
        /// <param name="outdata">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        protected override bool BenchmarkParseLine(string outdata)
        {
            if (outdata.Contains("Total:"))
            {
                string toParse = outdata.Substring(outdata.IndexOf("Total:")).Replace("Total:", "").Trim();
                var strings = toParse.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var s in strings)
                {
                    if (double.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out double lastSpeed))
                    {
                        Helpers.ConsolePrint("BENCHMARK " + MinerTAG(), "double.TryParse true. Last speed is" + lastSpeed.ToString());
                        BenchmarkAlgorithm.BenchmarkSpeed = Helpers.ParseDouble(s);
                        return true;
                    }
                }
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
    }
}
