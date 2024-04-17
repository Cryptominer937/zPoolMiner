namespace zPoolMiner
{
    using System;
    using zPoolMiner.Enums;

    /// <summary>
    /// Defines the <see cref="Algorithm" />
    /// </summary>
    public class Algorithm
    {
        /// <summary>
        /// Defines the AlgorithmName
        /// </summary>
        public readonly string AlgorithmName;

        /// <summary>
        /// Defines the MinerBaseTypeName
        /// </summary>
        public readonly string MinerBaseTypeName;

        /// <summary>
        /// Defines the CryptoMiner937ID
        /// </summary>
        public readonly AlgorithmType CryptoMiner937ID;

        /// <summary>
        /// Defines the SecondaryCryptoMiner937ID
        /// </summary>
        public readonly AlgorithmType SecondaryCryptoMiner937ID;

        /// <summary>
        /// Defines the MinerBaseType
        /// </summary>
        public readonly MinerBaseType MinerBaseType;

        /// <summary>
        /// Defines the AlgorithmStringID
        /// </summary>
        public readonly string AlgorithmStringID;

        // Miner name is used for miner ALGO flag parameter
        // Miner name is used for miner ALGO flag parameter        /// <summary>
        /// Defines the MinerName
        /// </summary>
        public string MinerName;

        /// <summary>
        /// Gets or sets the BenchmarkSpeed
        /// </summary>
        public double BenchmarkSpeed { get; set; }

        /// <summary>
        /// Gets or sets the SecondaryBenchmarkSpeed
        /// </summary>
        public double SecondaryBenchmarkSpeed { get; set; }

        /// <summary>
        /// Gets or sets the ExtraLaunchParameters
        /// </summary>
        public string ExtraLaunchParameters { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Enabled
        /// </summary>
        public bool Enabled { get; set; }

        // CPU miners only setting

        // CPU miners only setting
        /// <summary>
        /// Gets or sets the LessThreads
        /// </summary>
        public int LessThreads { get; set; }

        // avarage speed of same devices to increase mining stability

        // avarage speed of same devices to increase mining stability
        /// <summary>
        /// Gets or sets the AvaragedSpeed
        /// </summary>
        public double AvaragedSpeed { get; set; }

        /// <summary>
        /// Gets or sets the SecondaryAveragedSpeed
        /// </summary>
        public double SecondaryAveragedSpeed { get; set; }

        // based on device and settings here we set the miner path
        // based on device and settings here we set the miner path        /// <summary>
        /// Defines the MinerBinaryPath
        /// </summary>
        public string MinerBinaryPath = "";

        // these are changing (logging reasons)
        // these are changing (logging reasons)        /// <summary>
        /// Defines the CurrentProfit
        /// </summary>
        public double CurrentProfit = 0;

        /// <summary>
        /// Defines the CurNhmSMADataVal
        /// </summary>
        public double CurNhmSMADataVal = 0;

        /// <summary>
        /// Defines the SecondaryCurNhmSMADataVal
        /// </summary>
        public double SecondaryCurNhmSMADataVal = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Algorithm"/> class.
        /// </summary>
        /// <param name="minerBaseType">The <see cref="MinerBaseType"/></param>
        /// <param name="cryptominer937ID">The <see cref="AlgorithmType"/></param>
        /// <param name="minerName">The <see cref="string"/></param>
        /// <param name="secondaryCryptoMiner937ID">The <see cref="AlgorithmType"/></param>
        public Algorithm(MinerBaseType minerBaseType, AlgorithmType cryptominer937ID, string minerName, AlgorithmType secondaryCryptoMiner937ID = AlgorithmType.NONE)
        {
            CryptoMiner937ID = cryptominer937ID;
            SecondaryCryptoMiner937ID = secondaryCryptoMiner937ID;

            AlgorithmName = AlgorithmCryptoMiner937Names.GetName(DualCryptoMiner937ID());
            MinerBaseTypeName = Enum.GetName(typeof(MinerBaseType), minerBaseType);
            AlgorithmStringID = MinerBaseTypeName + "_" + AlgorithmName;

            MinerBaseType = minerBaseType;
            MinerName = minerName;

            BenchmarkSpeed = 0.0d;
            SecondaryBenchmarkSpeed = 0.0d;
            ExtraLaunchParameters = "";
            LessThreads = 1;
            Enabled = !(CryptoMiner937ID == AlgorithmType.Nist5 || CryptoMiner937ID == AlgorithmType.Skein || CryptoMiner937ID == AlgorithmType.Blake2s || CryptoMiner937ID == AlgorithmType.Phi) && minerBaseType == MinerBaseType.sgminer;
            Enabled = !(CryptoMiner937ID == AlgorithmType.cryptonight) || (CryptoMiner937ID == AlgorithmType.Keccak) && minerBaseType == MinerBaseType.GatelessGate;
            Enabled = !(CryptoMiner937ID == AlgorithmType.karlsenhash) || (CryptoMiner937ID == AlgorithmType.pyrinhash) || (CryptoMiner937ID == AlgorithmType.ethash) || (CryptoMiner937ID == AlgorithmType.ethashb3) || (CryptoMiner937ID == AlgorithmType.nexapow) || (CryptoMiner937ID == AlgorithmType.sha512256d) || (CryptoMiner937ID == AlgorithmType.equihash144) || (CryptoMiner937ID == AlgorithmType.equihash192) && minerBaseType == MinerBaseType.lolMinerAmd;
            BenchmarkStatus = "";
        }

        // benchmark info

        // benchmark info
        /// <summary>
        /// Gets or sets the BenchmarkStatus
        /// </summary>
        public string BenchmarkStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsBenchmarkPending
        /// </summary>
        public bool IsBenchmarkPending { get; private set; }

        /// <summary>
        /// Gets the CurPayingRatio
        /// </summary>
        public string CurPayingRatio
        {
            get
            {
                // string ratio = International.GetText("BenchmarkRatioRateN_A");
                var ratio = "N/A Please Disable";

                if (Globals.CryptoMiner937Data != null)
                {
                    try
                    {
                        ratio = Globals.CryptoMiner937Data[CryptoMiner937ID].paying.ToString("F8");

                        if (IsDual() && Globals.CryptoMiner937Data.ContainsKey(SecondaryCryptoMiner937ID))
                        {
                            ratio += "/" + Globals.CryptoMiner937Data[SecondaryCryptoMiner937ID].paying.ToString("F8");
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("BAD API DATA", "Look in benchmarks for N/A to find the bad Algo");
                    }
                }

                return ratio;
            }
        }

        /// <summary>
        /// Gets the CurPayingRate
        /// </summary>
        public string CurPayingRate
        {
            get
            {
                var rate = International.GetText("BenchmarkRatioRateN_A");
                var payingRate = 0.0d;

                if (Globals.CryptoMiner937Data != null)
                {
                    if (BenchmarkSpeed > 0)
                    {
                        try
                        { payingRate += BenchmarkSpeed * Globals.CryptoMiner937Data[CryptoMiner937ID].paying * 0.000000001; }
                        catch
                        {
                            payingRate += 0;
                        }
                    }

                    if (SecondaryBenchmarkSpeed > 0 && IsDual())
                    {
                        payingRate += SecondaryBenchmarkSpeed * Globals.CryptoMiner937Data[SecondaryCryptoMiner937ID].paying * 0.000000001;
                    }

                    rate = payingRate.ToString("F8");
                }

                return rate;
            }
        }

        /// <summary>
        /// The SetBenchmarkPending
        /// </summary>
        public void SetBenchmarkPending()
        {
            IsBenchmarkPending = true;
            BenchmarkStatus = International.GetText("Algorithm_Waiting_Benchmark");
        }

        /// <summary>
        /// The SetBenchmarkPendingNoMsg
        /// </summary>
        public void SetBenchmarkPendingNoMsg() => IsBenchmarkPending = true;

        /// <summary>
        /// The IsPendingString
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        private bool IsPendingString()
        {
            return BenchmarkStatus == International.GetText("Algorithm_Waiting_Benchmark")
                || BenchmarkStatus == "."
                || BenchmarkStatus == ".."
                || BenchmarkStatus == "...";
        }

        /// <summary>
        /// The ClearBenchmarkPending
        /// </summary>
        public void ClearBenchmarkPending()
        {
            IsBenchmarkPending = false;

            if (IsPendingString()) BenchmarkStatus = "";
        }

        /// <summary>
        /// The ClearBenchmarkPendingFirst
        /// </summary>
        public void ClearBenchmarkPendingFirst()
        {
            IsBenchmarkPending = false;
            BenchmarkStatus = "";
        }

        /// <summary>
        /// The BenchmarkSpeedString
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string BenchmarkSpeedString()
        {
            if (Enabled && IsBenchmarkPending && !string.IsNullOrEmpty(BenchmarkStatus))
            {
                return BenchmarkStatus;
            }
            else if (BenchmarkSpeed > 0)
            {
                return Helpers.FormatDualSpeedOutput(CryptoMiner937ID, BenchmarkSpeed, SecondaryBenchmarkSpeed);
            }
            else if (!IsPendingString() && !string.IsNullOrEmpty(BenchmarkStatus))
            {
                return BenchmarkStatus;
            }

            return International.GetText("BenchmarkSpeedStringNone");
        }

        // return hybrid type if dual, else standard ID
        /// <summary>
        /// The DualCryptoMiner937ID
        /// </summary>
        /// <returns>The <see cref="AlgorithmType"/></returns>
        public AlgorithmType DualCryptoMiner937ID()
        {
            if (CryptoMiner937ID == AlgorithmType.DaggerHashimoto)
            {
                switch (SecondaryCryptoMiner937ID)
                {
                    case AlgorithmType.Decred:
                        return AlgorithmType.DaggerDecred;

                    case AlgorithmType.Lbry:
                        return AlgorithmType.DaggerLbry;

                    case AlgorithmType.Pascal:
                        return AlgorithmType.DaggerPascal;

                    case AlgorithmType.Sia:
                        return AlgorithmType.DaggerSia;

                    case AlgorithmType.Blake2s:
                        return AlgorithmType.DaggerBlake2s;
                }
            }

            return CryptoMiner937ID;
        }

        /// <summary>
        /// The IsDual
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool IsDual()
        {
            return AlgorithmType.DaggerBlake2s <= DualCryptoMiner937ID() && DualCryptoMiner937ID() <= AlgorithmType.DaggerPascal;
        }
    }
}