using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using zPoolMiner.Configs;
using zPoolMiner.Enums;
using zPoolMiner.Miners.Grouping;
using zPoolMiner.Miners.Parsing;

namespace zPoolMiner.Miners
{
    public class ZEnemy : Miner
    {
        public ZEnemy() : base("Z-Enemy_NVIDIA")
        { }
        private int TotalCount = 0;
        private double Total = 0;
        private double speed;
        private const int TotalDelim = 2;
        private bool _benchmarkException => MiningSetup.MinerPath == MinerPaths.Data.ZEnemy;
        protected override int GetMaxCooldownTimeInMilliseconds()
        {
            return 60 * 1000 * 8;
        }
        //protected override int GET_MAX_CoolUpTimeInMilliseconds()
        //{
        //    return 60 * 1000 * 8;
        //}
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
            if (!IsInit)
            {
                Helpers.ConsolePrint(MinerTag(), "MiningSetup is not initialized exiting Start()");
                return;
            }
            string address = btcAddress;
            // IsApiReadException = MiningSetup.MinerPath == MinerPaths.Data.ZEnemy;
            var algo = "";
            var apiBind = "";
            algo = "-a " + MiningSetup.MinerName;
            apiBind = " --api-bind=" + ApiPort;
            LastCommandLine = algo +
               " -o " + url + " -u " + address + " -p " + worker + "" + "" + apiBind +
               " --devices " + GetDevicesCommandString() + " " +
               ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA) + " ";
            ProcessHandle = _Start();
        }
        protected override void _Stop(MinerStopType willswitch)
        {
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
        }
        // new decoupled benchmarking routines
        #region Decoupled benchmarking routines
        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            string url = Globals.GetLocationURL(algorithm.CryptoMiner937ID, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], ConectionType);
            //string url = "stratum+tcp://" + algorithm.algorithUrl;
            string alg = url.Substring(url.IndexOf("://") + 3, url.IndexOf(".") - url.IndexOf("://") - 3);
            string port = url.Substring(url.IndexOf(".com:") + 5, url.Length - url.IndexOf(".com:") - 5);
            var username = Globals.DemoUser;
            var worker = " -p c=BTC,Benchmark ";
            var timeLimit = (_benchmarkException) ? "" : " --time-limit 300";
            var commandLine = " -a " + algorithm.MinerName + " -o " + url + " -u " + username + " " + worker +
                              timeLimit + " " +
                              ExtraLaunchParametersParser.ParseForMiningSetup(
                                  MiningSetup,
                                  DeviceType.NVIDIA) +
                              "--no-color  --devices ";
            commandLine += GetDevicesCommandString();
            TotalCount = 2;
            Total = 0.0d;
            return commandLine;
        }
        protected override bool BenchmarkParseLine(string outdata)
        {
            int count = 0;
            double tmp = 0;


            if (_benchmarkException)
            {
                if (outdata.Contains("GPU") && outdata.Contains("/s")) //GPU#4: ASUS GTX 1060 3GB, 10.56MH/s
                //GPU#4: ASUS GTX 1060 3GB - 14.80MH/s [T:42C, F:54%, P:111W, E:0.13MH/W]
                //GPU#4: ASUS GTX 1060 3GB - 8765.76kH/s [T:41C, F:54%, P:111W, E:0.079MH/W]
                //GPU#4: ASUS GTX 1060 3GB - 25.58MH/s [T:32C, F:42%, P:103W, E:0.25MH/W]
                {

                    var st = outdata.IndexOf("- ");
                    var e = outdata.IndexOf("/s [");
                    try
                    {
                        var parse = outdata.Substring(st + 2, e - st - 4).Trim();
                        tmp = Double.Parse(parse, CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        MessageBox.Show("Unsupported miner version - " + MiningSetup.MinerPath,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        BenchmarkSignalFinnished = true;
                        return false;
                    }
                    // save speed

                    if (outdata.ToUpper().Contains("KH/S"))
                        tmp *= 1000;
                    else if (outdata.ToUpper().Contains("MH/S"))
                        tmp *= 1000000;
                    else if (outdata.ToUpper().Contains("GH/S"))
                        tmp *= 1000000000;

                    speed = tmp;
                    count++;
                    TotalCount--;
                }

                if (TotalCount <= 0)
                {
                    BenchmarkAlgorithm.BenchmarkSpeed = speed;
                    BenchmarkSignalFinnished = true;
                    return true;
                }

                // return false;
            }
            /*
            if (speed > 0.0d)
            {
                BenchmarkAlgorithm.BenchmarkSpeed = speed/2;
                return true;
            }
            */
            return false;
        }
        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
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
        #endregion // Decoupled benchmarking routines
        public override async Task<APIData> GetSummaryAsync()
        {
            if (!IsApiReadException) return await GetSummaryCPU_CCMINERAsync();
            // check if running
            if (ProcessHandle == null)
            {
                CurrentMinerReadStatus = MinerApiReadStatus.RESTART;
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Could not read data from CryptoNight Proccess is null");
                return null;
            }
            try
            {
                Process.GetProcessById(ProcessHandle.Id);
            }
            catch (ArgumentException ex)
            {
                CurrentMinerReadStatus = MinerApiReadStatus.RESTART;
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Could not read data from CryptoNight reason: " + ex.Message);
                return null; // will restart outside
            }
            catch (InvalidOperationException ex)
            {
                CurrentMinerReadStatus = MinerApiReadStatus.RESTART;
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Could not read data from CryptoNight reason: " + ex.Message);
                return null; // will restart outside
            }
            var totalSpeed = MiningSetup.MiningPairs
               .Select(miningPair =>
                   miningPair.Device.GetAlgorithm(MinerBaseType.ZEnemy, AlgorithmType.x16r, AlgorithmType.NONE))
               .Where(algo => algo != null).Sum(algo => algo.BenchmarkSpeed);
            var zenemyData = new APIData(MiningSetup.CurrentAlgorithmType)
            {
                Speed = totalSpeed
            };
            CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
            // check if speed zero
            if (zenemyData.Speed == 0) CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;
            return zenemyData;
        }
    }
}
