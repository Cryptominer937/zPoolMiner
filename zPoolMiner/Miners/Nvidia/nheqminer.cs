namespace zPoolMiner.Miners
{
    using System.Diagnostics;
    using zPoolMiner.Enums;
    using zPoolMiner.Miners.Parsing;

    /// <summary>
    /// Defines the <see cref="Nheqminer" />
    /// </summary>
    public class Nheqminer : NheqBase
    {
        private const double DevFee = 6.0;
        /// <summary>
        /// Initializes a new instance of the <see cref="Nheqminer"/> class.
        /// </summary>
        public Nheqminer()
            : base("nheqminer")
        {
            ConectionType = NHMConectionType.NONE;
        }

        // CPU aff set from NHM
        /// <summary>
        /// The _Start
        /// </summary>
        /// <returns>The <see cref="HashKingsProcess"/></returns>
        protected override HashKingsProcess _Start()
        {
            HashKingsProcess P = base._Start();
            if (CPU_Setup.IsInit && P != null)
            {
                var AffinityMask = CPU_Setup.MiningPairs[0].Device.AffinityMask;
                if (AffinityMask != 0)
                {
                    CPUID.AdjustAffinity(P.Id, AffinityMask);
                }
            }

            return P;
        }

        /// <summary>
        /// The Start
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="btcAddress">The <see cref="string"/></param>
        /// <param name="worker">The <see cref="string"/></param>
        public override void Start(string url, string btcAddress, string worker)
        {
            string username = GetUsername(btcAddress, worker);
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
            LastCommandLine = GetDevicesCommandString() + " -a " + ApiPort + " -l " + url + " -u " + username + " -p " + worker +"";
            ProcessHandle = _Start();
        }

        /// <summary>
        /// The GetDevicesCommandString
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        protected override string GetDevicesCommandString()
        {
            string deviceStringCommand = " ";

            if (CPU_Setup.IsInit)
            {
                deviceStringCommand += " " + ExtraLaunchParametersParser.ParseForMiningSetup(CPU_Setup, DeviceType.CPU);
            }
            else
            {
                // disable CPU
                deviceStringCommand += " -t 0 ";
            }

            if (NVIDIA_Setup.IsInit)
            {
                deviceStringCommand += " -cd ";
                foreach (var nvidia_pair in NVIDIA_Setup.MiningPairs)
                {
                    deviceStringCommand += nvidia_pair.Device.ID + " ";
                }
                deviceStringCommand += " " + ExtraLaunchParametersParser.ParseForMiningSetup(NVIDIA_Setup, DeviceType.NVIDIA);
            }

            if (AMD_Setup.IsInit)
            {
                deviceStringCommand += " -op " + AMD_OCL_PLATFORM.ToString();
                deviceStringCommand += " -od ";
                foreach (var amd_pair in AMD_Setup.MiningPairs)
                {
                    deviceStringCommand += amd_pair.Device.ID + " ";
                }
                deviceStringCommand += " " + ExtraLaunchParametersParser.ParseForMiningSetup(AMD_Setup, DeviceType.AMD);
            }

            return deviceStringCommand;
        }

        // benchmark stuff
        /// <summary>
        /// The BenchmarkStartProcess
        /// </summary>
        /// <param name="CommandLine">The <see cref="string"/></param>
        /// <returns>The <see cref="Process"/></returns>
        protected override Process BenchmarkStartProcess(string CommandLine)
        {
            Process BenchmarkHandle = base.BenchmarkStartProcess(CommandLine);

            if (CPU_Setup.IsInit && BenchmarkHandle != null)
            {
                var AffinityMask = CPU_Setup.MiningPairs[0].Device.AffinityMask;
                if (AffinityMask != 0)
                {
                    CPUID.AdjustAffinity(BenchmarkHandle.Id, AffinityMask);
                }
            }

            return BenchmarkHandle;
        }

        /// <summary>
        /// The BenchmarkParseLine
        /// </summary>
        /// <param name="outdata">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        protected override bool BenchmarkParseLine(string outdata)
        {
            if (outdata.Contains(Iter_PER_SEC))
            {
                curSpeed = GetNumber(outdata, "Speed: ", Iter_PER_SEC) * SolMultFactor;
            }
            if (outdata.Contains(Sols_PER_SEC))
            {
                var sols = GetNumber(outdata, "Speed: ", Sols_PER_SEC);
                if (sols > 0)
                {
                    BenchmarkAlgorithm.BenchmarkSpeed = (curSpeed) *(1.0 - DevFee * 0.01);
                    return true;
                }
            }
            return false;
        }
    }
}
