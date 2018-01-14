using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using zPoolMiner.Enums;

namespace zPoolMiner
{
    public class Globals
    {
        // Constants
        public static string[] MiningLocation = { "eu", "usa", "hk", "jp", "in", "br" };

        public static readonly string DemoUser = "197LigiMdTnFrVwySeiEtLCiDoaGGzma67";

        // change this if TOS changes
        public static int CURRENT_TOS_VER = 3;

        // Variables
        public static Dictionary<AlgorithmType, NiceHashSMA> NiceHashData = null;

        public static double BitcoinUSDRate;
        public static JsonSerializerSettings JsonSettings = null;
        public static int ThreadsPerCPU;

        // quickfix guard for checking internet conection
        public static bool IsFirstNetworkCheckTimeout = true;

        public static int FirstNetworkCheckTimeoutTimeMS = 500;
        public static int FirstNetworkCheckTimeoutTries = 10;

        public static string GetLocationURL(AlgorithmType AlgorithmType, string miningLocation, NHMConectionType ConectionType)
        {
            if (Globals.NiceHashData != null && Globals.NiceHashData.ContainsKey(AlgorithmType))
            {
                string name = Globals.NiceHashData[AlgorithmType].name;
                int n_port = Globals.NiceHashData[AlgorithmType].port;
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
                        + name
                        + ".mine.zpool.ca:"
                        //+ "." + miningLocation
                        //+ ".nicehash.com:"
                        + port;
            }
            return "";
        }

        public static string GetBitcoinUser()
        {
            if (BitcoinAddress.ValidateBitcoinAddress((Configs.ConfigManager.GeneralConfig.BitcoinAddress.Trim())))
            {
                return Configs.ConfigManager.GeneralConfig.BitcoinAddress.Trim();
            }
            else
            {
                return DemoUser;
            }
        }
    }
}