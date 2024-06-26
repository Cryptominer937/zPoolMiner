﻿namespace zPoolMiner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using zPoolMiner.Configs;
    using zPoolMiner.Enums;

    /// <summary>
    /// Defines the <see cref="CryptoMiner937API" />
    /// </summary>
    [Serializable]
    public class CryptoMiner937API
    {
        /// <summary>
        /// Defines the port
        /// </summary>
        public int port;

        /// <summary>
        /// Defines the name
        /// </summary>
        public string name;

        public string url;
        public string pool;

        /// <summary>
        /// Defines the algo
        /// </summary>
        public int algo;

        /// <summary>
        /// Defines the paying
        /// </summary>
        public double paying;
    }

    /// <summary>
    /// Defines the <see cref="CryptoMiner937Data" />
    /// </summary>
    public class CryptoMiner937Data
    {
        /// <summary>
        /// Defines the recentPaying
        /// </summary>
        private Dictionary<AlgorithmType, List<double>> recentPaying;

        /// <summary>
        /// Defines the currentSMA
        /// </summary>
        private Dictionary<AlgorithmType, CryptoMiner937API> currentSMA;

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoMiner937Data"/> class.
        /// </summary>
        public CryptoMiner937Data()
        {
            recentPaying = new Dictionary<AlgorithmType, List<double>>();
            var sma = new Dictionary<AlgorithmType, CryptoMiner937API>();

            foreach (AlgorithmType algo in Enum.GetValues(typeof(AlgorithmType)))
            {
                if (algo >= 0)
                {
                    sma[algo] = new CryptoMiner937API
                    {
                        port = (int)algo + 3333,
                        pool = algo.ToString().ToLower(),
                        name = algo.ToString().ToLower(),
                        url = algo.ToString().ToLower(),
                        algo = (int)algo,
                        paying = 0
                    };

                    recentPaying[algo] = new List<double> { 0 };
                }
            }

            currentSMA = sma;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoMiner937Data"/> class.
        /// </summary>
        /// <param name="data">The <see cref="ZPoolAlgo[]"/></param>
        public CryptoMiner937Data(ZPoolAlgo[] data)
        {
            recentPaying = new Dictionary<AlgorithmType, List<double>>();
            var sma = new Dictionary<AlgorithmType, CryptoMiner937API>();

            foreach (var algo in data)
            {
                sma[(AlgorithmType)algo.NiceHashAlgoId()] = new CryptoMiner937API
                {
                    port = algo.Port,
                    pool = ("mine.zergpool.com"),
                    name = algo.Algorithm.ToString().ToLower(),
                    url = algo.Algorithm.ToString().ToLower() + ".mine.zergpool.com:" + algo.Port,
                    algo = algo.NiceHashAlgoId(),
                    paying = 0
                };

                recentPaying[(AlgorithmType)algo.NiceHashAlgoId()] = new List<double> { 0 };
            }

            currentSMA = sma;
        }

        /// <summary>
        /// The AppendPayingForAlgo
        /// </summary>
        /// <param name="algo">The <see cref="AlgorithmType"/></param>
        /// <param name="paying">The <see cref="double"/></param>
        public void AppendPayingForAlgo(AlgorithmType algo, double paying)
        {
            if (algo >= 0 && recentPaying.ContainsKey(algo))
            {
                if (recentPaying[algo].Count >= ConfigManager.GeneralConfig.NormalizedProfitHistory || CurrentPayingForAlgo(algo) == 0)
                {
                    recentPaying[algo].RemoveAt(0);
                }

                recentPaying[algo].Add(paying);
            }
        }

        /// <summary>
        /// The NormalizedSMA
        /// </summary>
        /// <returns>The <see cref="Dictionary{AlgorithmType, CryptoMiner937API}"/></returns>
        public Dictionary<AlgorithmType, CryptoMiner937API> NormalizedSMA()
        {
            foreach (AlgorithmType algo in recentPaying.Keys)
            {
                if (currentSMA.ContainsKey(algo))
                {
                    var current = CurrentPayingForAlgo(algo);

                    if (ConfigManager.GeneralConfig.NormalizedProfitHistory > 0
                        && recentPaying[algo].Count >= ConfigManager.GeneralConfig.NormalizedProfitHistory)
                    {
                        // Find IQR
                        var quartiles = recentPaying[algo].Quartiles();
                        var IQR = quartiles.Item3 - quartiles.Item1;
                        var TQ = quartiles.Item3;

                        if (current > (IQR * ConfigManager.GeneralConfig.IQROverFactor) + TQ)
                        {  // result is deviant over
                            var norm = (IQR * ConfigManager.GeneralConfig.IQRNormalizeFactor) + TQ;

                            Helpers.ConsolePrint("PROFITNORM", string.Format("Algorithm {0} profit deviant, {1} IQRs over ({2} actual, {3} 3Q). Normalizing to {4}",
                                currentSMA[algo].name,
                                (current - TQ) / IQR,
                                current,
                                TQ,
                                norm));

                            currentSMA[algo].paying = (norm) * (.9);
                        }
                        else
                        {
                            currentSMA[algo].paying = (current) * (.9);
                        }
                    }
                    else
                    {
                        currentSMA[algo].paying = (current) * (.9);
                    }
                }
            }

            return currentSMA;
        }

        /// <summary>
        /// The CurrentPayingForAlgo
        /// </summary>
        /// <param name="algo">The <see cref="AlgorithmType"/></param>
        /// <returns>The <see cref="double"/></returns>
        private double CurrentPayingForAlgo(AlgorithmType algo)
        {
            if (recentPaying.ContainsKey(algo))
                return recentPaying[algo].LastOrDefault();

            return 0;
        }
    }
}