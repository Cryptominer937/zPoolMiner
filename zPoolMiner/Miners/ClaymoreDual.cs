namespace zPoolMiner.Miners
{
    using System;
    using zPoolMiner.Configs;
    using zPoolMiner.Enums;

    /// <summary>
    /// Defines the <see cref="ClaymoreDual" />
    /// </summary>
    public class ClaymoreDual : ClaymoreBaseMiner
    {
        /// <summary>
        /// Defines the _LOOK_FOR_START
        /// </summary>
        private const string _LOOK_FOR_START = "ETH - Total Speed:";

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaymoreDual"/> class.
        /// </summary>
        /// <param name="secondaryAlgorithmType">The <see cref="AlgorithmType"/></param>
        public ClaymoreDual(AlgorithmType secondaryAlgorithmType)
            : base("ClaymoreDual", _LOOK_FOR_START)
        {
            ignoreZero = true;
            api_read_mult = 1000;
            ConectionType = NHMConectionType.STRATUM_TCP;
            SecondaryAlgorithmType = secondaryAlgorithmType;
        }

        // eth-only: 1%
        // eth-dual-mine: 2%
        /// <summary>
        /// The DevFee
        /// </summary>
        /// <returns>The <see cref="double"/></returns>
        protected override double DevFee()
        {
            return IsDual() ? 8.0 : 7.0;
        }

        // the short form the miner uses for secondary algo in cmd line and log
        /// <summary>
        /// The SecondaryShortName
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string SecondaryShortName()
        {
            switch (SecondaryAlgorithmType)
            {
                case AlgorithmType.Decred:
                    return "dcr";

                case AlgorithmType.Lbry:
                    return "lbc";

                case AlgorithmType.Pascal:
                    return "pasc";

                case AlgorithmType.Sia:
                    return "sc";

                case AlgorithmType.Blake2s:
                    return "b2s";
            }
            return "";
        }

        /// <summary>
        /// The SecondaryLookForStart
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        protected override string SecondaryLookForStart()
        {
            return (SecondaryShortName() + " - Total Speed:").ToLower();
        }

        /// <summary>
        /// The GET_MAX_CooldownTimeInMilliseconds
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        protected override int GetMaxCooldownTimeInMilliseconds()
        {
            return 90 * 1000; // 1.5 minute max, whole waiting time 75seconds
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
            string username = GetUsername(btcAddress, worker);

            string dualModeParams = "";
            if (!IsDual())
            {  // leave convenience param for non-dual entry
                foreach (var pair in MiningSetup.MiningPairs)
                {
                    if (pair.CurrentExtraLaunchParameters.Contains("-dual="))
                    {
                        AlgorithmType dual = AlgorithmType.NONE;
                        string coinP = "";
                        if (pair.CurrentExtraLaunchParameters.Contains("Decred"))
                        {
                            dual = AlgorithmType.Decred;
                            coinP = " -dcoin dcr ";
                        }
                        if (pair.CurrentExtraLaunchParameters.Contains("Siacoin"))
                        {
                            dual = AlgorithmType.Sia;
                            coinP = " -dcoin sc";
                        }
                        if (pair.CurrentExtraLaunchParameters.Contains("Lbry"))
                        {
                            dual = AlgorithmType.Lbry;
                            coinP = " -dcoin lbc ";
                        }
                        if (pair.CurrentExtraLaunchParameters.Contains("Pascal"))
                        {
                            dual = AlgorithmType.Pascal;
                            coinP = " -dcoin pasc ";
                        }
                        if (pair.CurrentExtraLaunchParameters.Contains("Blake2s"))
                        {
                            dual = AlgorithmType.Blake2s;
                            coinP = " -dcoin blake2s ";
                        }
                        if (dual != AlgorithmType.NONE)
                        {
                            string urlSecond = Globals.GetLocationURL(dual, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], ConectionType);
                            if (MiningSession.DONATION_SESSION)
                            {
                                if (urlSecond.Contains("zpool.ca"))
                                {
                                    btcAddress = Globals.DemoUser;
                                    worker = "c=DOGE,ID=Donation";
                                }
                                if (urlSecond.Contains("ahashpool.com"))
                                {
                                    btcAddress = Globals.DemoUser;
                                    worker = "c=DOGE,ID=Donation";
                                }
                                if (urlSecond.Contains("hashrefinery.com"))
                                {
                                    btcAddress = Globals.DemoUser;
                                    worker = "c=DOGE,ID=Donation";
                                }
                                if (urlSecond.Contains("nicehash.com"))
                                {
                                    btcAddress = Globals.DemoUser;
                                    worker = "c=DOGE,ID=Donation";
                                }
                                if (urlSecond.Contains("zergpool.com"))
                                {
                                    btcAddress = Globals.DemoUser;
                                    worker = "c=DOGE,ID=Donation";
                                }
                                if (urlSecond.Contains("blockmasters.co"))
                                {
                                    btcAddress = Globals.DemoUser;
                                    worker = "c=DOGE,ID=Donation";
                                }
                                if (urlSecond.Contains("blazepool.com"))
                                {
                                    btcAddress = Globals.DemoUser;
                                    worker = "c=DOGE,ID=Donation";
                                }
                                if (urlSecond.Contains("miningpoolhub.com"))
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
                                if (urlSecond.Contains("zpool.ca"))
                                {
                                    btcAddress = zPoolMiner.Globals.GetzpoolUser();
                                    worker = zPoolMiner.Globals.GetzpoolWorker();
                                }
                                if (urlSecond.Contains("ahashpool.com"))
                                {
                                    btcAddress = zPoolMiner.Globals.GetahashUser();
                                    worker = zPoolMiner.Globals.GetahashWorker();
                                }
                                if (urlSecond.Contains("hashrefinery.com"))
                                {
                                    btcAddress = zPoolMiner.Globals.GethashrefineryUser();
                                    worker = zPoolMiner.Globals.GethashrefineryWorker();
                                }
                                if (urlSecond.Contains("nicehash.com"))
                                {
                                    btcAddress = zPoolMiner.Globals.GetnicehashUser();
                                    worker = zPoolMiner.Globals.GetnicehashWorker();
                                }
                                if (urlSecond.Contains("zergpool.com"))
                                {
                                    btcAddress = zPoolMiner.Globals.GetzergUser();
                                    worker = zPoolMiner.Globals.GetzergWorker();
                                }
                                if (urlSecond.Contains("minemoney.co"))
                                {
                                    btcAddress = zPoolMiner.Globals.GetminemoneyUser();
                                    worker = zPoolMiner.Globals.GetminemoneyWorker();
                                }
                                if (urlSecond.Contains("blazepool.com"))
                                {
                                    btcAddress = zPoolMiner.Globals.GetblazepoolUser();
                                    worker = zPoolMiner.Globals.GetblazepoolWorker();
                                }
                                if (urlSecond.Contains("blockmasters.co"))
                                {
                                    btcAddress = zPoolMiner.Globals.GetblockmunchUser();
                                    worker = zPoolMiner.Globals.GetblockmunchWorker();
                                }
                                if (urlSecond.Contains("miningpoolhub.com"))
                                {
                                    btcAddress = zPoolMiner.Globals.GetMPHUser();
                                    worker = zPoolMiner.Globals.GetMPHWorker();
                                }
                            }
                            dualModeParams = String.Format(" {0} -dpool {1} -dwal {2}", coinP, urlSecond, username);
                            break;
                        }
                    }
                }
            }
            else
            {
                string urlSecond = Globals.GetLocationURL(SecondaryAlgorithmType, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], ConectionType);
                if (MiningSession.DONATION_SESSION)
                {
                    if (urlSecond.Contains("zpool.ca"))
                    {
                        btcAddress = Globals.DemoUser;
                        worker = "c=DOGE,ID=Donation";
                    }
                    if (urlSecond.Contains("ahashpool.com"))
                    {
                        btcAddress = Globals.DemoUser;
                        worker = "c=DOGE,ID=Donation";
                    }
                    if (urlSecond.Contains("hashrefinery.com"))
                    {
                        btcAddress = Globals.DemoUser;
                        worker = "c=DOGE,ID=Donation";
                    }
                    if (urlSecond.Contains("nicehash.com"))
                    {
                        btcAddress = Globals.DemoUser;
                        worker = "c=DOGE,ID=Donation";
                    }
                    if (urlSecond.Contains("zergpool.com"))
                    {
                        btcAddress = Globals.DemoUser;
                        worker = "c=DOGE,ID=Donation";
                    }
                    if (urlSecond.Contains("blockmasters.co"))
                    {
                        btcAddress = Globals.DemoUser;
                        worker = "c=DOGE,ID=Donation";
                    }
                    if (urlSecond.Contains("blazepool.com"))
                    {
                        btcAddress = Globals.DemoUser;
                        worker = "c=DOGE,ID=Donation";
                    }
                    if (urlSecond.Contains("miningpoolhub.com"))
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
                    if (urlSecond.Contains("zpool.ca"))
                    {
                        btcAddress = zPoolMiner.Globals.GetzpoolUser();
                        worker = zPoolMiner.Globals.GetzpoolWorker();
                    }
                    if (urlSecond.Contains("ahashpool.com"))
                    {
                        btcAddress = zPoolMiner.Globals.GetahashUser();
                        worker = zPoolMiner.Globals.GetahashWorker();
                    }
                    if (urlSecond.Contains("hashrefinery.com"))
                    {
                        btcAddress = zPoolMiner.Globals.GethashrefineryUser();
                        worker = zPoolMiner.Globals.GethashrefineryWorker();
                    }
                    if (urlSecond.Contains("nicehash.com"))
                    {
                        btcAddress = zPoolMiner.Globals.GetnicehashUser();
                        worker = zPoolMiner.Globals.GetnicehashWorker();
                    }
                    if (urlSecond.Contains("zergpool.com"))
                    {
                        btcAddress = zPoolMiner.Globals.GetzergUser();
                        worker = zPoolMiner.Globals.GetzergWorker();
                    }
                    if (urlSecond.Contains("minemoney.co"))
                    {
                        btcAddress = zPoolMiner.Globals.GetminemoneyUser();
                        worker = zPoolMiner.Globals.GetminemoneyWorker();
                    }
                    if (urlSecond.Contains("blazepool.com"))
                    {
                        btcAddress = zPoolMiner.Globals.GetblazepoolUser();
                        worker = zPoolMiner.Globals.GetblazepoolWorker();
                    }
                    if (urlSecond.Contains("blockmasters.co"))
                    {
                        btcAddress = zPoolMiner.Globals.GetblockmunchUser();
                        worker = zPoolMiner.Globals.GetblockmunchWorker();
                    }
                    if (urlSecond.Contains("miningpoolhub.com"))
                    {
                        btcAddress = zPoolMiner.Globals.GetMPHUser();
                        worker = zPoolMiner.Globals.GetMPHWorker();
                    }
                }
                dualModeParams = String.Format(" -dcoin {0} -dpool {1} -dwal {2} -dpsw " + worker, SecondaryShortName(), urlSecond, btcAddress);
            }

            return " "
                + GetDevicesCommandString()
                + String.Format("  -epool {0} -ewal {1} -mport 127.0.0.1:{2} -esm 3 -epsw x -allpools 1", url, username, ApiPort)
                + dualModeParams;
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
            string username = GetUsername(btcAddress, worker);
            LastCommandLine = GetStartCommand(url, btcAddress, worker) + " -dbg -1";
            ProcessHandle = _Start();
        }

        /// <summary>
        /// The DeviceCommand
        /// </summary>
        /// <param name="amdCount">The <see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        protected override string DeviceCommand(int amdCount = 1)
        {
            // If no AMD cards loaded, instruct CD to only regard NV cards for indexing
            // This will allow proper indexing if AMD GPUs or APUs are present in the system but detection disabled
            string ret = (amdCount == 0) ? " -platform 2" : "";
            return ret + base.DeviceCommand(amdCount);
        }

        // benchmark stuff
        /// <summary>
        /// The BenchmarkCreateCommandLine
        /// </summary>
        /// <param name="algorithm">The <see cref="Algorithm"/></param>
        /// <param name="time">The <see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            // network stub
            string url = Globals.GetLocationURL(algorithm.CryptoMiner937ID, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], ConectionType);
            // demo for benchmark
            string btcAddress = "";
            string worker = "";
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
            string ret = GetStartCommand(url, btcAddress, worker);
            // local benhcmark
            if (!IsDual())
            {
                benchmarkTimeWait = time;
                return ret + "  -benchmark 1";
            }
            else
            {
                benchmarkTimeWait = Math.Max(60, Math.Min(120, time * 3));  // dual seems to stop mining after this time if redirect output is true
                return ret;  // benchmark 1 does not output secondary speeds
            }
        }
    }
}