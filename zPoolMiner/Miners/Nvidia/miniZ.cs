using Newtonsoft.Json;
using zPoolMiner.Configs;
using zPoolMiner.Miners.Grouping;
using zPoolMiner.Miners.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using zPoolMiner;
using zPoolMiner.Enums;
using System.Windows.Forms;

namespace zPoolMiner.Miners
{
    public class MiniZ : Miner
    { 
   #pragma warning disable IDE1006
    private class Result
    {
        public uint gpuid { get; set; }
        public uint cudaid { get; set; }
        public string busid { get; set; }
        public uint gpu_status { get; set; }
        public int solver { get; set; }
        public int temperature { get; set; }
        public uint gpu_power_usage { get; set; }
        public double speed_sps { get; set; }
        public uint accepted_shares { get; set; }
        public uint rejected_shares { get; set; }
    }

    private class JsonApiResponse
    {
        public uint id { get; set; }
        public string method { get; set; }
        public object error { get; set; }
        public List<Result> result { get; set; }
    }
#pragma warning restore IDE1006

    private int _benchmarkTimeWait = 2 * 45;
    private int _benchmarkReadCount = 0;
    private double _benchmarkSum;
    private const string LookForStart = "(";
    private const string LookForEnd = ")sol/s";
    private double prevSpeed = 0;
    private DateTime _started;
    private bool firstStart = true;
    //private string[,] myServers = Form_Main.myServers;

    public MiniZ() : base("miniZ")
    {
        //ConectionType = NhmConectionType.NONE;
    }

    public override void Start(string url, string btcAddress, string worker)
    {
        IsApiReadException = false;
        firstStart = true;
        LastCommandLine = GetStartCommand(url, btcAddress, worker);
        ProcessHandle = _Start();
    }
    static int GetWinVer(Version ver)
    {
        if (ver.Major == 6 & ver.Minor == 1)
            return 7;
        else if (ver.Major == 6 & ver.Minor == 2)
            return 8;
        else
            return 10;
    }

    private string GetStartCommand(string url, string btcAddress, string worker)
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
            var server = url.Split(':')[0].Replace("stratum+tcp://", "");
        var ARG = "";
            var pool = "";
            string address = btcAddress;
            string username = GetUsername(btcAddress, worker);
            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.equihash144)
            {
                ARG = "--algo 144,5 --pers auto --oc1 ";
                pool = "equihash144.mine.zergpool.com:2146";
            }
            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.equihash125)
            {
                ARG = "--algo 125,4 --oc1 ";
                pool = "equihash125.mine.zergpool.com:2150";
            }
            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.equihash192)
            {
                ARG = "--algo 192,7 --pers auto --oc1 ";
                pool = "equihash192.mine.zergpool.com:2144";
            }
            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.equihash96)
            {
                ARG = "--algo 96,5 --pers auto --oc1 ";
                pool = "equihash96.mine.zergpool.com:2148";
            }
            string sColor = "";
        if (GetWinVer(Environment.OSVersion.Version) < 8)
        {
            sColor = " --nocolor";
        }


        var ret = GetDevicesCommandString()
                  + sColor + ARG + "--templimit 95 --intensity 100 --latency --tempunits C " + " --url " + address + "@" + pool  + " --pass=" + worker +"" + " --telemetry=" + ApiPort;

        return ret;
    }

    protected override string GetDevicesCommandString()
    {
        var deviceStringCommand = MiningSetup.MiningPairs.Aggregate(" --cuda-devices ",
            (current, nvidiaPair) => current + (nvidiaPair.Device.IDByBus + " "));

        deviceStringCommand +=
            " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

        return deviceStringCommand;
    }

    // benchmark stuff
    protected void KillMinerBase(string exeName)
    {
        foreach (var process in Process.GetProcessesByName(exeName))
        {
            try { process.Kill(); }
            catch (Exception e) { Helpers.ConsolePrint(MinerDeviceName, e.ToString()); }
        }
    }

    protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
    {
            //CleanOldLogs();
            string url = Globals.GetLocationURL(algorithm.CryptoMiner937ID, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], ConectionType);
            var server = url.Split(':')[0].Replace("stratum+tcp://", "");
            var ARG = "";
            var pool = "";
            //string address = btcAddress;
            //string username = GetUsername(btcAddress, worker);
            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.equihash144)
            {
                ARG = "--algo 144,5 --pers auto --oc1 ";
                pool = "equihash144.mine.zergpool.com:2146";
            }
            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.equihash125)
            {
                ARG = "--algo 125,4 --oc1 ";
                pool = "equihash125.mine.zergpool.com:2150";
            }
            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.equihash192)
            {
                ARG = "--algo 192,7 --pers auto --oc1 ";
                pool = "equihash192.mine.zergpool.com:2144";
            }
            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.equihash96)
            {
                ARG = "--algo 96,5 --pers auto --oc1 ";
                pool = "equihash96.mine.zergpool.com:2148";
            }
            string sColor = "";
            if (GetWinVer(Environment.OSVersion.Version) < 8)
            {
                sColor = " --nocolor";
            }


            var ret = GetDevicesCommandString()
                      + sColor + ARG + "--templimit 95 --intensity 100 --latency --tempunits C " + " --url " + "DE8BDPdYu9LadwV4z4KamDqni43BUhGb66" + "@" + pool + " --pass=" + "c=DOGE,ID=Benchmark" +"" + " --telemetry=" + ApiPort + " --log-file=" + "bench.txt";

            return ret;
        }

    protected override void BenchmarkThreadRoutine(object commandLine)
    {
        BenchmarkSignalQuit = false;
        BenchmarkSignalHanged = false;
        BenchmarkSignalFinnished = false;
        BenchmarkException = null;

        Thread.Sleep(ConfigManager.GeneralConfig.MinerRestartDelayMS);

        try
        {
            Helpers.ConsolePrint("BENCHMARK", "Benchmark starts");
            Helpers.ConsolePrint(MinerTag(), "Benchmark should end in : " + _benchmarkTimeWait + " seconds");
            BenchmarkHandle = BenchmarkStartProcess((string)commandLine);
            BenchmarkHandle.WaitForExit(_benchmarkTimeWait + 2);
            var benchmarkTimer = new Stopwatch();
            benchmarkTimer.Reset();
            benchmarkTimer.Start();

            BenchmarkProcessStatus = BenchmarkProcessStatus.Running;
            var keepRunning = true;
            while (IsActiveProcess(BenchmarkHandle.Id))
            {
                if (benchmarkTimer.Elapsed.TotalSeconds >= (_benchmarkTimeWait + 2)
                    || BenchmarkSignalQuit
                    || BenchmarkSignalFinnished
                    || BenchmarkSignalHanged
                    || BenchmarkSignalTimedout
                    || BenchmarkException != null)
                {
                    var imageName = MinerExeName.Replace(".exe", "");
                    // maybe will have to KILL process
                    KillMinerBase(imageName);
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
            var latestLogFile = "bench.txt";
            var dirInfo = new DirectoryInfo(WorkingDirectory);
            foreach (var file in dirInfo.GetFiles("bench.txt"))
            {
                latestLogFile = file.Name;
                break;
            }

            // read file log
            if (File.Exists(WorkingDirectory + latestLogFile))
            {
                var lines = new string[0];
                var read = false;
                var iteration = 0;
                while (!read)
                {
                    if (iteration < 10)
                    {
                        try
                        {
                            lines = File.ReadAllLines(WorkingDirectory + latestLogFile);
                            read = true;
                            Helpers.ConsolePrint(MinerTag(),
                                "Successfully read log after " + iteration + " iterations");
                        }
                        catch (Exception ex)
                        {
                            Helpers.ConsolePrint(MinerTag(), ex.Message);
                            Thread.Sleep(1000);
                        }

                        iteration++;
                    }
                    else
                    {
                        read = true; // Give up after 10s
                        Helpers.ConsolePrint(MinerTag(), "Gave up on iteration " + iteration);
                    }
                }

                var addBenchLines = bench_lines.Count == 0;
                foreach (var line in lines)
                {
                    if (line != null)
                    {
                        bench_lines.Add(line);
                        var lineLowered = line.ToLower();
                        if (lineLowered.Contains(LookForStart) && lineLowered.Contains(LookForEnd))
                        {
                            if (_benchmarkReadCount > 2) //3 skip
                            {
                                _benchmarkSum += GetNumber(lineLowered);
                            }
                            ++_benchmarkReadCount;
                        }
                    }
                }

                if (_benchmarkReadCount > 0)
                {
                    BenchmarkAlgorithm.BenchmarkSpeed = _benchmarkSum / (_benchmarkReadCount - 3);
                }
            }
                if (File.Exists(WorkingDirectory + latestLogFile))
                {
                    File.Delete(WorkingDirectory + latestLogFile);
                    Console.WriteLine("MiniZ benchmark log Deleted.");
                }
                    BenchmarkThreadRoutineFinish();
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

    protected double GetNumber(string outdata)
    {
        return GetNumber(outdata, LookForStart, LookForEnd);
    }

    protected double GetNumber(string outdata, string lookForStart, string lookForEnd)
    {
        try
        {
            double mult = 1;
            var speedStart = outdata.IndexOf(lookForStart.ToLower());
            var speed = outdata.Substring(speedStart, outdata.Length - speedStart);
            speed = speed.Replace(lookForStart.ToLower(), "");
            speed = speed.Substring(0, speed.IndexOf(lookForEnd.ToLower()));

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

            //Helpers.ConsolePrint("speed", speed);
            speed = speed.Trim();
            try
            {
                return double.Parse(speed, CultureInfo.InvariantCulture) * mult;
            }
            catch
            {
                MessageBox.Show("Unsupported miner version - " + MiningSetup.MinerPath,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BenchmarkSignalFinnished = true;
            }
        }
        catch (Exception ex)
        {
            Helpers.ConsolePrint("GetNumber",
                ex.Message + " | args => " + outdata + " | " + lookForEnd + " | " + lookForStart);
            MessageBox.Show("Unsupported miner version - " + MiningSetup.MinerPath,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return 0;
    }

    public override async Task<APIData> GetSummaryAsync()
    {
        CurrentMinerReadStatus = MinerApiReadStatus.NONE;
        var ad = new APIData(MiningSetup.CurrentAlgorithmType);
        var elapsedSeconds = DateTime.Now.Subtract(_started).Seconds; ////PVS-Studio - stupid program! 

        // if (elapsedSeconds < 15 && firstStart)
        if (firstStart)
        //          if (ad.Speed <= 0.0001)
        {
            Thread.Sleep(3000);
            ad.Speed = 1;
            firstStart = false;
            return ad;
        }

        JsonApiResponse resp = null;
        try
        {
            var bytesToSend = Encoding.ASCII.GetBytes("{\"id\":\"0\", \"method\":\"getstat\"}\\n");
            var client = new TcpClient("127.0.0.1", ApiPort);
            var nwStream = client.GetStream();
            await nwStream.WriteAsync(bytesToSend, 0, bytesToSend.Length);
            var bytesToRead = new byte[client.ReceiveBufferSize];
            var bytesRead = await nwStream.ReadAsync(bytesToRead, 0, client.ReceiveBufferSize);
            var respStr = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                           // Helpers.ConsolePrint("miniZ API:", respStr);
            if (!respStr.Contains("speed_sps") && prevSpeed != 0)
            {
                client.Close();
                CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
                ad.Speed = prevSpeed;
                return ad;
            }
            resp = JsonConvert.DeserializeObject<JsonApiResponse>(respStr, Globals.JsonSettings);
            client.Close();
        }
        catch (Exception ex)
        {
            Helpers.ConsolePrint(MinerTag(), ex.Message);
            CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
            ad.Speed = prevSpeed;
        }

        if (resp != null && resp.error == null)
        {
            ad.Speed = resp.result.Aggregate<Result, double>(0, (current, t1) => current + t1.speed_sps);
            prevSpeed = ad.Speed;
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
    }
}
