using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using zPoolMiner.Configs;
using zPoolMiner.Devices;
using zPoolMiner.Miners.Grouping;
using zPoolMiner.Miners.Parsing;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using zPoolMiner.Enums;
using System.Linq;

namespace zPoolMiner.Miners
{
    public class trex : Miner
    {
        private int _benchmarkTimeWait = 60;

        private const int TotalDelim = 2;
        public trex() : base("trex_NVIDIA")
        {
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

            if (!IsInit)
            {
                Helpers.ConsolePrint(MinerTAG(), "MiningSetup is not initialized exiting Start()");
                return;
            }
            string address = btcAddress;



            IsAPIReadException = MiningSetup.MinerPath == MinerPaths.Data.trex;
            
            var apiBind = "--api-bind-telnet 127.0.0.1:" + APIPort;
            var apiBindHttp = "-b 127.0.0.1:" + APIPort;

            //apiBind = " --api-bind 127.0.0.1:" + ApiPort;

            IsAPIReadException = true; //no api
                                       /*
                                       LastCommandLine = algo +
                                           " -o " + url + " -u " + username + " -p x " +
                                           " --url=stratum+tcp://" + alg + ".hk.nicehash.com:" + port + " " + " -u " + username + " -p x " +
                                           " -o " + alg + ".jp.nicehash.com:" + port + " " + " -u " + username + " -p x " +
                                           " -o " + alg + ".in.nicehash.com:" + port + " " + " -u " + username + " -p x " +
                                           " -o " + alg + ".br.nicehash.com:" + port + " " + " -u " + username + " -p x " +
                                           " -o " + alg + ".usa.nicehash.com:" + port + " " + " -u " + username + " -p x " +
                                           " -o " + alg + ".eu.nicehash.com:" + port + " -u " + username + " -p x " +
                                           apiBind +
                                           " -d " + GetDevicesCommandString() + " " +
                                           ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA) + " ";
                                           */
            LastCommandLine = " -a " + MiningSetup.MinerName + " -o "+ url + " -u " + address + " -p " + worker +"" + " " + apiBindHttp + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA) + " -d ";
            LastCommandLine += GetDevicesCommandString();
            ProcessHandle = _Start();
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

            string url = Globals.GetLocationURL(algorithm.CryptoMiner937ID, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], ConectionType);
            var apiBindHttp = "-b 127.0.0.1:" + APIPort;

            string address = Globals.DemoUser;


            LastCommandLine = " -a " + MiningSetup.MinerName + " -o " + url + " -u " + address + " -p c=BTC,Benchmark" + " " + apiBindHttp + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA) + " -d ";
            LastCommandLine += GetDevicesCommandString();
            

            return LastCommandLine;
        }

        protected override void BenchmarkThreadRoutine(object commandLine)
        {
            BenchmarkSignalQuit = false;
            BenchmarkSignalHanged = false;
            BenchmarkSignalFinnished = false;
            BenchmarkException = null;
            

            Thread.Sleep(ConfigManager.GeneralConfig.MinerRestartDelayMS);

            Helpers.ConsolePrint("BENCHMARK", "Benchmark starts");
            Helpers.ConsolePrint(MinerTAG(), "Benchmark should end in : " + _benchmarkTimeWait + " seconds");

            BenchmarkHandle = BenchmarkStartProcess((string)commandLine);
            BenchmarkHandle.WaitForExit(_benchmarkTimeWait + 2);

            var benchmarkTimer = new Stopwatch();
            benchmarkTimer.Reset();
            benchmarkTimer.Start();
            BenchmarkProcessStatus = BenchmarkProcessStatus.Running;

            var keepRunning = true;
            List<double> speed = new List<double>();

            try
            {
                while (keepRunning && IsActiveProcess(BenchmarkHandle.Id))
                {
                    Task<APIData> AD = GetSummaryAsync();
                    Task.WaitAll(AD);

                    speed.Add(AD.Result.Speed);

                    //string outdata = BenchmarkHandle.StandardOutput.ReadLine();
                    //BenchmarkOutputErrorDataReceivedImpl(outdata);
                    // terminate process situations
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
            }catch(Exception ex)
            {
                  BenchmarkThreadRoutineCatch(ex);
                Helpers.ConsolePrint("Benchmark Error:", " " + ex.ToString());
            }
            finally
            {
                if (speed.Count == 0)
                {
                    Helpers.ConsolePrint("Benchmark Error:", " Benchmarks returned 0" );

                }
                else
                {
                    BenchmarkAlgorithm.BenchmarkSpeed = 0;
                    BenchmarkAlgorithm.BenchmarkSpeed = speed.Average();
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
            //Helpers.ConsolePrint("BENCHMARK", outdata);
            return false;
        }



        public override async Task<APIData> GetSummaryAsync()
        {
            _currentMinerReadStatus = MinerAPIReadStatus.NONE;
            var ad = new APIData(MiningSetup.CurrentAlgorithmType, MiningSetup.CurrentSecondaryAlgorithmType);
            string resp = null;
            try
            {
                var bytesToSend = Encoding.ASCII.GetBytes("summary\r\n");
                var client = new TcpClient("127.0.0.1", APIPort);
                var nwStream = client.GetStream();
                await nwStream.WriteAsync(bytesToSend, 0, bytesToSend.Length);
                var bytesToRead = new byte[client.ReceiveBufferSize];
                var bytesRead = await nwStream.ReadAsync(bytesToRead, 0, client.ReceiveBufferSize);
                var respStr = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);

                client.Close();
                resp = respStr;
                //Helpers.ConsolePrint(MinerTag(), "API: " + respStr);
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint(MinerTAG(), "GetSummary exception: " + ex.Message);
            }

            if (resp != null)
            {
                var st = resp.IndexOf(";KHS=");
                var e = resp.IndexOf(";SOLV=");
                var parse = resp.Substring(st + 5, e - st - 5).Trim();
                double tmp = Double.Parse(parse, CultureInfo.InvariantCulture);
                ad.Speed = tmp * 1000;




                if (ad.Speed == 0)
                {
                    _currentMinerReadStatus = MinerAPIReadStatus.READ_SPEED_ZERO;
                }
                else
                {
                    _currentMinerReadStatus = MinerAPIReadStatus.GOT_READ;
                }

                // some clayomre miners have this issue reporting negative speeds in that case restart miner
                if (ad.Speed < 0)
                {
                    Helpers.ConsolePrint(MinerTAG(), "Reporting negative speeds will restart...");
                    Restart();
                }
            }

            return ad;
        }





        protected override void _Stop(MinerStopType willswitch)
        {
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
        }
        
        //protected override int GET_MAX_CoolUpTimeInMilliseconds()
        //{
        //    return 5 * 60 * 1000; // 5 min
        //}

        protected override int GET_MAX_CooldownTimeInMilliseconds()
        {
            return 60 * 1000 * 5; // 5 minute max, whole waiting time 75seconds
        }
    }

    public class Gpu
    {
        public int device_id { get; set; }
        public int gpu_id { get; set; }
        public int hashrate { get; set; }
        //public int intensity { get; set; }
    }

    public class RootObject
    {
        public int accepted_count { get; set; }
        public string algorithm { get; set; }
        public string api { get; set; }
        public string cuda { get; set; }
        public string description { get; set; }
        public double difficulty { get; set; }
        public int gpu_total { get; set; }
        public List<Gpu> gpus { get; set; }
        public int hashrate { get; set; }
        public string name { get; set; }
        public string os { get; set; }
        public int rejected_count { get; set; }
        public int ts { get; set; }
        public int uptime { get; set; }
        public string version { get; set; }
    }
}
