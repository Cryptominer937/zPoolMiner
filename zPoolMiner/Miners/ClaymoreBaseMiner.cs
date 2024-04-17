using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using zPoolMiner.Devices;
using zPoolMiner.Enums;
using zPoolMiner.Miners.Grouping;
using zPoolMiner.Miners.Parsing;

namespace zPoolMiner.Miners
{
    public abstract class ClaymoreBaseMiner : Miner
    {
        protected int benchmarkTimeWait = 2 * 45; // Ok... this was all wrong
        private int benchmark_read_count;
        private double benchmark_sum;
        private int secondary_benchmark_read_count;
        private double secondary_benchmark_sum;
        protected readonly string LOOK_FOR_START;
        private const string LOOK_FOR_END = "h/s";

        // only dagger change
        protected bool ignoreZero;

        protected double api_read_mult = 1;
        protected AlgorithmType SecondaryAlgorithmType = AlgorithmType.NONE;

        public ClaymoreBaseMiner(string minerDeviceName, string look_FOR_START)
            : base(minerDeviceName)
        {
            ConectionType = NHMConectionType.STRATUM_TCP;
            LOOK_FOR_START = look_FOR_START.ToLower();
            IsKillAllUsedMinerProcs = true;
        }

        protected abstract double DevFee();

        protected virtual string SecondaryLookForStart() => "";

        // return true if a secondary algo is being used
        public bool IsDual() => (SecondaryAlgorithmType != AlgorithmType.NONE);

        protected override int GetMaxCooldownTimeInMilliseconds()
        {
            return 60 * 1000 * 5; // 5 minute max, whole waiting time 75seconds
        }

        private class JsonApiResponse
        {
            public List<string> Result { get; set; }
            public int Id { get; set; }
            public object Error { get; set; }
        }

        public override async Task<ApiData> GetSummaryAsync()
        {
            CurrentMinerReadStatus = MinerApiReadStatus.NONE;
            var ad = new ApiData(MiningSetup.CurrentAlgorithmType, MiningSetup.CurrentSecondaryAlgorithmType);

            TcpClient client = null;
            JsonApiResponse resp = null;

            try
            {
                var bytesToSend = ASCIIEncoding.ASCII.GetBytes("{\"id\":0,\"jsonrpc\":\"2.0\",\"method\":\"miner_getstat1\"}n");
                client = new TcpClient("127.0.0.1", ApiPort);
                var nwStream = client.GetStream();
                await nwStream.WriteAsync(bytesToSend, 0, bytesToSend.Length);
                var bytesToRead = new byte[client.ReceiveBufferSize];
                var bytesRead = await nwStream.ReadAsync(bytesToRead, 0, client.ReceiveBufferSize);
                var respStr = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                resp = JsonConvert.DeserializeObject<JsonApiResponse>(respStr, Globals.JsonSettings);
                client.Close();
                // Helpers.ConsolePrint("ClaymoreZcashMiner API back:", respStr);
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint(MinerTag(), "GetSummary exception: " + ex.Message);
            }

            if (resp != null && resp.Error == null)
            {
                // Helpers.ConsolePrint("ClaymoreZcashMiner API back:", "resp != null && resp.error == null");
                if (resp.Result != null && resp.Result.Count > 4)
                {
                    // Helpers.ConsolePrint("ClaymoreZcashMiner API back:", "resp.result != null && resp.result.Count > 4");
                    var speeds = resp.Result[3].Split(';');
                    var secondarySpeeds = (IsDual()) ? resp.Result[5].Split(';') : new string[0];
                    ad.Speed = 0;
                    ad.SecondarySpeed = 0;

                    foreach (var speed in speeds)
                    {
                        // Helpers.ConsolePrint("ClaymoreZcashMiner API back:", "foreach (var speed in speeds) {");
                        double tmpSpeed = 0;

                        try
                        {
                            tmpSpeed = double.Parse(speed, CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            tmpSpeed = 0;
                        }

                        ad.Speed += tmpSpeed;
                    }

                    foreach (var speed in secondarySpeeds)
                    {
                        double tmpSpeed = 0;

                        try
                        {
                            tmpSpeed = double.Parse(speed, CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            tmpSpeed = 0;
                        }

                        ad.SecondarySpeed += tmpSpeed;
                    }

                    if (MiningSetup.CurrentAlgorithmType == AlgorithmType.NeoScrypt)
                    {
                        api_read_mult = 1000;
                    }
                    //   Helpers.ConsolePrint("speed:", ad.Speed.ToString() + " "+api_read_mult.ToString());

                    ad.Speed *= api_read_mult;
                    ad.SecondarySpeed *= api_read_mult;
                    CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
                }

                if (ad.Speed == 0)
                {
                    CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;
                }

                // some clayomre miners have this issue reporting negative speeds in that case restart miner
                if (ad.Speed < 0)
                {
                    Helpers.ConsolePrint(MinerTag(), "Reporting negative speeds will restart...");
                    Restart();
                }
            }

            return ad;
        }

        protected override void _Stop(MinerStopType willswitch)
        {
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
        }

        protected virtual string DeviceCommand(int amdCount = 1) => " -di ";

        // This method now overridden in ClaymorecryptonightMiner
        // Following logic for ClaymoreDual and ClaymoreZcash
        protected override string GetDevicesCommandString()
        {
            // First by device type (AMD then NV), then by bus ID index
            var sortedMinerPairs = MiningSetup.MiningPairs
                .OrderByDescending(pair => pair.Device.DeviceType)
                .ThenBy(pair => pair.Device.IDByBus)
                .ToList();

            var extraParams = ExtraLaunchParametersParser.ParseForMiningPairs(sortedMinerPairs, DeviceType.AMD);

            var ids = new List<string>();

            var amdDeviceCount = ComputeDeviceManager.Query.AMD_Devices.Count;
            Helpers.ConsolePrint("ClaymoreIndexing", string.Format("Found {0} AMD devices", amdDeviceCount));

            foreach (var mPair in sortedMinerPairs)
            {
                var id = mPair.Device.IDByBus;

                if (id < 0)
                {
                    // should never happen
                    Helpers.ConsolePrint("ClaymoreIndexing", "ID by Bus too low: " + id.ToString() + " skipping device");
                    continue;
                }

                if (mPair.Device.DeviceType == DeviceType.NVIDIA)
                {
                    Helpers.ConsolePrint("ClaymoreIndexing", "NVIDIA device increasing index by " + amdDeviceCount.ToString());
                    id += amdDeviceCount;
                }

                if (id > 9)
                {  // New >10 GPU support in CD9.8
                    if (id < 36)
                    {  // CD supports 0-9 and a-z indexes, so 36 GPUs
                        var idchar = (char)(id + 87);  // 10 = 97(a), 11 - 98(b), etc
                        ids.Add(idchar.ToString());
                    }
                    else
                    {
                        Helpers.ConsolePrint("ClaymoreIndexing", "ID " + id + " too high, ignoring");
                    }
                }
                else
                {
                    ids.Add(id.ToString());
                }
            }

            var deviceStringCommand = DeviceCommand(amdDeviceCount) + string.Join("", ids);

            return deviceStringCommand + extraParams;
        }

        // benchmark stuff

        protected override void BenchmarkThreadRoutine(object CommandLine)
        {
            BenchmarkThreadRoutineAlternate(CommandLine, benchmarkTimeWait);
        }

        protected override void ProcessBenchLinesAlternate(string[] lines)
        {
            foreach (var line in lines)
            {
                if (line != null)
                {
                    BenchLines.Add(line);
                    var lineLowered = line.ToLower();

                    if (lineLowered.Contains(LOOK_FOR_START))
                    {
                        if (ignoreZero)
                        {
                            var got = GetNumber(lineLowered);

                            if (got != 0)
                            {
                                benchmark_sum += got;
                                ++benchmark_read_count;
                            }
                        }
                        else
                        {
                            benchmark_sum += GetNumber(lineLowered);
                            ++benchmark_read_count;
                        }
                    }
                    else if (!string.IsNullOrEmpty(SecondaryLookForStart()) && lineLowered.Contains(SecondaryLookForStart()))
                    {
                        if (ignoreZero)
                        {
                            var got = GetNumber(lineLowered, SecondaryLookForStart(), LOOK_FOR_END);

                            if (got != 0)
                            {
                                secondary_benchmark_sum += got;
                                ++secondary_benchmark_read_count;
                            }
                        }
                        else
                        {
                            secondary_benchmark_sum += GetNumber(lineLowered);
                            ++secondary_benchmark_read_count;
                        }
                    }
                }
            }

            if (benchmark_read_count > 0)
            {
                BenchmarkAlgorithm.BenchmarkSpeed = benchmark_sum / benchmark_read_count;
                BenchmarkAlgorithm.SecondaryBenchmarkSpeed = secondary_benchmark_sum / secondary_benchmark_read_count;
            }
        }

        // stub benchmarks read from file
        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            CheckOutdata(outdata);
        }

        protected override bool BenchmarkParseLine(string outdata)
        {
            Helpers.ConsolePrint("BENCHMARK", outdata);
            return false;
        }

        protected double GetNumber(string outdata) => GetNumber(outdata, LOOK_FOR_START, LOOK_FOR_END);

        protected double GetNumber(string outdata, string LOOK_FOR_START, string LOOK_FOR_END)
        {
            try
            {
                double mult = 1;
                var speedStart = outdata.IndexOf(LOOK_FOR_START);
                var speed = outdata.Substring(speedStart, outdata.Length - speedStart);
                speed = speed.Replace(LOOK_FOR_START, "");
                speed = speed.Substring(0, speed.IndexOf(LOOK_FOR_END));

                if (speed.Contains("k"))
                {
                    mult = 1000;
                    speed = speed.Replace("k", "");
                }
                else if (speed.Contains("m"))
                {
                    mult = 1000000;
                    speed = speed.Replace("m", "");
                }
                // Helpers.ConsolePrint("speed", speed);
                // Helpers.ConsolePrint("speed:", speed.ToString() + " " + mult + MiningSetup.CurrentAlgorithmType.ToString());
                speed = speed.Trim();
                return (double.Parse(speed, CultureInfo.InvariantCulture) * mult) * (1.0 - DevFee() * 0.01);
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint("getNumber", ex.Message + " | args => " + outdata + " | " + LOOK_FOR_END + " | " + LOOK_FOR_START);
            }

            return 0;
        }
    }
}