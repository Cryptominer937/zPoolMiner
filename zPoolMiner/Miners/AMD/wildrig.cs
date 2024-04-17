using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using zPoolMiner.Configs;
using zPoolMiner.Devices;
using zPoolMiner.Enums;
using zPoolMiner.Miners.Grouping;
using zPoolMiner.Miners.Parsing;

namespace zPoolMiner.Miners
{
    public class WildRig : Miner
    {
        private readonly int GPUPlatformNumber;
        private int _benchmarkTimeWait = 300;
        private const string _lookForStart = "speed 10s/60s/15m";
        private const string _lookForEnd = "n/a kh/s max";

        public WildRig() : base("WildRig")
        {
            GPUPlatformNumber = ComputeDeviceManager.Available.AmdOpenCLPlatformNum;
        }

        public override void Start(string url, string btcAdress, string worker)
        {
            LastCommandLine = GetStartCommand(url, btcAdress, worker);
            ProcessHandle = _Start();
        }

        protected override string GetDevicesCommandString()
        {
            var deviceStringCommand = " ";

            var ids = MiningSetup.MiningPairs.Select(mPair => mPair.Device.IDByBus.ToString()).ToList();
            deviceStringCommand += string.Join(",", ids);

            return deviceStringCommand;
        }

        private string GetStartCommand(string url, string btcAdress, string worker)
        {
            var extras = ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.AMD);
            var algo = "";
            var port = "";

            if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.Skunk))
            {
                algo = "skunkhash";
                port = "3362";
            }

            if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.x16r))
            {
                algo = "x16r";
                port = "3366";
            }

            return $" -a {algo} -o {url} -u {btcAdress}.{worker}:x {extras} --api-port {ApiPort} "
                + $" -o stratum+tcp://{algo}.usa.nicehash.com:{port} -u {btcAdress}.{worker}:x "
                + $" -o stratum+tcp://{algo}.hk.nicehash.com:{port} -u {btcAdress}.{worker}:x "
                + $" -o stratum+tcp://{algo}.jp.nicehash.com:{port} -u {btcAdress}.{worker}:x "
                + $" -o stratum+tcp://{algo}.in.nicehash.com:{port} -u {btcAdress}.{worker}:x "
                + " --opencl-devices=" + GetDevicesCommandString().TrimStart() + " --opencl-platform=" + GPUPlatformNumber;
        }

        private string GetStartBenchmarkCommand(string url, string btcAdress, string worker)
        {
            var extras = ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.AMD);
            var algo = "";
            var port = "";

            if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.Skunk))
            {
                algo = "skunkhash";
                port = "3362";

                return $" -a {algo} -o stratum+tcp://skunk.eu.mine.zpool.ca:8433 -u 1JqFnUR3nDFCbNUmWiQ4jX6HRugGzX55L2 -p c=BTC {extras} --api-port {ApiPort} "
               + $" -o stratum+tcp://{algo}.eu.nicehash.com:{port} -u {btcAdress}.{worker}:x "
               + " --multiple-instance --opencl-devices=" + GetDevicesCommandString().TrimStart() + " --opencl-platform=" + GPUPlatformNumber;
            }

            if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.x16r))
            {
                algo = "x16r";
                port = "3366";

                return $" -a {algo} -o stratum+tcp://x16r.eu.mine.zpool.ca:3636 -u 1JqFnUR3nDFCbNUmWiQ4jX6HRugGzX55L2 -p c=BTC {extras} --api-port {ApiPort} "
               + $" -o stratum+tcp://{algo}.eu.nicehash.com:{port} -u {btcAdress}.{worker}:x "
               + " --multiple-instance --opencl-devices=" + GetDevicesCommandString().TrimStart() + " --opencl-platform=" + GPUPlatformNumber + " --benchmark";
            }

            return "oops... strange algo";
        }

        protected override void _Stop(MinerStopType willswitch)
        {
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
        }

        protected override int GetMaxCooldownTimeInMilliseconds()
        {
            return 60 * 1000 * 5;  // 5 min
        }

        public override Task<ApiData> GetSummaryAsync() => GetSummaryCPUAsync();

        protected override bool IsApiEof(byte third, byte second, byte last)
        {
            return third == 0x7d && second == 0xa && last == 0x7d;
        }

        #region Benchmark

        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            var server = Globals.GetLocationURL(algorithm.CryptoMiner937ID,
                Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation],
                ConectionType);
            //   _benchmarkTimeWait = time;
            return GetStartBenchmarkCommand(server, Globals.GetBitcoinUser(), ConfigManager.GeneralConfig.WorkerName.Trim())
                + " --print-time=2";
        }

        protected override void BenchmarkThreadRoutine(object CommandLine)
        {
            BenchmarkThreadRoutineAlternate(CommandLine, _benchmarkTimeWait);
        }

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

                if (lineLowered.Contains(_lookForStart.ToLower()))
                {
                    var speeds = Regex.Match(lineLowered, $"{_lookForStart.ToLower()} (.+?) {_lookForEnd.ToLower()}").Groups[1].Value.Split();

                    try
                    {
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
                    catch
                    {
                        MessageBox.Show("Unsupported miner version - " + MiningSetup.MinerPath,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        BenchmarkSignalFinnished = true;
                        return;
                    }
                }
            }

            if (sixtySecCount > 0 && sixtySecTotal > 0)
            {
                // Run iff 60s averages are reported
                BenchmarkAlgorithm.BenchmarkSpeed = (sixtySecTotal / sixtySecCount) * 1000;
            }
            else if (twoSecCount > 0)
            {
                // Run iff no 60s averages are reported but 2.5s are
                BenchmarkAlgorithm.BenchmarkSpeed = (twoSecTotal / twoSecCount) * 1000;
            }
        }

        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            CheckOutdata(outdata);
        }

        protected override bool BenchmarkParseLine(string outdata)
        {
            Helpers.ConsolePrint(MinerTag(), outdata);
            return false;
        }

        #endregion Benchmark
    }
}