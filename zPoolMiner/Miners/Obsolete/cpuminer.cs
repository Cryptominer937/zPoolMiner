﻿using System.Diagnostics;
using System.Threading.Tasks;
using zPoolMiner.Enums;
using zPoolMiner.Miners.Grouping;
using zPoolMiner.Miners.Parsing;

namespace zPoolMiner.Miners
{
    public class Cpuminer : Miner
    {
        public Cpuminer()
            : base("cpuminer_CPU")
        {
        }

        protected override int GET_MAX_CooldownTimeInMilliseconds()
        {
            return 3600000; // 1hour
        }

        public override void Start(string url, string btcAdress, string worker)
        {
            if (!IsInit)
            {
                Helpers.ConsolePrint(MinerTAG(), "MiningSetup is not initialized exiting Start()");
                return;
            }
            string username = GetUsername(btcAdress, worker);

            LastCommandLine = "--algo=" + MiningSetup.MinerName +
                              " --url=" + url +
                              " --userpass=" + username + ":x " +
                              ExtraLaunchParametersParser.ParseForMiningSetup(
                                                                MiningSetup,
                                                                DeviceType.CPU) +
                              " --api-bind=" + APIPort.ToString();

            ProcessHandle = _Start();
        }

        public override Task<APIData> GetSummaryAsync()
        {
            return GetSummaryCPU_CCMINERAsync();
        }

        protected override void _Stop(MinerStopType willswitch)
        {
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
        }

        protected override NiceHashProcess _Start()
        {
            NiceHashProcess P = base._Start();

            var AffinityMask = MiningSetup.MiningPairs[0].Device.AffinityMask;
            if (AffinityMask != 0 && P != null)
                CPUID.AdjustAffinity(P.Id, AffinityMask);

            return P;
        }

        // new decoupled benchmarking routines

        #region Decoupled benchmarking routines

        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            return "--algo=" + algorithm.MinerName +
                         " --benchmark" +
                         ExtraLaunchParametersParser.ParseForMiningSetup(
                                                                MiningSetup,
                                                                DeviceType.CPU) +
                         " --time-limit " + time.ToString();
        }

        protected override Process BenchmarkStartProcess(string CommandLine)
        {
            Process BenchmarkHandle = base.BenchmarkStartProcess(CommandLine);

            var AffinityMask = MiningSetup.MiningPairs[0].Device.AffinityMask;
            if (AffinityMask != 0 && BenchmarkHandle != null)
                CPUID.AdjustAffinity(BenchmarkHandle.Id, AffinityMask);

            return BenchmarkHandle;
        }

        protected override bool BenchmarkParseLine(string outdata)
        {
            if (double.TryParse(outdata, out double lastSpeed))
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
    }
}