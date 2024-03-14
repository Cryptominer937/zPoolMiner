using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using zPoolMiner.Devices;
using zPoolMiner.Enums;
using zPoolMiner.Miners.Grouping;
using zPoolMiner.Miners.Parsing;

namespace zPoolMiner.Miners.equihash
{
    public class OptiminerZcashMiner : Miner
    {
        public OptiminerZcashMiner()
            : base("OptiminerZcashMiner")
        {
            ConectionType = NHMConectionType.NONE;
        }

        private class Stratum
        {
            public string Target { get; set; }
            public bool Connected { get; set; }
            public int Connection_failures { get; set; }
            public string Host { get; set; }
            public int Port { get; set; }
        }

        private class JsonApiResponse
        {
            public double uptime;
            public Dictionary<string, Dictionary<string, double>> solution_rate;
            public Dictionary<string, double> share;
            public Dictionary<string, Dictionary<string, double>> iteration_rate;
            public Stratum stratum;
        }

        // give some time or else it will crash
        private Stopwatch _startAPI = null;

        private bool _skipAPICheck = true;
        private int waitSeconds = 30;

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
            LastCommandLine = " " + GetDevicesCommandString() + " -m " + ApiPort + " -s " + url + " -u " + username + " -p " + worker;
            ProcessHandle = _Start();

            //
            _startAPI = new Stopwatch();
            _startAPI.Start();
        }

        protected override void _Stop(MinerStopType willswitch)
        {
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
        }

        protected override int GetMaxCooldownTimeInMilliseconds()
        {
            return 60 * 1000 * 5; // 5 minute max, whole waiting time 75seconds
        }

        protected override string GetDevicesCommandString()
        {
            string extraParams = ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.AMD);
            string deviceStringCommand = " -c " + ComputeDeviceManager.Avaliable.AMDOpenCLPlatformNum;
            deviceStringCommand += " ";
            List<string> ids = new List<string>();
            foreach (var mPair in MiningSetup.MiningPairs)
            {
                ids.Add("-d " + mPair.Device.ID.ToString());
            }
            deviceStringCommand += String.Join(" ", ids);

            return deviceStringCommand + extraParams;
        }

        public override async Task<APIData> GetSummaryAsync()
        {
            CurrentMinerReadStatus = MinerApiReadStatus.NONE;
            APIData ad = new APIData(MiningSetup.CurrentAlgorithmType);

            if (_skipAPICheck == false)
            {
                JsonApiResponse resp = null;
                try
                {
                    string DataToSend = GetHttpRequestNHMAgentStrin("");
                    string respStr = await GetAPIDataAsync(ApiPort, DataToSend, true);
                    if (respStr != null && respStr.Contains("{"))
                    {
                        int start = respStr.IndexOf("{");
                        if (start > -1)
                        {
                            string respStrJSON = respStr.Substring(start);
                            resp = JsonConvert.DeserializeObject<JsonApiResponse>(respStrJSON.Trim(), Globals.JsonSettings);
                        }
                    }
                    //Helpers.ConsolePrint("OptiminerZcashMiner API back:", respStr);
                }
                catch (Exception ex)
                {
                    Helpers.ConsolePrint("OptiminerZcashMiner", "GetSummary exception: " + ex.Message);
                }

                if (resp != null && resp.solution_rate != null)
                {
                    //Helpers.ConsolePrint("OptiminerZcashMiner API back:", "resp != null && resp.error == null");
                    const string total_key = "Total";
                    const string _5s_key = "5s";
                    if (resp.solution_rate.ContainsKey(total_key))
                    {
                        var total_solution_rate_dict = resp.solution_rate[total_key];
                        if (total_solution_rate_dict != null && total_solution_rate_dict.ContainsKey(_5s_key))
                        {
                            ad.Speed = total_solution_rate_dict[_5s_key];
                            CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
                        }
                    }
                    if (ad.Speed == 0)
                    {
                        CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;
                    }
                }
            }
            else if (_skipAPICheck && _startAPI.Elapsed.TotalSeconds > waitSeconds)
            {
                _startAPI.Stop();
                _skipAPICheck = false;
            }

            return ad;
        }

        // benchmark stuff

        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            int t = time / 9; // sgminer needs 9 times more than this miner so reduce benchmark speed
            string ret = " " + GetDevicesCommandString() + " --benchmark " + t;
            return ret;
        }

        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            CheckOutdata(outdata);
        }

        protected override bool BenchmarkParseLine(string outdata)
        {
            const string FIND = "Benchmark:";
            if (outdata.Contains(FIND))
            {
                int start = outdata.IndexOf("Benchmark:") + FIND.Length;
                string itersAndVars = outdata.Substring(start).Trim();
                var ar = itersAndVars.Split(new char[] { ' ' });
                if (ar.Length >= 4)
                {
                    // gets sols/s
                    BenchmarkAlgorithm.BenchmarkSpeed = Helpers.ParseDouble(ar[2]);
                    return true;
                }
            }
            return false;
        }
    }
}