using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using zPoolMiner;
using zPoolMiner.Configs;
using zPoolMiner.Enums;
using zPoolMiner.Miners;
using zPoolMiner.Miners.Parsing;

namespace NiceHashMiner.Miners
{
    public class CPU_Xmrig : Miner
    {
        [DllImport("psapi.dll")]
        public static extern bool EmptyWorkingSet(IntPtr hProcess);

        private int benchmarkTimeWait = 360;
        private const string LookForStart = "speed 10s/60s/15m";
        private const string LookForEnd = "h/s max";
        private System.Diagnostics.Process CMDconfigHandle;
        private string platform = "";
        private string platform_prefix = "";

        public CPU_Xmrig() : base("CPU_Xmrig")
        { }

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
            LastCommandLine = GetStartCommand(url, btcAddress, worker);

            ProcessHandle = _Start();
        }

        /*
        private string GetStartCommand(string url, string btcAddress, string worker)
        {
            var extras = ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.CPU);
            return $" -o {url} -u {btcAddress}.{worker}:x --nicehash {extras} --api-port {ApiPort}";
        }
        */

        public void FreeMem()
        {
            EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    EmptyWorkingSet(process.Handle);
                }
                catch (Exception ex)
                {
                    Helpers.ConsolePrint(MinerTag(), ex.Message);
                }
            }
        }

        protected override string GetDevicesCommandString()
        {
            var deviceStringCommand = " ";
            if (platform == "")//cpu
            {
                return "";
            }
            var ids = MiningSetup.MiningPairs.Select(mPair => mPair.Device.ID.ToString()).ToList();
            deviceStringCommand += string.Join(",", ids);

            return deviceStringCommand;
        }

        private string GetStartCommand(string url, string btcAddress, string worker)
        {
            var extras = ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.CPU);
            var algo = "cryptonightv7";
            var port = "3363";
            var variant = " --variant 1 ";
            //cn/r cryptonight/r
            string nhsuff = "";
            string username = GetUsername(btcAddress, worker);

            //FreeMem();

            foreach (var pair in MiningSetup.MiningPairs)
            {
                if (pair.Device.DeviceType == DeviceType.NVIDIA)
                {
                    platform = " --no-cpu --cuda-devices=";
                }
                else if (pair.Device.DeviceType == DeviceType.AMD)
                {
                    platform = " --no-cpu --opencl-devices=";
                }
                else if (pair.Device.DeviceType == DeviceType.CPU)
                {
                    platform = "";
                }
            }

            if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.randomx))
            {
                algo = "rx/0";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {ApiPort} {platform}"
                 + GetDevicesCommandString().TrimStart();
            }
            /*if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.randomarq))
            {
                algo = "rx/arq";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {APIPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }*/
            /*if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.randomsfx))
            {
                algo = "rx/sfx";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {APIPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }
            if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.cryptonight_heavy))
            {
                algo = "cn-heavy/0";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {APIPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }*/
            if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.cryptonight_heavyx))
            {
                algo = "cn/double";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {ApiPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }
            /*if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.cryptonight_saber))
            {
                algo = "cn-heavy/0";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {APIPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }*/
            /*if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.cryptonight_fast))
            {
                algo = "cn/half";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {APIPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }*/
            if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.cryptonight_haven))
            {
                algo = "cn-heavy/xhv";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {ApiPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }
            /*if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.chukwa))
            {
                algo = "argon2/chukwa";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {APIPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }*/
            return "unsupported algo";
        }

        private string GetStartBenchmarkCommand(string url, string btcAddress, string worker)
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

            foreach (var pair in MiningSetup.MiningPairs)
            {
                if (pair.Device.DeviceType == DeviceType.NVIDIA)
                {
                    platform = " --no-cpu --cuda-devices=";
                    platform_prefix = "nvidia_";
                }
                else if (pair.Device.DeviceType == DeviceType.AMD)
                {
                    platform = " --no-cpu --opencl-devices=";
                    platform_prefix = "amd_"; ;
                }
                else if (pair.Device.DeviceType == DeviceType.CPU)
                {
                    platform = "";
                    platform_prefix = "cpu_";
                }
            }

            var extras = ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.CPU);
            var algo = "cryptonightv7";
            var port = "3363";
            var variant = " --variant 1 ";
            string username = GetUsername(btcAddress, worker);
            if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.randomx))
            {
                algo = "rx/0";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {ApiPort} {platform}"
                 + GetDevicesCommandString().TrimStart();
            }
            /*if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.randomarq))
            {
                algo = "rx/arq";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {APIPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }*/
            /*if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.randomsfx))
            {
                algo = "rx/sfx";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {APIPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }
            if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.cryptonight_heavy))
            {
                algo = "cn-heavy/0";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {APIPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }*/
            if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.cryptonight_heavyx))
            {
                algo = "cn/double";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {ApiPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }
            /*if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.cryptonight_saber))
            {
                algo = "cn-heavy/0";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {APIPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }
            if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.cryptonight_fast))
            {
                algo = "cn/half";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {APIPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }*/
            if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.cryptonight_haven))
            {
                algo = "cn-heavy/xhv";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {ApiPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }
            /*if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.chukwa))
            {
                algo = "argon2/chukwa";
                port = "3367";
                variant = " --variant 2 ";
                return $"-a {algo} -o {url} -u {btcAddress} --pass {worker} --nicehash {extras} --http-port {APIPort} {platform}"
               + GetDevicesCommandString().TrimStart();
            }*/
            return "unsupported algo";
        }

        protected override void _Stop(MinerStopType willswitch)
        {
            Helpers.ConsolePrint("XMRIG", "_Stop");
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
        }

        //protected override int GetMaxCooldownTimeInMilliseconds()
        //{
        //   return 60 * 1000 * 5; // 5 min
        //}

        protected async Task<ApiData> GetSummaryCpuAsyncXMRig(string method = "", bool overrideLoop = false)
        {
            var ad = new ApiData(MiningSetup.CurrentAlgorithmType);

            try
            {
                HttpWebRequest WR = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:" + ApiPort.ToString() + "/1/summary");
                WR.UserAgent = "GET / HTTP/1.1\r\n\r\n";
                WR.Timeout = 30 * 1000;
                WR.Credentials = CredentialCache.DefaultCredentials;
                WebResponse Response = WR.GetResponse();
                Stream SS = Response.GetResponseStream();
                SS.ReadTimeout = 20 * 1000;
                StreamReader Reader = new StreamReader(SS);
                var respStr = await Reader.ReadToEndAsync();

                Reader.Close();
                Response.Close();
                //Helpers.ConsolePrint(MinerTag(), respStr);

                if (string.IsNullOrEmpty(respStr))
                {
                    CurrentMinerReadStatus = MinerApiReadStatus.NETWORK_EXCEPTION;
                    throw new Exception("Response is empty!");
                }

                dynamic resp = JsonConvert.DeserializeObject(respStr);

                if (resp != null)
                {
                    JArray totals = resp.hashrate.total;
                    foreach (var total in totals)
                    {
                        if (total.Value<string>() == null) continue;
                        ad.Speed = total.Value<double>();
                        break;
                    }

                    if (ad.Speed == 0)
                    {
                        CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;
                    }
                    else
                    {
                        CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
                    }
                }
                else
                {
                    throw new Exception($"Response does not contain speed data: {respStr.Trim()}");
                }
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint(MinerTag(), ex.Message);
            }

            return ad;
        }

        public override async Task<ApiData> GetSummaryAsync()
        {
            return await GetSummaryCpuAsyncXMRig();
        }

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
            benchmarkTimeWait = time;
            return GetStartBenchmarkCommand(server, Globals.GetBitcoinUser(), ConfigManager.GeneralConfig.WorkerName.Trim())
                + $" -l benchmark_log.txt --print-time=10 --nicehash";
        }

        protected override void BenchmarkThreadRoutine(object commandLine)
        {
            BenchmarkThreadRoutineAlternateXmRig(commandLine, benchmarkTimeWait);
        }

        protected void BenchmarkThreadRoutineAlternateXmRig(object commandLine, int benchmarkTimeWait)
        {
            //CleanOldLogs();

            BenchmarkSignalQuit = false;
            BenchmarkSignalHanged = false;
            BenchmarkSignalFinnished = false;
            BenchmarkException = null;

            Thread.Sleep(ConfigManager.GeneralConfig.MinerRestartDelayMS);

            if (File.Exists("bin\\CPU\\CPU-XMRig\\benchmark_log.txt"))
                File.Delete("bin\\CPU\\CPU-XMRig\\benchmark_log.txt");

            try
            {
                Helpers.ConsolePrint("BENCHMARK-routineAlt", "Benchmark starts");
                Helpers.ConsolePrint(MinerTag(), "Benchmark should end in : " + benchmarkTimeWait + " seconds");
                BenchmarkHandle = BenchmarkStartProcess((string)commandLine);
                BenchmarkHandle.WaitForExit(benchmarkTimeWait + 2);
                var benchmarkTimer = new Stopwatch();
                benchmarkTimer.Reset();
                benchmarkTimer.Start();
                //BenchmarkThreadRoutineStartSettup();
                // wait a little longer then the benchmark routine if exit false throw
                //var timeoutTime = BenchmarkTimeoutInSeconds(BenchmarkTimeInSeconds);
                //var exitSucces = BenchmarkHandle.WaitForExit(timeoutTime * 1000);
                // don't use wait for it breaks everything
                BenchmarkProcessStatus = BenchmarkProcessStatus.Running;
                var keepRunning = true;
                while (keepRunning && IsActiveProcess(BenchmarkHandle.Id))
                {
                    //string outdata = BenchmarkHandle.StandardOutput.ReadLine();
                    //BenchmarkOutputErrorDataReceivedImpl(outdata);
                    // terminate process situations
                    if (benchmarkTimer.Elapsed.TotalSeconds >= (benchmarkTimeWait + 2)
                        || BenchmarkSignalQuit
                        || BenchmarkSignalFinnished
                        || BenchmarkSignalHanged
                        || BenchmarkSignalTimedout
                        || BenchmarkException != null)
                    {
                        var imageName = MinerExeName.Replace(".exe", "");
                        // maybe will have to KILL process
                        KillProspectorClaymoreMinerBase(imageName);
                        if (BenchmarkSignalTimedout)
                        {
                            throw new Exception("Benchmark timedout");
                        }

                        if (BenchmarkException != null)
                        {
                            throw BenchmarkException;
                        }

                        if (BenchmarkSignalQuit)
                        {
                            throw new Exception("Termined by user request");
                        }

                        if (BenchmarkSignalFinnished)
                        {
                            break;
                        }

                        keepRunning = false;
                        break;
                    }

                    // wait a second reduce CPU load
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                BenchmarkThreadRoutineCatch(ex);
            }
            finally
            {
                BenchmarkAlgorithm.BenchmarkSpeed = 0;
                // find latest log file
                string latestLogFile = "benchmark_log.txt";
                var dirInfo = new DirectoryInfo(WorkingDirectory);
                foreach (var file in dirInfo.GetFiles("benchmark_log.txt"))
                {
                    latestLogFile = file.Name;
                    //     Helpers.ConsolePrint("BENCHMARK-routineAlt", latestLogFile);
                    break;
                }

                BenchmarkHandle?.WaitForExit(10000);
                // read file log
                Helpers.ConsolePrint("BENCHMARK-routineAlt", WorkingDirectory + latestLogFile);
                if (File.Exists(WorkingDirectory + latestLogFile))
                {
                    var lines = File.ReadAllLines(WorkingDirectory + latestLogFile);
                    Helpers.ConsolePrint("BENCHMARK-routineAlt", lines.ToString());
                    ProcessBenchLinesAlternate(lines);
                }

                BenchmarkThreadRoutineFinish();
            }
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
                if (!lineLowered.Contains(LookForStart)) continue;
                var speeds = Regex.Match(lineLowered, $"{LookForStart} (.+?) {LookForEnd}").Groups[1].Value.Split();

                try
                {
                    if (double.TryParse(speeds[1], out var sixtySecSpeed))
                    {
                        sixtySecTotal += sixtySecSpeed;
                        ++sixtySecCount;
                    }
                    else if (double.TryParse(speeds[0], out var twoSecSpeed))
                    {
                        // Store 10s data in case 60s is never reached
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

            if (sixtySecCount > 0 && sixtySecTotal > 0)
            {
                // Run iff 60s averages are reported
                BenchmarkAlgorithm.BenchmarkSpeed = sixtySecTotal / sixtySecCount;
            }
            else if (twoSecCount > 0)
            {
                // Run iff no 60s averages are reported but 2.5s are
                BenchmarkAlgorithm.BenchmarkSpeed = twoSecTotal / twoSecCount;
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

        protected override int GetMaxCooldownTimeInMilliseconds()
        {
            return 60 * 1000 * 5;  // 5 min
        }

        #endregion Benchmark
    }
}