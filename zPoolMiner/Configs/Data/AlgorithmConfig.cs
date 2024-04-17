namespace zPoolMiner.Configs.Data
{
    using System;
    using zPoolMiner.Enums;

    /// <summary>
    /// Defines the <see cref="AlgorithmConfig" />
    /// </summary>
    [Serializable]
    public class AlgorithmConfig
    {
        /// <summary>
        /// Defines the Name
        /// </summary>
        public string Name = "";// Used as an indicator for easier user interaction

        /// <summary>
        /// Defines the CryptoMiner937ID
        /// </summary>
        public AlgorithmType CryptoMiner937ID = AlgorithmType.NONE;

        /// <summary>
        /// Defines the SecondaryCryptoMiner937ID
        /// </summary>
        public AlgorithmType SecondaryCryptoMiner937ID = AlgorithmType.NONE;

        /// <summary>
        /// Defines the MinerBaseType
        /// </summary>
        public MinerBaseType MinerBaseType = MinerBaseType.NONE;

        /// <summary>
        /// Defines the MinerName
        /// </summary>
        public string MinerName = "";// probably not needed

        /// <summary>
        /// Defines the BenchmarkSpeed
        /// </summary>
        public double BenchmarkSpeed = 0;

        /// <summary>
        /// Defines the SecondaryBenchmarkSpeed
        /// </summary>
        public double SecondaryBenchmarkSpeed = 0;

        /// <summary>
        /// Defines the ExtraLaunchParameters
        /// </summary>
        public string ExtraLaunchParameters = "";

        /// <summary>
        /// Defines the Enabled
        /// </summary>
        public bool Enabled = true;

        /// <summary>
        /// Defines the LessThreads
        /// </summary>
        public int LessThreads = 2;
    }
}