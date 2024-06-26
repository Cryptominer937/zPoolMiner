﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using zPoolMiner.Configs;
using zPoolMiner.Enums;
using zPoolMiner.Miners.Grouping;
using zPoolMiner.Miners.Parsing;

namespace zPoolMiner.Miners
{
    public class CryptoDredge26 : Miner
    {
        public CryptoDredge26() : base("CryptoDredge_NVIDIA")
        { }

        private int TotalCount;
        private double Total = 0;
        private const int TotalDelim = 2;
        private double speed = 0;
        private int count;
        private int _benchmarkTimeWait = 180;
        private bool _benchmarkException => MiningSetup.MinerPath == MinerPaths.Data.CryptoDredge26;

        protected override int GetMaxCooldownTimeInMilliseconds() => 60 * 1000 * 8;

        // protected override int GET_MAX_CoolUpTimeInMilliseconds()
        // {
        //    return 60 * 1000 * 8;
        // }
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

            if (!IsInit)
            {
                Helpers.ConsolePrint(MinerTag(), "MiningSetup is not initialized exiting Start()");
                return;
            }

            var address = btcAddress;
            IsApiReadException = MiningSetup.MinerPath == MinerPaths.Data.CryptoDredge26;
            var algo = "";
            var apiBind = "";

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.cryptonight_upx)
            { algo = "--algo cnupx2 "; }
            else
            { algo = "--algo " + MiningSetup.MinerName; }

            apiBind = " --api-bind 127.0.0.1:" + ApiPort;
            IsApiReadException = false;

            LastCommandLine = algo + " -o " + url + " -u " + address + " -p " + worker + "" + " " + apiBind + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA)
                + " --no-watchdog " + " -d " + GetDevicesCommandString() + " ";
            /*algo +
                " -o " + url + " -u " + username + " -p x " +
                " --url=stratum+tcp://" + alg + ".hk.nicehash.com:" + port + " " + " -u " + username + " -p x " +
                " -o " + alg + ".jp.nicehash.com:" + port + " " + " -u " + username + " -p x " +
                " -o " + alg + ".in.nicehash.com:" + port + " " + " -u " + username + " -p x " +
                " -o " + alg + ".br.nicehash.com:" + port + " " + " -u " + username + " -p x " +
                " -o " + alg + ".usa.nicehash.com:" + port + " " + " -u " + username + " -p x " +
                " -o " + alg + ".eu.nicehash.com:" + port + " -u " + username + " -p x " +
                apiBind +
                " -d " +*/
            // LastCommandLine += GetDevicesCommandString();
            ProcessHandle = _Start();
        }

        protected override void _Stop(MinerStopType willswitch)
        {
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
            Thread.Sleep(200);

            try
            {
                ProcessHandle.SendCtrlC((uint)Process.GetCurrentProcess().Id);
            }
            catch (Exception e)
            {
                // Helpers.ConsolePrint("Crypto Dredge", e.ToString());
            }

            Thread.Sleep(200);

            foreach (var process in Process.GetProcessesByName("CryptoDredge.exe"))
            {
                try
                {
                    process.Kill();
                    Thread.Sleep(200);
                    process.Kill();
                }
                catch (Exception e) { Helpers.ConsolePrint(MinerDeviceName, e.ToString()); }
            }
        }

        // new decoupled benchmarking routines

        #region Decoupled benchmarking routines

        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            var algo = "";
            var url = Globals.GetLocationURL(algorithm.CryptoMiner937ID, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], ConectionType);
            var alg = url.Substring(url.IndexOf("://") + 3, url.IndexOf(".") - url.IndexOf("://") - 3);
            var port = url.Substring(url.IndexOf(".com:") + 5, url.Length - url.IndexOf(".com:") - 5);
            var username = Globals.DemoUser;
            var worker = Globals.DemoWorker;

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.cryptonight_upx)
            { algo = "--algo cnupx2 "; }
            else
            { algo = "--algo " + MiningSetup.MinerName; }

            var commandLine = algo + " -o " + url + " -u " + username + " -p " + worker + "" + " " + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA) + " --no-watchdog " + " -d " + GetDevicesCommandString() + " ";
            // commandLine += GetDevicesCommandString();
            TotalCount = 2;
            Total = 0.0d;
            return commandLine;
        }

        protected override bool BenchmarkParseLine(string outdata)
        {
            if (_benchmarkException)
            {
                if (outdata.Contains("GPU") && outdata.Contains("/s"))
                {
                    var st = outdata.IndexOf("Avr ");
                    var e = outdata.IndexOf("/s)");
                    var parse = outdata.Substring(st + 4, e - st - 6).Trim();
                    var tmp = double.Parse(parse, CultureInfo.InvariantCulture);
                    // save speed
                    if (outdata.ToLower().Contains("kh/s"))
                        tmp *= 1000;
                    else if (outdata.ToLower().Contains("mh/s"))
                        tmp *= 1000000;
                    else if (outdata.ToLower().Contains("gh/s"))
                        tmp *= 1000000000;

                    speed += tmp;
                    count++;
                    TotalCount--;
                }

                if (TotalCount <= 0)
                {
                    BenchmarkAlgorithm.BenchmarkSpeed = speed / count;
                    BenchmarkSignalFinnished = true;
                    return true;
                }

                return false;
            }

            if (speed > 0.0d)
            {
                BenchmarkAlgorithm.BenchmarkSpeed = speed / count;
                return true;
            }

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
                    // break;
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

        #endregion Decoupled benchmarking routines

        public override async Task<ApiData> GetSummaryAsync()
        {
            CurrentMinerReadStatus = MinerApiReadStatus.NONE;
            var ad = new ApiData(MiningSetup.CurrentAlgorithmType, MiningSetup.CurrentSecondaryAlgorithmType);
            string resp = null;

            try
            {
                var bytesToSend = Encoding.ASCII.GetBytes("summary\r\n");
                var client = new TcpClient("127.0.0.1", ApiPort);
                var nwStream = client.GetStream();
                await nwStream.WriteAsync(bytesToSend, 0, bytesToSend.Length);
                var bytesToRead = new byte[client.ReceiveBufferSize];
                var bytesRead = await nwStream.ReadAsync(bytesToRead, 0, client.ReceiveBufferSize);
                var respStr = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);

                client.Close();
                resp = respStr;
                // Helpers.ConsolePrint(MinerTag(), "API: " + respStr);
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint(MinerTag(), "GetSummary exception: " + ex.Message);
            }

            if (resp != null)
            {
                var st = resp.IndexOf(";KHS=");
                var e = resp.IndexOf(";SOLV=");
                var parse = resp.Substring(st + 5, e - st - 5).Trim();
                var tmp = double.Parse(parse, CultureInfo.InvariantCulture);
                ad.Speed = tmp * 1000;

                if (ad.Speed == 0)
                {
                    CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;
                }
                else
                {
                    CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
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
    }
}