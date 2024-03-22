using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using zPoolMiner.Enums;
using zPoolMiner.Miners.Grouping;
using zPoolMiner.Miners.Parsing;

namespace zPoolMiner.Miners
{
    public class Excavator : Miner
    {

        private const double DevFee = 6.0;
        private class DeviceStat
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Speed_hps { get; set; }
        }

        private class Result
        {
            public bool Connected { get; set; }
            public double Interval_seconds { get; set; }
            public double Speed_hps { get; set; }
            public List<DeviceStat> Devices { get; set; }
            public double Accepted_per_minute { get; set; }
            public double Rejected_per_minute { get; set; }
        }

        private class JsonApiResponse
        {
            public string Method { get; set; }
            public Result Result { get; set; }
            public object Error { get; set; }
        }

        public Excavator()
            : base("excavator")
        {
            ConectionType = NHMConectionType.NONE;
            IsNeverHideMiningWindow = true;
        }

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
            LastCommandLine = GetDevicesCommandString() + " -a " + MiningSetup.MinerName + " -p " + ApiPort + " -s " + url + " -u " + username + ":" + worker + "";
            ProcessHandle = _Start();
        }

        protected override string GetDevicesCommandString()
        {
            List<MiningPair> CT_MiningPairs = new List<MiningPair>();
            string deviceStringCommand = " -cd ";
            int default_CT = 1;
            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.equihash)
            {
                default_CT = 2;
            }
            foreach (var nvidia_pair in MiningSetup.MiningPairs)
            {
                if (nvidia_pair.CurrentExtraLaunchParameters.Contains("-ct"))
                {
                    for (int i = 0; i < ExtraLaunchParametersParser.GetEqmCudaThreadCount(nvidia_pair); ++i)
                    {
                        deviceStringCommand += nvidia_pair.Device.ID + " ";
                        CT_MiningPairs.Add(nvidia_pair);
                    }
                }
                else
                { // use default default_CT for best performance
                    for (int i = 0; i < default_CT; ++i)
                    {
                        deviceStringCommand += nvidia_pair.Device.ID + " ";
                        CT_MiningPairs.Add(nvidia_pair);
                    }
                }
            }

            MiningSetup CT_MiningSetup = new MiningSetup(CT_MiningPairs);
            //deviceStringCommand += " " + ExtraLaunchParametersParser.ParseForMiningSetup(this.MiningSetup, DeviceType.NVIDIA);
            deviceStringCommand += " " + ExtraLaunchParametersParser.ParseForMiningSetup(CT_MiningSetup, DeviceType.NVIDIA);

            return deviceStringCommand;
        }

        // benchmark stuff

        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            string ret = " -a " + MiningSetup.MinerName + " -b " + time + " " + GetDevicesCommandString();
            return ret;
        }

        private const string TOTAL_MES = "Total measured:";

        protected override bool BenchmarkParseLine(string outdata)
        {
            if (outdata.Contains(TOTAL_MES))
            {
                try
                {
                    int speedStart = outdata.IndexOf(TOTAL_MES);
                    string speed = outdata.Substring(speedStart, outdata.Length - speedStart).Replace(TOTAL_MES, "");
                    var splitSrs = speed.Trim().Split(' ');
                    if (splitSrs.Length >= 2)
                    {
                        string speedStr = splitSrs[0];
                        string postfixStr = splitSrs[1];
                        double spd = Double.Parse(speedStr, CultureInfo.InvariantCulture);
                        if (postfixStr.Contains("kH/s"))
                            spd *= 1000;
                        else if (postfixStr.Contains("MH/s"))
                            spd *= 1000000;
                        else if (postfixStr.Contains("GH/s"))
                            spd *= 1000000000;

                        // wrong benchmark workaround over 3gh/s is considered false
                        if (MiningSetup.CurrentAlgorithmType == AlgorithmType.Pascal
                            && spd > 3.0d * 1000000000.0d
                            )
                        {
                            return false;
                        }

                        BenchmarkAlgorithm.BenchmarkSpeed = (spd) * (1.0 - DevFee * 0.01);
                        return true;
                    }
                }
                catch
                {
                }
            }
            return false;
        }

        public override async Task<APIData> GetSummaryAsync()
        {
            CurrentMinerReadStatus = MinerApiReadStatus.NONE;
            APIData ad = new APIData(MiningSetup.CurrentAlgorithmType);

            TcpClient client = null;
            JsonApiResponse resp = null;
            try
            {
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes("status\n");
                client = new TcpClient("127.0.0.1", ApiPort);
                NetworkStream nwStream = client.GetStream();
                await nwStream.WriteAsync(bytesToSend, 0, bytesToSend.Length);
                byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                int bytesRead = await nwStream.ReadAsync(bytesToRead, 0, client.ReceiveBufferSize);
                string respStr = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                resp = JsonConvert.DeserializeObject<JsonApiResponse>(respStr, Globals.JsonSettings);
                client.Close();
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint("ERROR", ex.Message);
            }

            if (resp != null && resp.Error == null)
            {
                ad.Speed = resp.Result.Speed_hps;
                CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
                if (ad.Speed == 0)
                {
                    CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;
                }
            }

            return ad;
        }

        protected override void _Stop(MinerStopType willswitch)
        {
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
        }

        protected override int GetMaxCooldownTimeInMilliseconds()
        {
            return 60 * 1000 * 5; // 5 minute max, whole waiting time 75seconds
        }

        // benchmark stuff

        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            CheckOutdata(outdata);
        }
    }
}