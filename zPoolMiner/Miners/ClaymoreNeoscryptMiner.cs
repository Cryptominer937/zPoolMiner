using System;
using System.Collections.Generic;
using zPoolMiner.Configs;
using zPoolMiner.Enums;
using zPoolMiner.Miners.Grouping;
using zPoolMiner.Miners.Parsing;

namespace zPoolMiner.Miners
{
    public class ClaymoreNeoscryptMiner : ClaymoreBaseMiner
    {

        private bool isOld;

        const string _LOOK_FOR_START = "NS - Total Speed:";
        const string _LOOK_FOR_START_OLD = "hashrate =";
        public ClaymoreNeoscryptMiner()
            : base("ClaymoreNeoscryptMiner", _LOOK_FOR_START)
        {
        }

        protected override double DevFee()
        {
            return 5.0;
        }

        protected override string GetDevicesCommandString()
        {
            if (!isOld) return base.GetDevicesCommandString();

            string extraParams = ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.AMD);
            string deviceStringCommand = "";
            List<string> ids = new List<string>();
            foreach (var mPair in MiningSetup.MiningPairs)
            {
                var id = mPair.Device.ID;
                ids.Add(id.ToString());
            }
            deviceStringCommand += String.Join("", ids);

            return deviceStringCommand + extraParams;
        }

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
            url = Globals.GetLocationURL(AlgorithmType.NeoScrypt, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], NHMConectionType.STRATUM_TCP);
            LastCommandLine = " " + GetDevicesCommandString() + " -mport -" + ApiPort + " -pool " + url +
                              " -wal " + btcAddress + " -psw " + worker + " -dbg -1 -ftime 10 -retrydelay 5";


            ProcessHandle = _Start();
        }
        // benchmark stuff
        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            benchmarkTimeWait = time; // Takes longer as of v10
                                      // network workaround
            string url = Globals.GetLocationURL(algorithm.CryptoMiner937ID, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], NHMConectionType.STRATUM_TCP);
            string btcAddress = "";
            string worker = "";
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
            // demo for benchmark
            string username = Globals.DemoUser;
            if (ConfigManager.GeneralConfig.WorkerName.Length > 0)
                username += "." + ConfigManager.GeneralConfig.WorkerName.Trim();
            string ret;
            ret = " " + GetDevicesCommandString() + " -mport -" + ApiPort + " -pool " + url + " -wal " +
                         btcAddress + " -psw " + worker;
            return ret;
        }
    }
}
