namespace zPoolMiner.Miners.Grouping
{
    using System;
    using System.Collections.Generic;
    using zPoolMiner.Devices;
    using zPoolMiner.Enums;

    /// <summary>
    /// Defines the <see cref="MiningDevice" />
    /// </summary>
    public class MiningDevice
    {
        // switch testing quick and dirty, runtime versions
#if (SWITCH_TESTING)
        static List<AlgorithmType> testingAlgos = new List<AlgorithmType>() {
            //AlgorithmType.X13,
            //AlgorithmType.Keccak,
            //AlgorithmType.X15,
            //AlgorithmType.Nist5,
            //AlgorithmType.NeoScrypt,
            AlgorithmType.Lyra2RE,
            //AlgorithmType.WhirlpoolX,
            //AlgorithmType.Qubit,
            //AlgorithmType.Quark,
            //AlgorithmType.Lyra2REv2,
            //AlgorithmType.Blake256r8,
            //AlgorithmType.Blake256r14,
            //AlgorithmType.Blake256r8vnl,
            AlgorithmType.Hodl,
            //AlgorithmType.DaggerHashimoto,
            //AlgorithmType.Decred,
            AlgorithmType.cryptonight,
            //AlgorithmType.Lbry,
            AlgorithmType.equihash
        };
        static int next = -1;
        public static void SetNextTest() {
            ++next;
            if (next >= testingAlgos.Count) next = 0;
            var mostProfitKeyName = AlgorithmCryptoMiner937Names.GetName(testingAlgos[next]);
            Helpers.ConsolePrint("SWITCH_TESTING", String.Format("Setting most MostProfitKey to {0}", mostProfitKeyName));
        }

        static bool ForceNone = false;
        // globals testing variables
        static int seconds = 20;
        public static int SMAMinerCheckInterval = seconds * 1000; // 30s
        public static bool ForcePerCardMiners = false;
#endif
        /// <summary>
        /// Initializes a new instance of the <see cref="MiningDevice"/> class.
        /// </summary>
        /// <param name="device">The <see cref="ComputeDevice"/></param>
        public MiningDevice(ComputeDevice device)
        {
            Device = device;
            foreach (var algo in Device.GetAlgorithmSettings())
            {
                bool isAlgoMiningCapable = GroupSetupUtils.IsAlgoMiningCapable(algo);
                bool isValidMinerPath = MinerPaths.IsValidMinerPath(algo.MinerBinaryPath);
                if (isAlgoMiningCapable && isValidMinerPath)
                {
                    Algorithms.Add(algo);
                }
            }
            MostProfitableAlgorithmType = AlgorithmType.NONE;
            MostProfitableMinerBaseType = MinerBaseType.NONE;
        }

        /// <summary>
        /// Gets or sets the Device
        /// </summary>
        public ComputeDevice Device { get; private set; }

        /// <summary>
        /// Defines the Algorithms
        /// </summary>
        public List<Algorithm> Algorithms = new List<Algorithm>();

        /// <summary>
        /// The GetMostProfitableString
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string GetMostProfitableString()
        {
            return
                Enum.GetName(typeof(MinerBaseType), MostProfitableMinerBaseType)
                + "_"
                + Enum.GetName(typeof(AlgorithmType), MostProfitableAlgorithmType);
        }

        /// <summary>
        /// Gets or sets the MostProfitableAlgorithmType
        /// </summary>
        public AlgorithmType MostProfitableAlgorithmType { get; private set; }

        /// <summary>
        /// Gets or sets the MostProfitableMinerBaseType
        /// </summary>
        public MinerBaseType MostProfitableMinerBaseType { get; private set; }

        // prev state

        // prev state
        /// <summary>
        /// Gets or sets the PrevProfitableAlgorithmType
        /// </summary>
        public AlgorithmType PrevProfitableAlgorithmType { get; private set; }

        /// <summary>
        /// Gets or sets the PrevProfitableMinerBaseType
        /// </summary>
        public MinerBaseType PrevProfitableMinerBaseType { get; private set; }

        /// <summary>
        /// The GetMostProfitableIndex
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        private int GetMostProfitableIndex()
        {
            return Algorithms.FindIndex((a) => a.DualCryptoMiner937ID() == MostProfitableAlgorithmType && a.MinerBaseType == MostProfitableMinerBaseType);
        }

        /// <summary>
        /// The GetPrevProfitableIndex
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        private int GetPrevProfitableIndex()
        {
            return Algorithms.FindIndex((a) => a.DualCryptoMiner937ID() == PrevProfitableAlgorithmType && a.MinerBaseType == PrevProfitableMinerBaseType);
        }

        /// <summary>
        /// Gets the GetCurrentMostProfitValue
        /// </summary>
        public double GetCurrentMostProfitValue
        {
            get
            {
                int mostProfitableIndex = GetMostProfitableIndex();
                if (mostProfitableIndex > -1)
                {
                    return Algorithms[mostProfitableIndex].CurrentProfit;
                }
                return 0;
            }
        }

        /// <summary>
        /// Gets the GetPrevMostProfitValue
        /// </summary>
        public double GetPrevMostProfitValue
        {
            get
            {
                int mostProfitableIndex = GetPrevProfitableIndex();
                if (mostProfitableIndex > -1)
                {
                    return Algorithms[mostProfitableIndex].CurrentProfit;
                }
                return 0;
            }
        }

        /// <summary>
        /// The GetMostProfitablePair
        /// </summary>
        /// <returns>The <see cref="MiningPair"/></returns>
        public MiningPair GetMostProfitablePair()
        {
            return new MiningPair(Device, Algorithms[GetMostProfitableIndex()]);
        }

        /// <summary>
        /// The HasProfitableAlgo
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool HasProfitableAlgo()
        {
            return GetMostProfitableIndex() > -1;
        }

        /// <summary>
        /// The RestoreOldProfitsState
        /// </summary>
        public void RestoreOldProfitsState()
        {
            // restore last state
            MostProfitableAlgorithmType = PrevProfitableAlgorithmType;
            MostProfitableMinerBaseType = PrevProfitableMinerBaseType;
        }

        /// <summary>
        /// The SetNotMining
        /// </summary>
        public void SetNotMining()
        {
            // device isn't mining (e.g. below profit threshold) so set state to none
            PrevProfitableAlgorithmType = AlgorithmType.NONE;
            PrevProfitableMinerBaseType = MinerBaseType.NONE;
            MostProfitableAlgorithmType = AlgorithmType.NONE;
            MostProfitableMinerBaseType = MinerBaseType.NONE;
        }

        /// <summary>
        /// The CalculateProfits
        /// </summary>
        /// <param name="CryptoMiner937Data">The <see cref="Dictionary{AlgorithmType, CryptoMiner937API}"/></param>
        public void CalculateProfits(Dictionary<AlgorithmType, CryptoMiner937API> CryptoMiner937Data)
        {
            // save last state
            PrevProfitableAlgorithmType = MostProfitableAlgorithmType;
            PrevProfitableMinerBaseType = MostProfitableMinerBaseType;
            // assume none is profitable
            MostProfitableAlgorithmType = AlgorithmType.NONE;
            MostProfitableMinerBaseType = MinerBaseType.NONE;
            // calculate new profits
            foreach (var algo in Algorithms)
            {
                AlgorithmType key = algo.CryptoMiner937ID;
                AlgorithmType secondaryKey = algo.SecondaryCryptoMiner937ID;
                if (CryptoMiner937Data.ContainsKey(key))
                {
                    algo.CurNhmSMADataVal = CryptoMiner937Data[key].paying;
                    algo.CurrentProfit = algo.CurNhmSMADataVal * algo.AvaragedSpeed * 0.000000001;
                    if (CryptoMiner937Data.ContainsKey(secondaryKey))
                    {
                        algo.SecondaryCurNhmSMADataVal = CryptoMiner937Data[secondaryKey].paying;
                        algo.CurrentProfit += algo.SecondaryCurNhmSMADataVal * algo.SecondaryAveragedSpeed * 0.000000001;
                    }
                }
                else
                {
                    algo.CurrentProfit = 0;
                }
            }
            // find max paying value and save key
            double maxProfit = 0;
            foreach (var algo in Algorithms)
            {
                if (maxProfit < algo.CurrentProfit)
                {
                    maxProfit = algo.CurrentProfit;
                    MostProfitableAlgorithmType = algo.DualCryptoMiner937ID();
                    MostProfitableMinerBaseType = algo.MinerBaseType;
                }
            }
        }
    }
}
