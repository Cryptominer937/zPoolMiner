using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using zPoolMiner;
using zPoolMiner.Configs;
using zPoolMiner.Devices;
using zPoolMiner.Enums;
using zPoolMiner.Miners.Grouping;
using zPoolMiner.Miners.Parsing;

namespace NiceHashMiner.Miners
{
    public class SRBMiner : Miner
    {
        private readonly int GPUPlatformNumber;
        private int _benchmarkTimeWait = 180;

        private int TotalCount = 2;
        private const int TotalDelim = 2;
        private double speed = 0;
        private int count = 0;

        public SRBMiner() : base("SRBMiner")
        {
            GPUPlatformNumber = ComputeDeviceManager.Available.AmdOpenCLPlatformNum;
        }

        /*
                protected override int GetMaxCooldownTimeInMilliseconds()
                {
                    if (this.MiningSetup.MinerPath == MinerPaths.Data.SRBMiner)
                    {
                        return 60 * 1000 * 12; // wait for hashrate string
                    }
                    _maxCooldownTimeInMilliseconds = 60 * 1000 * 12;
                    return 60 * 1000 * 12;
                }

        */

        public override void Start(string url, string btcAdress, string worker)
        {
            //IsApiReadException = MiningSetup.MinerPath == MinerPaths.Data.SRBMiner;

            LastCommandLine = GetStartCommand(url, btcAdress, worker);
            ProcessHandle = _Start();
        }

        private string GetStartCommand(string url, string btcAdress, string worker)
        {
            var extras = ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.AMD);
            var algo = "cryptonightv8";
            var port = "3367";
            var variant = " --ccryptonighttype normalv8";
            url = url.Replace("stratum+tcp://", "");
            if (File.Exists("bin_3rdparty\\SRBMiner\\poolsV8.txt"))
                File.Delete("bin_3rdparty\\SRBMiner\\poolsV8.txt");
            var str1 = "{\r\n" +
                       "\"pools\" :\r\n" +
                       "[\r\n";
            var str2 = "        {\r\n" +
                "                \"pool\" : \"cryptonightv8.hk.nicehash.com:3367\",\r\n" +
                "                \"wallet\" : \"" + btcAdress + "." + worker + "\",\r\n" +
                "                \"password\" : \"x\"\r\n" +
                "},\r\n" +
                "        {\r\n" +
                "                \"pool\" : \"cryptonightv8.in.nicehash.com:3367\",\r\n" +
                "                \"wallet\" : \"" + btcAdress + "." + worker + "\",\r\n" +
                "                \"password\" : \"x\"\r\n" +
                "},\r\n" +
                "        {\r\n" +
                "                \"pool\" : \"cryptonightv8.jp.nicehash.com:3367\",\r\n" +
                "                \"wallet\" : \"" + btcAdress + "." + worker + "\",\r\n" +
                "                \"password\" : \"x\"\r\n" +
                "},\r\n" +
                "        {\r\n" +
                "                \"pool\" : \"cryptonightv8.br.nicehash.com:3367\",\r\n" +
                "                \"wallet\" : \"" + btcAdress + "." + worker + "\",\r\n" +
                "                \"password\" : \"x\"\r\n" +
                "},\r\n" +
                "        {\r\n" +
                "                \"pool\" : \"cryptonightv8.jp.nicehash.com:3367\",\r\n" +
                "                \"wallet\" : \"" + btcAdress + "." + worker + "\",\r\n" +
                "                \"password\" : \"x\"\r\n" +
                "        }\r\n";
            var str3 = "]\r\n" +
                       "}";
            try
            {
                FileStream fs = new FileStream("bin_3rdparty\\SRBMiner\\poolsV8.txt", FileMode.Create, FileAccess.Write);
                StreamWriter w = new StreamWriter(fs);
                w.WriteAsync(str1 + str2 + str3);
                w.Flush();
                w.Close();
            }
            catch (Exception e)
            {
                Helpers.ConsolePrint("poolsV8.txt write error:", e.ToString());
            }

            /*if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.CryptoNightHeavy))
            {
                algo = "cryptonightheavy";
                port = "3364";
                variant = " --ccryptonighttype heavy";
                if (File.Exists("bin_3rdparty\\SRBMiner\\poolsH.txt"))
                    File.Delete("bin_3rdparty\\SRBMiner\\poolsH.txt");
                var strh1 = "{\r\n" +
                           "\"pools\" :\r\n" +
                           "[\r\n";
                var strh2 = "        {\r\n" +
                    "                \"pool\" : \"cryptonightheavy.hk.nicehash.com:3364\",\r\n" +
                    "                \"wallet\" : \"" + btcAdress + "." + worker + "\",\r\n" +
                    "                \"password\" : \"x\"\r\n" +
                    "},\r\n" +
                    "        {\r\n" +
                    "                \"pool\" : \"cryptonightheavy.in.nicehash.com:3364\",\r\n" +
                    "                \"wallet\" : \"" + btcAdress + "." + worker + "\",\r\n" +
                    "                \"password\" : \"x\"\r\n" +
                    "},\r\n" +
                    "        {\r\n" +
                    "                \"pool\" : \"cryptonightheavy.jp.nicehash.com:3364\",\r\n" +
                    "                \"wallet\" : \"" + btcAdress + "." + worker + "\",\r\n" +
                    "                \"password\" : \"x\"\r\n" +
                    "},\r\n" +
                    "        {\r\n" +
                    "                \"pool\" : \"cryptonightheavy.br.nicehash.com:3364\",\r\n" +
                    "                \"wallet\" : \"" + btcAdress + "." + worker + "\",\r\n" +
                    "                \"password\" : \"x\"\r\n" +
                    "},\r\n" +
                    "        {\r\n" +
                    "                \"pool\" : \"cryptonightv8.jp.nicehash.com:3367\",\r\n" +
                    "                \"wallet\" : \"" + btcAdress + "." + worker + "\",\r\n" +
                    "                \"password\" : \"x\"\r\n" +
                    "        }\r\n";
                var strh3 = "]\r\n" +
                           "}";
                try
                {
                    FileStream fs = new FileStream("bin_3rdparty\\SRBMiner\\poolsH.txt", FileMode.Create, FileAccess.Write);
                    StreamWriter w = new StreamWriter(fs);
                    w.WriteAsync(strh1 + strh2 + strh3);
                    w.Flush();
                    w.Close();
                }
                catch (Exception e)
                {
                    Helpers.ConsolePrint("poolsH.txt write error:", e.ToString());
                }
                return $" {variant} --cgpuid {GetDevicesCommandString().TrimStart()} {extras} --cnicehash true --apienable --apiport {ApiPort} --cpool {url} --cwallet {btcAdress}.{worker} --cpassword x --pools poolsH.txt";
            }*/

            return $" {variant} --cgpuid {GetDevicesCommandString().TrimStart()} {extras} --cnicehash true --apienable --apiport {ApiPort} --cpool {url} --cwallet {btcAdress}.{worker} --cpassword x --pools poolsV8.txt";
        }

        private string GetStartBenchmarkCommand(string url, string btcAdress, string worker)
        {
            var extras = ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.AMD);
            var algo = "cryptonightv8";
            var port = "3367";
            var variant = " --ccryptonighttype normalv8";
            url = url.Replace("stratum+tcp://", "");

            /*if (File.Exists(GetLogFileName()))
                File.Delete(GetLogFileName());*/
            Thread.Sleep(500);
            if (MiningSetup.CurrentAlgorithmType.Equals(AlgorithmType.kawpow))
            {
                algo = "kawpow";
                port = "3364";
                variant = " --ccryptonighttype heavy";
                return $" {variant} --cgpuid {GetDevicesCommandString().TrimStart()} {extras} --apienable --apiport {ApiPort} --cpool loki.miner.rocks:5555 --cwallet L95cF8XmPzzhBA1tkiL1NMijNNbj58vs1iJExK84oi2LKc6RQm2q1Z4PmDxYB7sicHVXY1J5YV9yg6vkMxKpuCK1L1SwoDi --cpassword w={ConfigManager.GeneralConfig.WorkerName.Trim()} --logfile ";
            }

            return $" {variant} --cgpuid {GetDevicesCommandString().TrimStart()} {extras} --apienable --apiport {ApiPort} --cpool xmr-eu.dwarfpool.com:8005 --cwallet 42fV4v2EC4EALhKWKNCEJsErcdJygynt7RJvFZk8HSeYA9srXdJt58D9fQSwZLqGHbijCSMqSP4mU7inEEWNyer6F7PiqeX.{worker} --cpassword x --logfile ";
        }

        protected override void _Stop(MinerStopType willswitch)
        {
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
        }

        protected override int GetMaxCooldownTimeInMilliseconds()
        {
            return 60 * 1000 * 5;  // 5 min
        }

        public override async Task<ApiData> GetSummaryAsync()
        {
            var ad = new ApiData(MiningSetup.CurrentAlgorithmType);
            string ResponseFromSRBMiner;
            try
            {
                HttpWebRequest WR = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:" + ApiPort.ToString());
                WR.UserAgent = "GET / HTTP/1.1\r\n\r\n";
                WR.Timeout = 30 * 1000;
                WR.Credentials = CredentialCache.DefaultCredentials;
                WebResponse Response = WR.GetResponse();
                Stream SS = Response.GetResponseStream();
                SS.ReadTimeout = 20 * 1000;
                StreamReader Reader = new StreamReader(SS);
                ResponseFromSRBMiner = await Reader.ReadToEndAsync();
                //Helpers.ConsolePrint("API...........", ResponseFromSRBMiner);
                //if (ResponseFromSRBMiner.Length == 0 || (ResponseFromSRBMiner[0] != '{' && ResponseFromSRBMiner[0] != '['))
                //    throw new Exception("Not JSON!");
                Reader.Close();
                Response.Close();
            }
            catch (Exception ex)
            {
                //Helpers.ConsolePrint("API", ex.Message);
                return null;
            }

            dynamic resp = JsonConvert.DeserializeObject(ResponseFromSRBMiner);

            if (resp != null)
            {
                int totals = resp.hashrate_total_now;
                //Helpers.ConsolePrint("API hashrate...........", totals.ToString());

                ad.Speed = totals;
                if (ad.Speed == 0)
                {
                    CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;
                }
                else
                {
                    CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
                }
            }

            Thread.Sleep(1000);
            return ad;
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
            //   _benchmarkTimeWait = time;
            return GetStartBenchmarkCommand(server, Globals.GetBitcoinUser(), ConfigManager.GeneralConfig.WorkerName.Trim());
        }

        /*
        protected override bool BenchmarkParseLine(string outdata)
        {
            //[2018-10-27 22:59:44] hashrate: GPU0: 932 H/s[BUS:2]
            //[2018 - 10 - 27 22:59:44] hashrate: Total: 932 H/s
                Helpers.ConsolePrint("SRBMiner bench:", outdata);
            if (_benchmarkException)
            {
                if (outdata.Contains("Total:"))
                {
                    TotalCount--;
                }

                if (outdata.Contains("Total: "))
                {
                    var st = outdata.IndexOf("Total: ");
                    var e = outdata.IndexOf("/s");
                    var parse = outdata.Substring(st + 7, e - st - 10).Trim().Replace(",", ".");
                    speed = Double.Parse(parse, CultureInfo.InvariantCulture);

                    if (outdata.ToUpper().Contains("KH/S"))
                        speed *= 1000;
                    else if (outdata.ToUpper().Contains("MH/S"))
                        speed *= 1000000;
                    else if (outdata.ToUpper().Contains("GH/S"))
                        speed *= 10000000000;
                }

                if (TotalCount <= 0)
                {
                    BenchmarkAlgorithm.BenchmarkSpeed = speed;
                    BenchmarkSignalFinnished = true;
                    return true;
                }

                return false;
            }
            return false;
        }
        */

        /*
        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            Helpers.ConsolePrint("BENCHMARK-1", "");
            CheckOutdata(outdata);
        }

        protected override void BenchmarkThreadRoutine(object CommandLine)
        {
            BenchmarkSignalQuit = false;
            BenchmarkSignalHanged = false;
            BenchmarkSignalFinnished = false;
            BenchmarkException = null;

            Thread.Sleep(ConfigManager.GeneralConfig.MinerRestartDelayMS);

            try
            {
                Helpers.ConsolePrint("BENCHMARK", "Benchmark starts");
                BenchmarkHandle = BenchmarkStartProcess((string)CommandLine);

                BenchmarkThreadRoutineStartSettup();
                BenchmarkTimeInSeconds = 300;
                BenchmarkProcessStatus = BenchmarkProcessStatus.Running;
                var exited = BenchmarkHandle.WaitForExit((BenchmarkTimeoutInSeconds(BenchmarkTimeInSeconds) + 20) * 1000);
                if (BenchmarkSignalTimedout && !TimeoutStandard)
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

                if (BenchmarkSignalHanged || !exited)
                {
                    throw new Exception("Miner is not responding");
                }

                if (BenchmarkSignalFinnished)
                {
                    //break;
                }
            }
            catch (Exception ex)
            {
                BenchmarkThreadRoutineCatch(ex);
            }
            finally
            {
                BenchmarkThreadRoutineFinish();
            }
        }
        */

        protected override void BenchmarkThreadRoutine(object CommandLine)
        {
            BenchmarkThreadRoutineAlternate(CommandLine, _benchmarkTimeWait);
        }

        protected override void ProcessBenchLinesAlternate(string[] lines)
        {
            foreach (var line in lines)
            {
                Helpers.ConsolePrint(MinerTag(), line);
                BenchLines.Add(line);
                var lineLowered = line.ToLower();

                if (lineLowered.Contains("Total:".ToLower()))
                {
                    var st = lineLowered.IndexOf("Total: ".ToLower());
                    var e = lineLowered.IndexOf("/s".ToLower());
                    var parse = lineLowered.Substring(st + 7, e - st - 9).Trim().Replace(",", ".");
                    speed = Double.Parse(parse, CultureInfo.InvariantCulture);

                    if (lineLowered.Contains("kh/s"))
                        speed *= 1000;
                    else if (lineLowered.Contains("mh/s"))
                        speed *= 1000000;

                    BenchmarkAlgorithm.BenchmarkSpeed = speed;
                }
            }
        }

        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            Helpers.ConsolePrint("SRBMiner BENCH-3", "");
            CheckOutdata(outdata);
        }

        protected override bool BenchmarkParseLine(string outdata)
        {
            Helpers.ConsolePrint("SRBMiner BENCH-4", "");
            Helpers.ConsolePrint(MinerTag(), outdata);
            return false;
        }

        #endregion Benchmark
    }
}