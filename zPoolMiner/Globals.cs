namespace zPoolMiner
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using zPoolMiner.Enums;

    /// <summary>
    /// Defines the <see cref="Globals" />
    /// </summary>
    public class Globals
    {
        // Constants
        // Constants        /// <summary>
        /// Defines the MiningLocation
        /// </summary>
        public static string[] MiningLocation = { "", "", "", "", "", "" };

        /// <summary>
        /// Defines the DemoUser
        /// </summary>
        public static readonly string DemoUser = "DE8BDPdYu9LadwV4z4KamDqni43BUhGb66";
        public static readonly string DemoWorker = "c=DOGE,Devfee";

        // change this if TOS changes
        // change this if TOS changes        /// <summary>
        /// Defines the CURRENT_TOS_VER
        /// </summary>
        public static int CURRENT_TOS_VER = 3;

        // Variables
        // Variables        /// <summary>
        /// Defines the CryptoMiner937Data
        /// </summary>
        public static Dictionary<AlgorithmType, CryptoMiner937API> CryptoMiner937Data = null;

        /// <summary>
        /// Defines the BitcoinUSDRate
        /// </summary>
        public static double BitcoinUSDRate;

        /// <summary>
        /// Defines the JsonSettings
        /// </summary>
        public static JsonSerializerSettings JsonSettings = null;

        /// <summary>
        /// Defines the ThreadsPerCPU
        /// </summary>
        public static int ThreadsPerCPU;

        // quickfix guard for checking internet conection
        // quickfix guard for checking internet conection        /// <summary>
        /// Defines the IsFirstNetworkCheckTimeout
        /// </summary>
        public static bool IsFirstNetworkCheckTimeout = true;

        /// <summary>
        /// Defines the FirstNetworkCheckTimeoutTimeMS
        /// </summary>
        public static int FirstNetworkCheckTimeoutTimeMS = 500;

        /// <summary>
        /// Defines the FirstNetworkCheckTimeoutTries
        /// </summary>
        public static int FirstNetworkCheckTimeoutTries = 10;

        /// <summary>
        /// The GetLocationURL
        /// </summary>
        /// <param name="AlgorithmType">The <see cref="AlgorithmType"/></param>
        /// <param name="miningLocation">The <see cref="string"/></param>
        /// <param name="ConectionType">The <see cref="NHMConectionType"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string GetLocationURL(AlgorithmType AlgorithmType, string miningLocation, NHMConectionType ConectionType)
        {
            if (Globals.CryptoMiner937Data != null && Globals.CryptoMiner937Data.ContainsKey(AlgorithmType))
            {
                string name = Globals.CryptoMiner937Data[AlgorithmType].name;
                string url = Globals.CryptoMiner937Data[AlgorithmType].url;
                int n_port = Globals.CryptoMiner937Data[AlgorithmType].port;
                int ssl_port = 30000 + n_port;

                // NHMConectionType.NONE
                string prefix = "";
                int port = n_port;
                if (NHMConectionType.LOCKED == ConectionType)
                {
                    return miningLocation;
                }
                if (NHMConectionType.STRATUM_TCP == ConectionType)
                {
                    prefix = "stratum+tcp://";
                }
                if (NHMConectionType.STRATUM_SSL == ConectionType)
                {
                    throw new NotImplementedException("zPool does not support stratum+ssl");
                    //prefix = "stratum+ssl://";
                    //port = ssl_port;
                }

                return prefix
                        + url;
            }
            return "";
        }

        /// <summary>
        /// The GetBitcoinUser
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public static string GetBitcoinUser()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.BitcoinAddress.Trim())))
            {
                //Helpers.ConsolePrint("Address Get", "Main Address");
                return Configs.ConfigManager.GeneralConfig.BitcoinAddress.Trim();
            }
            else
            {
                return DemoUser;
            }
        }
        public static string GetzpoolUser()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.zpoolAddress.Trim())))
            {
                //Helpers.ConsolePrint("Address Get", "zpool Address");
                return Configs.ConfigManager.GeneralConfig.zpoolAddress.Trim();
            }
            else
            {
                return DemoUser;
            }
        }
        public static string GetahashUser()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.ahashAddress.Trim())))
            {
                //Helpers.ConsolePrint("Address Get", "ahash Address");
                return Configs.ConfigManager.GeneralConfig.ahashAddress.Trim();
            }
            else
            {
                return DemoUser;
            }
        }
        public static string GethashrefineryUser()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.hashrefineryAddress.Trim())))
            {
                //Helpers.ConsolePrint("Address Get", "hashrefinery Address");
                return Configs.ConfigManager.GeneralConfig.hashrefineryAddress.Trim();
            }
            else
            {
                return DemoUser;
            }
        }
        public static string GetnicehashUser()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.nicehashAddress.Trim())))
            {
               // Helpers.ConsolePrint("Address Get", "nicehash");
                return Configs.ConfigManager.GeneralConfig.nicehashAddress.Trim();
            }
            else
            {
                return DemoUser;
            }
        }
        public static string GetzergUser()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.zergAddress.Trim())))
            {
                //Helpers.ConsolePrint("Address Get", "zergpool Address");
                return Configs.ConfigManager.GeneralConfig.zergAddress.Trim();
            }
            else
            {
                return DemoUser;
            }
        }
        public static string GetMPHUser()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.MPHAddress.Trim())))
            {
                //Helpers.ConsolePrint("Address Get", "MPH Address");
                return Configs.ConfigManager.GeneralConfig.MPHAddress.Trim();
            }
            else
            {
                return "cryptominer937.invalid";
            }
        }
        public static string GetminemoneyUser()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.minemoneyAddress.Trim())))
            {
                //Helpers.ConsolePrint("Address Get", "MineMoney Address");
                return Configs.ConfigManager.GeneralConfig.minemoneyAddress.Trim();
            }
            else
            {
                return DemoUser;
            }
        }
        public static string GetstarpoolUser()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.starpoolAddress.Trim())))
            {
                //Helpers.ConsolePrint("Address Get", "starpool Address");
                return Configs.ConfigManager.GeneralConfig.starpoolAddress.Trim();
            }
            else
            {
                return DemoUser;
            }
        }
        public static string GetblockmunchUser()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.blockmunchAddress.Trim())))
            {
                //Helpers.ConsolePrint("Address Get", "blockmunch Address");
                return Configs.ConfigManager.GeneralConfig.blockmunchAddress.Trim();
            }
            else
            {
                return DemoUser;
            }
        }
        public static string GetblazepoolUser()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.blazepoolAddress.Trim())))
            {
                //Helpers.ConsolePrint("Address Get", "blazepool Address");
                return Configs.ConfigManager.GeneralConfig.blazepoolAddress.Trim();
            }
            else
            {
                return DemoUser;
            }
        }
        public static string GetzpoolWorker()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.zpoolWorkerName.Trim())))
            {
                return Configs.ConfigManager.GeneralConfig.zpoolWorkerName.Trim();
            }
            else
            {
                return DemoWorker;
            }
        }
        public static string GetahashWorker()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.ahashWorkerName.Trim())))
            {
                return Configs.ConfigManager.GeneralConfig.ahashWorkerName.Trim();
            }
            else
            {
                return DemoWorker;
            }
        }
        public static string GethashrefineryWorker()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.hashrefineryWorkerName.Trim())))
            {
                return Configs.ConfigManager.GeneralConfig.hashrefineryWorkerName.Trim();
            }
            else
            {
                return DemoWorker;
            }
        }
        public static string GetnicehashWorker()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.nicehashWorkerName.Trim())))
            {
                return Configs.ConfigManager.GeneralConfig.nicehashWorkerName.Trim();
            }
            else
            {
                return DemoWorker;
            }
        }
        public static string GetzergWorker()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.zergWorkerName.Trim())))
            {
                return Configs.ConfigManager.GeneralConfig.zergWorkerName.Trim();
            }
            else
            {
                return DemoWorker;
            }
        }
        public static string GetminemoneyWorker()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.minemoneyWorkerName.Trim())))
            {
                return Configs.ConfigManager.GeneralConfig.minemoneyWorkerName.Trim();
            }
            else
            {
                return DemoWorker;
            }
        }
        public static string GetstarpoolWorker()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.starpoolWorkerName.Trim())))
            {
                return Configs.ConfigManager.GeneralConfig.starpoolWorkerName.Trim();
            }
            else
            {
                return DemoWorker;
            }
        }
        public static string GetblockmunchWorker()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.blockmunchWorkerName.Trim())))
            {
                return Configs.ConfigManager.GeneralConfig.blockmunchWorkerName.Trim();
            }
            else
            {
                return DemoWorker;
            }
        }
        public static string GetblazepoolWorker()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.blazepoolWorkerName.Trim())))
            {
                return Configs.ConfigManager.GeneralConfig.blazepoolWorkerName.Trim();
            }
            else
            {
                return DemoWorker;
            }
        }
        public static string GetMPHWorker()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.MPHWorkerName.Trim())))
            {
                return Configs.ConfigManager.GeneralConfig.MPHWorkerName.Trim();
            }
            else
            {
                return "x";
            }
        }
    }
}
