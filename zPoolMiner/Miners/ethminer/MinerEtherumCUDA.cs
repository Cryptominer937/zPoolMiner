using System.Collections.Generic;
using zPoolMiner.Enums;
using zPoolMiner.Miners.Parsing;

namespace zPoolMiner.Miners
{
    public class MinerEtherumCUDA : MinerEtherum
    {
        // reference to all MinerEtherumCUDA make sure to clear this after miner Stop
        // we make sure only ONE instance of MinerEtherumCUDA is running
        private static List<MinerEtherum> MinerEtherumCUDAList = new List<MinerEtherum>();

        public MinerEtherumCUDA()
            : base("MinerEtherumCUDA", "NVIDIA")
        {
            MinerEtherumCUDAList.Add(this);
        }

        ~MinerEtherumCUDA()
        {
            // remove from list
            MinerEtherumCUDAList.Remove(this);
        }

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

            Helpers.ConsolePrint(MinerTag(), "Starting MinerEtherumCUDA, checking existing MinerEtherumCUDA to stop");
            base.Start(url, btcAddress, worker, MinerEtherumCUDAList);
        }

        protected override string GetStartCommandStringPart(string url, string username)
        {
            return " --cuda"
                + " "
                + ExtraLaunchParametersParser.ParseForMiningSetup(
                                                    MiningSetup,
                                                    DeviceType.NVIDIA)
                + " -S " + url.Substring(14)
                + " -O " + username + ""
                + " --api-port " + ApiPort.ToString()
                + " --cuda-devices ";
        }

        protected override string GetBenchmarkCommandStringPart(Algorithm algorithm)
        {
            return " --benchmark-warmup 40 --benchmark-trial 20"
                + " "
                + ExtraLaunchParametersParser.ParseForMiningSetup(
                                                    MiningSetup,
                                                    DeviceType.NVIDIA)
                + " --cuda --cuda-devices ";
        }
    }
}