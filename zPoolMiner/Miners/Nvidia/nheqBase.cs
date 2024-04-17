using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using zPoolMiner.Devices;
using zPoolMiner.Enums;
using zPoolMiner.Miners.Grouping;

namespace zPoolMiner.Miners
{
    public abstract class NheqBase : Miner
    {
        protected MiningSetup CPU_Setup = new MiningSetup(null);
        protected MiningSetup NVIDIA_Setup = new MiningSetup(null);
        protected readonly int AMD_OCL_PLATFORM;
        protected MiningSetup AMD_Setup = new MiningSetup(null);

        // extra benchmark stuff
        protected double curSpeed = 0;

        protected static readonly String Iter_PER_SEC = "I/s";
        protected static readonly String Sols_PER_SEC = "Sols/s";
        protected const double SolMultFactor = 1.9;

        private class Result
        {
            public double Interval_seconds { get; set; }
            public double Speed_ips { get; set; }
            public double Speed_sps { get; set; }
            public double Accepted_per_minute { get; set; }
            public double Rejected_per_minute { get; set; }
        }

        private class JsonApiResponse
        {
            public string Method { get; set; }
            public Result Result { get; set; }
            public object Error { get; set; }
        }

        public NheqBase(string minerDeviceName)
            : base(minerDeviceName)
        {
            AMD_OCL_PLATFORM = ComputeDeviceManager.Available.AmdOpenCLPlatformNum;
        }

        public override void InitMiningSetup(MiningSetup miningSetup)
        {
            base.InitMiningSetup(miningSetup);
            List<MiningPair> CPUs = new List<MiningPair>();
            List<MiningPair> NVIDIAs = new List<MiningPair>();
            List<MiningPair> AMDs = new List<MiningPair>();
            foreach (var pairs in MiningSetup.MiningPairs)
            {
                if (pairs.Device.DeviceType == DeviceType.CPU)
                {
                    CPUs.Add(pairs);
                }
                if (pairs.Device.DeviceType == DeviceType.NVIDIA)
                {
                    NVIDIAs.Add(pairs);
                }
                if (pairs.Device.DeviceType == DeviceType.AMD)
                {
                    AMDs.Add(pairs);
                }
            }
            // reinit
            CPU_Setup = new MiningSetup(CPUs);
            NVIDIA_Setup = new MiningSetup(NVIDIAs);
            AMD_Setup = new MiningSetup(AMDs);
        }

        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            // TODO nvidia extras
            String ret = "-b " + GetDevicesCommandString();
            return ret;
        }

        public override async Task<ApiData> GetSummaryAsync()
        {
            CurrentMinerReadStatus = MinerApiReadStatus.NONE;
            ApiData ad = new ApiData(MiningSetup.CurrentAlgorithmType);

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
                ad.Speed = resp.Result.Speed_sps;
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

        protected double GetNumber(string outdata, string startF, string remF)
        {
            try
            {
                int speedStart = outdata.IndexOf(startF);
                String speed = outdata.Substring(speedStart, outdata.Length - speedStart);
                speed = speed.Replace(startF, "");
                speed = speed.Replace(remF, "");
                speed = speed.Trim();
                return Double.Parse(speed, CultureInfo.InvariantCulture);
            }
            catch
            {
            }
            return 0;
        }

        // benchmark stuff

        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            CheckOutdata(outdata);
        }
    }
}