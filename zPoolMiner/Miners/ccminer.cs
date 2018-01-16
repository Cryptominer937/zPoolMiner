﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using zPoolMiner.Enums;
using zPoolMiner.Miners.Grouping;
using zPoolMiner.Miners.Parsing;

namespace zPoolMiner.Miners
{
    public class Ccminer : Miner
    {
        public Ccminer() : base("ccminer_NVIDIA")
        {
        }

        // cryptonight benchmark exception
        private int _cryptonightTotalCount = 0;

        private double _cryptonightTotal = 0;
        private const int _cryptonightTotalDelim = 2;

        private bool BenchmarkException
        {
            get
            {
                return MiningSetup.MinerPath == MinerPaths.Data.ccminer_cryptonight
                    || MiningSetup.MinerPath == MinerPaths.Data.ccminer_klaust;
            }
        }

        protected override int GET_MAX_CooldownTimeInMilliseconds()
        {
            if (this.MiningSetup.MinerPath == MinerPaths.Data.ccminer_x11gost)
            {
                return 60 * 1000 * 3; // wait a little longer
            }
            return 60 * 1000; // 1 minute max, whole waiting time 75seconds
        }

        public override void Start(string url, string btcAdress, string worker)
        {
            if (!IsInit)
            {
                Helpers.ConsolePrint(MinerTAG(), "MiningSetup is not initialized exiting Start()");
                return;
            }
            string username = GetUsername(btcAdress, worker);

            IsAPIReadException = MiningSetup.MinerPath == MinerPaths.Data.ccminer_cryptonight;

            string algo = "";
            string apiBind = "";
            if (!IsAPIReadException)
            {
                algo = "--algo=" + MiningSetup.MinerName;
                apiBind = " --api-bind=" + APIPort.ToString();
            }

            LastCommandLine = algo +
                                  " --url=" + url + " --userpass=" + username + ":" + worker + " " + apiBind + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup,DeviceType.NVIDIA) + " --devices ";

            LastCommandLine += GetDevicesCommandString();

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
            string timeLimit = (BenchmarkException) ? "" : " --time-limit " + time.ToString();
            string CommandLine = " --algo=" + algorithm.MinerName +
                              " --benchmark" +
                              timeLimit + " " +
                              ExtraLaunchParametersParser.ParseForMiningSetup(
                                                                MiningSetup,
                                                                DeviceType.NVIDIA) +
                              " --devices ";

            CommandLine += GetDevicesCommandString();

            // cryptonight exception helper variables
            _cryptonightTotalCount = BenchmarkTimeInSeconds / _cryptonightTotalDelim;
            _cryptonightTotal = 0.0d;

            return CommandLine;
        }

        protected override bool BenchmarkParseLine(string outdata)
        {
            // cryptonight exception
            if (BenchmarkException)
            {
                int speedLength = (BenchmarkAlgorithm.NiceHashID == AlgorithmType.CryptoNight) ? 6 : 8;
                if (outdata.Contains("Total: "))
                {
                    int st = outdata.IndexOf("Total:") + 7;
                    int len = outdata.Length - speedLength - st;

                    string parse = outdata.Substring(st, len).Trim();
                    Double.TryParse(parse, NumberStyles.Any, CultureInfo.InvariantCulture, out double tmp);

                    // save speed
                    int i = outdata.IndexOf("Benchmark:");
                    int k = outdata.IndexOf("/s");
                    string hashspeed = outdata.Substring(i + 11, k - i - 9);
                    int b = hashspeed.IndexOf(" ");
                    if (hashspeed.Contains("kH/s"))
                        tmp *= 1000;
                    else if (hashspeed.Contains("MH/s"))
                        tmp *= 1000000;
                    else if (hashspeed.Contains("GH/s"))
                        tmp *= 1000000000;

                    _cryptonightTotal += tmp;
                    _cryptonightTotalCount--;
                }
                if (_cryptonightTotalCount <= 0)
                {
                    double spd = _cryptonightTotal / (BenchmarkTimeInSeconds / _cryptonightTotalDelim);
                    BenchmarkAlgorithm.BenchmarkSpeed = spd;
                    BenchmarkSignalFinnished = true;
                }
            }

            double lastSpeed = BenchmarkParseLine_cpu_ccminer_extra(outdata);
            if (lastSpeed > 0.0d)
            {
                BenchmarkAlgorithm.BenchmarkSpeed = lastSpeed;
                return true;
            }

            if (double.TryParse(outdata, out lastSpeed))
            {
                BenchmarkAlgorithm.BenchmarkSpeed = lastSpeed;
                return true;
            }
            return false;
        }

        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            CheckOutdata(outdata);
        }

        #endregion Decoupled benchmarking routines

        public override async Task<APIData> GetSummaryAsync()
        {
            // CryptoNight does not have api bind port
            if (IsAPIReadException)
            {
                // check if running
                if (ProcessHandle == null)
                {
                    _currentMinerReadStatus = MinerAPIReadStatus.RESTART;
                    Helpers.ConsolePrint(MinerTAG(), ProcessTag() + " Could not read data from CryptoNight Proccess is null");
                    return null;
                }
                try
                {
                    var runningProcess = Process.GetProcessById(ProcessHandle.Id);
                }
                catch (ArgumentException ex)
                {
                    _currentMinerReadStatus = MinerAPIReadStatus.RESTART;
                    Helpers.ConsolePrint(MinerTAG(), ProcessTag() + " Could not read data from CryptoNight reason: " + ex.Message);
                    return null; // will restart outside
                }
                catch (InvalidOperationException ex)
                {
                    _currentMinerReadStatus = MinerAPIReadStatus.RESTART;
                    Helpers.ConsolePrint(MinerTAG(), ProcessTag() + " Could not read data from CryptoNight reason: " + ex.Message);
                    return null; // will restart outside
                }

                var totalSpeed = 0.0d;
                foreach (var miningPair in MiningSetup.MiningPairs)
                {
                    var algo = miningPair.Device.GetAlgorithm(MinerBaseType.ccminer, AlgorithmType.CryptoNight, AlgorithmType.NONE);
                    if (algo != null)
                    {
                        totalSpeed += algo.BenchmarkSpeed;
                    }
                }

                APIData CryptoNightData = new APIData(MiningSetup.CurrentAlgorithmType)
                {
                    Speed = totalSpeed
                };
                _currentMinerReadStatus = MinerAPIReadStatus.GOT_READ;
                // check if speed zero
                if (CryptoNightData.Speed == 0) _currentMinerReadStatus = MinerAPIReadStatus.READ_SPEED_ZERO;
                return CryptoNightData;
            }
            return await GetSummaryCPU_CCMINERAsync();
        }
    }
}